using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public KeyCode ReloadScene;
    public string mainMenuScene;
    public string AcualSceneName;

    FMOD.Studio.Bus MasterBus;

    void Start()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(ReloadScene))
        {
            MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
  
            UnityEngine.SceneManagement.SceneManager.LoadScene(AcualSceneName);
        }
    }
    public void BackToMenu()
    {
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuScene);
    }
}
