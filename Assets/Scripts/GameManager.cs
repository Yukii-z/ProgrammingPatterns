using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
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
        //Services.Enemy = gameObject.AddComponent<EnemyManager>();
        //Services.Event = new EventManager();
        //Services.UI = gameObject.AddComponent<UISystem>();
        //Services.Achienement = gameObject.AddComponent<AchievementManager>();
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
        Services.Event.Clear();
        Services.Event = null;
        Services.Achienement.Clear();
        Services.Achienement = null;
        Services.UI.Clear();
        Services.UI = null;
        Services.Enemy.Clear();
        Services.Enemy = null;
        Services.GameState = null;
    }
}
