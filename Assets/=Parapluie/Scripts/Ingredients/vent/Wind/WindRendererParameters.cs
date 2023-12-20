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
    [SerializeField] [Min(0f)] private float maxWindRenderingDistance;
    [SerializeField] [Min(0f)] private AnimationCurve densityDampCurve;
    

    [Header("Component Links")] [SerializeField]
    private ParticleSystem particles;

    [SerializeField] private Transform player;
    
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

        //UnityEditor.Undo.RecordObject(particles.gameObject, "Edit wind particles");
        
        ParticleSystem.MainModule main = particles.main; 
        ParticleSystem.TrailModule trails = particles.trails;
        ParticleSystem.ShapeModule shape = particles.shape;
        ParticleSystem.EmissionModule emission = particles.emission;
        ParticleSystem.VelocityOverLifetimeModule velocity = particles.velocityOverLifetime;

        float distanceToPlayer = (transform.position - player.position).magnitude;
        float dampedDensity = density * densityDampCurve.Evaluate(Mathf.Min(distanceToPlayer / maxWindRenderingDistance, 1f));
        if (distanceToPlayer >= maxWindRenderingDistance) return;

        
        main.startLifetime = ((transform.localScale.z / 100f) * 2f) / (force/1.3f);
        trails.lifetime = (10f / transform.localScale.z) ;
        shape.scale = new Vector3(0, transform.localScale.y, transform.localScale.x);
        emission.rateOverTime = transform.localScale.x * transform.localScale.y * (dampedDensity / 100f);
        velocity.speedModifier = force/1.3f;
        
        PrefabUtility.RecordPrefabInstancePropertyModifications(particles.gameObject);

        paramChanged = false;
    } 
    
    
}
 