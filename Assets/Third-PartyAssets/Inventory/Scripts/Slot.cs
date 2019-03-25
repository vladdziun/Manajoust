using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    #region variables

    /// <summary>
    /// The items that the slot contains
    /// </summary>
    private Stack<ItemScript> items;

    /// <summary>
    /// Indicates the number of items stacked on the slot
    /// </summary>
    public Text stackTxt;

    /// <summary>
    /// The slot's empty sprite
    /// </summary>
    public Sprite slotEmpty;

    /// <summary>
    /// The slot's highlighted sprite
    /// </summary>
    public Sprite slotHighlight;

    /// <summary>
    /// The slots canvas group
    /// </summary>
    [SerializeField]
    private CanvasGroup canvasGroup;

    /// <summary>
    /// Defines what kind of items this slot can contain
    /// </summary>
    public ItemType canContain;

    public bool clickAble = true;

    #endregion

    #region properties

    /// <summary>
    /// A property for accessing the stack of items
    /// </summary>
    public Stack<ItemScript> Items
    {
        get { return items; }
        set { items = value; }
    }

    /// <summary>
    /// Indicates if the slot is empty
    /// </summary>
    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }

    /// <summary>
    /// Indicates if the slot is avaialble for stacking more items
    /// </summary>
    public bool IsAvailable
    {
        get { return CurrentItem.Item.MaxSize > items.Count; }
    }

    /// <summary>
    /// Returns the current item in the stack
    /// </summary>
    public ItemScript CurrentItem
    {
        get { return items.Peek(); }
    }

    #endregion

    void Awake()
    {
        //Instantiates the items stack
        items = new Stack<ItemScript>();
    }

    // Use this for initialization
    void Start()
    {
        //Creates a reference to the slot slot's recttransform
        RectTransform slotRect = GetComponent<RectTransform>();

        //Creates a reference to the stackTxt's recttransform
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

        //Calculates the scalefactor of the text by taking 60% of the slots width
        int txtScleFactor = (int)(slotRect.sizeDelta.x * 0.60);

        //Sets the min and max textSize of the stackTxt
        stackTxt.resizeTextMaxSize = txtScleFactor;
        stackTxt.resizeTextMinSize = txtScleFactor;

        //Sets the actual size of the txtRect
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);

        if (transform.parent != null && gameObject.name != "Hover")
        {
           
            if (canvasGroup == null)
            {
                canvasGroup = transform.parent.GetComponent<CanvasGroup>();
            }
            EventTrigger trigger = GetComponentInParent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { transform.parent.GetComponent<Inventory>().ShowToolTip(gameObject); });
            trigger.triggers.Add(entry);
        }
    }

    /// <summary>
    /// Adds a single item to th inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    public void AddItem(ItemScript item)
    {

        if (IsEmpty) //if the slot is empty
        {
            transform.parent.GetComponent<Inventory>().EmptySlots--; //Reduce the number of empty slots
        }

        items.Push(item); //Adds the item to the stack

        if (items.Count > 1) //Checks if we have a stacked item
        {
            stackTxt.text = items.Count.ToString(); //If the item is stacked then we need to write the stack number on top of the icon
        }

        ChangeSprite(item.spriteNeutral, item.spriteHighlighted); //Changes the sprite so that it reflects the item the slot is occupied by
    }

    /// <summary>
    /// Adds a stack of items to the slot
    /// </summary>
    /// <param name="items">The stack of items to add</param>
    public void AddItems(Stack<ItemScript> items)
    {
        //FIX REDUCED AMOUNT OF ITEMS
        if (IsEmpty) //if the slot is empty
        {
            transform.parent.GetComponent<Inventory>().EmptySlots--; //Reduce the number of empty slots
        }

        this.items = new Stack<ItemScript>(items); //Adds the stack of items to the slot

        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty; //Writes the correct stack number on the icon

        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted); //Changes the sprite so that it reflects the item the slot is occupied by
    }

    /// <summary>
    /// Changes the sprite of a slot
    /// </summary>
    /// <param name="neutral">The neutral sprite</param>
    /// <param name="highlight">The highlighted sprite</param>
    private void ChangeSprite(Sprite neutral, Sprite highlight)
    {
        //Sets the neutralsprite
        GetComponent<Image>().sprite = neutral;

        //Creates a spriteState, so that we can change the sprites of the different states
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        //Sets the sprite state
        GetComponent<Button>().spriteState = st;
    }

    /// <summary>
    /// Uses an item on the slot.
    /// </summary>
    public void UseItem()
    {
        if (!IsEmpty)
        {
            if (tag == "EquipSlot")
            {
                Player.Instance.inventory.AddItem(items.Pop());
                ClearSlot();
                CharacterPanel.Instance.CalcStats();
            }
            else if (transform.parent.GetComponent<Inventory>() is VendorInventory)
            {
                if (CurrentItem.Item.BuyPrice <= Player.Instance.Gold && Player.Instance.inventory.AddItem(CurrentItem))
                {
                    Player.Instance.Gold -= CurrentItem.Item.BuyPrice;
                }
            }
            else if (VendorInventory.Instance.IsOpen)
            {
                Player.Instance.Gold += CurrentItem.Item.SellPrice;
                RemoveItem();
            }
            else if (transform.parent.GetComponent<Inventory>() is ChestInventory)
            {
                Player.Instance.inventory.AddItem(CurrentItem);
                RemoveItem();
            }

            else if (clickAble) //If there is an item on the slot
            {
                items.Peek().Use(this); //Removes the top item from the stack and uses it

                stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty; //Writes the correct stack number on the icon

                if (IsEmpty) //Checks if we just removed the last item from the inventory
                {
                    ChangeSprite(slotEmpty, slotHighlight); //Changes the sprite to empty if the slot is empty

                    transform.parent.GetComponent<Inventory>().EmptySlots++; //Adds 1 to the amount of empty slots

                }
            }
        }

    }

    /// <summary>
    /// Clears the slot
    /// </summary>
    public void ClearSlot()
    {
        //Clears all items on the slot
        items.Clear();

        //Changes the sprite to empty
        ChangeSprite(slotEmpty, slotHighlight);

        //Clears the text
        stackTxt.text = string.Empty;

        if (transform.parent != null)
        {
            transform.parent.GetComponent<Inventory>().EmptySlots++;
        }


    }

    /// <summary>
    /// Removes an amount of items from the slot and  returns them
    /// </summary>
    /// <param name="amount">The amount of items to remove</param>
    /// <returns>Stack of removed items</returns>
    public Stack<ItemScript> RemoveItems(int amount)
    {
        //Creates a temporary stack for containing the items the we need to remove
        Stack<ItemScript> tmp = new Stack<ItemScript>();

        for (int i = 0; i < amount; i++) //Runs through the slots items and pops the into the tmp stack
        {
            tmp.Push(items.Pop());
        }

        //Makes sure that the correct number is shown on the slot
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        //Returns the items that we just removed
        return tmp;
    }

    /// <summary>
    /// Removes the top item from the slot and returns it
    /// </summary>
    /// <returns>The removed item</returns>
    public ItemScript RemoveItem()
    {
        if (!IsEmpty)
        {
            //Remove the item from the stack and stores it in a tmp variable
            ItemScript tmp = items.Pop();

            //Makes sure that the correct number is shown on the slot
            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
            if (IsEmpty)
            {
                ClearSlot();
            }
            //Returns the removed item
            return tmp;
        }

        return null;
    }


    /// <summary>
    /// Handles OnPointer events
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {

        //If the right mousebutton was clicked, and we aren't moving an item and the inventory is visible
        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover") && canvasGroup != null && canvasGroup.alpha > 0)
        {
            //Uses an item on the slot
            UseItem();
        }
        //Checks if we need to show the split stack dialog , this is only done if we shiftclick a slot and we aren't moving an item
        else if (eventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift) && !IsEmpty && !GameObject.Find("Hover") && transform.parent.GetComponent<Inventory>().IsOpen)
        {
            //The dialogs spawnposition
            Vector2 position;

            //Translates the mouse position to onscreen coords so that we can spawn the dialog at the correct position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);

            //Shows the dialog
            InventoryManager.Instance.selectStackSize.SetActive(true);

            //Sets the position
            InventoryManager.Instance.selectStackSize.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);

            //Tell the inventory the item count on the selected slot
            InventoryManager.Instance.SetStackInfo(items.Count);
        }
    }

    /// <summary>
    /// Swaps two items from one slot to another
    /// </summary>
    /// <param name="from">The slot that we are moving from</param>
    /// <param name="to">The slot that we are moving to</param>
    public static void SwapItems(Slot from, Slot to)
    {

        if (to != null && from != null)
        {
            bool calcStats = from.transform.parent == CharacterPanel.Instance.transform || to.transform.parent == CharacterPanel.Instance.transform;

            if (CanSwap(from, to))
            {
                Stack<ItemScript> tmpTo = new Stack<ItemScript>(to.Items); //Stores the items from the to slot, so that we can do a swap

                to.AddItems(from.Items); //Stores the items in the "from" slot in the "to" slot

                if (tmpTo.Count == 0) //If "to" slot if 0 then we dont need to move anything to the "from " slot.
                {
                    //FIX REMOVED SLOTS MINUSMINUS
                    from.ClearSlot(); //clears the from slot
                }
                else
                {
                    from.AddItems(tmpTo); //If the "to" slot contains items thne we need to move the to the "from" slot
                }

            }

            if (calcStats) //Calculates the stats if we need to
            {
                CharacterPanel.Instance.CalcStats();
            }
        }
    }

    /// <summary>
    /// Checks if we can swap an item
    /// </summary>
    /// <param name="from">The slot that we are moving from</param>
    /// <param name="to">The slot that we are moving to</param>
    /// <returns>true if the items can be swapped</returns>
    private static bool CanSwap(Slot from, Slot to)
    {
        ItemType fromType = from.CurrentItem.Item.ItemType; //The type of the item, that we are moving

        if (to.canContain == from.canContain) //Swapping two items in the inventory
        {
            return true;
        }
        if (fromType != ItemType.OFFHAND && to.canContain == fromType) //Equipping an item from the inventory
        {
            return true;
        }
        if (to.canContain == ItemType.GENERIC && (to.IsEmpty || to.CurrentItem.Item.ItemType == fromType)) //Dequipping an item
        {
            return true;
        }
        if (fromType == ItemType.MAINHAND && to.canContain == ItemType.GENERICWEAPON) //Equipping a mainhand
        {
            return true;
        }
        if (fromType == ItemType.TWOHAND && to.canContain == ItemType.GENERICWEAPON && CharacterPanel.Instance.OffHandSlot.IsEmpty) //Equipping a twoand
        {
            return true;
        }
        if (fromType == ItemType.OFFHAND && (to.IsEmpty || to.CurrentItem.Item.ItemType == ItemType.OFFHAND) && (CharacterPanel.Instance.WeaponSlot.IsEmpty || CharacterPanel.Instance.WeaponSlot.CurrentItem.Item.ItemType != ItemType.TWOHAND)) //Equipping the shield
        {
            return true;
        }

        return false;
    }
}

