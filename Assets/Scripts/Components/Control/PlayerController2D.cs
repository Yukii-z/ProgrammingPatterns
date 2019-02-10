using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    //attach this to the player sprite
    // Start is called before the first frame update
    private GameObject bullet;
    bool bulletStart = true;
    private float bulletWaitTime = 2f;
    void Start()
    {
        bullet= Resources.Load<GameObject>("Prefabs/Flower");
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithKeyboard(KeyCode.W,KeyCode.S,KeyCode.A, KeyCode.D, 3f);
    }

    void MoveWithKeyboard(KeyCode up, KeyCode down, KeyCode left, KeyCode right, float speed=0f)
    {
        if (Input.GetKey(up))
        {
            transform.position +=new Vector3(0f, speed * Time.deltaTime, 0f);
            //Debug.Log("Up");
        }

        if (Input.GetKey(down))
        {
            transform.position +=new Vector3(0f, -speed * Time.deltaTime, 0f);
        }

        if (Input.GetKey(left))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(right))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
    }

}
