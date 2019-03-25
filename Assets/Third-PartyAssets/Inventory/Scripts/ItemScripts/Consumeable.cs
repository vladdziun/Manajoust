using UnityEngine;
using System.Collections;

public class Consumeable : Item
{
    /// <summary>
    /// The amount of health the item will restore
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// The amount of mana, the item will restore
    /// </summary>
    public int Mana { get; set; }

    /// <summary>
    /// Empty constructor used for serializing the object
    /// </summary>
    public Consumeable()
    { }

    /// <summary>
    /// The consumeable's constructor
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="description">The item's description</param>
    /// <param name="itemType">The type of time</param>
    /// <param name="quality">The item's quality</param>
    /// <param name="spriteNeutral">Path to the item's sprite</param>
    /// <param name="spriteHighlighted">Path to the items highlighted sprite</param>
    /// <param name="maxSize">The item's max size</param>
    /// <param name="health">The amount of health the item will restore</param>
    /// <param name="mana">The amount of mana the item will restore</param>
    public Consumeable(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int health, int mana)
        : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize)
    {
        this.Health = health;
        this.Mana = mana;
    }

    /// <summary>
    /// Uses the item
    /// </summary>
    public override void Use(Slot slot, ItemScript item)
    {
       
        Debug.Log("Used " + ItemName);
        if(ItemName == "Hands of Doom")
            GameObject.FindGameObjectWithTag("Player").GetComponent<Ability1>().TrapAbilityUse(5);
        slot.RemoveItem();
        InventoryManager.Instance.Save();
    }

    /// <summary>
    /// Creates a tooltip
    /// </summary>
    /// <returns>A complete tooltip</returns>
    public override string GetTooltip(Inventory inv)
    {
        string stats = string.Empty;

        if (Health > 0) //Adds health to the tooltip if it is larger than 0
        {
            stats += "\nRestores " + Health.ToString() + " Health";
        }
        if (Mana > 0) //Adds mana to the tooltip if it is larger than 0
        {
            stats += "\nRestores " + Mana.ToString() + " Mana";
        }

        //Gets the tooltip from the base class
        string itemTip = base.GetTooltip(inv);

        //Returns the complete tooltip

        if (inv is VendorInventory)
        {
            return string.Format("{0}" + "<size=14>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats,BuyPrice);
        }
        else if (VendorInventory.Instance.IsOpen)
        {
            return string.Format("{0}" + "<size=14>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats, SellPrice);
        }
        else
        {
            return string.Format("{0}" + "<size=14>{1}</size>", itemTip, stats);
        }
        
    }
}
