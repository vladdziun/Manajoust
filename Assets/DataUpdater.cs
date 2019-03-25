using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUpdater : MonoBehaviour {

    public string inputUsername;
    public int inputGold;
    public int inputScore;
    public string inputInventory;
    public string inputEquip;
    public string inputProgress;

    public GameObject inventoryObject;
    public GameObject characterPanelObject;

    private Inventory _inventory;
    private CharacterPanel _charPanel;
    private Player _player;

    

    string UpdateUserURL = "http://chimaeragames.com/updateUser.php";
    // Use this for initialization
    void Start()
    {
        inputUsername = PlayerPrefs.GetString("username");

        _inventory = inventoryObject.GetComponent<Inventory>();
        _charPanel = characterPanelObject.GetComponent<CharacterPanel>();
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            PushUserData();
        }

            
    }

    public void UpdateUserData(string username, int gold, int score, string inventory, string equip, string progress)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("goldPost", gold);
        form.AddField("scorePost", score);
        form.AddField("inventoryPost", inventory);
        form.AddField("equipPost", equip);
        form.AddField("progressPost", progress);

        WWW www = new WWW(UpdateUserURL, form);
    }

    public void PushUserData()
    {
        
            inputInventory = _inventory.content;
            inputEquip = _charPanel.content;
            inputGold = _player.Gold;

            print("push psuh");
            UpdateUserData(inputUsername, inputGold, inputScore, inputInventory, inputEquip, inputProgress);
 
    }
}
