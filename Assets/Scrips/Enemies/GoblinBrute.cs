using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBrute : BasicGoblin
{
    public GameObject chargeSpot, chargeBox;
    //Quaternion lastRotation;
    protected override void Start()
    {
        health = 10;
        moveSpeed = 2f;
        attackSpeed = 0.5f;
        damage = 1f;
        hitRadius = 3f;
        playerCloseSightDistance = 10f;
        playerCloseAttackDistance = 6f;
        attackWindUpTime = 1f;
        if(chargeBox.activeInHierarchy)
        {
            chargeBox.SetActive(false);
        }
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // if close, stop moving and attack
        if (attackPlayer)
        {
            if (woundUp)
            {
                // attack player
                StartCoroutine(Charge());
                base.CreateDamageCircle(transform.position + (transform.forward), hitRadius, damage, attackSpeed);
                base.CreateDamageCircle(transform.position + (transform.forward*hitRadius), hitRadius, damage, attackSpeed);
                base.CreateDamageCircle(transform.position + (transform.forward*hitRadius*2), hitRadius, damage, attackSpeed);
                //lastRotation = transform.rotation;
                
                woundUp = false;
            }
        }
        if(!woundUp && attackPlayer)
        {
           // transform.rotation = lastRotation;
        }
        /*
        if(charging)
        {
            transform.position += transform.forward * Time.deltaTime * chargeSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        */
    }
    IEnumerator Charge()
    {
        //charging = true;
        //chargeBox.SetActive(true);
        yield return new WaitForSeconds(1/attackSpeed);
        //charging = false;

        woundUp = false;
        attackPlayer = false;
        //chargeBox.SetActive(false);
        attackingSign.SetActive(false);
        chasePlayer = true;
    }
    /*
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().LoseHealth();
            // stop charge
            charging = false;
            woundUp = false;
            attackPlayer = false;
            chargeBox.SetActive(false);
            attackingSign.SetActive(false);
            chasePlayer = true;
        }
    }
    */
}
