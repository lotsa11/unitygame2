﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachPlatform : MonoBehaviour
{
    public GameObject Player;


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
  
            Player.transform.parent = null;
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }
}