using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Speed), typeof(TargetObj))]
public class JumpTowardsPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovementsLoop());
    }

   
    public enum MoveStates
    {
        SlowMove,
        Rush,
        Prepare,
    }

    private new MoveStates myState = MoveStates.SlowMove;

    IEnumerator MovementsLoop()
    {
        while (true)
        {
            myState = MoveStates.SlowMove;
            Coroutine slowMove = StartCoroutine(SlowMove());
            yield return new WaitForSeconds(6.0f);
            myState = MoveStates.Prepare;
            StopCoroutine(slowMove);
            Prepare();
            yield return new WaitForSeconds(1.0f);
            myState = MoveStates.Rush;
            Rush();
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += (GetComponent<TargetObj>().target - transform.position).normalized * GetComponent<Speed>().speed *
                             Time.deltaTime;
    }

    void Rush()
    {
        GetComponent<Speed>().speed = 12.0f;
        GetComponent<TargetObj>().target =
            (GameObject.FindWithTag("Player").transform.position - transform.position) * 10f + transform.position;
    }

    IEnumerator SlowMove()
    {
        while (true)
        {
            GetComponent<Speed>().speed = 1.0f;
            GetComponent<TargetObj>().target = transform.position + transform.right * 10f;
            yield return new WaitForSeconds(2.0f);
            GetComponent<TargetObj>().target = transform.position + transform.up * 10f;
            yield return new WaitForSeconds(2.0f);
            GetComponent<TargetObj>().target = transform.position - transform.right * 10f;
            yield return new WaitForSeconds(2.0f);
            GetComponent<TargetObj>().target = transform.position - transform.up * 10f;
            yield return new WaitForSeconds(2.0f);
        }
        
    }

    void Prepare()
    {
            GetComponent<Speed>().speed = 0.0f;
    }
}
