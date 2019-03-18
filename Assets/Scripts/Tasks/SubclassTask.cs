using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionTask : Task
{
    // Action is part of the System library
    // it's just shorthand for a delegate that
    // takes no parameters and returns nothing:
    // delegate void Action();
    private readonly UnityAction _action;

    public ActionTask(UnityAction action)
    {
        _action = action;
    }

    internal override void Update()
    {
        SetStatus(TaskStatus.Success);
        _action();
    }
}

public class WaitTask : Task
{
    private readonly float _duration;
    private float _startTime;

    public WaitTask(float duration)
    {
        this._duration = duration;
    }

    // We don't want to set the start time right away
    // since the task may be in a sequence and not start til later. Wait until the task is started to initialize it.
    protected override void Init()
    {
        _startTime = Time.time;
    }

    internal override void Update()
    {
        var now = Time.time;
        var durationElapsed = (now - _startTime) > _duration;
        if (durationElapsed)
        {
            SetStatus(TaskStatus.Success);
        }
    }
}

public class ChangeScaleTask : Task
{
    private readonly float _duration;
    private float _startTime;
    private GameObject _myObj;
    //private float _time;
    private float _aimScale;
    public ChangeScaleTask(float duration, GameObject myObj, float aimScale)
    {
        this._duration = duration;
        _myObj = myObj;
        _aimScale = aimScale;
    }
    
    protected override void Init()
    {
        _startTime = Time.time;
    }

    internal override void Update()
    {

        _myObj.transform.localScale = new Vector3(Mathf.Lerp(_myObj.transform.localScale.x, _aimScale, 0.5f * Time.deltaTime),
            Mathf.Lerp(_myObj.transform.localScale.y, _aimScale, 0.5f * Time.deltaTime),
            Mathf.Lerp(_myObj.transform.localScale.z, _aimScale, 0.5f * Time.deltaTime));
        var now = Time.time;
        var durationElapsed = (now - _startTime) > _duration;
        if (durationElapsed)
        {
            SetStatus(TaskStatus.Success);
        }
    }
}

public class WalkTask : Task
{
    private float _speed;
    private GameObject _target;
    private GameObject _moveObj;
    private float _duration;
    private float _startTime;
    
    public WalkTask(float speed, GameObject target, GameObject moveObj, float duration)
    {
        _speed = speed;
        _target = target;
        _moveObj = moveObj;
        _duration = duration;
    }
    
    protected override void Init()
    {
        _startTime = Time.time;
    }
    internal override void Update()
    {
        _moveObj.transform.position += (_target.transform.position - _moveObj.transform.position) * _speed *
                              Time.deltaTime;
        var now = Time.time;
        var durationElapsed = (now - _startTime) > _duration;
        if (durationElapsed)
        {
            SetStatus(TaskStatus.Success);
        }
    }
}