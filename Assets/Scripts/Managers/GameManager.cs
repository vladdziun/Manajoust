using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject Player;
    public GameObject Boss;
    public GameObject[] Adds;
    public GameObject Chest;
    public GameObject skillBookBar;
    public GameObject sphereSkillBookBar;
    public GameObject actionSkillBookBar;
    public Text PlayerHealth;
    public Text BossHealth;
    public Slider healthSliderBoss;
    public Slider healthSliderPlayer;
    public static GameManager gm;
    public GameObject inventoryManager;

    public GameObject playAgainButtons;
    public string playAgainLevelToLoad;

    public bool gameIsOver = false;
    public bool isVictory = false;

    public bool chestSpawned = false;


    private Health playerHP;
    private Health bossHP;
    private float maxWidth;

    private Inventory chest;


    void Start ()
    {
        skillBookBar.transform.position = new Vector2(-1000, 50);
        sphereSkillBookBar.transform.position = new Vector2(-1000, 50);
        actionSkillBookBar.transform.position = new Vector2(-1000, 50);

        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        Player = GameObject.FindGameObjectWithTag("Player");
        playerHP = Player.GetComponent<Health>();
        

        Boss = GameObject.FindGameObjectWithTag("Boss");
        bossHP = Boss.GetComponent<Health>();

        if (playAgainButtons)
            playAgainButtons.SetActive(false);

        healthSliderBoss.maxValue = bossHP.healthPoints;
        healthSliderPlayer.maxValue = playerHP.healthPoints;



    }


	void Update ()
    {

        PlayerHealth.text = playerHP.healthPoints.ToString();

       BossHealth.text = bossHP.healthPoints.ToString();

        healthSliderBoss.value =  bossHP.healthPoints;
        healthSliderPlayer.value = playerHP.healthPoints;

        if (playerHP.healthPoints <= 0)
            EndGame();
        if (bossHP.healthPoints <= 0)
            Victory();

        if (!gameIsOver)
            Time.timeScale = 1;

    }

    void EndGame()
    {
        // game is over
        gameIsOver = true;

        if (playAgainButtons)
            playAgainButtons.SetActive(true);

        Time.timeScale = 0;
    }

    void Victory()
    {
      
        if(chestSpawned ==false)
        {
            Chest.transform.position = Boss.transform.position + new Vector3(0, 3, -5);
            chestSpawned = true;
        }
        isVictory = true;

    }

    public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		Application.LoadLevel (playAgainLevelToLoad);
	}

    public void loadLevel(string leveltoLoad)
    {
        // start new game so initialize player state
        

        // load the specified level
        Application.LoadLevel(leveltoLoad);
    }

}
