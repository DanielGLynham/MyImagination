using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    private float size, damage, speed;
    public GameObject largeCirlce, growingCirlce;
    private bool checkHitPlayer = false;

    public void InitiateCirlce(float sz, float dmg, float spd)
    {
        damage = dmg;
        speed = spd;
        size = sz;
        largeCirlce.transform.localScale = new Vector3(sz, sz, sz);
        growingCirlce.transform.localScale = new Vector3(0, 0, 0);

    }
    private void Update()
    {
        if (growingCirlce.transform.localScale.x <= 1)
        {
            growingCirlce.transform.localScale += new Vector3(speed, speed, speed) * Time.deltaTime;
        }
        else
        {
            checkHitPlayer = true;

            Collider[] hitColls = Physics.OverlapSphere(transform.position, growingCirlce.transform.localScale.x);
            foreach (var hit in hitColls)
            {
                if(hit.gameObject.tag == "Player")
                {
                    hit.GetComponent<Health>().LoseHealth();
                    Destroy(this.gameObject);
                }
            }
            Destroy(this.gameObject);
        }
    }

}
