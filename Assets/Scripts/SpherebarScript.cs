using UnityEngine;
using System.Collections;

public class SpherebarScript : MonoBehaviour
{

    public int color;
    public static Transform[] spheres;
    public string combinations;
    public GameObject skillBar;
    public GameObject skillBookBar;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Red"))
        {
            gameObject.transform.GetChild(0).GetComponent<SphereColor>().color += 1;
        }
        else if (Input.GetButtonDown("Blue"))
        {
            gameObject.transform.GetChild(1).GetComponent<SphereColor>().color += 1;
        }
        else if (Input.GetButtonDown("Purple"))
        {
            gameObject.transform.GetChild(2).GetComponent<SphereColor>().color += 1;
        }

        spheres = new Transform[transform.childCount];
        combinations = string.Empty;
        for (int i = 0; i < spheres.Length; i++)
        {

            spheres[i] = transform.GetChild(i);
            //adding skill names in skill book to a string
            combinations += spheres[i].GetComponent<SphereColor>().color;

        }

        switch (combinations)
        {
            case "123":
                GameObject.Find("SkillIcon4").transform.SetParent(skillBar.transform);
                if(skillBar.transform.childCount > 1)
                skillBar.transform.GetChild(0).SetParent(skillBookBar.transform);
                break;
            case "231":
                GameObject.Find("SkillIcon3").transform.SetParent(skillBar.transform);
                if (skillBar.transform.childCount > 1)
                    skillBar.transform.GetChild(0).SetParent(skillBookBar.transform);
                break;
            case "312":
                GameObject.Find("SkillIcon2").transform.SetParent(skillBar.transform);
                if (skillBar.transform.childCount > 1)
                    skillBar.transform.GetChild(0).SetParent(skillBookBar.transform);
                break;

        }
    }
}
