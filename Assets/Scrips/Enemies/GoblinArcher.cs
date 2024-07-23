using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinArcher : BasicGoblin
{
    public GameObject projectile, bow;
    public Sprite arrowSprite;
    private bool shotArrow = false;
    private GameObject circleTracker;
    protected override void Start()
    {
        health = 2;
        moveSpeed = 5f;
        attackSpeed = 0.5f;
        damage = 1f;
        hitRadius = 2f;
        playerCloseSightDistance = 10f;
        playerCloseAttackDistance = 8f;
        attackWindUpTime = 2f;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // if close, stop moving and attack
        if (attackPlayer)
        {
            if(!shotArrow)
            {
                transform.LookAt(player.transform, Vector3.up);
            }
            if (woundUp)
            {
                // attack player
                /* old shooting code, now using cirlce system
                GameObject projectileTemp = Instantiate(projectile, bow.transform.position, transform.rotation);
                projectileTemp.GetComponent<Arrow>().SetPowerAndLandedSprite(5, arrowSprite, false, 0, 1);
                woundUp = false;
                attackPlayer = false;
                attackingSign.SetActive(false);
                chasePlayer = true;
                */

                circleTracker = base.CreateDamageCircle(player.transform.position, hitRadius, damage, attackSpeed);
                StartCoroutine(ArrowTrackPlayer());
                woundUp = false;
                attackPlayer = false;
                attackingSign.SetActive(false);
                chasePlayer = true;

            }
        }
        if(shotArrow)
        {
            if(circleTracker != null)
            {
                circleTracker.transform.position = player.transform.position;
            }
        }
    }
    IEnumerator ArrowTrackPlayer()
    {
        shotArrow = true;
        yield return new WaitForSeconds(attackSpeed * 1.5f);
        shotArrow = false;
        circleTracker = null;
    }    
}
