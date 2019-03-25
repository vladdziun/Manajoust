using UnityEngine;
using System.Collections;

public abstract class Item
{
    /// <summary>
    /// The item's type
    /// </summary>
    public ItemType ItemType { get; set; }

    /// <summary>
    /// The item's quality
    /// </summary>
    public Quality Quality { get; set; }

    /// <summary>
    /// The path to the neutral sprite
    /// </summary>
    public string SpriteNeutral { get; set; }

    /// <summary>
    /// The path to the highlighted sprite
    /// </summary>
    public string SpriteHighlighted { get; set; }

    /// <summary>
    /// The maximum amount of stacks
    /// </summary>
    public int MaxSize { get; set; }

    /// <summary>
    /// The name of the item, this name should be unique
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// The item's description
    /// </summary>
    public string Description { get; set; }

    public int BuyPrice { get; set; }

    public int SellPrice { get; set; }


    /// <summary>
    /// Empty constructor for serilization
    /// </summary>
    public Item()
    { 
        
    }


    /// <summary>
    /// The items's constructor
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="description">The item's description</param>
    /// <param name="itemType">The type of time</param>
    /// <param name="quality">The item's quality</param>
    /// <param name="spriteNeutral">Path to the item's sprite</param>
    /// <param name="spriteHighlighted">Path to the items highlighted sprite</param>
    /// <param name="maxSize">The item's max size</param>
    public Item(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize)
    {
        //Sets all the stats
        this.ItemName = itemName;
        this.Description = description;
        this.ItemType = itemType;
        this.Quality = quality;
        this.SpriteNeutral = spriteNeutral;
        this.SpriteHighlighted = spriteHighlighted;
        this.MaxSize = maxSize;
    }

    /// <summary>
    /// This function uses the item
    /// </summary>
    public abstract void Use(Slot slot, ItemScript item);

    /// <summary>
    /// Creates a tooltip
    /// </summary>
    /// <returns>The base tooltip</returns>
    public virtual string GetTooltip(Inventory inv)
    {

        string stats = string.Empty;  //Resets the stats info
        string color = string.Empty;  //Resets the color info
        string newLine = string.Empty; //Resets the new line

        if (Description != string.Empty) //Creates a newline if the item has a description, this is done to makes sure that the headline and the describion isn't on the same line
        {
            newLine = "\n";
        }

        switch (Quality) //Sets the color accodring to the quality of the item
        {
            case Quality.COMMON:
                color = "white";
                break;
            case Quality.UNCOMMON:
                color = "lime";
                break;
            case Quality.RARE:
                color = "blue";
                break;
            case Quality.EPIC:
                color = "magenta";
                break;
            case Quality.LEGENDARY:
                color = "orange";
                break;
            case Quality.ARTIFACT:
                color = "red";
                break;
        }

        //Returns the item info so that we can use it in the tooltip
        return string.Format("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=lime>" + newLine + "{1}</color></i>\n{2}</size>", ItemName, Description,ItemType.ToString().ToLower());
    }

}
