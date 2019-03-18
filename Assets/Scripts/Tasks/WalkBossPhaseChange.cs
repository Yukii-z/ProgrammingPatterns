using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WalkBossPhaseChange : MonoBehaviour
{
    private float _totalBlood;
    private SerialTasks serialRunner = new SerialTasks();
    private SerialTasks serialRunner2 = new SerialTasks();
    private static GameObject myObj;
    static GameObject player;

   

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        myObj= gameObject;
        _totalBlood = gameObject.GetComponent<Blood>().blood;
    }
    
 //private ChangeScaleTask growUp = new ChangeScaleTask(1f, gameObject,26f);
    //ChangeScaleTask shrink = new ChangeScaleTask(1f, myObj, 13f);
    WaitTask growWait = new WaitTask(3);
    WalkTask walkAround = new WalkTask(3f, player, myObj, 10f);
    private float bloodRate
    {
        get { return gameObject.GetComponent<Blood>().blood / _totalBlood; }
    }
    
// run the tasks (in a monobehaviour Update for example)
    private void Update()
    {    
        if (bloodRate > 0.7f)
        {
            
        }
        else if (bloodRate > 0.4)
        {
            if (serialRunner.HasTasks)
                        {
                            serialRunner.Update();
                        }
                        else
                        {
                            serialRunner.Add(new WalkTask(0.2f, player, gameObject, 10f));
                            serialRunner.Update();
                            Debug.Log(serialRunner.HasTasks);
                        }
        }
        else
        {
            if (serialRunner.HasTasks)
            {
                serialRunner.Update();
            }
            else
            {
                serialRunner.Add(new ChangeScaleTask(1f, gameObject,26f));
                serialRunner.Add(growWait);
                serialRunner.Add(new ChangeScaleTask(1f, gameObject, 13f));
                serialRunner.Update();
            }
            if (serialRunner2.HasTasks)
            {
                Debug.Log("imwalking");
                serialRunner2.Update();
            }
            else
            {
                serialRunner2.Add(new WalkTask(1f, player, gameObject, 10f));
                serialRunner2.Update();
            }
        }
        
    }
}
