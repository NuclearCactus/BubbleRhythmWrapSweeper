using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CustomGridScript CustomGridScript;

    internal void PlayExplosionSound()
    {
        //throw new NotImplementedException();
    }

    // update pls
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayMouseClick();
        }
    }

    private void PlayMouseClick()
    {
        //throw new NotImplementedException();
    }
}
