using UnityEngine;
using System.Collections;

public class Weapon : Equipment
{
    /// <summary>
    /// The weapon's attack speed
    /// </summary>
    public float AttackSpeed { get; set; }

    public Weapon()
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
    /// <param name="attackSpeed">The weapon's attackspeed</param>
    public Weapon(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int intellect, int agility, int stamina, int strength, float attackSpeed)
        : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize, intellect, agility, stamina, strength)
    {
        this.AttackSpeed = attackSpeed;
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
    /// <returns>Returns a complete tooltip</returns>
    public override string GetTooltip(Inventory inv)
    {
        string equipmentTip = base.GetTooltip(inv);

        if (inv is VendorInventory)
        {
            //Adds the attackspeed to the tooltip
            return string.Format("{0} \n <size=14>AttackSpeed: {1}\n<color=yellow>Price: {2}</color></size>", equipmentTip, AttackSpeed,BuyPrice);
        }
        else if (VendorInventory.Instance.IsOpen)
        {
            //Adds the attackspeed to the tooltip
            return string.Format("{0} \n <size=14>AttackSpeed: {1}\n<color=yellow>Price: {2}</color></size>", equipmentTip, AttackSpeed,SellPrice);
        }
        else
        {
            //Adds the attackspeed to the tooltip
            return string.Format("{0} \n <size=14>AttackSpeed: {1}</size>", equipmentTip, AttackSpeed);
        }

  
    }
}
