using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager _instance;
    
    const float MaxVolumeBGM = 1f;
    const float MaxVolumeSFX = 1f;
    static float CurrentVolumeNormalizedMain = 1f;
    static float CurrentVolumeNormalizedBGM = 1f;
    static float CurrentVolumeNormalizedSFX = 1f;
    static bool isMuted = false;

    List<AudioSource> sfxSources;
    AudioSource[] bgmSource;
    int activeBGMSourceIndex;

    // TODO: to be place in other script, like BGM/SFX list???
    public AudioClip theme;
    public AudioClip theme2;

    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;
    public AudioClip sfx4;

    void Awake ()
    {
        #region Singleton

        if (_instance != null)
        {
            Debug.LogWarning ("More then one SoundManager");
            Destroy (gameObject);
            return;
        }
        else
            _instance = this;

        #endregion

        Initialize ();
    }

    public static SoundManager GetInstance ()
    {
        if (!_instance)
        {
            GameObject audioManager = new GameObject ("SoundManager");
            _instance = audioManager.AddComponent (typeof (SoundManager)) as SoundManager;
            _instance.Initialize ();
        }

        return _instance;
    }

    void Initialize ()
    {
        bgmSource = new AudioSource[2];

        for (int i = 0; i < 2; i++)
        {
            bgmSource[i] = gameObject.AddComponent (typeof (AudioSource)) as AudioSource;
            bgmSource[i].loop = true;
            bgmSource[i].playOnAwake = false;
            bgmSource[i].volume = GetBGMVolume ();
        }
        
        DontDestroyOnLoad (gameObject);
    }

    public void Start ()
    {
        //PlayBGM (theme);        
    }

    public void Update ()
    {
        //if (Input.GetKeyDown (KeyCode.P))
        //{
        //    PlayBGM (theme);
        //}

        //if (Input.GetKeyDown (KeyCode.S))
        //{
        //    StopBGMWithFade (3);
        //}

        //if (Input.GetKeyDown (KeyCode.G))
        //{
        //    //PlayBGMWithFade (theme2, 3);
        //    PlayBGMCrossFade (theme2, 10);
        //}

        //if (Input.GetKeyDown (KeyCode.H))
        //{
        //    PlayBGMWithFade (theme2, 3);
        //    //PlayBGMCrossFade (theme, 10);
        //}

        //if (Input.GetKeyDown (KeyCode.T))
        //{
        //    PlaySFX (sfx1);
        //    PlaySFX (sfx2);
        //    PlaySFX (sfx3);
        //    PlaySFX (sfx4);            
        //}
    }

    #region ==================== General Utils ====================

    public static float GetBGMVolume ()
    {
        return isMuted ? 0f : MaxVolumeBGM * CurrentVolumeNormalizedBGM * CurrentVolumeNormalizedMain;
    }

    public static float GetSFXVolume ()
    {
        return isMuted ? 0f : MaxVolumeSFX * CurrentVolumeNormalizedSFX * CurrentVolumeNormalizedMain;
    }

    bool CheckIfAudioClipExist ()
    {
        if (bgmSource[activeBGMSourceIndex].clip)
            return true;
        else
        {
            Debug.LogError ("Error: AudioClip do not exist.");
            return false;
        }            
    }

    bool CheckIfAudioClipIsPlaying ()
    {
        if (bgmSource[activeBGMSourceIndex].isPlaying)
            return true;
        else
        {
            Debug.LogError ("Error: AudioClip is not playing.");
            return false;
        }
    }

    #endregion

    #region ==================== BGM Utils ====================

    // fade out the BGM within duration after a delay, also stop BGM
    IEnumerator FadeBGMOut (float delay, float duration)
    {
        yield return new WaitForSeconds (delay);

        SoundManager soundMan = GetInstance ();

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            float volume = Mathf.Lerp (GetBGMVolume (), 0, percent);
            soundMan.bgmSource[activeBGMSourceIndex].volume = volume;

            if (percent >= 1)
                soundMan.bgmSource[activeBGMSourceIndex].Stop ();
            
            yield return null;
        }
    }

    // fade in the BGM within duration after a delay
    IEnumerator FadeBGMIn (AudioClip clip, float delay, float duration)
    {
        yield return new WaitForSeconds (delay);

        SoundManager soundMan = GetInstance ();
        soundMan.bgmSource[activeBGMSourceIndex].clip = clip;
        soundMan.bgmSource[activeBGMSourceIndex].Play ();

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            float volume = Mathf.Lerp (0, GetBGMVolume (), percent);
            soundMan.bgmSource[activeBGMSourceIndex].volume = volume;

            yield return null;
        }
    }

    // cross fade BGM within duration after a delay, swap active BGM index and stop inactive BGM
    IEnumerator CrossFade (AudioClip clip, float delay, float duration)
    {
        yield return new WaitForSeconds (delay);

        SoundManager soundMan = GetInstance ();
    
        activeBGMSourceIndex = 1 - activeBGMSourceIndex;
        soundMan.bgmSource[activeBGMSourceIndex].clip = clip;
        soundMan.bgmSource[activeBGMSourceIndex].Play ();
        
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            soundMan.bgmSource[activeBGMSourceIndex].volume = Mathf.Lerp (0, GetBGMVolume (), percent);
            soundMan.bgmSource[1 - activeBGMSourceIndex].volume = Mathf.Lerp (GetBGMVolume (), 0, percent);

            if (percent >= 1)
                soundMan.bgmSource[1 - activeBGMSourceIndex].Stop ();

            yield return null;            
        }
    }

    #endregion

    #region ==================== BGM Functions ====================

    // play BGM immediately
    public static void PlayBGM (AudioClip bgmClip)
    {
        SoundManager soundMan = GetInstance ();

        soundMan.bgmSource[soundMan.activeBGMSourceIndex].clip = bgmClip;
        soundMan.bgmSource[soundMan.activeBGMSourceIndex].volume = GetBGMVolume ();        
        soundMan.bgmSource[soundMan.activeBGMSourceIndex].Play ();
    }

    // fade out then fade in new BGM if there is BGM playing. Simpliy fade in new BGM if no BGM is playing, 
    public static void PlayBGMWithFade (AudioClip bgmClip, float fadeDuration)
    {
        SoundManager soundMan = GetInstance ();
                
        if (soundMan.bgmSource[soundMan.activeBGMSourceIndex].clip && soundMan.bgmSource[soundMan.activeBGMSourceIndex].isPlaying)
        {
            // fade out, then change BGM and fade in
            Debug.Log ("start fade out and in");
            soundMan.StartCoroutine(soundMan.FadeBGMOut (0,fadeDuration));
            soundMan.StartCoroutine(soundMan.FadeBGMIn (bgmClip, fadeDuration, fadeDuration));            
        }
        else
        {
            // just fade in
            Debug.Log ("simply fade in");
            float delay = 0f;
            soundMan.StartCoroutine (soundMan.FadeBGMIn (bgmClip, delay, fadeDuration));
        }        
    }

    // cross fade BGM within a duration if there is an BGM playing, stop inactive BGM, swap BGM index, play BGM
    public static void PlayBGMCrossFade (AudioClip bgmClip, float fadeDuration)
    {
        SoundManager soundMan = GetInstance ();

        if (soundMan.CheckIfAudioClipExist () && soundMan.CheckIfAudioClipIsPlaying ())
            soundMan.StartCoroutine (soundMan.CrossFade (bgmClip, 0, fadeDuration));
    }

    // stop BGM immediately if there is an BGM and the BGM is playing
    public static void StopBGM ()
    {
        SoundManager soundMan = GetInstance ();

        if (soundMan.CheckIfAudioClipExist () && soundMan.CheckIfAudioClipIsPlaying ())
            soundMan.bgmSource[soundMan.activeBGMSourceIndex].Stop ();
    }

    // fade out and stop BGM within a duration if there is an BGM playing, maybe add delay function later if needed
    public static void StopBGMWithFade (float fadeDuration)
    {
        SoundManager soundMan = GetInstance ();

        if (soundMan.CheckIfAudioClipExist () && soundMan.CheckIfAudioClipIsPlaying ())
            soundMan.StartCoroutine (soundMan.FadeBGMOut (0, fadeDuration));
    }

    #endregion

    #region ==================== SFX Utils ====================

    // set up a new SFX sound source for each new SFX clip
    AudioSource GetSFXSource ()
    {
        AudioSource sfxSource = gameObject.AddComponent<AudioSource> ();

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = GetSFXVolume ();

        if (sfxSources == null)
            sfxSources = new List<AudioSource> ();

        sfxSources.Add (sfxSource);

        return sfxSource;
    }

    // remove SFX immediately
    IEnumerator RemoveSFXSource (AudioSource sfxSource)
    {
        yield return new WaitForSeconds (sfxSource.clip.length);
        sfxSources.Remove (sfxSource);
        Destroy (sfxSource);
    }

    // remove SFX after a certain time
    IEnumerator RemoveSFXSource (AudioSource sfxSource, float duration)
    {
        yield return new WaitForSeconds (duration);
        sfxSources.Remove (sfxSource);
        Destroy (sfxSource);
    }

    #endregion

    #region ==================== SFX Functions ====================

    // play SFX
    public static void PlaySFX (AudioClip sfxClip)
    {
        SoundManager soundMan = GetInstance ();
        AudioSource source = soundMan.GetSFXSource ();        
        source.clip = sfxClip;
        source.volume = GetSFXVolume ();
        source.Play ();
        soundMan.StartCoroutine (soundMan.RemoveSFXSource (source));
    }

    // play SFX with random volume and pitch, adjsuting settings volume will make random volume reset, improve later???
    public static void PlaySFXWithRamdomVolumeAndPitch (AudioClip sfxClip, bool isRandomVol, bool isRandomPitch)
    {
        SoundManager soundMan = GetInstance ();
        AudioSource source = soundMan.GetSFXSource ();
        source.clip = sfxClip;
        source.volume = Random.Range (0.85f * GetSFXVolume (), 1.2f * GetSFXVolume ());
        source.pitch = Random.Range (0.85f, 1.2f);
        source.Play ();
        soundMan.StartCoroutine (soundMan.RemoveSFXSource (source));
    }

    // play SFX withun duration
    public static void PlaySFXWithFixedDuration (AudioClip sfxClip, float duration)
    {
        SoundManager soundMan = GetInstance ();
        AudioSource source = soundMan.GetSFXSource ();
        source.clip = sfxClip;
        source.volume = GetSFXVolume ();        
        source.loop = true;
        source.Play ();
        soundMan.StartCoroutine (soundMan.RemoveSFXSource (source, duration));
    }

    #endregion

    #region ==================== Volume Control Functions ====================

    public static void DisableSoundImmediate ()
    {
        SoundManager soundMan = GetInstance ();
        soundMan.StopAllCoroutines ();

        if (soundMan.sfxSources != null)
        {
            foreach (AudioSource source in soundMan.sfxSources)
            {
                source.volume = 0;
            }
        }

        soundMan.bgmSource[soundMan.activeBGMSourceIndex].volume = 0f;
        isMuted = true;
    }

    public static void EnableSoundImmediate ()
    {
        SoundManager soundMan = GetInstance ();

        if (soundMan.sfxSources != null)
        {
            foreach (AudioSource source in soundMan.sfxSources)
            {
                source.volume = GetSFXVolume ();
            }
        }

        soundMan.bgmSource[soundMan.activeBGMSourceIndex].volume = GetBGMVolume ();
        isMuted = false;
    }

    public static void SetMainVolume (float newVolume)
    {
        CurrentVolumeNormalizedMain = newVolume;
        AdjustSoundImmediate ();
    }

    public static void SetBGMVolume (float newVolume)
    {
        CurrentVolumeNormalizedBGM = newVolume;
        AdjustSoundImmediate ();
    }

    public static void SetSFXVolume (float newVolume)
    {
        CurrentVolumeNormalizedSFX = newVolume;
        AdjustSoundImmediate ();
    }

    public static void AdjustSoundImmediate ()
    {
        SoundManager soundMan = GetInstance ();

        if (soundMan.sfxSources != null)
            foreach (AudioSource source in soundMan.sfxSources)
                source.volume = GetSFXVolume ();
        
        soundMan.bgmSource[soundMan.activeBGMSourceIndex].volume = GetBGMVolume ();

        //Debug.Log ("Main Volume coeff: " + CurrentVolumeNormalizedMain);
        //Debug.Log ("BGM Volume coeff: " + CurrentVolumeNormalizedBGM);
        //Debug.Log ("SFX Volume coeff: " + CurrentVolumeNormalizedSFX);        
        //Debug.Log ("BGM Volume: " + GetBGMVolume ());
        //Debug.Log ("SFX Volume: " + GetSFXVolume ());

        DebugPanel.Log ("Main vol", "Settings", CurrentVolumeNormalizedMain);
        DebugPanel.Log ("BGM vol", "Settings", GetBGMVolume ());
        DebugPanel.Log ("SFX vol", "Settings", GetSFXVolume ());
    }
    
    #endregion
}
