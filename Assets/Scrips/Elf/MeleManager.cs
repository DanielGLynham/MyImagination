using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleManager : MonoBehaviour
{
    int meleDamageAmount = 1;
    private bool meleActive = false;
    private bool canSwing = true;
    private Vector3 playerWithSpriteSize;
    private float meleAttackRange = 20f;
    private BasicGoblin closestEnemy = null;
    private BasicGoblin[] allenemies;
    private int attackMode = 1;
    private float timer = 0f;
    private bool comboTriggered = false;
    private void Start()
    {

    }
    public void SetMeleActive(bool toggle)
    {
        meleActive = toggle;
    }
    private void Update()
    {
        if(meleActive)
        {
            DoMele();

        }
    }
    private void DoMele()
    {
        // do attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && canSwing && timer < 60)
        {
            if (CheckMele()) // is close enough to attack to an enemy
            {
                Debug.Log(attackMode);
                canSwing = false;
                switch(attackMode)
                {
                    case 1:
                        StartCoroutine(DoMeleSwing(attackMode, 0.1f, 0.6f));
                        StartCoroutine(WaitForMeleLoad(0.5f));
                        timer = 0; // reset timer for next combo move
                        attackMode++;
                        break;
                    case 2:
                        comboTriggered = true;
                        StartCoroutine(DoMeleSwing(attackMode, 0.1f, 0.4f));
                        StartCoroutine(WaitForMeleLoad(0.5f));
                        timer = 0; // reset timer for next combo move
                        attackMode++;
                        break;
                    case 3:
                        StartCoroutine(DoMeleSwing(attackMode, 0.5f, 1f));
                        StartCoroutine(WaitForMeleLoad(1.5f));
                        attackMode = 1;
                        StartCoroutine(ReleasePlayer(1.5f));
                        break;
                }
            }
        }
        if(attackMode != 1) // for combo
        {// could be if atk 1 do meleSwig, if 2 do roundSwing
            timer += Time.deltaTime;
            if(timer > 1) // combo missed, reset for normal attack
            {
                comboTriggered = false;
                attackMode = 1;
                timer = 0;
                GetComponent<Movement>().SetPaused(false);
            }
        }
    }
    IEnumerator ReleasePlayer(float time)
    {
        yield return new WaitForSeconds(time);
        comboTriggered = false;
        GetComponent<Movement>().SetPaused(false);
    }
    public bool CheckMele()
    {
        playerWithSpriteSize = GetComponent<CombatManger>().GetSpriteSize() + transform.position;
        allenemies = GameObject.FindObjectsOfType<BasicGoblin>();
        // get closest enemy ready to hit
        //TODO, take into account size of enemy ofset
        float distanceToClosestEnemy = Mathf.Infinity;

        foreach (BasicGoblin currentenemy in allenemies)
        {
            Vector3 EnemyWithSpriteSize = currentenemy.GetSpriteSize() + currentenemy.transform.position;

            float distToEnemy = (EnemyWithSpriteSize - playerWithSpriteSize).sqrMagnitude;
            if(distToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distToEnemy;
                closestEnemy = currentenemy;
            }
        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        Vector3 enemyWithSpriteSize = closestEnemy.GetSpriteSize() + closestEnemy.transform.position;
        float distToClosestEnemy = (enemyWithSpriteSize - playerWithSpriteSize).sqrMagnitude;

        if (distToClosestEnemy <= meleAttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator WaitForMeleLoad(float waitTime)
    {
        yield return new WaitForSeconds(0.5f);
        canSwing = true;
    }
    private IEnumerator DoMeleSwing(int atkMode, float windUpTime, float recoveryTime)
    {
        yield return new WaitForSeconds(windUpTime);
        closestEnemy.TakeDamage(atkMode);
        GetComponent<Movement>().SetPaused(true);
        transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, 50 * Time.deltaTime);
        transform.LookAt(closestEnemy.transform, Vector3.up);
        yield return new WaitForSeconds(recoveryTime);
        if(!comboTriggered)
        {
            GetComponent<Movement>().SetPaused(false);
        }
    }    
}
