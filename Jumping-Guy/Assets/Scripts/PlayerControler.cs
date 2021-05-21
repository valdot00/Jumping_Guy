using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown("up") || Input.GetMouseButtonDown(0)){
            UpdateState("PlayerJumping");
        }
        
    }
    public void UpdateState(string state = null){
        if(state != null){
            animator.Play(state);
        }

    }
}
