using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using Random = UnityEngine.Random;

public class TrailOnCollected : MonoBehaviour
{
    [SerializeField] private Gradient color;
    [SerializeField] [Min(0.1f)] private float duration;
    [SerializeField] private float minCurvature;
    [SerializeField] private float maxCurvature;

    [Header("Component Links")] 
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private BonusFlap bonusFlap;
    
    // private variables
    private Vector3 angleDirection;
    private Transform player;
    private float curvature;
    
    private float timer = 0f;
    private float t => timer / duration;

    private bool isCollected = false;
    
    void OnEnable()
    {
        player = bonusFlap.Parapluie;
        
        trailRenderer.startColor = color.Evaluate(Random.Range(0f, 1f));
        trailRenderer.endColor = trailRenderer.startColor;
        trailRenderer.time = duration;

        float angleRad = Random.Range(0f, Mathf.PI * 2f);
        angleDirection = player.transform.right * Mathf.Cos(angleRad) + player.transform.forward * Mathf.Sin(angleRad);

        Debug.DrawRay(player.transform.position, angleDirection * curvature, trailRenderer.startColor, 2f);
        
        curvature = Random.Range(minCurvature, maxCurvature);
        
        timer = 0f;
        isCollected = false;
    }

    private void Update()
    {
        if (t < 1f)
        {
            transform.position = player.transform.position + angleDirection * ((1f - (2f * t - 1f) * (2f * t - 1f)) * curvature);
        }

        if (!isCollected && t > 1f)
        {
            bonusFlap.Player.EnergieFlap = 100;
            isCollected = true;
        }

        if (t > 2f) transform.parent.gameObject.SetActive(false);

        timer += Time.deltaTime;
    }
}
