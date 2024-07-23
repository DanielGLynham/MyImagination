using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemType;
    public string itemName;
    public string itemDescription;
    public int quantitiyOfItem;
    public int itemValue;

    public void InitialiseItem(int type, string name, string descri, int quantity, int value)
    {
        itemType = type;
        itemName = name;
        itemDescription = descri;
        quantitiyOfItem = quantity;
        itemValue = value;
    }
}
