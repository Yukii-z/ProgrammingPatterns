using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Random = System.Random;

public class JumpEnemy_BT : MonoBehaviour
{
    private Tree<JumpEnemy_BT> _tree;
    private GameObject _player;
    private float _speed = 2f;
    private float _attackSpeed = 6f;
    private float waitTime = 0.5f;
    private float startTime= 0f;
    private float _moveTime = 1f;
    private int _moveCount = 0;
    private float _visibilityRange = 3f;
    private Vector3 _aimDirection = new Vector3(0f, 0f, 0f);

    float _bloodRate
    {
        get { return gameObject.GetComponent<Blood>().blood / gameObject.GetComponent<Blood>().initialBlood; }
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        _player = GameObject.FindWithTag("Player");
        _tree = new Tree<JumpEnemy_BT>(new Selector<JumpEnemy_BT>(
            // change the position once in a while
            new Sequence<JumpEnemy_BT>(
                new IsTimeChange(),
                new Selector<JumpEnemy_BT>(
                    new Sequence<JumpEnemy_BT>(new Not<JumpEnemy_BT>( new IsPlayerInRange()), new ChangeDirection()),
                    new Sequence<JumpEnemy_BT>(new IsPlayerInRange(), new ChangeDirectionToPlayer()))
            ),
            //
            new Sequence<JumpEnemy_BT>(new IsStopTime(), new Stop()),
            new Sequence<JumpEnemy_BT>( 
                new IsPlayerInRange(), 
                new Attack() // Attack
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
    private void MoveTowardsDirection()
    {     
        var direction = (_aimDirection - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }
    
    private void MoveTowardsPlayer()
    {
        var direction = (_aimDirection - transform.position).normalized;
        transform.position += direction * _attackSpeed * Time.deltaTime;
    }

    private bool timeChange{get
    {
        var i = (int)((Time.time - startTime) / _moveTime);
        Debug.Log(i);
        return i > _moveCount;
    }}

    private bool stopTime
    {
        get
        {
            var i = -(int)((Time.time - startTime) / _moveTime) * _moveTime + Time.time - startTime;
            Debug.Log((i / _moveTime) < 0.5f);
            return (i / _moveTime) < 0.5f;
        }
    }
    void ChangeAimDirection()
    {
        var x = UnityEngine.Random.Range(-1f, 1f);
        var y = UnityEngine.Random.Range(-1f, 1f);
        _aimDirection = new Vector3(x,y , 0f).normalized *100f;
    }
    
    void ChangeAimDirectionToPlayer()
    {
        _aimDirection = (_player.transform.position - transform.position).normalized *100f;
    }
    
    // condition
    private class IsTimeChange : Node<JumpEnemy_BT>
    {
        public override bool Update(JumpEnemy_BT jumpEnemy)
        {
            if (jumpEnemy.timeChange)
            {
                jumpEnemy._moveCount++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    private class IsPlayerInRange : Node<JumpEnemy_BT>
    {
        public override bool Update(JumpEnemy_BT jumpEnemy)
        {
            var playerPos = jumpEnemy._player.transform.position;
            var enemyPos = jumpEnemy.transform.position;
            return Vector3.Distance(playerPos, enemyPos) < jumpEnemy._visibilityRange;
        }
    }
    
    private class IsStopTime : Node<JumpEnemy_BT>
    {
        public override bool Update(JumpEnemy_BT jumpEnemy)
        {
            return jumpEnemy.stopTime;
        }
    }
    
    //action
    private class ChangeDirection : Node<JumpEnemy_BT>
    {
        public override bool Update(JumpEnemy_BT jumpEnemy)
        {
            jumpEnemy.ChangeAimDirection();
            return true;
        }
    }
    
    private class ChangeDirectionToPlayer : Node<JumpEnemy_BT>
    {
        public override bool Update(JumpEnemy_BT jumpEnemy)
        {
            jumpEnemy.ChangeAimDirectionToPlayer();
            return true;
        }
    }
    
    private class Attack : Node<JumpEnemy_BT>
    {
        public override bool Update(JumpEnemy_BT jumpEnemy)
        {
            jumpEnemy.MoveTowardsPlayer();
            return true;
        }
    }
    
    private class Walk : Node<JumpEnemy_BT>
    {
        public override bool Update(JumpEnemy_BT jumpEnemy)
        {
            jumpEnemy.MoveTowardsDirection();
            return true;
        }
    }
    
    private class Stop : Node<JumpEnemy_BT>
    {
        public override bool Update(JumpEnemy_BT jumpEnemy)
        {
            return true;
        }
    }
}
