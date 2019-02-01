using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    //attach this to the player sprite
    // Start is called before the first frame update
    private GameObject bullet;
    private GameObject bulletManager;
    bool bulletStart = true;
    private float bulletWaitTime = 2f;
    void Start()
    {
        bulletManager = ObjectFactory.CreateGameObject("BulletManager");
        bullet= Resources.Load<GameObject>("Prefabs/Flower");
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithKeyboard(KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.LeftArrow, KeyCode.RightArrow, 3f);
        ShootWithKeyboard(KeyCode.W,KeyCode.S,KeyCode.A, KeyCode.D, 8f);
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
    
    void ShootWithKeyboard(KeyCode up, KeyCode down, KeyCode left, KeyCode right, float speed=0f)
    {
        if (Input.GetKeyDown(up) || Input.GetKeyDown(down) || Input.GetKeyDown(left)|| Input.GetKeyDown(right))
                          {
                              bulletStart = true;
                              Debug.Log("bullet start = " + bulletStart);
                          }
        if (bulletStart)
        {
           
            
            if (Input.GetKey(up))
            {
                GameObject newBullet = GameObject.Instantiate(bullet, bulletManager.transform);
                newBullet.transform.position = transform.position;
                newBullet.GetComponent<BulletMove>().speed = speed;
                bullet.GetComponent<BulletMove>().direction = BulletMove.Direction.Up;
                //bulletStart = !bulletStart;
                StartCoroutine(StartShootBullet(bulletWaitTime));
            }

            if (Input.GetKey(down))
            {
                GameObject newBullet = GameObject.Instantiate(bullet, bulletManager.transform);
                newBullet.transform.position = transform.position;
                newBullet.GetComponent<BulletMove>().speed = speed;
                bullet.GetComponent<BulletMove>().direction = BulletMove.Direction.Down;
                //bulletStart = !bulletStart;
                StartCoroutine(StartShootBullet(bulletWaitTime));
            }

            if (Input.GetKey(left))
            {
                GameObject newBullet = GameObject.Instantiate(bullet, bulletManager.transform);
                newBullet.transform.position = transform.position;
                newBullet.GetComponent<BulletMove>().speed = speed;
                bullet.GetComponent<BulletMove>().direction = BulletMove.Direction.Left;
                //bulletStart = !bulletStart;
                StartCoroutine(StartShootBullet(bulletWaitTime));
            }

            if (Input.GetKey(right))
            {
                GameObject newBullet = GameObject.Instantiate(bullet, bulletManager.transform);
                newBullet.transform.position = transform.position;
                newBullet.GetComponent<BulletMove>().speed = speed;
                bullet.GetComponent<BulletMove>().direction = BulletMove.Direction.Right;
                //bulletStart = !bulletStart;
                StartCoroutine(StartShootBullet(bulletWaitTime));
            }
        }
    }
    
    IEnumerator StartShootBullet(float waitTime)
    {
        bulletStart = false;
        yield return new WaitForSeconds(waitTime);
        bulletStart = true;
    }
}
