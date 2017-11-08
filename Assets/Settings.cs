using UnityEngine;

public class Settings
{
    public enum Difficulty { Easy, Normal, Hard };

    public Difficulty difficulty = Difficulty.Normal;
    public bool isFullScreen;
    public int resoulutionIndex;
    public int textureQuality;
    public int antialiasing;
    public int vSync;
    public float mainVol;
    public float BGMVol;
    public float SFXVol;

    //public void SetSettingsToDefault ()
    //{
    //    GameManager.instance.settings.difficulty = Difficulty.Normal;
    //    GameManager.instance.settings.isFullScreen = true;
    //    GameManager.instance.settings.mainVol = 10f;
    //    GameManager.instance.settings.musicVol = 10f;
    //}    
}
