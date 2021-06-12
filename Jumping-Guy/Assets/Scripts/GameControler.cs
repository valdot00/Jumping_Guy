using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Idle,Playing,Ended,Ready};

public class GameControler : MonoBehaviour
{
    [Range(0f,0.20f)] //para poner un rango en Unity para trabajar mas comodo
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage Platform;
    public GameObject uiIdle; //titulo del juego informacion de inicio

    //public enum GameState {Idle, Playing, Ended};
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        bool userAction = Input.GetKeyDown("up")||Input.GetMouseButtonDown(0); 
        
        //EMPIEZA EL JUEGO
        if (gameState == GameState.Idle && userAction){
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            player.SendMessage("UpdateState","PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");
        }
        //Juego en marcha
        else if (gameState == GameState.Playing){
            Parallax();
        }
        //juego preparado para reiniciar
        else if (gameState == GameState.Ready){
           if(userAction){
               RestartGame();
           }
        }
  }

  void Parallax(){
         float finalSpeed = parallaxSpeed * Time.deltaTime;
         background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f,1f,1f);
         Platform.uvRect = new Rect(Platform.uvRect.x + finalSpeed * 4,0f,1f,1f);

    } 

    public void RestartGame(){
        SceneManager.LoadScene("SampleScene");//colocar el nombre pprincipal de tu esena
    }    
}
