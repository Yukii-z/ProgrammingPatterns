using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager _enemyManager;
    public EnemyManager.TypeOfEnemy _myType = EnemyManager.TypeOfEnemy.NoType;
    public bool isAlive { get; private set; }

    public void setEnemy(EnemyManager enemyManager, EnemyManager.TypeOfEnemy myType)
    {
        _enemyManager = enemyManager;
        _myType = myType;
        isAlive = true;
    }

    public void deadEnemy()
    {
        isAlive = false;
        if (gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    
}
   