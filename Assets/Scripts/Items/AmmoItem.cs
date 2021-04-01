using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : Item
{
    public float amount;

    public override void Use(Player player)
    {
        player.ammo += amount;
        base.Use(player);
    }
}
