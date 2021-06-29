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
    public GameObject uiScore; //muestra los puntos del jugador

    public Text pointsText;
    public Text recordText;

    //public enum GameState {Idle, Playing, Ended};
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    public float scaleTime = 6f;
    public float scaleInc = .25f;

    private AudioSource musicPlayer;
    private int points = 0; 

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        recordText.text = "Best: " + GetMaxScore().ToString();
    }

    // Update is called once per frame
    void Update(){

        bool userAction = Input.GetKeyDown("up")||Input.GetMouseButtonDown(0); 
        
        //EMPIEZA EL JUEGO
        if (gameState == GameState.Idle && userAction){
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            uiScore.SetActive(true);
            player.SendMessage("UpdateState","PlayerRun");
            player.SendMessage("DustPlay");
            enemyGenerator.SendMessage("StartGenerator");
            musicPlayer.Play();
            InvokeRepeating("GameTimeScale",scaleTime,scaleTime);
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
        ResetTimeScale();
        SceneManager.LoadScene("SampleScene");//colocar el nombre principal de tu esena
    }

    void GameTimeScale(){
        Time.timeScale += scaleInc;
        Debug.Log("Ritmo incrementado" + Time.timeScale.ToString());
    }

    public void ResetTimeScale(float newTimeScale = 1f){
        CancelInvoke("GameTimeScale");
        //Time.timeScale = 1f;
        Time.timeScale = newTimeScale;
        Debug.Log("Ritmo reestablecido: " + Time.timeScale.ToString());
    }

    public void IncreasePoints(){
        //points++;
        pointsText.text = (++points).ToString();

        if(points >= GetMaxScore()){
           recordText.text = "Best: " + points.ToString();
           SaveScore(points);
        }
    }

    public int GetMaxScore(){
        return PlayerPrefs.GetInt("Max Points",0);
    } 

    public void SaveScore(int currentPoints){
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }   
}
