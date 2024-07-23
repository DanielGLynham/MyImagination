using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;
    private GameObject UIManager;

    private void Awake()
    {
        UIManager = GameObject.Find("ManagerObj");
        currentHealth = maxHealth;
        
    }

    public int GetHealth()
    {
        return currentHealth;
    }
    public void LoseHealth()
    {
        currentHealth--;
        UIManager.GetComponent<UI_elf>().LessHealth();
    }
}
