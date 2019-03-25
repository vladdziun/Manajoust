using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class InventoryManager : MonoBehaviour
{

    #region fields

    /// <summary>
    /// This is the InventoryManager's singleton instance
    /// </summary>
    private static InventoryManager instance;

    /// <summary>
    /// This item container contains all the items in the game
    /// </summary>
    private ItemContainer itemContainer = new ItemContainer();

    /// <summary>
    /// The slots prefab
    /// </summary>
    public GameObject slotPrefab;

    /// <summary>
    /// A prefab used for instantiating the hoverObject
    /// </summary>
    public GameObject iconPrefab;

    /// <summary>
    /// This object is used for instantiating items
    /// </summary>
    public GameObject itemObject;

    /// <summary>
    /// A reference to the object that hovers next to the mouse
    /// </summary>
    private GameObject hoverObject;

    /// <summary>
    /// A prototype of the item to drop
    /// </summary>
    public GameObject dropItem;

    /// <summary>
    /// The tool tip to show at the screen
    /// </summary>
    public GameObject tooltipObject;

    /// <summary>
    /// This object is used for scaling the tooltip
    /// </summary>
    public Text sizeTextObject;

    /// <summary>
    /// This is the visual text on the tooltip
    /// </summary>
    public Text visualTextObject;

    /// <summary>
    /// A reference to the inventorys canvas
    /// </summary>
    public Canvas canvas;

    /// <summary>
    /// The slots that we are moving an item from
    /// </summary>
    private Slot from;

    /// <summary>
    /// The slots that we are moving and item to
    /// </summary>
    private Slot to;


    /// <summary>
    /// This is sed to store our items when moving them from one slot to another
    /// </summary>
    private Slot movingSlot;

    /// <summary>
    /// The clicked object
    /// </summary>
    private GameObject clicked;


    /// <summary>
    /// The amount of items to pickup (this is the text on the UI element we use for splitting)
    /// </summary>
    public Text stackText;


    /// <summary>
    /// The UI element that we are using when we need to split a stack
    /// </summary>
    public GameObject selectStackSize;


    /// <summary>
    /// The amount of items we have in our "hand"
    /// </summary>
    private int splitAmount;

    /// <summary>
    /// The maximum amount of items we are allowed to remove from the stack
    /// </summary>
    private int maxStackCount;

    /// <summary>
    /// A reference to the EventSystem 
    /// </summary>
    public EventSystem eventSystem;

    #endregion

    #region properties

    /// <summary>
    /// This is the property for the singleton instance
    /// </summary>
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
            }

            return instance;

        }
    }

    /// <summary>
    /// Property for accssing the item's container
    /// </summary>
    public ItemContainer ItemContainer
    {
        get { return itemContainer; }
        set { itemContainer = value; }
    }

    /// <summary>
    /// The slot we are moving from
    /// </summary>
    public Slot From
    {
        get { return from; }
        set { from = value; }
    }

    /// <summary>
    /// The slot we are moving to
    /// </summary>
    public Slot To
    {
        get { return to; }
        set { to = value; }
    }

    /// <summary>
    /// The clicked item
    /// </summary>
    public GameObject Clicked
    {
        get { return clicked; }
        set { clicked = value; }
    }

    /// <summary>
    /// The acmount if items we are splitting
    /// </summary>
    public int SplitAmount
    {
        get { return splitAmount; }
        set { splitAmount = value; }
    }

    /// <summary>
    /// The max amount of times an item can stack
    /// </summary>
    public int MaxStackCount
    {
        get { return maxStackCount; }
        set { maxStackCount = value; }
    }

    /// <summary>
    /// The slot that contains the items  that we are moing
    /// </summary>
    public Slot MovingSlot
    {
        get { return movingSlot; }
        set { movingSlot = value; }
    }

    /// <summary>
    /// The hover object, that shows the object we have in her hand
    /// </summary>
    public GameObject HoverObject
    {
        get { return hoverObject; }
        set { hoverObject = value; }
    }

    #endregion





    public void Awake()
    {
        //Creates an XML document
        XmlDocument doc = new XmlDocument();

        //Loads the item xml
        TextAsset myXmlAsset = Resources.Load<TextAsset>("Items");

        //Loads it into the xml document
        doc.LoadXml(myXmlAsset.text);
       
        //Defines all the item types that we can serialize
        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumeable), typeof(Material_Item), typeof(Ward) };
       
        //Creates a serializer so that we can serialize the itemContainer
        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);
   
        //Instantiates a stringreader, so that we can read the document
        TextReader textReader = new StringReader(doc.InnerXml);

        //Deserializes the itemcontainer, so that we can access the items
        itemContainer = (ItemContainer)serializer.Deserialize(textReader);

        //Closes to textReader, to clear up
        textReader.Close();

        //Creates the blueprints so that we can craft some items
        CraftingBench.Instance.CreateBlueprints();


    }

    /// <summary>
    /// Sets the stacks info, so that we know how many items we can remove
    /// </summary>
    /// <param name="maxStackCount"></param>
    public void SetStackInfo(int maxStackCount)
    {
        //Shows the UI for splitting a stack
        selectStackSize.SetActive(true);

        //Hides the tooltip so that it doesn't overlap the splitstack ui
        tooltipObject.SetActive(false);
        

        //Resets the amount of split items
        splitAmount = 0;

        //Stores the maxcount
        this.maxStackCount = maxStackCount;

        //Writes writes the selected amount of itesm in the UI
        stackText.text = splitAmount.ToString();
    }

    /// <summary>
    /// Saves every single inventory in the scene
    /// </summary>
    public void Save()
    {   
        //Finds all inventories
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");

        //Loads all inventories
        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().SaveInventory();
        }

        foreach (GameObject chest in chests)
        {
            chest.GetComponent<InventoryLink>().SaveInventory();
        }


    }

    /// <summary>
    /// Loads every single inventory in the scene
    /// </summary>
    public void Load()
    {
        //Finds all inventorys
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");

        //Loads all inventories
        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().LoadInventory();
        }

        foreach (GameObject chest in chests)
        {
            chest.GetComponent<InventoryLink>().LoadInventory();
        }
    }
}
