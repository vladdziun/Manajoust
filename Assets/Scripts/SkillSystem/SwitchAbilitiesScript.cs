using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchAbilitiesScript : MonoBehaviour {

  public int number;

    public int _childCount;
    public int maxAbilitiesAmount = 3;

	void Start () {
        number = 1;
        countChildren();

    }
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetKeyDown(KeyCode.Tab))
            {
            if(number >= 1)
            number += 1;


        }
        if (number >= maxAbilitiesAmount+1)
            number = 1;
        //TODO: refactor repeatative code
        switch (number)
        {
            case 1:
                countChildren();
                if (_childCount != 0 && _childCount >=1)
                {
                    if (gameObject.transform.GetChild(0).gameObject != null)
                        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SwitchSkills>().spellButton = "Spell";

                }
                break;

            case 2:
                countChildren();
                if (number != 1 && _childCount != 0 && _childCount >= 2)
                {
                    if (gameObject.transform.GetChild(1).gameObject != null)
                        gameObject.transform.GetChild(1).gameObject.GetComponentInChildren<SwitchSkills>().spellButton = "Spell";

                }
                break;

            case 3:
                countChildren();
                if (number != 1 && _childCount != 0 && _childCount >= 3)
                {
                    if (gameObject.transform.GetChild(2).gameObject != null)
                        gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<SwitchSkills>().spellButton = "Spell";

                }
                break;

            default:

                break;

               
        }

        //TODO: This entire idiotism should be refactored
        if (number != 1 && _childCount != 0 && _childCount >= 1 && gameObject.transform.GetChild(0).gameObject != null)
            gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SwitchSkills>().spellButton = "InactiveButton";

        if (number != 2 && _childCount != 0 && _childCount >= 2 && gameObject.transform.GetChild(1).gameObject != null)
            gameObject.transform.GetChild(1).gameObject.GetComponentInChildren<SwitchSkills>().spellButton = "InactiveButton";

        if (number != 3 && _childCount != 0 && _childCount >= 3 && gameObject.transform.GetChild(1).gameObject != null)
            gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<SwitchSkills>().spellButton = "InactiveButton";
    }

    public void countChildren()
    {
        _childCount = gameObject.transform.childCount;
    }
}
