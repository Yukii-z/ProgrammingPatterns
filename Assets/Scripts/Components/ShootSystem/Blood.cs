using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private float _initialBlood;
    public float blood = 5.0f;

    private void Awake()
    {
        _initialBlood = blood;
    }

    public void ResetBlood()
    {
        blood = _initialBlood;
    }
}
