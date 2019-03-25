using UnityEngine;
using System.Collections;

public class Ward :Item {

    /// <summary>
    /// The amount of health the item will restore
    /// </summary>
    public int Duration { get; set; }


    /// <summary>
    /// Empty constructor used for serializing the object
    /// </summary>
    public Ward()
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
    /// <param name="duration">The amount of health the item will restore</param>
    public Ward(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int duration)
        : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize)
    {
        this.Duration = duration;
    }

    /// <summary>
    /// Uses the item
    /// </summary>
    public override void Use(Slot slot, ItemScript item)
    {
        //Debug.Log("Used " + ItemName);
        //if (ItemName == "Haste Potion")
        //    GameObject.FindGameObjectWithTag("Player").GetComponent<Ability1>().TrapAbilityUse(5);
        slot.RemoveItem();
    }

    /// <summary>
    /// Creates a tooltip
    /// </summary>
    /// <returns>A complete tooltip</returns>
    public override string GetTooltip(Inventory inv)
    {
        string stats = string.Empty;

        if (Duration > 0) //Adds health to the tooltip if it is larger than 0
        {
            stats += "\nDuration " + Duration.ToString() + " seconds";
        }
   

        //Gets the tooltip from the base class
        string itemTip = base.GetTooltip(inv);

        //Returns the complete tooltip

        if (inv is VendorInventory)
        {
            return string.Format("{0}" + "<size=14>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats, BuyPrice);
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
