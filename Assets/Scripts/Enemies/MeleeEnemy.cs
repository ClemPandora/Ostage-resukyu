using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MeleeEnemy : EnemyAI
{
    public GameObject knife;
    public override void Action()
    {
        if (Vector3.Distance(transform.position, target.position) > positionRange
            || Physics.Linecast(transform.position, target.position, coverLayer))
        {
            SetState(new MoveState(this));
        }
        else
        {
            nextAttack = Time.time + attackCooldown;
            knife.SetActive(true);
            knife.transform.DOLocalRotate(new Vector3(0, -50, 0), 0.4f)
                .OnComplete(() =>
                {
                    knife.SetActive(false);
                    knife.transform.localRotation = Quaternion.Euler(0, 50, 0);
                });
        }
    }
}
