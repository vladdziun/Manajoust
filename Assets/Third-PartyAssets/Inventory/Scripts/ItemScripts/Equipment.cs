using UnityEngine;
using System.Collections;

public class Equipment : Item
{
    /// <summary>
    /// The item's intellect
    /// </summary>
    public int Intellect { get; set; }

    /// <summary>
    /// The item's agility
    /// </summary>
    public int Agility { get; set; }

    /// <summary>
    /// The item's stamina
    /// </summary>
    public int Stamina { get; set; }

    /// <summary>
    /// The items strength 
    /// </summary>
    public int Strength { get; set; }

    public Equipment()
    { }

    /// <summary>
    /// The Equipment's constructor
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="description">The item's description</param>
    /// <param name="itemType">The type of time</param>
    /// <param name="quality">The item's quality</param>
    /// <param name="spriteNeutral">Path to the item's sprite</param>
    /// <param name="spriteHighlighted">Path to the items highlighted sprite</param>
    /// <param name="maxSize">The item's max size</param>
    /// <param name="intellect">The item's intellect</param>
    /// <param name="agility">The item's agility</param>
    /// <param name="stamina">The Item's stamina</param>
    /// <param name="strength">The item's strength</param>
    public Equipment(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int intellect, int agility, int stamina, int strength)
        : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize)
    {
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.Strength = strength;
    }

    /// <summary>
    /// Uses the item
    /// </summary>
    public override void Use(Slot slot, ItemScript item)
    {
        CharacterPanel.Instance.EquipItem(slot, item);
    }

    /// <summary>
    /// Creates a tooltip
    /// </summary>
    /// <returns>A complete tooltip</returns>
    public override string GetTooltip(Inventory inv)
    {
        string stats = string.Empty;

        if (Strength > 0) //Adds Strength to the tooltip if it is larger than 0
        {
            stats += "\n+" + Strength.ToString() + " Strength";
        }
        if (Intellect > 0) //Adds Intellect to the tooltip if it is larger than 0
        {
            stats += "\n+" + Intellect.ToString() + " Intellect";
        }
        if (Agility > 0)//Adds Agility to the tooltip if it is larger than 0
        {
            stats += "\n+" + Agility.ToString() + " Agility";
        }
        if (Stamina > 0)//Adds Stamina to the tooltip if it is larger than 0
        {
            stats += "\n+" + Stamina.ToString() + " Stamina";
        }

        //Gets the tooltip from the base class
        string itemTip = base.GetTooltip(inv);

        if (inv is VendorInventory && !(this is Weapon))
        {
            return string.Format("{0}" + "<size=14>{1}</size>\n<color=yellow>Price: {2} </color>", itemTip, stats, BuyPrice);
        }
        else if (VendorInventory.Instance.isActiveAndEnabled && !(this is Weapon))
        {
            return string.Format("{0}" + "<size=14>{1}</size>\n<color=yellow>Price: {2} </color>", itemTip, stats, SellPrice);
        }
        else
        {
            //Returns the complete tooltip
            return string.Format("{0}" + "<size=14>{1}</size>", itemTip, stats);
        }
        

    }
}
