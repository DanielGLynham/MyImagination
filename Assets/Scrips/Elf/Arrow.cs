using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float moveSpeed = 40;
    private float power = 0;
    public GameObject landedOnGround;
    private Sprite landedSprite;
    private bool shotByPlayer;
    private int type;
    private float damage;
    public GameObject Explosive;
    private void Start()
    {
        StartCoroutine(LandOnFloor());
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
    public void SetPowerAndLandedSprite(float p, Sprite LOG, bool fromPlayer, int projType, float ProjDamage)
    {
        shotByPlayer = fromPlayer;
        power = p;
        landedSprite = LOG;
        type = projType;
        damage = ProjDamage;
    }
    IEnumerator LandOnFloor()
    {
        yield return new WaitForSeconds(power);
        GameObject LandedOBJ = Instantiate(landedOnGround, transform.position, Quaternion.identity);
        LandedOBJ.GetComponentInChildren<SpriteRenderer>().sprite = landedSprite;
        if(type == 1)
        {
            GameObject kaboom = Instantiate(Explosive, transform.position, Quaternion.identity);
            kaboom.GetComponent<Explosive>().SetStats(shotByPlayer);
        }
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (type == 0)
        {
            if (shotByPlayer)
            {
                if (other.gameObject.tag != "Player")
                {
                    // avoids player hit
                    Destroy(this.gameObject);
                    other.gameObject.GetComponent<BasicGoblin>().TakeDamage(damage);
                }
            }
            else
            {
                if(other.gameObject.tag == "Player")
                {
                    other.gameObject.GetComponent<Health>().LoseHealth();
                    Destroy(this.gameObject);
                }
            }
        }
        else if(type == 1)
        {
            if (shotByPlayer)
            {
                if (other.gameObject.tag != "Player")
                {
                    GameObject kaboom = Instantiate(Explosive, transform.position, Quaternion.identity);
                    kaboom.GetComponent<Explosive>().SetStats(shotByPlayer);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                if (other.gameObject.tag == "Player")
                {
                    GameObject kaboom = Instantiate(Explosive, transform.position, Quaternion.identity);
                    kaboom.GetComponent<Explosive>().SetStats(shotByPlayer);
                    Destroy(this.gameObject);
                }
            }

        }
    }
    public bool ShotFromPlayer()
    {
        return shotByPlayer;
    }
}
