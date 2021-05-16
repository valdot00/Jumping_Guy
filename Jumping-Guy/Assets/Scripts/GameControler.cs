using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    [Range(0f,0.20f)] //para poner un rango en Unity para trabajar mas comodo
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage Platform;
    public GameObject uiIdle; //titulo del juego informacion de inicio

    public enum GameState {Idle, Playing};
    public GameState gameState = GameState.Idle;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //EMPIEZA EL JUEGO
        if (gameState == GameState.Idle && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))){
            gameState = GameState.Playing;
            uiIdle.SetActive(false);

            player.SendMessage("UpdateState","PlayerRun");
        }
        //Juego en marcha
        else if (gameState == GameState.Playing){
            Parallax();
    }
  }

  void Parallax(){
         float finalSpeed = parallaxSpeed * Time.deltaTime;
         background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f,1f,1f);
         Platform.uvRect = new Rect(Platform.uvRect.x + finalSpeed * 4,0f,1f,1f);

    }     
}
