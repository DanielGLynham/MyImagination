using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManger : MonoBehaviour
{
    int combatMode = 0;
    public GameObject currentWeapon; // projectiles at top of list
    public List<Sprite> WeaponSprites;
    public GameObject elfUI;
    private Shooting shootingScript;
    private MeleManager meleManagerScript;
    private Vector3 spriteSize;
    public GameObject spriteObj;
    private void Start()
    {
        // initialise
        shootingScript = GetComponent<Shooting>();
        meleManagerScript = GetComponent<MeleManager>();
        spriteSize = new Vector3(spriteObj.GetComponent<SpriteRenderer>().bounds.size.x * transform.localScale.x, spriteObj.GetComponent<SpriteRenderer>().bounds.size.y * transform.localScale.y, 0);

        SwapWeapon();



    }
    private void Update()
    {
        // swap between weapons
        if (Input.GetKeyDown(KeyCode.Space))
        {
            combatMode++;
            if (combatMode > WeaponSprites.Count - 1)
            {
                combatMode = 0;
            }
            SwapWeapon();
        }
    }
    private void SwapWeapon()
    {
        elfUI.GetComponent<UI_elf>().ChangeWeaponUISprite(combatMode);
        currentWeapon.GetComponentInChildren<SpriteRenderer>().sprite = WeaponSprites[combatMode];
        // activate script for combat
        switch (combatMode)
        {
            case 0:
                shootingScript.SetShootingActive(true, combatMode, currentWeapon);
                meleManagerScript.SetMeleActive(false);
                break;
            case 1:
                shootingScript.SetShootingActive(true, combatMode, currentWeapon);
                meleManagerScript.SetMeleActive(false);
                break;
            case 2:
                meleManagerScript.SetMeleActive(true);
                shootingScript.SetShootingActive(false, combatMode, currentWeapon);

                break;
        }
    }
    public Vector3 GetSpriteSize()
    {
        return spriteSize;
    }
}
