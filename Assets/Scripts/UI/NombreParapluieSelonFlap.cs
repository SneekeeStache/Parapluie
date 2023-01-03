using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NombreParapluieSelonFlap : MonoBehaviour
{
    public float NombreFlap;
    private player PnombreFlap;
    private Image image;
    private float scaleChange = 1f;
    private float speedOfChangeScale = 1.3f;

    void Start()
    {
        PnombreFlap = GameObject.Find("parapluie").GetComponent<player>();
        image = gameObject.GetComponent<Image>();
    }


    void Update()
    {
        if (PnombreFlap.FlapingNumber < NombreFlap)
        {
            scaleChange -= Time.deltaTime * speedOfChangeScale;
        }
        else
        {
            image.enabled = true;
            scaleChange += Time.deltaTime * speedOfChangeScale;
            if (scaleChange >= 1f)
            {
                scaleChange = 1f;
            }
        }

        gameObject.GetComponent<RectTransform>().localScale = new Vector3(scaleChange, scaleChange, 1f);

        if (scaleChange <= 0f)
        {
            scaleChange = 0f;
            image.enabled = false;
        }
    }
}
