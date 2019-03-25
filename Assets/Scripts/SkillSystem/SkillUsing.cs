using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class SkillUsing : MonoBehaviour
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
            

   //public void SaveSkillbar()
   // {
   //     string skillSlots = string.Empty;

   //     abilities = new Transform[transform.childCount];
   //     for (int i = 0; i < abilities.Length; i++)
   //     {
   //             abilities[i] = transform.GetChild(i);
   //         //adding each skill name to a string . skillSlot is gonna look like "skill1;skill2;..."
   //         if (abilities[i].tag == "Skill")
   //         skillSlots += abilities[i].name +";";

   //     }

   //    // PlayerPrefs.SetString("skillBar", skillSlots);
   //    // PlayerPrefs.Save();
        
        
   // }

    //public void LoadSkillBar()
    //{
    //    string skillSlots = PlayerPrefs.GetString("skillBar");
    //    //array with skills on action bar. Using for separete skill names from skillSlots string and write each skill name into sting array
    //    splitSkillSlots = skillSlots.Split(';');
      
    //}

    public void AssignButtons()
    {
        //using to go through each child in action bar and declare button to each slot
        abilities = new Transform[transform.childCount];
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i] = transform.GetChild(i);

            if (abilities[i].tag == "Skill")
                abilities[i].GetComponentInChildren<UseSkills>().button = "Spell";
        }

    }

    void Update()
        

    {
        AssignButtons();


            if (Input.GetButtonDown(skills[0].skillIcon.GetComponentInChildren<UseSkills>().button) && isCasting == false)
            {
            
            if (skills[0].skillTimer >= skills[0].skillCD)
            {


                //use skill
                Player.GetComponent<Ability1>().FireballAbilityUse();
                skills[0].skillTimer = 0;

            }
        }

        else if (Input.GetButtonDown(skills[1].skillIcon.GetComponentInChildren<UseSkills>().button) && isCasting == false)
        {

            if (skills[1].skillTimer >= skills[1].skillCD)
            {

                //use skill
                Player.GetComponent<Ability1>().BlinkAbilityUse();
                skills[1].skillTimer = 0;
                

            }
        }

        else if (Input.GetButtonDown(skills[2].skillIcon.GetComponentInChildren<UseSkills>().button) && isCasting == false)
        {

            if (skills[2].skillTimer >= skills[2].skillCD)
            {

                if (skills[2].isCastable)
                {
                    skills[2].castSlider.gameObject.SetActive(true);
                    skills[2].castSlider.value = 0;

                   
                    skills[2].castSlider.maxValue = skills[2].castTime; 
                }
                else
                {
                    Player.GetComponent<Ability1>().frostBallAbilityUse();
                    skills[2].skillTimer = 0;
                }
             
               

            }
        }

        else if (Input.GetButtonDown(skills[3].skillIcon.GetComponentInChildren<UseSkills>().button) && isCasting == false)
        {

            if (skills[3].skillTimer >= skills[3].skillCD)
            {

                if (skills[3].isCastable)
                {
                    skills[3].castSlider.gameObject.SetActive(true);
                    skills[3].castSlider.value = 0;

                   
                    skills[3].castSlider.maxValue = skills[3].castTime;
                }
                else
                {
                    Player.GetComponent<Ability1>().frostBallAbilityUse();
                }

            }
        }

        else if (Input.GetButtonDown(skills[4].skillIcon.GetComponentInChildren<UseSkills>().button) && isCasting == false)
        {

            if (skills[4].skillTimer >= skills[4].skillCD)
            {

                //use skill
                Player.GetComponent<Ability1>().BlinkAbilityUse();
                skills[4].skillTimer = 0;


            }
        }


        else if (Input.GetButtonDown(skills[5].skillIcon.GetComponentInChildren<UseSkills>().button) && isCasting == false)
        {

            if (skills[5].skillTimer >= skills[5].skillCD)
            {

                if (skills[5].isCastable)
                {
                    skills[5].castSlider.gameObject.SetActive(true);
                    skills[5].castSlider.value = 0;


                    skills[5].castSlider.maxValue = skills[3].castTime;
                }
                else
                {
                    Player.GetComponent<Ability1>().frostBallAbilityUse();
                }

            }
        }


        //use castable skills
        //if cast slider full - use skill and start CD
        if (skills[2].castSlider.value == skills[2].castSlider.maxValue)
        {
            Player.GetComponent<Ability1>().frostBallAbilityUse();
            isCasting = false;
            skills[2].skillTimer = 0; //start skill CD

            skills[2].castSlider.value = 0;
          skills[2].castSlider.gameObject.SetActive(false);
        }

        if (skills[3].castSlider.value == skills[3].castSlider.maxValue)
        {
            Player.GetComponent<Ability1>().arcaneBallAbilityUse();
            isCasting = false;
            skills[3].skillTimer = 0; //start skill CD
            skills[3].castSlider.value = 0;
            skills[3].castSlider.gameObject.SetActive(false);
        }

        if (skills[5].castSlider.value == skills[3].castSlider.maxValue)
        {
            Player.GetComponent<Ability1>().arcaneBallAbilityUse();
            isCasting = false;
            skills[5].skillTimer = 0; //start skill CD
            skills[5].castSlider.value = 0;
            skills[5].castSlider.gameObject.SetActive(false);
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

    [System.Serializable]
    public class Skill
    {
        //variables for each skill

        public float skillCD;
        public float skillTimer;
        public float castTime;
        public Image skillIcon;
        public Slider castSlider;
        public bool isCastable;

    }