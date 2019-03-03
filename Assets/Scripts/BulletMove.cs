using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float destroyWaitTime = 2f;
    // Start is called before the first frame update

    Vector3 ShootTarget;
    float bulletSpeed;
    private void Start()
    {
        StartCoroutine(DestroyItem(destroyWaitTime));
    }

    public void SetBullet(Vector3 targetInput, float bulletSpeedInput)
    {
        ShootTarget = targetInput;
        bulletSpeed = bulletSpeedInput;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += (ShootTarget - transform.position).normalized * bulletSpeed * Time.deltaTime;
    }

    IEnumerator DestroyItem(float destroyWaitTime)
    {
        yield return new WaitForSeconds(destroyWaitTime);
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
