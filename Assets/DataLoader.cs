using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    public string[] players;

    public GameObject _inventory;
    public GameObject _charPanel;

    public string inputUsername;

    bool isLoaded = false;

    string DataURL = "http://chimaeragames.com/PlayersData.php";

    // Use this for initialization
    private void Awake()
    {
        inputUsername = PlayerPrefs.GetString("username");
    }

    void Start()
    {
      
        StartCoroutine(LoadData(inputUsername));
    }

    //IEnumerator Start () {

    //    WWW playersData = new WWW("http://chimaeragames.com/PlayersData.php");
    //    yield return playersData;
    //    string playersDataString = playersData.text;

    //    print(playersDataString);

    //    players = playersDataString.Split('|');

        
    //}
	
	// Update is called once per frame
	void Update () {

        //if (players.Length >= 1 && Time.timeSinceLevelLoad < 1)
        //{
        //    if (isLoaded)
        //        return;

        //    PlayerPrefs.SetString("Inventorycontent", players[0]);
        //    PlayerPrefs.SetString("CharPanel", players[1]);
        //    PlayerPrefs.Save();
        //    InventoryManager.Instance.Load();
        //    isLoaded = true;
        //}        
    }

    IEnumerator LoadData(string username)
    {

        print(username);
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);

        WWW playersData = new WWW(DataURL, form);

        yield return playersData;
        string playersDataString = playersData.text;

        print(playersDataString);

        players = playersDataString.Split('|');
        ///
        PlayerPrefs.SetString("Inventorycontent", players[0]);
        PlayerPrefs.SetString("CharPanel", players[1]);
        PlayerPrefs.Save();
        InventoryManager.Instance.Load();
    }

}
