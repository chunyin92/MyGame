using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleScence : MonoBehaviour {

    void Update()
    {
        //if (GameManager.currentGameState == GameManager.GameState.NORAML)
        {
            Debug.Log("destroy");
            SceneManager.UnloadSceneAsync("Battle");
        }
    }
}
