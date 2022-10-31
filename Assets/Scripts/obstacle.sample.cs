using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    //Boulder
    //Energy Screen
    //D-Eon Vehicles

    //property
    private Rigidbody2D rigidbody2d;
    [SerializeField] Vector2 move;
    [SerializeField] float speed;

    //option
    [SerializeField] bool option_physics;
    [SerializeField] bool option_momentum;
    [SerializeField] bool option_animation;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        getOptioned();
    }
    private void getOptioned()
    {
        if (option_momentum == true) getPhysic();
        if (option_momentum == true) getMomentum();
        if (option_animation == true) getAnimation();
    }

    private void getAnimation()
    {
        throw new NotImplementedException();
    }

    private void getPhysic()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void getMomentum()
    {
        move = gameObject.transform.position;
        move.x -= speed; 
        gameObject.transform.position = move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
