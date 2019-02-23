using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class EnemyManager : MonoBehaviour
{
    public enum TypeOfEnemy
    {
        NoType,
        JumpEnemy,
        WalkEnemy,
        StayShootEnemy,
    }

    public List<GameObject> _enemy = new List<GameObject>();
    public Dictionary<string, int[]> _waveData = new Dictionary<string, int[]>();
    Dictionary<TypeOfEnemy, string> _prefabPath = new Dictionary<TypeOfEnemy,string>();
    private GameObject _player;
    bool emitControl;
    // Start is called before the first frame update
    void Start()
    {
        _waveData.Add("stayEnemy", new int[]{0, 0, 4});
        _waveData.Add("jumpEnemy", new int[]{6, 2, 0});
        _waveData.Add("walkEnemy", new int[]{1, 5, 2});
        _prefabPath.Add(TypeOfEnemy.JumpEnemy, "Prefabs/JumpEnemy");
        _prefabPath.Add(TypeOfEnemy.WalkEnemy, "Prefabs/WalkEnemy");
        _prefabPath.Add(TypeOfEnemy.StayShootEnemy, "Prefabs/StayEnemy");
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy.Count ==0 && emitControl ==false)
        {
            emitControl = true;
            StartCoroutine(EmitEnemy(RandomWave()));
        }

        foreach (var enemy in _enemy)
        {
            if (enemy.GetComponent<Enemy>().isAlive == false)
            {   
                _enemy.Remove(enemy);
                Destroy(enemy);
            }
        }
    }

    int[] RandomWave()
    {
        KeyValuePair<string,int[]> ChosenWave;
        int randomChoice = UnityEngine.Random.Range(0,_waveData.Count);
        int i = 0;
        foreach (KeyValuePair<string, int[]> a in _waveData)
        {
            i++;
            ChosenWave = a;
            if (i == randomChoice)
            {
                break;
            }
        }
        Debug.Log("ChosenWave = "+ ChosenWave.Value);
        return ChosenWave.Value;
    }
    IEnumerator EmitEnemy(int[] chosenWave)
    {
        yield return new WaitForSeconds(2f);
        int jumpEne=chosenWave[0];
        int walkEne=chosenWave[1];
        int stayEne =chosenWave[2];
        while (jumpEne > 0)
        {
            jumpEne--;
            CreateEnemy(TypeOfEnemy.JumpEnemy);
        }

        while (walkEne > 0)
        {
            walkEne--;
            CreateEnemy(TypeOfEnemy.WalkEnemy);
        }

        while (stayEne > 0)
        {
            stayEne--;
            CreateEnemy(TypeOfEnemy.StayShootEnemy);
        }

        emitControl = false;
    }

    void CreateEnemy(TypeOfEnemy enemyType)
    {
        GameObject newEnemy = Instantiate(Resources.Load<GameObject>(_prefabPath[enemyType]), gameObject.transform);
        newEnemy.GetComponent<Enemy>().setEnemy(this,enemyType);
        newEnemy.transform.position = EnemyPosition(enemyType);
        _enemy.Add(newEnemy);
    }

    private int stayEnemyPos = 0;
    Vector3 EnemyPosition(TypeOfEnemy enemyType)
    {
        float x = 0f;
        float y = 0f;
        if (enemyType == TypeOfEnemy.JumpEnemy)
        {
            x = 12.0f * UnityEngine.Random.value-6.0f;
            y = 6.0f * UnityEngine.Random.value - 3.0f;
        }

        if (enemyType == TypeOfEnemy.WalkEnemy)
        {
            x = 12.0f * UnityEngine.Random.value-6.0f;
            y = 6.0f * UnityEngine.Random.value - 3.0f;
        }

        if (enemyType == TypeOfEnemy.StayShootEnemy)
        {
            switch (stayEnemyPos)
            {
                case 0:
                    x = 6.14f;
                    y = 3.11f;
                    stayEnemyPos++;
                    break;
                case 1:
                    x = 6.14f;
                    y = -3.11f;
                    stayEnemyPos++;
                    break;
                case 2:
                    x = -6.14f;
                    y = 3.11f;
                    stayEnemyPos++;
                    break;
                case 3:
                    x = -6.14f;
                    y = -3.11f;
                    stayEnemyPos=0;
                    break;
            }
            
        }
        return new Vector3(x,y,-1f);
    }
}
