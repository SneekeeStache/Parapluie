using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titre : MonoBehaviour
{
    private bool yes;
    public float speedDispear = 0.5f;
    private void Update()
    {
        if (yes)
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
        yes = true;
    }

    public void ResetTitre()
    {
        gameObject.SetActive(true);
        yes = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
}
