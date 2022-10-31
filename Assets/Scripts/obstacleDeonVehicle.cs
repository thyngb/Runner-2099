using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class obstacleDeonVehicle : MonoBehaviour
{
    //D-Eon Vehicles

    //property
    private Rigidbody2D rigidbody2d;
    [SerializeField] Vector2 move;
    [SerializeField] float speed;

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
        getPhysic();
        getMomentum();
        getAnimation();
    }

    private void getAnimation()
    {
        throw new NotImplementedException();
    }

    private void getPhysic()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //implement here how dis appearance
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
        //implement here how dis dies
    }
}
