using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChestInventory : Inventory 
{
    public Animator chestAnimator;

    private List<Stack<ItemScript>> chestItems;
    private int chestSlots;

    void Start()
    {
        int randNumber;

        randNumber = Random.Range(1, 10);

        EmptySlots = slots;

        base.Start();


        GiveItem("Ring of Recklessness");
        //GiveItem("Great Axe Of Sasheke");
        


    }


    protected void GiveItem(string itemName)
    {
        GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);

        tmp.AddComponent<ItemScript>();

        ItemScript newItem = tmp.GetComponent<ItemScript>();

        if (InventoryManager.Instance.ItemContainer.Consumeables.Exists(x => x.ItemName == itemName))
        {
            newItem.Item = InventoryManager.Instance.ItemContainer.Consumeables.Find(x => x.ItemName == itemName);

        }
        else if (InventoryManager.Instance.ItemContainer.Weapons.Exists(x => x.ItemName == itemName))
        {
            newItem.Item = InventoryManager.Instance.ItemContainer.Weapons.Find(x => x.ItemName == itemName);
        }
        else if (InventoryManager.Instance.ItemContainer.Equipment.Exists(x => x.ItemName == itemName))
        {
            newItem.Item = InventoryManager.Instance.ItemContainer.Equipment.Find(x => x.ItemName == itemName);
        }

        if (newItem != null)
        {
            AddItem(newItem);
        }

        Destroy(tmp);
    }

    public override void MoveItem(GameObject clicked)
    {

    }

    public override void CreateLayout()
    {
        allSlots = new List<GameObject>();

        for (int i = 0; i < slots; i++)
        {
            GameObject newSlot = Instantiate(InventoryManager.Instance.slotPrefab);

            newSlot.name = "Slot";

            newSlot.transform.SetParent(this.transform);

            allSlots.Add(newSlot);

            newSlot.GetComponent<Button>().onClick.AddListener
                (
                     delegate { MoveItem(newSlot); }
                );

            newSlot.SetActive(false);

        }

        //Calculates the hoverYOffset by taking 1% of the slot size
        hoverYOffset = slotSize * 0.1f;
    }

    public void UpdateLayout(List<Stack<ItemScript>> items, int rows, int slots)
    {

        this.chestItems = items;
        this.chestSlots = slots;

        //Calculates the width of the inventory
        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft);

        //Calculates the highs of the inventory
        inventoryHight = rows * (slotSize + slotPaddingTop);

        //Creates a reference to the inventory's RectTransform
        inventoryRect = GetComponent<RectTransform>();

        //Sets the with and height of the inventory.
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth + slotPaddingLeft);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight + slotPaddingTop);

        //Calculates the amount of columns
        int columns = slots / rows;

        int index = 0;

        for (int y = 0; y < rows; y++) //Runs through the rows
        {
            for (int x = 0; x < columns; x++) //Runs through the columns
            {
                //Instantiates the slot and creates a reference to it 
                GameObject newSlot = allSlots[index];

                //Makes a reference to the rect transform
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                //Sets the canvas as the parent of the slots, so that it will be visible on the screen
                newSlot.transform.SetParent(this.transform.parent);

                //Sets the slots position
                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                //Sets the size of the slot
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                
                newSlot.transform.SetParent(this.transform);

                if (items.Count != 0 && items.Count >= index && items[index].Count > 0)
                {
                    newSlot.GetComponent<Slot>().AddItems(items[index]);
                }

                index++;

            }
        }
    }

    public override void Open()
    {
        base.Open();

        if (IsOpen)
        {
            if(chestAnimator)
            chestAnimator.SetBool("isOpen", true);

            MoveItemsFromChest();
        }
        else if(!IsOpen)
        {
            if (chestAnimator)
                chestAnimator.SetBool("isOpen", false);
        }
            

    }

    //public void MoveItemsToChest()
    //{
    //    chestItems.Clear();

    //    for (int i = 0; i < chestSlots; i++)
    //    {
    //        Slot tmpSlot = allSlots[i].GetComponent<Slot>();

    //        if (!tmpSlot.IsEmpty)
    //        {
    //            chestItems.Add(new Stack<ItemScript>(tmpSlot.Items));

    //            if (!IsOpen)
    //            {
    //                tmpSlot.ClearSlot();
    //            }

    //        }
    //        else
    //        {
    //            chestItems.Add(new Stack<ItemScript>());
    //        }
    //        if (!IsOpen)
    //        {
    //            allSlots[i].SetActive(false);
    //        }
    //    }
    //}

    public void MoveItemsFromChest()
    {
        for (int i = 0; i < chestSlots; i++)
        {
            if (chestItems.Count != 0 && chestItems.Count >= i && chestItems[i] != null && chestItems[i].Count > 0)
            {
                GameObject newSlot = allSlots[i];
                newSlot.GetComponent<Slot>().AddItems(chestItems[i]);
            }
        }

        for (int i = 0; i < chestSlots; i++)
        {
            allSlots[i].SetActive(true);
        }
    }

    protected override IEnumerator FadeOut()
    {
        yield return StartCoroutine(base.FadeOut());

        //MoveItemsToChest();
    }

    public override void LoadInventory()
    {
        //foreach (GameObject slot in allSlots)
        //{
        //    slot.GetComponent<Slot>().ClearSlot();
        //}
    }

    public override void SaveInventory()
    {
        
    }

}
