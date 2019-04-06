using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    private FSM<Game> _fsm;
    private GameObject _player;
    public Text ShowText;
    private float _playerBlood
    {
        get { return _player.GetComponent<Blood>().blood; }
    }
    private void Start()
    {
        // Initialize the FSM with the context (in this case the critter whose states we're managing)
        _fsm = new FSM<Game>(this);

        // Set the initial state. Don't forget to do this!!
        _fsm.TransitionTo<PlayState>();
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // Update the 'brain'
        _fsm.Update();
        Debug.Log(_fsm.CurrentState);
             if (Input.GetKeyDown(KeyCode.R))
             {
                     ((GameState) _fsm.CurrentState).OnResetGame();
             }
             
     
             if (Input.GetKeyDown(KeyCode.Escape))
             {
                 ((GameState)_fsm.CurrentState).OnPauseGame();
                 ((GameState)_fsm.CurrentState).OnStopPause();
             }
     
             if (_playerBlood <= 0f)
             {
                 ((GameState)_fsm.CurrentState).OnPlayerDead();
             }
     
             if (EnemyManager.Instance.finishedWave == 6 && _playerBlood>0f)
             {
                 ((GameState)_fsm.CurrentState).OnGameWin();
             }
         }
    
    public class GameState : FSM<Game>.State
    {
        public virtual void OnResetGame(){}
        public virtual void OnPauseGame(){} 
        public virtual void OnStopPause(){}
        public virtual void OnPlayerDead(){}
        public virtual void OnGameWin(){}
    }
    
    public class EnterState : GameState
    {
        private float _waitTime = 0f;
        public override void OnEnter()
        {
            EnemyManager.Instance.Clear();
            UISystem.Instance.Clear();
            AchievementManager.Instance.Clear();
            _waitTime = 0f;
            Context._player.GetComponent<Blood>().ResetBlood();
        }
    
        public override void Update()
        {
            _waitTime += Time.deltaTime;
            if (_waitTime > 1f)
            {
                TransitionTo<PlayState>();
                EnemyManager.Instance.emitControl = false;
            }
        }
    
    }
    
    public class PlayState : GameState
    {
        public override void OnPauseGame()
        {
            TransitionTo<PauseState>();
        }
    
        public override void OnResetGame()
        {
            TransitionTo<EnterState>();
        }

        public override void OnEnter()
        {
            
        }

        public override void OnPlayerDead()
        {
            TransitionTo<DeadState>();
        }
    
        public override void OnGameWin()
        {
            TransitionTo<WinningState>();
        }
    }
    
    public class PauseState : GameState
    {
        public override void OnStopPause()
        {
            TransitionTo<PlayState>();
        }
    
        public override void OnEnter()
        {
            Time.timeScale = 0;
        }
    
        public override void OnExit()
        {
            Time.timeScale = 1;
        }
    }
    
    public class DeadState : GameState
    {
        public override void OnResetGame()
        {
            TransitionTo<EnterState>();
        }
    
        public override void OnEnter()
        {
            Context.ShowText.text = "You Died!";
            Time.timeScale = 0;
        }
        
        public override void OnExit()
        {
            Context.ShowText.text = String.Empty;
            Time.timeScale = 1;
        }
    }
    
    public class WinningState : GameState
    {
        public override void OnResetGame()
        {
            TransitionTo<EnterState>();
        }
        
        public override void OnEnter()
        {
            Context.ShowText.text = "You Win!";
            Time.timeScale = 0;
        }
        
        public override void OnExit()
        {
            Context.ShowText.text = String.Empty;
            Time.timeScale = 1;
        }
    }
}


