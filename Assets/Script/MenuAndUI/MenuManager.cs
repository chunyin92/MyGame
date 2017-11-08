using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    public Camera cam;    

    [Header("Panels")]
    public GameObject areYouSurePanel;
    public GameObject generalPanel;
    public GameObject graphicsPanel;
    public GameObject audioPanel;

    [Header ("General")]
    public TMP_Dropdown difficultyDropdown;

    [Header ("Graphics")]
    public TMP_Dropdown displayModeDropdown;
    public TMP_Dropdown screenResolutionDropdown;
    public TMP_Dropdown textureQualityDropdown;
    public TMP_Dropdown antialiasingDropdown;
    public TMP_Dropdown vSyncDropdown;
    public int[] screenResoltution;

    [Header ("Audio")]
    public Slider sliderMainVol;
    public Slider sliderBGMVol;
    public Slider sliderSFXVol;

    public Resolution[] resoluations;

    // TODO: place settings to settings manager later??? since there will be different menu in the game
    public Settings settings;

    void OnEnable ()
    {
        settings = new Settings ();

        // TODO: needed to improve later
        settings.resoulutionIndex = 2;

        difficultyDropdown.onValueChanged.AddListener (delegate { OnDifficultyChange (); });
        displayModeDropdown.onValueChanged.AddListener (delegate { OnDisplayModeChange (); });
        screenResolutionDropdown.onValueChanged.AddListener (delegate { OnScreenResolutionChange (); });
        textureQualityDropdown.onValueChanged.AddListener (delegate { OnTextureQualityChange (); });
        antialiasingDropdown.onValueChanged.AddListener (delegate { OnAntialisingChange (); });
        vSyncDropdown.onValueChanged.AddListener (delegate { OnvSyncChange (); });
        sliderMainVol.onValueChanged.AddListener (delegate { OnMainVolChange (); });
        sliderBGMVol.onValueChanged.AddListener (delegate { OnBGMVolChange (); });
        sliderSFXVol.onValueChanged.AddListener (delegate { OnSFXVolChange (); });

        AddScreenResulotionOptions ();
                
        if (File.Exists (Application.persistentDataPath + "/settings.json") == true)
        {
            LoadSettings ();
        }        
    }

    void Awake ()
    {
        #region Singleton

        if (instance != null)
        {
            Debug.LogWarning ("More then one MenuManager");
            Destroy (gameObject);
            return;
        }
        else
            instance = this;

        #endregion

        DontDestroyOnLoad (gameObject);

        areYouSurePanel.SetActive (false);
        generalPanel.SetActive (true);
        graphicsPanel.SetActive (false);
        audioPanel.SetActive (false);
    }

    public void Update ()
    {
        if (Input.GetKeyDown (KeyCode.D))
        {
            DebugPanelViewport.main.gameObject.SetActive (!DebugPanelViewport.main.gameObject.activeSelf);
        }  
    }    

	public void BtnContinueOnClick ()
    {
        Debug.Log ("Continue Game");
    }

    public void BtnNewGameOnClick ()
    {
        Debug.Log ("Start New Game");
        SceneManager.LoadScene (2);
    }

    public void BtnLoadGameOnClick ()
    {
        Debug.Log ("Load Game");
    }

    public void BtnOptionsOnClick ()
    {
        cam.GetComponent<RotateObject> ().Rotate (90, Vector3.up);
    }

    public void BtnQuitOnClick ()
    {
        areYouSurePanel.SetActive (true);
    }

    public void BtnYesOnClick ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void BtnNoOnClick ()
    {
        areYouSurePanel.SetActive (false);
    }

    public void BtnBackOnClick ()
    {
        cam.GetComponent<RotateObject> ().Rotate (-90, Vector3.up);
    }

    public void BtnGeneralOnClick ()
    {
        generalPanel.SetActive (true);
        graphicsPanel.SetActive (false);
        audioPanel.SetActive (false);
    }

    public void BtnGraphicsOnClick ()
    {
        generalPanel.SetActive (false);
        graphicsPanel.SetActive (true);
        audioPanel.SetActive (false);
    }

    public void BtnAudioOnClick ()
    {
        generalPanel.SetActive (false);
        graphicsPanel.SetActive (false);
        audioPanel.SetActive (true);
    }

    public void OnDifficultyChange ()
    {
        settings.difficulty = (Settings.Difficulty) difficultyDropdown.value;
        Debug.Log ("Difficulty is " + settings.difficulty);
    }

    public void OnDisplayModeChange ()
    {
        if (displayModeDropdown.value == 0)
            Screen.fullScreen = settings.isFullScreen = true;
        else
            Screen.fullScreen = settings.isFullScreen = false;
    }

    public void OnScreenResolutionChange ()
    {
        settings.resoulutionIndex = screenResolutionDropdown.value;
        float aspectRatio = 16 / 9f;
        Screen.SetResolution (screenResoltution[settings.resoulutionIndex], (int)(screenResoltution[settings.resoulutionIndex] / aspectRatio), Screen.fullScreen);

        Debug.Log (screenResoltution[settings.resoulutionIndex].ToString () + " x " + (((screenResoltution[settings.resoulutionIndex] / aspectRatio)).ToString ()));
    }

    public void OnTextureQualityChange ()
    {
        QualitySettings.masterTextureLimit = settings.textureQuality = textureQualityDropdown.value;
    }

    public void OnAntialisingChange ()
    {
        QualitySettings.antiAliasing = settings.antialiasing = (int)Mathf.Pow (2, antialiasingDropdown.value);
        Debug.Log ("haah : " + settings.antialiasing);
    }

    public void OnvSyncChange ()
    {
        QualitySettings.vSyncCount = settings.vSync = vSyncDropdown.value;
    }

    public void OnMainVolChange ()
    {
        settings.mainVol = sliderMainVol.value;
        SoundManager.SetMainVolume (settings.mainVol);
    }
    
    public void OnBGMVolChange ()
    {
        settings.BGMVol = sliderBGMVol.value;
        SoundManager.SetBGMVolume (settings.BGMVol);
    }

    public void OnSFXVolChange ()
    {
        settings.SFXVol = sliderSFXVol.value;
        SoundManager.SetSFXVolume (settings.SFXVol);
    }    

    public void SaveSettings ()
    {
        string jsonData = JsonUtility.ToJson (settings, true);
        File.WriteAllText (Application.persistentDataPath + "/settings.json", jsonData);
    }

    public void LoadSettings ()
    {
        settings = JsonUtility.FromJson<Settings> (File.ReadAllText (Application.persistentDataPath + "/settings.json"));

        difficultyDropdown.value = (int) settings.difficulty;
        difficultyDropdown.RefreshShownValue ();

        if (settings.isFullScreen)
            displayModeDropdown.value = 0;
        else
            displayModeDropdown.value = 1;

        screenResolutionDropdown.value = settings.resoulutionIndex;
        textureQualityDropdown.value = settings.textureQuality;

        // if settings.antialiasing = 1/2/4, antialisasingDropdown.value = 0/1/2
        antialiasingDropdown.value = Mathf.RoundToInt(settings.antialiasing / 2);
        
        vSyncDropdown.value = settings.vSync;
        sliderMainVol.value = settings.mainVol;
        sliderBGMVol.value = settings.BGMVol;
        sliderSFXVol.value = settings.SFXVol;
    }

    public void AddScreenResulotionOptions ()
    {
        int height;
        float aspectRatio = 16 / 9f;

        for (int i = 0; i < screenResoltution.Length; i++)
        {
            height = (int) Mathf.Round ((screenResoltution[i] / aspectRatio));
            screenResolutionDropdown.options.Add (new TMPro.TMP_Dropdown.OptionData (screenResoltution[i].ToString() + " x " + height));
        }        
    }
    
}
