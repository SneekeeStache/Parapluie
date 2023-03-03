using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;


    [Header("Gameplay Setting")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;
    public static MenuController instance;

    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;


    [Header("Levels to load")]

    public string _newGameLevel1;
    public string _newGameLevel2;
    public string _newGameLevel3;
    public string _newGameLevel4;

    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel1);
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(_newGameLevel1);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(_newGameLevel2);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(_newGameLevel3);
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene(_newGameLevel4);
    }
    // Start is called before the first frame update
    public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel1");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

 
    // Update is called once per frame
    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void setContrelloerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if (MenuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;

            GameplayApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }
}
