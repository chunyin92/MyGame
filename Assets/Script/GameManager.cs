using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.BattleSystem;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public enum GameState { Default, InBattle, InConversation, InConversationBeforeBattle};
    public GameState gameState = GameState.Default;

    public Camera mainCam;
    public Camera battleCam;

    BattleController battleController;

    void Awake ()
    {
        #region Singleton

        if (instance != null)
        {
            Debug.LogWarning ("More then one GameManager");
            Destroy (gameObject);
            return;
        }
        else
            instance = this;

        #endregion

        DontDestroyOnLoad (gameObject);

        Init ();
    }

    void Update ()
    {
        //if (gameState == GameState.InBattle)
        //{
        //    //Time.timeScale = 0f;
        //    Debug.Log ("Enter battle");
        //}
    }

    void Init ()
    {
        if (!mainCam)
        {
            Debug.LogWarning ("Please assign Main Camera");
            return;
        }
            
        if (!battleCam)
        {
            Debug.LogWarning ("Please assign Battle Camera");
            return;
        }

        battleController = BattleController.instance;
    }

    public void EnterBattle ()
    {
        // TODO: add battle music
        // TODO: add screen transition

        Debug.Log ("EnterBattleScene");
        mainCam.gameObject.SetActive (false);
        battleCam.gameObject.SetActive (true);

        if (battleController == null)
        {
            battleController = BattleController.instance;
            Debug.Log ("AAA");
        }

        if (battleController.stateMachine == null)
        {
            Debug.Log ("BBB");
        }
        battleController.stateMachine.ChangeState (BattleInit.Instance);

        //SceneManager.LoadScene ("BattleScene", LoadSceneMode.Additive);        
    }

    public void LeaveBattle ()
    {
        gameState = GameState.Default;
        Debug.Log ("LeaveBattleScene");
        mainCam.gameObject.SetActive (true);
        battleCam.gameObject.SetActive (false);

        //SceneManager.UnloadSceneAsync ("BattleScene");
    }
}
