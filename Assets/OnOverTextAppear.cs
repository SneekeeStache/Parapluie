using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOverTextAppear : MonoBehaviour
{
    private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).gameObject;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveText()
    {
        text.SetActive(true);
    }

    public void DisableText()
    {
        text.SetActive(false);
    }
}
