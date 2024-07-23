using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : BasicGoblin
{
    public GameObject ratHole;
    protected override void Start()
    {
        health = 0.5f;
        moveSpeed = 4f;
        playerCloseSightDistance = 10f;
        playerCloseAttackDistance = 2f;
        attackWindUpTime = 0;
        Instantiate(ratHole, transform.position, Quaternion.identity);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // if close, stop moving and attack
        if (attackPlayer)
        {
            transform.LookAt(player.transform, Vector3.up);
            if (woundUp)
            {
                // attack player
                attackPlayer = false;
                attackingSign.SetActive(false);
                chasePlayer = true;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().LoseHealth();
        }
    }
}
