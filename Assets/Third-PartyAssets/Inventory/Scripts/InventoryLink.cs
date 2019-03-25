using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryLink : MonoBehaviour 
{   
    /// <summary>
    /// A reference to the chests inventory
    /// </summary>
    public ChestInventory linkedInventory;

    public int rows, slots;

    private List<Stack<ItemScript>> allSlots;

    private bool active = false;

    void Start()
    {
        allSlots = new List<Stack<ItemScript>>(slots);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (linkedInventory.FadingOut)
            {
                linkedInventory.InstantClose = true;
                //linkedInventory.MoveItemsToChest();
            }
            active = true;

            linkedInventory.UpdateLayout(allSlots, rows, slots);
    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            active = false;
        }
    }

    public void SaveInventory()
    {
        if (linkedInventory.IsOpen)
        {
           // linkedInventory.MoveItemsToChest();
        }

        string content = string.Empty; //Creates a string for containing infor about the items inside the inventory

        for (int i = 0; i < allSlots.Count; i++) //Runs through all slots in the inventory
        {
            if (allSlots[i] != null && allSlots[i].Count > 0)
            {
                content += i + "-" + allSlots[i].Peek().Item.ItemName + "-" + allSlots[i].Count.ToString() + ";";
            }
        }

        //Stores all the info in the PlayerPrefs
        PlayerPrefs.SetString(gameObject.name + "content", content);
        PlayerPrefs.Save();
    }

    public virtual void LoadInventory()
    {
        //Loads all the inventory's data from the playerprefs
        string content = PlayerPrefs.GetString(gameObject.name + "content");
        allSlots = new List<Stack<ItemScript>>();

        for (int i = 0; i < slots; i++)
        {
            allSlots.Add(new Stack<ItemScript>());
        }

        if (content != string.Empty)
        {
            
            //Splits the loaded content string into segments, so that each index inthe splitContent array contains information about a single slot
            //e.g[0]0-MANA-3
            string[] splitContent = content.Split(';');

            //Runs through every single slot we have infor about -1 is to avoid an empty string error
            for (int x = 0; x < splitContent.Length - 1; x++)
            {
                //Splits the slot's information into single values, so that each index in the splitValues array contains info about a value
                //E.g[0]InventorIndex [1]ITEMTYPE [2]Amount of items
                string[] splitValues = splitContent[x].Split('-');

                int index = Int32.Parse(splitValues[0]); //InventorIndex 

                string itemName = splitValues[1]; //ITEMTYPE

                int amount = Int32.Parse(splitValues[2]); //Amount of items

                Item tmp = null;

                for (int i = 0; i < amount; i++) //Adds the correct amount of items to the inventory
                {
                    GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);

                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Consumeables.Find(item => item.ItemName == itemName);
                    }
                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Equipment.Find(item => item.ItemName == itemName);
                    }
                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Weapons.Find(item => item.ItemName == itemName);
                    }
                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Materials.Find(item => item.ItemName == itemName);
                    }
                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Ward.Find(item => item.ItemName == itemName);
                    }

                    loadedItem.AddComponent<ItemScript>();
                    loadedItem.GetComponent<ItemScript>().Item = tmp;
                    allSlots[index].Push(loadedItem.GetComponent<ItemScript>());
                    Destroy(loadedItem);
                }
            }
        }

        if (active)
        {
            linkedInventory.UpdateLayout(allSlots, rows, slots);
        }
    }

}
