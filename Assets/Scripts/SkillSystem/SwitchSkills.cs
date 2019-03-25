using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkills : MonoBehaviour {

    public string spellButton;

    private int _childCount;

    private Vector3 defaultScale;
	// Use this for initialization
	void Start () {
        _childCount = gameObject.transform.childCount;
        defaultScale = gameObject.GetComponent<RectTransform>().localScale;
    }
	
	// Update is called once per frame
	void Update () {

        if(gameObject.tag == "Skill")
        {
            if (spellButton == "Spell")
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
             }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.GetComponent<RectTransform>().localScale = defaultScale;
            }

        }
    }
}
