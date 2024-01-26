using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class ParticleIndicator : MonoBehaviour
{
    public GameObject ParticleIndicatorPrefab;
    public List<Transform> Paths;
    public float Speed;
    public float SpawnRate;
    private float SpawnRateReset;

    public PathType pathSystem = PathType.CatmullRom;

    public Vector3[] Pathval;
    
    /*void Start()
    {
        Pathval = new Vector3[Paths.Count];
        SpawnRateReset = SpawnRate;
        foreach (Transform T in Paths)
        {
            Vector3 goodPaths = T.position;
            Pathval[Paths.Count] = goodPaths;
        }
    }
    
    void Update()
    {
        SpawnRateReset -= Time.deltaTime;
        if (SpawnRateReset <= 0)
        {
            SpawnRateReset = SpawnRate;
            GameObject particle;
            particle = Instantiate(ParticleIndicatorPrefab, gameObject.transform);
            particle.transform.localPosition = new Vector3(0, 0, 0);
            particle.transform.DOPath(Pathval, Speed, pathSystem);
        }
        
        
    }*/
}
