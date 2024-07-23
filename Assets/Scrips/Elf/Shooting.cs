using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectile;
    public GameObject particle;
    private GameObject weaponPos;
    public List<Sprite> projectileSprites;
    public List<Sprite> projectileLandedSprites;
    private bool recharged = true;
    private float chargeAmount = 0;
    private int currentWeaponNum = 0;
    private bool shootingActive = false;
    private void Start()
    {
        particle.SetActive(false);
    }
    public void SetShootingActive(bool toggle, int weaponNum, GameObject weapon)
    {
        if (toggle)
        {
            shootingActive = toggle;
            currentWeaponNum = weaponNum;
            weaponPos = weapon;
            particle.SetActive(false);
        }
        else
        {
            shootingActive = toggle;
        }
    }
    // Update is called once per frame
    void Update()
    {
        /* /////////moved to combat manager 
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentWeaponNum++;
            if(currentWeaponNum > WeaponSprites.Count-1)
            {
                currentWeaponNum = 0;
            }
            elfUI.GetComponent<UI_elf>().ChangeWeaponUISprite(currentWeaponNum);
            currentWeapon.GetComponentInChildren<SpriteRenderer>().sprite = WeaponSprites[currentWeaponNum];
        }
        *//////////moved to combat manager 
        if (shootingActive)
        {
            if (recharged)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    // hold bow
                    chargeAmount += Time.deltaTime;
                    particle.SetActive(true);
                }
                if (Input.GetKeyUp(KeyCode.Mouse0) || chargeAmount > 1f)
                {
                    GameObject arrow = Instantiate(projectile, weaponPos.transform.position, transform.rotation);
                    arrow.GetComponent<Arrow>().SetPowerAndLandedSprite(chargeAmount, projectileLandedSprites[currentWeaponNum], true, currentWeaponNum, chargeAmount + (1 / (currentWeaponNum + 1)));
                    arrow.GetComponentInChildren<SpriteRenderer>().sprite = projectileSprites[currentWeaponNum];
                    recharged = false;
                    chargeAmount = 0;
                    particle.SetActive(false);
                    StartCoroutine(Recharge(1f));
                }
            }
        }
    }
    IEnumerator Recharge(float time)
    {
        yield return new WaitForSeconds(time);
        recharged = true;
    }
}
