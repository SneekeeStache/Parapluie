using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSoundDisable : MonoBehaviour
{
    public List<BoxCollider> listZone = new List<BoxCollider>();

    public float timerZoneDisable=45;
    public float time = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        foreach (BoxCollider ColliderZone in listZone)
        {
            ColliderZone.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(time < timerZoneDisable)
        {
            time += Time.deltaTime;
        }
        else
        {
            foreach (BoxCollider ColliderZone in listZone)
            {
                ColliderZone.enabled = true;
            }
        }

        
    }
}
