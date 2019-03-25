using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public string inputUsername;
    public string inputPassword;

    public InputField userNameField;
    public InputField passwordField;

    public Text loginText;

    public string LevelToLoad;

    string LoginURL = "http://chimaeragames.com/Login.php";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Application.loadedLevelName == "MainMenu")
        {
            inputUsername = userNameField.text;
            inputPassword = passwordField.text;

            if (loginText.text == "  login success")
            {
                PlayerPrefs.SetString("username", inputUsername);
                PlayerPrefs.Save();
                Application.LoadLevel(LevelToLoad);
            }
        }


        
    }

    IEnumerator LogintoDB(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);

        WWW www = new WWW(LoginURL, form);

        yield return www;
        loginText.text = www.text;


    }

    public void LoginToGame()
    {
        StartCoroutine(LogintoDB(inputUsername, inputPassword));
    }
}
