using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Blood), typeof(EnemyLayer))]
public class CanBeKilledByOther : MonoBehaviour
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
    private void OnCollisionEnter2D(Collision2D collideObj)
    {
        if (GetComponent<EnemyLayer>().EnemyLayerNumber.Length != 0)
        {
            foreach (var layer in GetComponent<EnemyLayer>().EnemyLayerNumber)
            {
                if (collideObj.gameObject.layer == layer)
                {
                    Debug.Log("get hurt");
                    blood = blood - collideObj.gameObject.GetComponent<Hurt>().hurt;
                    GetComponent<Blood>().blood = blood;
                    if (blood <= 0f)
                    {
                        gameObject.GetComponent<Enemy>().deadEnemy();
                    }
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
