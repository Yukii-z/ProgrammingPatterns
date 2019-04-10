using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        textBox[0] = GameObject.Find("Text (3)").GetComponent<Text>();
        textBox[1] = GameObject.Find("Text (4)").GetComponent<Text>();
        textBox[2] = GameObject.Find("Text (5)").GetComponent<Text>();
        waveTextBox = GameObject.Find("Text (7)").GetComponent<Text>();
        Services.Event.AddHandler<EnemyKilled>(OnEnemyKilled);
        Services.Event.AddHandler<EnemyWaveKilled>(OnEnemyWaveKilled);
    }

    private void OnDestroy()
    {
        Services.Event.RemoveHandler<EnemyKilled>(OnEnemyKilled);
        Services.Event.RemoveHandler<EnemyWaveKilled>(OnEnemyWaveKilled);
    }
    
    public Text[] textBox = new Text[3];
    private int[] killedEnemyNum = new[] {0, 0, 0};
    
    private void OnEnemyKilled(EnemyKilled evt)
    {
        int i= (int) evt.enemyType;
        //Debug.Log(evt.enemyType);
        //Debug.Log(i);
       
        if (textBox.Length > i)
        {
            killedEnemyNum[i]++;
            textBox[i].text = killedEnemyNum[i].ToString();
        }
    }

    private int killedWaveNum = -1;
    public Text waveTextBox;
    private void OnEnemyWaveKilled(EnemyWaveKilled evt)
    {
        killedWaveNum++;
        waveTextBox.text = killedWaveNum.ToString();
    }

    public void Clear()
    {
        killedWaveNum = -1;
        killedEnemyNum = new[] {0, 0, 0};
        foreach (var text in textBox)
        {
            text.text = "0";
        }
        waveTextBox.text = "0";
    }
}
