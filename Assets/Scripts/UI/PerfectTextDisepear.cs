using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerfectTextDisepear : MonoBehaviour
{
    private Vector2 positionInitiale;
    private bool disappearBool;
    private TextMeshProUGUI text;
    private float opacity;
    public float speedDisappear;
    public float speedTranslateUp;
    void Start()
    {
        positionInitiale = gameObject.GetComponent<RectTransform>().anchoredPosition;
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void Disappear()
    {
        opacity = 255;
        disappearBool = true;
        gameObject.GetComponent<RectTransform>().anchoredPosition = positionInitiale;
    }
    void Update()
    {
        if (disappearBool)
        {
            opacity -= speedDisappear * Time.deltaTime;
            text.alpha = opacity;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                gameObject.GetComponent<RectTransform>().anchoredPosition.x,
                gameObject.GetComponent<RectTransform>().anchoredPosition.y + speedTranslateUp * Time.deltaTime);
            if (opacity <= 0f)
            {
                disappearBool = false;
            }
        }
        //Debug.Log(opacity);
        
    }
}