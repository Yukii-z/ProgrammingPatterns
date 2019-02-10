using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Bullet), typeof(TargetForShooting), typeof(BulletSpeed))]
public class ShootTarget : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject bullet;
    GameObject player;
    private GameObject bulletManager;
    void Start()
    {   
        bulletManager = GameObject.Find("BulletManager");
        GetComponent<Bullet>().bullet = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        bullet = GetComponent<Bullet>().bullet;
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Shoot());

    }

    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject newBullet = GameObject.Instantiate(bullet, bulletManager.transform);
            newBullet.transform.position = transform.position;
            GetComponent<TargetForShooting>().targetForShooting = player.transform.position;
            Vector3 target = GetComponent<TargetForShooting>().targetForShooting;
            target = (target - transform.position).normalized * 50f + transform.position;
            newBullet.GetComponent<BulletMove>().SetBullet(target, GetComponent<BulletSpeed>().bulletSpeed);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
