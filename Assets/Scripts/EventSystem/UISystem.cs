using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    static private UISystem instance;
    static public UISystem Instance {
        get {
            if (instance == null) {
                instance = new UISystem();
            }
            return instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddHandler<EnemyKilled>(OnEnemyKilled);
        EventManager.Instance.AddHandler<EnemyWaveKilled>(OnEnemyWaveKilled);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<EnemyKilled>(OnEnemyKilled);
        EventManager.Instance.RemoveHandler<EnemyWaveKilled>(OnEnemyWaveKilled);
    }
    
    public Text[] textBox;
    private int[] killedEnemyNum = new[] {0, 0, 0};
    
    private void OnEnemyKilled(EnemyKilled evt)
    {
        int i= (int) evt.enemyType;
        Debug.Log(evt.enemyType);
        Debug.Log(i);
        killedEnemyNum[i]++;
        textBox[i].text = killedEnemyNum[i].ToString();
    }

    private int killedWaveNum = -1;
    public Text waveTextBox;
    private void OnEnemyWaveKilled(EnemyWaveKilled evt)
    {
        killedWaveNum++;
        waveTextBox.text = killedWaveNum.ToString();
    }
}
