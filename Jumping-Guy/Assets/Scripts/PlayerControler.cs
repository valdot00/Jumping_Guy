using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public GameObject game;
    public GameObject enemyGenerator;
    public AudioClip jumpClip;
    public AudioClip dieClip;
    public ParticleSystem dust;

    private Animator animator;
    private AudioSource audioPlayer;
    private float startY;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update(){

        bool isGrounded = transform.position.y == startY;
        bool gamePlaying = game.GetComponent<GameControler>().gameState == GameState.Playing;
        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);

        if(isGrounded && gamePlaying && userAction){
            UpdateState("PlayerJumping");
            //sonido de salto
            audioPlayer.clip = jumpClip;
            audioPlayer.Play();
        }
        
    }
    public void UpdateState(string state = null){
        if(state != null){
            animator.Play(state);
        }
    }    

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            //Debug.Log("Me muero!");
            UpdateState("PlayerDie");
            game.GetComponent<GameControler>().gameState = GameState.Ended;
            enemyGenerator.SendMessage("CancelGenerator",true);
            game.SendMessage("ResetTimeScale",0.5f);

            //musica y sonidos
            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = dieClip;
            audioPlayer.Play();

            DustStop();
        }
    }  
    void GameReady(){
        game.GetComponent<GameControler>().gameState = GameState.Ready;
    }  

    void DustPlay(){
        dust.Play();
    }

    void DustStop(){
        dust.Stop();
    } 

}

