using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inventory; 

    private void Awake()
    {
        inventory = new List<Item>();
        Item a = new Item();
        a.InitialiseItem(1,"key","A beaten-up key", 1, 0);
        inventory.Add(a);
        Item b = new Item();
        b.InitialiseItem(1, "coin", "A coin, used to trade for other items", 1, 0);
        inventory.Add(b);
        Item c = new Item();
        c.InitialiseItem(1, "bow", "shoots arrows", 1, 0);
        inventory.Add(c);
        Item d = new Item();
        d.InitialiseItem(1, "arrow", "Bet this would hurt if you shot it at something", 1, 0);
        inventory.Add(d);
        Item e = new Item();
        e.InitialiseItem(1, "fire staff", "kaboom?", 1, 0);
        inventory.Add(e);
    }

    public Item GetItemFromInventByPosition(int i)
    {
        return inventory[i];
    }
    public List<Item> GetInventory()
    {
        return inventory;
    }
    public bool SearchInventForItemName(string name)
    {
        foreach(Item item in inventory)
        {
            if(item.itemName == name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
