using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[ExecuteInEditMode]
public class WindRendererParameters : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float density;

    [Header("Component Links")] [SerializeField]
    private ParticleSystem particles;
    
    private bool paramChanged = false;

    private float force;
    public float Force
    {
        set
        {
            force = value;
            paramChanged = true;
        }
    }

    private void Update()
    {
        if (Application.isPlaying) return;
        
        if (!paramChanged && !transform.hasChanged) return;

        //UnityEditor.Undo.RecordObject(particles.gameObject, "Edit wind particles");
        
        ParticleSystem.MainModule main = particles.main; 
        ParticleSystem.TrailModule trails = particles.trails;
        ParticleSystem.ShapeModule shape = particles.shape;
        ParticleSystem.EmissionModule emission = particles.emission;
        ParticleSystem.VelocityOverLifetimeModule velocity = particles.velocityOverLifetime;

        main.startLifetime = ((transform.localScale.z / 10f) * 2f) / force;
        trails.lifetime = (2f / transform.localScale.z) ;
        shape.scale = new Vector3(0, transform.localScale.y, transform.localScale.x);
        emission.rateOverTime = transform.localScale.x * transform.localScale.y * density;
        velocity.speedModifier = force;
        
        PrefabUtility.RecordPrefabInstancePropertyModifications(particles.gameObject);

        paramChanged = false;
    } 
    
    
}
 