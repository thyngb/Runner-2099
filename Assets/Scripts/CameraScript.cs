using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] public int camSizeHorizontal;
    [SerializeField] private float cameraOffsetAtX, cameraOffsetAtY;
    [SerializeField] private Transform playerPosition;
    [SerializeField] Collider2D colider;

    // Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {
        camSizeHorizontal = (int)colider.bounds.size.x;
    }
    void Update()
    {
        transform.position = new Vector3(
            playerPosition.position.x + cameraOffsetAtX,
            transform.position.y,
            transform.position.z
        );
    }
    public void SetPosition(Vector2 newPos)
    {
        transform.position = new Vector3(
            newPos.x,
            newPos.y,
            transform.position.z
        );
    }
}
