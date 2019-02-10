using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Speed), typeof(TargetObj))]
public class MoveTowardsPlayer : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Speed>().speed = 0.5f;
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TargetObj>().target = Player.transform.position;
        transform.position += (GetComponent<TargetObj>().target - transform.position) * GetComponent<Speed>().speed *
                             Time.deltaTime;
    }
}
