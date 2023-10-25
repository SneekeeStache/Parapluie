using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class ScoreEtoileText : MonoBehaviour
{
    public TextMeshProUGUI scoreT;
    [FormerlySerializedAs("EtoilesScore")] public EtoilesScoreManager etoilesScoreManager;
    void Update()
    {
        scoreT.text = ("Vous avez obtenu " + etoilesScoreManager.score + " / " + etoilesScoreManager.etoiles.Count + " étoiles  dans le niveau.");
    }
}
