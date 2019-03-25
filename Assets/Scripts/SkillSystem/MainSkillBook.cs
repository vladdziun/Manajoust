using UnityEngine;
using System.Collections;

public class MainSkillBook : MonoBehaviour
{

    public static Transform[] abilities1;

    public GameObject skillBar;
    public GameObject skillBookBar;

    public string[] aNames;
    public string[] splitaNames;
    public string ss;



    void Start()
    {

        //loading skills from action bar
        skillBar.GetComponent<MainSkillUsing>().LoadSkillBar();

        //going thorugh each child in skillBook
        abilities1 = new Transform[transform.childCount];
       string aNames = string.Empty;
        for (int i = 0; i < abilities1.Length; i++)
        {   

            abilities1[i] = transform.GetChild(i);
            //adding skill names in skill book to a string
            if (abilities1[i].tag == "Skill")
                aNames += abilities1[i].name + ";" ;
        }
        //separete names in the aNames string and write skill name to each element in the new sting array
        splitaNames = aNames.Split(';');
       
        //check if skill names from actionbar (saved in PlayerPrefs) matching skill names in skillbook. If names are the same - move them to the action bar
        foreach (string a in skillBar.GetComponent<MainSkillUsing>().splitSkillSlots)
        {
            foreach (string b in splitaNames)
            {
                //comparing skill names
                if (a == b)
                {

                    if(b != "")
                        GameObject.Find(b).transform.SetParent(skillBar.transform);

                }
            }
        }
   }


    void Update()
    {
        abilities1 = new Transform[transform.childCount];
        for (int i = 0; i < abilities1.Length; i++)
        {
            abilities1[i] = transform.GetChild(i);

            if (abilities1[i].tag == "Skill")
                abilities1[i].GetComponentInChildren<UseSkills>().button = "InactiveButton";
        }

        }
    }

