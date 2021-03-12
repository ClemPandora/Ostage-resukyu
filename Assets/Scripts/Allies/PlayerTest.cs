using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public Vector3 playerPos;

    public void Update()
    {
        playerPos = transform.position;
    }
}
