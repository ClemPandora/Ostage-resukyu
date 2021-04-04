using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MeleeEnemy : EnemyAI
{
    public GameObject knife;
    
    //Attack the target with a knife
    public override void Action()
    {
        //Set the next attack timer
        nextAttack = Time.time + attackCooldown;
        
        //Look at the target
        transform.LookAt(target, transform.up);
        
        //enable the knife, and make it rotate to collide with the target
        knife.SetActive(true);
        knife.transform.DOLocalRotate(new Vector3(0, -50, 0), 0.4f)
            .OnComplete(() =>
            {
                knife.SetActive(false);
                knife.transform.localRotation = Quaternion.Euler(0, 50, 0);
            });
    }

    //Double the knife damage on phase 2
    public override void SwitchPhase()
    {
        base.SwitchPhase();
        Knife kn = knife.GetComponent<Knife>();
        if (kn != null)
        {
            kn.dmg *= 2;
        }
    }
}