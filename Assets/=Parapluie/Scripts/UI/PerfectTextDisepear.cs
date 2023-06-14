using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerfectTextDisepear : MonoBehaviour
{
    private Vector2 positionInitiale;
    private bool disappearBool;
    public RectTransform Position;
    private TextMeshProUGUI text;
    private float opacity;
    public float speedDisappear;
    public float speedTranslateUp;
    void Start()
    {
        positionInitiale = Position.anchoredPosition;
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void Disappear()
    {
        opacity = 255;
        disappearBool = true;
        Position.anchoredPosition = positionInitiale;
    }
    void Update()
    {
        if (disappearBool)
        {
            opacity -= speedDisappear * Time.deltaTime;
            text.alpha = opacity;
            Position.anchoredPosition = new Vector2(
                Position.anchoredPosition.x,
                 Position.anchoredPosition.y + speedTranslateUp * Time.deltaTime);
            
            if (opacity <= 0f)
            {
                disappearBool = false;
            }
        }
        //Debug.Log(opacity);
        
    }
}