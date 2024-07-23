using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_elf : MonoBehaviour
{
    public GameObject healthIcon;

    private GameObject UICanvas;
    private int currentHealth;
    private List<GameObject> healthBar;
    private GameObject player;
    public List<Sprite> weaponSprites;
    public GameObject weaponChoiceUIPlace;
    public GameObject bowSprite, fireStaffSprite;
    public GameObject currentWeaponUI;
    private int weaponActiveNum;
    public GameObject dashAciveSymbol;
    public GameObject menu, inventoryMenu;



    private void Start()
    {
        player = GameObject.Find("Elf");
        currentHealth = player.GetComponent<Health>().GetHealth();
        UICanvas = GameObject.Find("HealthBarStart");
        healthBar = new List<GameObject>();
        
        CreateUI();
    }
    public void CreateUI()
    {
        currentHealth = player.GetComponent<Health>().GetHealth();
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject heart = Instantiate(healthIcon);
            heart.transform.SetParent(UICanvas.transform, false);
            heart.GetComponent<RectTransform>().position = new Vector3(3 * i, 0, 0) + UICanvas.transform.position;
            healthBar.Add(heart);
        }
        ChangeWeaponUISprite(weaponActiveNum);
    }
    public void ChangeWeaponUISprite(int num)
    {
        weaponActiveNum = num;
        currentWeaponUI.GetComponent<Image>().sprite = weaponSprites[num];
    }
    public void LessHealth()
    {
        if(healthBar.Count-1 > -1)
        {
            Destroy(healthBar[healthBar.Count - 1].gameObject);
            healthBar.RemoveAt(healthBar.Count - 1);
        }
        else
        {
            Debug.Log("DONE DIED BRO");
        }
    }
    public void MoreHealth()
    {
        GameObject heart = Instantiate(healthIcon);
        heart.transform.SetParent(UICanvas.transform, false);
        heart.GetComponent<RectTransform>().position = new Vector3(2 * healthBar.Count, 0, 0) + UICanvas.transform.position;
        healthBar.Add(heart);
    }
    public void SetCurrentWeapon(int num)
    {
        weaponActiveNum = num;
    }
    public void SetDashActive(bool active)
    {
        dashAciveSymbol.SetActive(active);
    }
    public void SetMenuActive(bool toggle)
    {
        menu.SetActive(toggle);
    }
}
