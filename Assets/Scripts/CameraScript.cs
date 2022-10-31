using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int camSizeHorizontal;

    public float startMoveSpeed;
    public float maxMoveSpeed;

    private float currentMoveSpeed;

    private float Delay = 3;

    // Start is called before the first frame update
    void Start()
    {
        currentMoveSpeed = startMoveSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        if (Delay > 0)
        {
            Delay -= Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(
                    transform.position.x + (currentMoveSpeed * Time.deltaTime),
                    transform.position.y,
                    transform.position.z
                );
        }
    }

    public void SetPosition(Vector2 newPos)
    {
        transform.position = new Vector3(
                newPos.x,
                newPos.y,
                transform.position.z
            );
    }

    public float getCamCurrSpeed()
    {
        return this.currentMoveSpeed;
    }
}
