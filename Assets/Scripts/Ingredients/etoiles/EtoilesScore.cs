using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class EtoilesScore : MonoBehaviour
{
    private List<Transform> etoiles;
    public int score;
    public TextMeshProUGUI scoreT;
    public TextMeshProUGUI scoreT2;
    
    
    void Start()
    {
        etoiles = GetComponentsInChildren<Transform>().ToList();
        etoiles.RemoveAt(0);
    }
    
    void Update()
    {
        scoreT.text = ("Vous avez obtenu " + score + " / " + etoiles.Count + " étoiles  dans le niveau.");
        scoreT2.text = ("Vous avez obtenu " + score + " / " + etoiles.Count + " étoiles  dans le niveau.");
    }
}
