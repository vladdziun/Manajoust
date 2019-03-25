using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CraftingBench : Inventory
{
    private static CraftingBench instance;

    public static CraftingBench Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CraftingBench>();
            }
            return instance;
        }

    }

    public GameObject prefabButton;

    private GameObject previewSlot;

    private Dictionary<string, Item> craftingItems = new Dictionary<string, Item>();

    public override void CreateLayout()
    {

        base.CreateLayout();

        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight + slotSize + slotPaddingTop * 2);

        if (!GameObject.Find("CraftButton"))
        {
            //This si the button
            GameObject craftBtn;

            craftBtn = Instantiate(prefabButton);

            RectTransform btnRect = craftBtn.GetComponent<RectTransform>();

            craftBtn.name = "CraftButton";

            craftBtn.transform.SetParent(this.transform.parent);

            btnRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft, -slotPaddingTop * 4 - (slotSize * 3));

            btnRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((slotSize * 2) + (slotPaddingLeft * 2)) * InventoryManager.Instance.canvas.scaleFactor);

            btnRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);

            craftBtn.transform.SetParent(transform);

            craftBtn.GetComponent<Button>().onClick.AddListener(CraftItem);
        }

        if (!GameObject.Find("PreviewSlot"))
        {
            //This is the slot
            previewSlot = Instantiate(InventoryManager.Instance.slotPrefab);

            RectTransform slotRect = previewSlot.GetComponent<RectTransform>();

            previewSlot.name = "PreviewSlot";

            previewSlot.transform.SetParent(this.transform.parent);

            slotRect.localPosition = inventoryRect.localPosition + new Vector3((slotPaddingLeft * 3) + (slotSize * 2), -slotPaddingTop * 4 - (slotSize * 3));

            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);

            previewSlot.transform.SetParent(this.transform);

            previewSlot.GetComponent<Slot>().clickAble = false;
        }
 
    }

    public void CreateBlueprints()
    {

        craftingItems.Add("EMPTY-Iron-EMPTY-EMPTY-Iron-EMPTY-EMPTY-Wood-EMPTY-", InventoryManager.Instance.ItemContainer.Weapons.Find(x => x.ItemName == "Awesome Sword"));
        craftingItems.Add("EMPTY-EMPTY-EMPTY-Stone-Stone-Stone-Stone-EMPTY-Stone-", InventoryManager.Instance.ItemContainer.Equipment.Find(x => x.ItemName == "Strong Helmet"));
    }

    public void CraftItem()
    {
        string output = string.Empty;

        foreach (GameObject slot in allSlots)
        {
            Slot tmp = slot.GetComponent<Slot>();

            if (tmp.IsEmpty)
            {
                output += "EMPTY-";
            }
            else
            {
                output += tmp.CurrentItem.Item.ItemName + "-";
            }

        }

        if (craftingItems.ContainsKey(output))
        {
            GameObject tmpObj = Instantiate(InventoryManager.Instance.itemObject);

            tmpObj.AddComponent<ItemScript>();

            ItemScript craftedItem = tmpObj.GetComponent<ItemScript>();

            Item tmpItem;

            craftingItems.TryGetValue(output, out tmpItem);

            if (tmpItem != null)
            {
                craftedItem.Item = tmpItem;

                if (Player.Instance.inventory.AddItem(craftedItem))
                {
                    foreach (GameObject slot in allSlots)
                    {
                        slot.GetComponent<Slot>().RemoveItem();
                    }
                }

                Destroy(tmpObj);

            }
        }

        UpdatePreview();
    }

    public override void MoveItem(GameObject clicked)
    {
        base.MoveItem(clicked);

        UpdatePreview();
    }

    public void UpdatePreview()
    {


        string output = string.Empty;

        previewSlot.GetComponent<Slot>().ClearSlot();

        foreach (GameObject slot in allSlots)
        {
            Slot tmp = slot.GetComponent<Slot>();

            if (tmp.IsEmpty)
            {
                output += "EMPTY-";
            }
            else
            {
                output += tmp.CurrentItem.Item.ItemName + "-";
            }

        }

        if (craftingItems.ContainsKey(output))
        {
            GameObject tmpObj = Instantiate(InventoryManager.Instance.itemObject);

            tmpObj.AddComponent<ItemScript>();

            ItemScript craftedItem = tmpObj.GetComponent<ItemScript>();

            Item tmpItem;

            craftingItems.TryGetValue(output, out tmpItem);

            if (tmpItem != null)
            {
                craftedItem.Item = tmpItem;

                previewSlot.GetComponent<Slot>().AddItem(craftedItem);
                Destroy(tmpObj);

            }
        }
    }

    public override void LoadInventory()
    {
        base.LoadInventory();
        UpdatePreview();
    }

    public override void Open()
    {
        base.Open();

        foreach (GameObject slot in allSlots)
        {
            Slot tmpSlot = slot.GetComponent<Slot>();
            int count = tmpSlot.Items.Count;
            for (int i = 0; i < count; i++)
            {
                ItemScript tmpItem = tmpSlot.RemoveItem();

                if (!Player.Instance.inventory.AddItem(tmpItem))
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    v *= 25;

                    GameObject tmpDrp = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);


                    tmpDrp.AddComponent<ItemScript>();
                    tmpDrp.GetComponent<ItemScript>().Item = tmpItem.Item;
                }
                
            }
            
        }
    }
}
