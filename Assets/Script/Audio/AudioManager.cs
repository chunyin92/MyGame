using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;
    public Sound currentSound;

    void Awake ()
    {
        #region Singleton

        if (instance != null)
        {
            Debug.LogWarning ("More then one AudioManager");
            Destroy (gameObject);
            return;
        }else
            instance = this;

        #endregion

        DontDestroyOnLoad (gameObject);

        //MenuManager menuManager = MenuManager.instance;
        //if (menuManager == null)
        //    Debug.Log ("null");

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource> ();
            s.source.clip = s.clip;

            s.source.volume = s.volume;// * MenuManager.instance.settings.mainVol * MenuManager.instance.settings.musicVol;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start ()
    {
        Play ("Theme");
    }

    public void Play (string name)
    {
        currentSound = Array.Find (sounds, sound => sound.name == name);

        if (currentSound == null)
        {
            Debug.LogWarning ("Sound: " + name + " not found.");
            return;
        }

        currentSound.source.Play ();
    }











    



}
