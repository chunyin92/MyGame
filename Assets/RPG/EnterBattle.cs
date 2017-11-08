using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {        
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            //col.gameObject.GetComponent<PlayerController>().myRigidbody.velocity = Vector3.zero;

            //Time.timeScale = 0;

            //GameManager.currentGameState = GameManager.GameState.BATTLE;

            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);   
              
            Debug.Log("Battle Start");
        }
    }    
}
