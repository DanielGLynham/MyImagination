using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private GameObject player;
    public GameObject inventoryUIReference, item;
    Sprite emptySlot, key, money;
    List<GameObject> displayedInventoryItems;
    List<Item> playersInventoryTemp;
    private int maxItemTypes = 5; // can only be 5 types of item
    public Text itemDescription;
    int UIOFFSET = -400;
    int UIGAP = 200;
    private void Start()
    {
        //player = GameObject.Find("Elf");
        playersInventoryTemp = GetComponent<Inventory>().GetInventory();
        displayedInventoryItems = new List<GameObject>();
        for (int i = 0; i < maxItemTypes; i++)
        {
            int x = i;
            displayedInventoryItems.Add(Instantiate(item, inventoryUIReference.transform.position + new Vector3(UIOFFSET + (i * UIGAP), 300, 0), Quaternion.identity,inventoryUIReference.transform));
            displayedInventoryItems[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { UpdateItemDescriptionText(x); });
            UpdateInventoryUI();
        }
    }
    public void UpdateInventoryUI()
    {
        if (playersInventoryTemp != null)
        {
            if (playersInventoryTemp.Count > 0)
            {
                for (int i = 0; i < displayedInventoryItems.Count; i++)
                {
                    displayedInventoryItems[i].GetComponentInChildren<Text>().text = playersInventoryTemp[i].itemName;
                }
            }
        }
    }
    void UpdateItemDescriptionText(int num)
    {
        itemDescription.text = playersInventoryTemp[num].itemDescription;

    }
}
