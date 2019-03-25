using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class contains all the items
/// </summary>
public class ItemContainer
{
    /// <summary>
    /// This list contains all the weapons
    /// </summary>
    private List<Item> weapons = new List<Item>();

    /// <summary>
    /// This list contains all equipment
    /// </summary>
    private List<Item> equipment = new List<Item>();

    /// <summary>
    /// This list contains all consumeables
    /// </summary>
    private List<Item> consumeables = new List<Item>();

    /// <summary>
    /// This list contains all wards
    /// </summary>
    private List<Item> ward = new List<Item>();

    /// <summary>
    /// This list contains all the materials
    /// </summary>
    private List<Item> materials = new List<Item>();

    /// <summary>
    /// Property for accessing the material's list
    /// </summary>
    public List<Item> Materials
    {
        get { return materials; }
        set { materials = value; }
    }

    /// <summary>
    /// Property for accessing the consumeable's list
    /// </summary>
    public List<Item> Consumeables
    {
        get { return consumeables; }
        set { consumeables = value; }
    }

    /// <summary>
    /// Property for accessing the ward's list
    /// </summary>
    public List<Item> Ward
    {
        get { return ward; }
        set { ward = value; }
    }

    /// <summary>
    /// Property for accessing the weapon's list
    /// </summary>
    public List<Item> Weapons
    {
        get { return weapons; }
        set { weapons = value; }
    }

    /// <summary>
    /// Proprtyu for accessing the equipment list
    /// </summary>
    public List<Item> Equipment
    {
        get { return equipment; }
        set { equipment = value; }
    }

    /// <summary>
    /// An empty constructor, this is only used for serialization
    /// </summary>
    public ItemContainer()
    { }
}
