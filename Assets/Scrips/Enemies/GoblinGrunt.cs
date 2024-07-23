using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGrunt : BasicGoblin
{
    public GameObject sword;

    // Start is called before the first frame update
    protected override void Start()
    {

        health = 3;
        moveSpeed = 5;
        attackSpeed = 0.5f;
        damage = 1f;
        hitRadius = 3f;
        playerCloseSightDistance = 10f;
        playerCloseAttackDistance = 3f;
        attackWindUpTime = 0.5f;
        if(sword.activeInHierarchy)
        {
            sword.SetActive(false);
        }
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // if close, stop moving and attack
        if(attackPlayer)
        {
            if(woundUp)
            {
                // attack player
                StartCoroutine(Swing());
                base.CreateDamageCircle(transform.position + transform.forward, hitRadius, damage, attackSpeed);
                woundUp = false;

            }
        }
    }
    IEnumerator Swing()
    {
        //sword.SetActive(true);
        yield return new WaitForSeconds(1/attackSpeed);
        woundUp = false;
        attackPlayer = false;
        //sword.SetActive(false);
        attackingSign.SetActive(false);
        chasePlayer = true;
    }
}
