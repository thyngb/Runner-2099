using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class obstacleEnergyScreen : MonoBehaviour
{
    //Energy Screen

    //property
    private Rigidbody2D rigidbody2d;
    private Animator animate;

    [SerializeField] float speed;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }
    void Update()
    {
        getOptioned();
    }
    private void getOptioned()
    {
        getPhysic();
        getAnimation();
    }

    private void getAnimation()
    {
        throw new NotImplementedException();
        //implement here how dis appearance
    }

    private void getPhysic()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
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
