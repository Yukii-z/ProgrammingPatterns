using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public float initialBlood = 5.0f;
    public float blood = 5.0f;

    private void Awake()
    {
        initialBlood = blood;
    }

    public void ResetBlood()
    {
        blood = initialBlood;
    }
}
