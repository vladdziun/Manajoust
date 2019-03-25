using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class MainSkillUsing : MonoBehaviour
{

    public List<Skill> skills;
    public GameObject Player;
    public static Transform[] abilities;

    public GameObject skillBar;
    public GameObject skillBookBar;

    public Texture2D cursorTexture;

    public string obj;

    PlayerMovement3D playerMov;
    public bool isCasting;
    public bool skillCursorEnabled;
    Stats stats;

    public string[] splitSkillSlots;
    public string skillSlots;


    void Awake()
    {

    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerMov = Player.GetComponent<PlayerMovement3D>();
        stats = Player.GetComponent<Stats>();

        AssignButtons();

        foreach (Skill s in skills)
            {
                s.skillTimer = 999;

            }
        }
            

   public void SaveSkillbar()
    {
        string skillSlots = string.Empty;

        abilities = new Transform[transform.childCount];
        for (int i = 0; i < abilities.Length; i++)
        {
                abilities[i] = transform.GetChild(i);
            //adding each skill name to a string . skillSlot is gonna look like "skill1;skill2;..."
            if (abilities[i].tag == "Skill")
            skillSlots += abilities[i].name +";";

        }

        PlayerPrefs.SetString("MainskillBar", skillSlots);
        PlayerPrefs.Save();

        Debug.Log("Skillbar Saved");
    }

    public void LoadSkillBar()
    {
        string skillSlots = PlayerPrefs.GetString("MainskillBar");
        //array with skills on action bar. Using for separete skill names from skillSlots string and write each skill name into sting array
        splitSkillSlots = skillSlots.Split(';');
      
    }

    public void AssignButtons()
    {
        //using to go through each child in action bar and declare button to each slot
        abilities = new Transform[transform.childCount];
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i] = transform.GetChild(i);

            if (abilities[i].tag == "Skill" && abilities[i] != null)
                abilities[i].GetComponentInChildren<UseSkills>().button = "Trap"+(i+1);
        }

    }

    void Update()
    {
        AssignButtons();



        //    if (Input.GetButtonDown(skills[0].skillIcon.GetComponentInChildren<UseSkills>().button) && isCasting == false)
        //    {


        if (Input.GetButtonDown(skills[0].skillIcon.GetComponentInChildren<UseSkills>().button) && isCasting == false)
        {

            if (skills[0].skillTimer >= skills[0].skillCD)
            {
                //use skill  
                Player.GetComponent<Ability1>().TrapAbilityUse(5);
                skills[0].skillTimer = 0;
                

            }
        }


        foreach (Skill s in skills)
        {
                //interrupt casting if press Escape or character is walking
                if (Input.GetKeyDown(KeyCode.Escape) || playerMov.walking == true)
            {
                s.castSlider.gameObject.SetActive(false);
                isCasting = false;
            }


            //fill up cast bar
            if (s.castSlider.gameObject.active ==true)
            {
                s.castSlider.transform.position = new Vector2(Screen.width / 2, Screen.height / 8);
               s.castSlider.value += Time.deltaTime + (stats.castSpeed * 0.001f); //fill up castbar dependence on castSpeed stats
               isCasting = true;
            }
 
            //display skill CD
            if (s.skillTimer <= s.skillCD)
            {
                s.skillTimer += Time.deltaTime;
                s.skillIcon.fillAmount = s.skillTimer / s.skillCD;
            }
        }
    }

  void ChangeToSkillCursor()
    {
        //Replace the 'cursorTexture' with the cursor  
        Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto);
        skillCursorEnabled = true;
    }

    void ChangeToDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        skillCursorEnabled = false;
    }

}
