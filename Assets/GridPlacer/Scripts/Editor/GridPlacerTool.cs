using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class GridPlacerTool : EditorWindow
{
    [MenuItem("Tools/GridPlacer")]
    public static void Open() => GetWindow<GridPlacerTool>("GridPlacer");
    
    
    public float gridSize = 1f;
    public int gridExtents = 16;
    public int gridHeight = 0;

    private SerializedObject so;
    private SerializedProperty propGridSize;
    private SerializedProperty propGridExtents;
    private SerializedProperty propGridHeight;
    
    private BoxCollider plane;
    
    private GameObject[] prefabs;
    private GameObject spawnPrefab;
    
    private Material materialPreview;

    private int offset = 0;
    private SceneView currentScene;
    
    private void OnEnable()
    {
        so = new SerializedObject(this);
        propGridSize = so.FindProperty("gridSize");
        propGridExtents = so.FindProperty("gridExtents");
        propGridHeight = so.FindProperty("gridHeight");
        
        Selection.selectionChanged += Repaint;      // Update the UI when selection is changed
        SceneView.duringSceneGui += DuringSceneGUI; // Do stuff every editor frame
        
        // Load saved data
        EditorPrefs.GetFloat("SNAPPER_TOOL_gridSize", 1f);
        
        plane = Instantiate((GameObject)Resources.Load("GridPlacerPlane")).GetComponent<BoxCollider>();
        
        Shader sh = Shader.Find("Unlit/MeshPreview");
        materialPreview = new Material(sh);
        
        // Load prefabs that are in the prefab folder
        string[] guids = AssetDatabase.FindAssets("t:prefab", new []{"Assets/Prefabs"}); // Find all prefabs in the project, return the unique ID of each object
        IEnumerable<string> paths = guids.Select(AssetDatabase.GUIDToAssetPath);
        prefabs = paths.Select(AssetDatabase.LoadAssetAtPath<GameObject>).ToArray();
        
        currentScene = SceneView.currentDrawingSceneView;
    }

    private void OnDisable()
    {
        // Save data between sessions
        EditorPrefs.SetFloat("SNAPPER_TOOL_gridSize", gridSize);
        
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= DuringSceneGUI;
        
        DestroyImmediate(plane.gameObject);
    }

    void DrawSphere(Vector3 pos) 
    {
        Handles.SphereHandleCap(-1, pos, Quaternion.identity, 0.1f, EventType.Repaint);
    }
    
    void DuringSceneGUI(SceneView sceneView)
    {
        if (currentScene != SceneView.currentDrawingSceneView)
        {
            OnDisable();
            OnEnable();
            currentScene = SceneView.currentDrawingSceneView;
        }

        // Event.current.modifier is a bitfield. EventModifiers.Alt is the bitmask for the alt bit
        bool holdingAlt = (Event.current.modifiers & EventModifiers.Alt) != 0;
        

        if (Event.current.type == EventType.ScrollWheel && !holdingAlt)
        {
            float scrollDir = Mathf.Sign(Event.current.delta.y);
            offset += (int)scrollDir * 15;
            Event.current.Use();
        }
        
        Handles.BeginGUI();

        Rect rect = new Rect(8, 8 + offset, 64, 64);

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i];
            Texture icon = AssetPreview.GetAssetPreview(prefab);

            if (GUI.Button(rect, icon))
            {
                // Update selection
                spawnPrefab = prefab;
            }

            rect.y += rect.height + 2;
        }
        Handles.EndGUI();
        
        
        // Run the code only when repainting
        if (Event.current.type == EventType.Repaint)
        {
            DrawGridCartesian(gridExtents, gridHeight);
            plane.transform.position = gridHeight * Vector3.up;
        }
        
        Handles.zTest = CompareFunction.LessEqual;
        
        Transform camTf = sceneView.camera.transform;
        
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        if (Event.current.type == EventType.MouseMove)
        {
            sceneView.Repaint();
        }

        if (plane.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 hitPoint = GetSnappedPosition(hit.point);
            
            // Spawn on press
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.RightShift)
            {
                TrySpawnObject(hitPoint);
            }
            
            // Draw Mesh Preview
            if (spawnPrefab != null)
            {
                Matrix4x4 poseToWorld = Matrix4x4.TRS(hitPoint, Quaternion.identity, Vector3.one);
                DrawPrefab(spawnPrefab, poseToWorld, camTf.GetComponent<Camera>());
            }
            else
            {
                // Prefab missing
                Handles.SphereHandleCap(-1, hitPoint, Quaternion.identity, 0.1f, EventType.Repaint);
            }
        }
        
    }

    void DrawGridCartesian(float gridDrawExtent, float gridHeight)
    {
        int lineCount = Mathf.RoundToInt((gridDrawExtent * 2) / gridSize);
        if (lineCount % 2 == 0) lineCount++;
        int halfLineCount = lineCount / 2;

        for (int i = 0; i < lineCount; i++)
        {
            int intOffset = i - halfLineCount;
            float xCoord = intOffset * gridSize;
            float zCoord0 = halfLineCount * gridSize;
            float zCoord1 = -halfLineCount * gridSize;
            Vector3 p0 = new Vector3(xCoord, gridHeight, zCoord0);
            Vector3 p1 = new Vector3(xCoord, gridHeight, zCoord1);
            
            Handles.color = Color.grey;
            Handles.DrawAAPolyLine(p0, p1);
            p0 = new Vector3(zCoord0, gridHeight, xCoord);
            p1 = new Vector3(zCoord1, gridHeight, xCoord);
            Handles.DrawAAPolyLine(p0, p1);
            Handles.color = Color.white;
        }
    }


    private void OnGUI()
    {
        so.Update();
        EditorGUILayout.PropertyField(propGridSize);
        EditorGUILayout.PropertyField(propGridExtents);
        EditorGUILayout.PropertyField(propGridHeight);
        so.ApplyModifiedProperties();
        
        using (new EditorGUI.DisabledScope(Selection.gameObjects.Length == 0))
        {
            if (GUILayout.Button("Snap Selection"))
            {
                SnapSelection(gridSize);
            }
        }
        
        // if you left click in the editor window
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            GUI.FocusControl(null);
            Repaint();
        }
    }

    private void SnapSelection(float size)
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            // Mark components as dirty (you need to do that BEFORE modifying the components)
            Undo.RecordObject(go.transform, "snap objects");
            
            go.transform.position = GetSnappedPosition(go.transform.position);
        }
    }

    Vector3 GetSnappedPosition(Vector3 posOriginal) => posOriginal.Round(gridSize);
    
    
    void TrySpawnObject(Vector3 pos)
    {
        if (spawnPrefab == null) return;

        GameObject spawnedThing = (GameObject)PrefabUtility.InstantiatePrefab(spawnPrefab);
        Undo.RegisterCreatedObjectUndo(spawnedThing, "Spawn Objects");
        spawnedThing.transform.position = pos;
    }
    
    
    void DrawPrefab(GameObject prefab, Matrix4x4 poseToWorld, Camera cam)
    {
        MeshFilter[] filters = prefab.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter filter in filters)
        {
            Matrix4x4 childToPose = filter.transform.localToWorldMatrix;
            Matrix4x4 childToWorldMtx = poseToWorld * childToPose;
            Mesh mesh = filter.sharedMesh;

            Material mat = materialPreview;
            //mat.SetPass(0); 
            Graphics.DrawMesh(mesh, childToWorldMtx, mat, 0, cam);
        }   
    }
}

