using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGoblin : MonoBehaviour
{
    protected float moveSpeed, attackSpeed, damage, hitRadius;
    protected Rigidbody _rb;
    protected GameObject player;
    public GameObject spriteObj;
    public GameObject alertedSign;
    public GameObject attackingSign;
    public GameObject Skull;
    protected bool chasePlayer = false;
    protected bool attackPlayer = false;
    protected bool woundUp = false;
    protected float health;
    protected float playerCloseSightDistance, playerCloseAttackDistance, attackWindUpTime;
    private float maxHealth;
    private Color setColour;
    Vector3 spriteSize;
    public GameObject dmgText;
    public GameObject dmgCirlcething;
    public List<GameObject> circlesOwned;

    protected virtual void Start()
    {
        setColour = Color.white;
        maxHealth = health;
        _rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Elf");
        if (alertedSign.activeInHierarchy)
        {
            alertedSign.SetActive(false);
        }
        if (attackingSign.activeInHierarchy)
        {
            attackingSign.SetActive(false);
        }

        //sprite size stuff
        spriteSize = new Vector3(spriteObj.GetComponent<SpriteRenderer>().bounds.size.x * transform.localScale.x, spriteObj.GetComponent<SpriteRenderer>().bounds.size.y * transform.localScale.y, 0);
        circlesOwned = new List<GameObject>();
    }
    protected virtual void Update()
    {
        // needs to be in distance order
        if (PlayerClose(playerCloseAttackDistance) && attackPlayer == false)
        {
            attackPlayer = true;
            chasePlayer = false;
            StartCoroutine(WindUp(attackWindUpTime));
        }
        else if (PlayerClose(playerCloseSightDistance) && attackPlayer == false)
        {
            transform.LookAt(player.transform, Vector3.up);
            chasePlayer = true;
            alertedSign.SetActive(true);
        }
        // if player seen
        if (chasePlayer && !attackPlayer)
        {
            transform.LookAt(player.transform, Vector3.up);
            //transform.LookAt(player.transform, Vector3.up);
            Vector3 moveDir = player.transform.position - transform.position;
            transform.position += moveDir.normalized * Time.deltaTime * moveSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    IEnumerator WindUp(float amount)
    {
        attackingSign.SetActive(true);
        yield return new WaitForSeconds(amount);
        woundUp = true;
    }
    private bool PlayerClose(float checkDist)
    {
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        float py = Mathf.Sqrt((x * x) + (y * y));
        if (py < checkDist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator FlashRed(float colour)
    {
        //spriteObj.GetComponent<SpriteRenderer>().color = Color.red;
        spriteObj.GetComponent<SpriteRenderer>().color = new Color(1, colour, colour, 1);
        yield return new WaitForSeconds(0.3f);
        spriteObj.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        float temp = health / maxHealth;
        StartCoroutine(FlashRed(temp));
        GameObject tempTextObj = Instantiate(dmgText, transform.position, Quaternion.identity);
        tempTextObj.GetComponent<DamageText>().SetDamage(amount);
        //knockback
        Vector3 dir = transform.position - player.transform.position;
        transform.position += dir.normalized * 0.5f;
        //GetComponent<Rigidbody>().AddForce(dir.normalized * 2);
        if (health <= 0)
        {
            Instantiate(Skull, transform.position, Quaternion.identity);
            foreach(GameObject go in circlesOwned)
            {
                Destroy(go);
            }
            Destroy(this.gameObject);
        }

        // use for red colour stay 
        //float temp = health / maxHealth;
        //setColour = new Color(1, temp, temp, 1);
        //spriteObj.GetComponent<SpriteRenderer>().color = setColour;

    }

    public Vector3 GetSpriteSize()
    {
        return spriteSize;
    }
    public GameObject CreateDamageCircle(Vector3 locaiton, float size, float damage, float speed)
    {
        GameObject damageCirlce = Instantiate(dmgCirlcething, locaiton, Quaternion.identity);
        damageCirlce.GetComponent<DamageCircle>().InitiateCirlce(size, damage, speed);
        circlesOwned.Add(damageCirlce);
        return damageCirlce;
    }
}
