using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Blood), typeof(EnemyLayer))]
public class CanBeKilled : MonoBehaviour
{
    // Start is called before the first frame update
    private float blood;
    private GameObject Player;
    void Start()
    {
        blood = GetComponent<Blood>().blood;
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collideObj)
    {
        if (GetComponent<EnemyLayer>().EnemyLayerNumber != null)
        {
            foreach (var layer in GetComponent<EnemyLayer>().EnemyLayerNumber)
            {
                if (collideObj.gameObject.layer == layer)
                {
                    blood = blood - collideObj.gameObject.GetComponent<Hurt>().hurt;
                    GetComponent<Blood>().blood = blood;
                }

                if (blood <= 0f)
                {
                    //StartCoroutine(DestroyEnemy());
                }
            }
        }
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this);
    }
}
