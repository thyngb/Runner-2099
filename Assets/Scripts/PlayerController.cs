using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpforce;
    public Transform feet;
    private bool isGrounded;
    public LayerMask groundings;
    [SerializeField] Vector2 move;
    
    //for sliding
    private Animator anim;
    bool sliding = false;
    float slideTimer = 0f;
    public float maxSlideTime = 1.5f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update(){
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        move = gameObject.transform.position;
        move.x +=0.01f; //change the speed of the player's movement
        gameObject.transform.position = move;

        getInputMovement();    
        }

    void getInputMovement(){
        isGrounded = Physics2D.OverlapCircle(feet.position, 0.5f, groundings);
        print(Input.GetAxisRaw("Vertical"));
        //for jumping
        if ((isGrounded == true && (Input.GetAxisRaw("Vertical") == 1))){
            rb.velocity = Vector2.up * jumpforce;
        }

        //for sliding
        if(isGrounded == true && !sliding && (Input.GetAxisRaw("Vertical")==-1)){
            slideTimer = 0f;
            anim.SetBool("isSliding", true);
            sliding = true;
        }
        if(sliding){
            slideTimer += Time.deltaTime;
            if(slideTimer > maxSlideTime){
                sliding = false;
                anim.SetBool("isSliding", false);
            }
        }
    }
}
