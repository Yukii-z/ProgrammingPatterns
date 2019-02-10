using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Bullet), typeof(TargetForShooting), typeof(BulletSpeed))]
public class PlayerShoot : MonoBehaviour
{
    private GameObject bullet;
    private bool coroutineOn;
    private Coroutine bulletShoot;
    private GameObject bulletManager;
    private void Start()
    {
        GetComponent<Bullet>().bullet = Resources.Load<GameObject>("Prefabs/Flower");
        bullet = GetComponent<Bullet>().bullet;
        bulletManager = GameObject.Find("BulletManager");
    }

    void Update(){
        ShootWithKeyboard(KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.LeftArrow, KeyCode.RightArrow, 8f);
    }
    void ShootWithKeyboard(KeyCode up, KeyCode down, KeyCode left, KeyCode right, float speed=0f)
    {


        if (Input.GetKey(up))
            {
                GetComponent<TargetForShooting>().targetForShooting = transform.position + transform.up * 50f;
            }

            if (Input.GetKey(down))
            {
                GetComponent<TargetForShooting>().targetForShooting = transform.position - transform.up * 50f;
            }
            if (Input.GetKey(right))
            {
                GetComponent<TargetForShooting>().targetForShooting = transform.position + transform.right * 50f;
            }

            if (Input.GetKey(left))
            {
                GetComponent<TargetForShooting>().targetForShooting = transform.position - transform.right * 50f;
            }

            if (Input.GetKeyDown(up) || Input.GetKeyDown(down) || Input.GetKeyDown(left) || Input.GetKeyDown(right))
            {
                if (!coroutineOn)
                {
                    bulletShoot = StartCoroutine(StartShootBullet());
                    coroutineOn = true;
                }
            }
            if (Input.GetKey(up) || Input.GetKey(down) || Input.GetKey(left) || Input.GetKey(right)){}else
            {
                if (coroutineOn)
                {
                    StopCoroutine(bulletShoot);
                    coroutineOn = false;
                }
            }
    }

    IEnumerator StartShootBullet(){
        while (true)
        {
            
            GameObject newBullet = GameObject.Instantiate(bullet, bulletManager.transform);
            newBullet.transform.position = transform.position;
            Vector3 target = GetComponent<TargetForShooting>().targetForShooting;
            target = (target - transform.position).normalized * 50f + transform.position;
            newBullet.GetComponent<BulletMove>().SetBullet(target, GetComponent<BulletSpeed>().bulletSpeed);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
