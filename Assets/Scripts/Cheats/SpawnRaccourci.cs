using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnRaccourci : MonoBehaviour
{
    public GameObject Parapluie;
    public List<GameObject> Ateliers;
    public int AtelierTeleport;

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.F5)) SceneManager.LoadScene("test");
        //téléportations aux points de la liste
        if(Input.GetButtonDown("CheatSpawn"))
        {
            Parapluie.transform.position = Ateliers[AtelierTeleport].transform.position;
            AtelierTeleport += 1;
            Parapluie.GetComponent<player>().FlapingNumber = Parapluie.GetComponent<player>().NombreFlap;
            if(AtelierTeleport > Ateliers.Count-1) AtelierTeleport = 0;
        }
    }
}
