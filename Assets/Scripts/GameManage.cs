using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManage : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        // Initialize your services in the order     
        // and with the parameters you need
        gameObject.AddComponent<Game>();
        Services.GameState = gameObject.GetComponent<Game>();
       // gameObject.AddComponent<EnemyManager>();
        //Services.Enemy = gameObject.GetComponent<EnemyManager>();
        Services.Enemy = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        Services.Event = new EventManager();
        gameObject.AddComponent<UISystem>();
        Services.UI = gameObject.GetComponent<UISystem>();
        gameObject.AddComponent<AchievementManager>();
        Services.Achienement = gameObject.GetComponent<AchievementManager>();
    }   
    
    public void Update()
    {     
        // Retrieve the services as needed     
    }
    
    public void OnDestroy()   
    {     
        // Because you're not using singletons you can manage     
        // the order of destruction for your systems and also     
        // do any additional work required to wind your systems down
       
        Services.Achienement.Clear();
        Services.Achienement = null;
        Services.UI.Clear();
        Services.UI = null;
        Services.Event.Clear();
        Services.Event = null;
        Services.Enemy.Clear();
        Services.Enemy = null;
        Services.GameState = null;
    }
}
