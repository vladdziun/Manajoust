using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataInserter : MonoBehaviour {

    public string inputUsername;
    public string inputEmail;
    public string inputPassword;

    public GameObject createUserCanvas;
    public GameObject loginCanvas;

    public InputField userNameField;
    public InputField emailField;
    public InputField passwordField;

    public Text createUserText;


    string CreateUserURL = "http://chimaeragames.com/insertUser.php";
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        inputUsername = userNameField.text;
        inputEmail = emailField.text;
        inputPassword = passwordField.text;

    }

    IEnumerator CreateUser (string username, string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("emailPost", email); 
        form.AddField("passwordPost", password);

        WWW www = new WWW(CreateUserURL, form);

        yield return www;
        print(www.text);
        createUserText.text = www.text;
        if (createUserText.text == "  ")
        {
            loginCanvas.active = true;
            createUserCanvas.active = false;
        }

    }


    public void PushUserToDB()
    {
        createUserText.text = "";

        if (inputUsername.Length <= 4 && inputUsername.Length != 0)
        {
            createUserText.text = "Username is too short";
        }
        else if (inputPassword.Length <= 6 && inputPassword.Length != 0)
        {
            createUserText.text = "Password is too short";
        }
        else if(inputUsername.Length == 0 || inputPassword.Length == 0 || inputEmail.Length == 0)
        {
            createUserText.text = "You have to fill out all fields!";
        }
        else
        {
            createUserText.text = "";
        }


        if (createUserText.text == "")
        {
            StartCoroutine(CreateUser(inputUsername, inputEmail, inputPassword));
    
        }
    }
}
