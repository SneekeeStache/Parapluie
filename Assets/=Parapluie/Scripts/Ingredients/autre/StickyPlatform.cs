using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Parapluie") collision.gameObject.transform.SetParent(transform);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Parapluie") collision.gameObject.transform.SetParent(null);
    }
}
