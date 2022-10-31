using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Vector2 move;
    [SerializeField] float speed = 0.01f;
    // Start is called before the first frame update
    void Update()
    {
        move = gameObject.transform.position;
        move.x += speed;
        gameObject.transform.position = move;
    }
}
