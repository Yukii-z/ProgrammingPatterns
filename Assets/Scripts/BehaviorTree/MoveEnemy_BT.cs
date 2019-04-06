using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Random = System.Random;

public class MoveEnemy_BT : MonoBehaviour
{
    private Tree<MoveEnemy_BT> _tree;
    private GameObject _player;
    private int _totalEnemy;
    private int _enemyNumDifference;
    private float _speed = 0.5f;
    private float waitTime = 0.5f;
    private float startTime= 0f;
    private float _moveTime = 1f;
    private int _moveCount = 0;
    private float _visibilityRange = 5f;
    private Vector3 _aimDirection = new Vector3(0f, 0f, 0f);

    float _bloodRate
    {
        get { return gameObject.GetComponent<Blood>().blood / gameObject.GetComponent<Blood>().initialBlood; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _totalEnemy = GameObject.Find("EnemyManager").GetComponentsInChildren<Transform>().Length - 1;
        startTime = Time.time;
        _player = GameObject.FindWithTag("Player");
        _tree = new Tree<MoveEnemy_BT>(new Selector<MoveEnemy_BT>(
            // change the position once in a while
            new Sequence<MoveEnemy_BT>(
                new IsEnemyLess(),
                new SpeedUp()
            ),
            new Walk()
        ));
    }
    
    // Update is called once per frame
    void Update()
    {
        _tree.Update(this);
        
    }

    //Exact func
    private void MoveTowardsPlayer()
    {
        var direction = (_player.transform.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }

    bool LessEnemy
    {
        get
        {
            return GameObject.Find("EnemyManager").GetComponentsInChildren<Transform>().Length < _totalEnemy;
        }
    }

    void ChangeSpeed()
    {
        _enemyNumDifference = _totalEnemy - GameObject.Find("EnemyManager").GetComponentsInChildren<Transform>().Length;
        _totalEnemy = GameObject.Find("EnemyManager").GetComponentsInChildren<Transform>().Length;
        _speed += _speed * _enemyNumDifference * 0.5f;
    }
    
    // condition
    private class IsEnemyLess : Node<MoveEnemy_BT>
    {
        public override bool Update(MoveEnemy_BT jumpEnemy)
        {
            return jumpEnemy.LessEnemy;
        }
    }
    
    //action
    private class SpeedUp : Node<MoveEnemy_BT>
    {
        public override bool Update(MoveEnemy_BT jumpEnemy)
        {
            jumpEnemy.ChangeSpeed();
            return true;
        }
    }
    
    private class Walk : Node<MoveEnemy_BT>
    {
        public override bool Update(MoveEnemy_BT jumpEnemy)
        {
            jumpEnemy.MoveTowardsPlayer();
            return true;
        }
    }
}
