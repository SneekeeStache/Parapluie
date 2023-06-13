using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParapluieFeedBack : MonoBehaviour
{
    public ParticleSystem PerfectParticleSystemFeedback;
    public ParticleSystem MegaPerfectParticleSystemFeedback;
    public ParticleSystem BonusFlapParticleSystem;
    public ParticleSystem EtoileParticleSystem;
    
    public void PerfectFeedBack()
    {
        PerfectParticleSystemFeedback.Play();
    }
    public void MegaPerfectFeedBack()
    {
        MegaPerfectParticleSystemFeedback.Play();
    }
    public void BonusFlapFeedBack()
    {
        BonusFlapParticleSystem.Play();
    }
    public void EtoileFeedBack()
    {
        EtoileParticleSystem.Play();
    }
}
