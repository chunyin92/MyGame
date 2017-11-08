using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public Image splashImage;
    
    IEnumerator Start ()
    {
        splashImage.canvasRenderer.SetAlpha (0f);

        FadeIn ();
        yield return new WaitForSeconds (2.5f);
        FadeOut ();
        yield return new WaitForSeconds (2.5f);

        SceneManager.LoadScene (1);
    }
	
    void FadeIn ()
    {
        splashImage.CrossFadeAlpha (1f, 1.5f, false);
    }

    void FadeOut ()
    {
        splashImage.CrossFadeAlpha (0f, 1.5f, false);
    }
}
