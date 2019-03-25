using UnityEngine;
using System.Collections;

public enum ItemType {CONSUMEABLE, MAINHAND,TWOHAND, OFFHAND, HEAD,NECK,CHEST,RING,LEGS,BRACERS,BOOTS,TRINKET,SHOULDERS, BELT, GENERIC,GENERICWEAPON, MATERIAL, WARD};
public enum Quality {COMMON,UNCOMMON,RARE,EPIC,LEGENDARY,ARTIFACT}

public class ItemScript : MonoBehaviour 
{
    /// <summary>
    /// The item's neutral sprite
    /// </summary>
    public Sprite spriteNeutral;

    /// <summary>
    /// The item's highlighted sprite
    /// </summary>
    public Sprite spriteHighlighted;

    /// <summary>
    /// The scripts item, this contains all item functionality
    /// </summary>
    private Item item;

    /// <summary>
    /// Property for accesssing the item
    /// </summary>
    public Item Item
    {
        get { return item; }
        set 
        {
            
            item = value;
            spriteHighlighted = Resources.Load<Sprite>(value.SpriteHighlighted);
            spriteNeutral = Resources.Load<Sprite>(value.SpriteNeutral);
        
        }
    }

    /// <summary>
    /// Uses the item
    /// </summary>
    public void Use(Slot slot)
    {
        item.Use(slot, this);

    }

    /// <summary>
    /// Gets the tooltip
    /// </summary>
    /// <returns>The item's tooltip</returns>
    public string GetTooltip(Inventory inv)
    {
        return item.GetTooltip(inv);
    }

}
