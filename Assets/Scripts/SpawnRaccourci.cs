using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRaccourci : MonoBehaviour
{
    public GameObject Parapluie;
    public List<GameObject> Ateliers;
    public int AtelierTeleport;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Y)){
            Parapluie.transform.position = Ateliers[AtelierTeleport].transform.position;
            AtelierTeleport += 1;
            if(AtelierTeleport > Ateliers.Count-1) AtelierTeleport = 0;
        }
    }
}
