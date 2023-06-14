using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titre : MonoBehaviour
{
    private bool Diseappear;
    public float speedDispear = 0.5f;
    public float TimeBeforeDisapear = 0f;
    private float TimeBeforeDisapear2;
    public bool clic;

    private void Start()
    {
        TimeBeforeDisapear2 = TimeBeforeDisapear;
    }

    private void Update()
    {
        TimeBeforeDisapear2 -= Time.deltaTime;
        if (TimeBeforeDisapear2 <= 0 && !clic)
        {
            gameObject.GetComponent<CanvasGroup>().alpha -= speedDispear * Time.deltaTime;

            if (gameObject.GetComponent<CanvasGroup>().alpha == 0)
            {
                gameObject.SetActive(false);
            }
        }
        if (Diseappear && clic)
        {
            gameObject.GetComponent<CanvasGroup>().alpha -= speedDispear * Time.deltaTime;

            if (gameObject.GetComponent<CanvasGroup>().alpha == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void OnClick()
    {
        Diseappear = true;
    }

    public void ResetTitre()
    {
        TimeBeforeDisapear2 = TimeBeforeDisapear;
        gameObject.SetActive(true);
        Diseappear = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
}
