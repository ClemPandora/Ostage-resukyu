﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKitItem : Item
{
    public float healthAmount;
    
    public override void Use(Player player)
    {
        player.health += healthAmount;
        base.Use(player);
    }
}
