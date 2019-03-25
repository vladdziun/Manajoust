using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill_Tooltip : MonoBehaviour {

	public Text skillName;
    public Text skillDescription;

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToCursorPos()
    {
        gameObject.transform.position = Input.mousePosition;       
    }
}
