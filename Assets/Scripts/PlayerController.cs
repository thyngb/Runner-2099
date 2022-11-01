using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int curHealth = 0, maxHealth = 3;

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
    [SerializeField] private float speed;

    public float timeDuration = 0;
    public TMP_Text text;

    void Start()
    {
        curHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        getInputMovement();
        getPlayerHealth();
        getPlayerSpeed();
        setPlayerScore();
    }

    private void setPlayerScore()
    {
        PlayerPrefs.SetFloat("score", timeDuration);
        text.text = timeDuration.ToString();
    }

    private void getScoreAtTime()
    {
        timeDuration += Time.deltaTime;
    }

    private void getPlayerSpeed()
    {
        move = gameObject.transform.position;
        move.x += speed;
        gameObject.transform.position = move;
    }

    private void getPlayerHealth()
    {
        if (curHealth <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if(curHealth > 0)
        {
            getScoreAtTime();
        }
    }

    private void getInputMovement()
    {
        print(isGrounded);
        
        anim.SetBool("", true);
        isGrounded = Physics2D.OverlapCircle(feet.position, 0.5f, groundings);
        
        //for jumping
        if ((isGrounded == true && (Input.GetAxisRaw("Vertical") == 1)))
        {
            rb.velocity = Vector2.up * jumpforce;
        }

        //for sliding
        if (isGrounded == true && !sliding && (Input.GetAxisRaw("Vertical") == -1))
        {
            slideTimer = 0f;
            anim.SetBool("isSliding", true);
            sliding = true;
        }
        if (sliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer > maxSlideTime)
            {
                sliding = false;
                anim.SetBool("isSliding", false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("Camera"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Destroy(this.gameObject);
        }
        if (collision.collider.tag.Contains("Obstacle"))
        {
            curHealth--;
        }
    }

}
