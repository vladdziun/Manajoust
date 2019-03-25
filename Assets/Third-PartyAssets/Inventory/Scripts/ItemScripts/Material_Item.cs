using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Material_Item : Item
{

    /// <summary>
    /// The constructor of the material
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="description">The item's description</param>
    /// <param name="itemType">The type of time</param>
    /// <param name="quality">The item's quality</param>
    /// <param name="spriteNeutral">Path to the item's sprite</param>
    /// <param name="spriteHighlighted">Path to the items highlighted sprite</param>
    /// <param name="maxSize">The item's max size</param>
    public Material_Item(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize)
        : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize)
    { 
        
    }


    /// <summary>
    /// An empty constructor, this is only used for serializing
    /// </summary>
    public Material_Item()
    { }

    public override string GetTooltip(Inventory inv)
    {
        string materialTip = base.GetTooltip(inv);

        if (inv is VendorInventory)
        {
            return string.Format("{0} \n<size=14><color=yellow>Price: {1}</color></size>", materialTip, BuyPrice);
        }
        else if (VendorInventory.Instance.IsOpen)
        {
            return string.Format("{0} \n<size=14><color=yellow>Price: {1}</color></size>", materialTip, SellPrice);
        }

        return materialTip;
    }

    /// <summary>
    /// We are overriding the use function, so that we dont do anything.
    /// </summary>
    public override void Use(Slot slot, ItemScript item)
    {
       
    }
}
