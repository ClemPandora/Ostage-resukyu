using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MeleeEnemy : EnemyAI
{
    public GameObject knife;
    public override void Action()
    {
        
        nextAttack = Time.time + attackCooldown;
        transform.LookAt(target, transform.up);
        knife.SetActive(true);
        knife.transform.DOLocalRotate(new Vector3(0, -50, 0), 0.4f)
            .OnComplete(() =>
            {
                knife.SetActive(false);
                knife.transform.localRotation = Quaternion.Euler(0, 50, 0);
            });
    }

    public override void SwitchPhase()
    {
        base.SwitchPhase();
        Knife kn = knife.GetComponent<Knife>();
        if (kn != null)
        {
            Debug.Log("double damage");
            kn.dmg *= 2;
        }
    }
}