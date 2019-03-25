using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseManageerScript : MonoBehaviour {

    public GameObject tutorialNpc;

    public GameObject[] coachDialogs;
    public GameObject[] shadyDialogs;

    public GameObject vendorDialogPref;

    public GameObject frostyPref;
    public GameObject[] targets;
    public GameObject scarecrow;
    public GameObject shadyDealer;
    public GameObject maelstrom;
    public GameObject portal;
    public GameObject inventory;

    private bool canUseFireball;


    void Start () {
        Time.timeScale = 1;
	
	}

    // Update is called once per frame
    void Update()
    {
        if (coachDialogs[0].active == false)
        {
            tutorialNpc.GetComponent<Enemy_WaypointMovement>().enabled = true;

            if (tutorialNpc.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 2 && coachDialogs[1].GetComponentInChildren<Dialogue>()._isEndOfDialogue != true)
            {
                tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = true;
                coachDialogs[1].SetActive(true);

            }
            if (coachDialogs[1].GetComponentInChildren<Dialogue>()._isEndOfDialogue == true)
            {
                coachDialogs[1].SetActive(false);
                //GameObject.Find("Player").GetComponent<Shooter_Player>().enabled = true;

            }

        }


        if (targets[0].GetComponent<Health>().healthPoints <= 0 && targets[1].GetComponent<Health>().healthPoints <= 0 && targets[2].GetComponent<Health>().healthPoints <= 0 && coachDialogs[2].GetComponentInChildren<Dialogue>()._isEndOfDialogue == false)
        {
            targets[0].GetComponent<Health>().healthPoints = 2;
            targets[1].GetComponent<Health>().healthPoints = 2;
            targets[2].GetComponent<Health>().healthPoints = 2;
            canUseFireball = true;

            if (coachDialogs[2].GetComponentInChildren<Dialogue>()._isEndOfDialogue == false)
                coachDialogs[2].SetActive(true);
            else
            {
                coachDialogs[2].SetActive(false);
            }
        }

        if (targets[0].GetComponent<Health>().healthPoints <= 0 && targets[1].GetComponent<Health>().healthPoints <= 0 && targets[2].GetComponent<Health>().healthPoints <= 0 && canUseFireball == true)
        {
            if (coachDialogs[3].GetComponentInChildren<Dialogue>()._isEndOfDialogue == false)
                coachDialogs[3].SetActive(true);
            else
            {
                coachDialogs[3].SetActive(false);
                canUseFireball = false;
                tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = false;
            }
        }



        if (tutorialNpc.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 3 && coachDialogs[4].GetComponentInChildren<Dialogue>()._isEndOfDialogue != true)
        {
            tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = true;

            coachDialogs[4].SetActive(true);

        }
        else if (coachDialogs[4].GetComponentInChildren<Dialogue>()._isEndOfDialogue == true)
        {
            maelstrom.GetComponent<BoxCollider>().enabled = true;
        }


        if (frostyPref.transform.parent.name == "ActionSkillBar")
        {

            if (coachDialogs[5].GetComponentInChildren<Dialogue>()._isEndOfDialogue == false)
                coachDialogs[5].SetActive(true);

            else if (coachDialogs[5].GetComponentInChildren<Dialogue>()._isEndOfDialogue == true && coachDialogs[5].active == true)
            {
                coachDialogs[5].SetActive(false);
                tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = false;
            }


            if (tutorialNpc.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 4 && coachDialogs[6].GetComponentInChildren<Dialogue>()._isEndOfDialogue != true)
            {
                if (coachDialogs[6].active == false)
                {
                    tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = true;

                    coachDialogs[6].SetActive(true);
                    scarecrow.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }


        if (scarecrow.GetComponent<Health>().healthPoints <= 0)
        {
            if (coachDialogs[7].GetComponentInChildren<Dialogue>()._isEndOfDialogue == false)
                coachDialogs[7].SetActive(true);
            else if(coachDialogs[7].active == true)
            {
                coachDialogs[7].SetActive(false);
                tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = false;

                shadyDealer.GetComponent<Enemy_WaypointMovement>().enabled = true;
            }
        }

        if (tutorialNpc.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 5 && coachDialogs[8].GetComponentInChildren<Dialogue>()._isEndOfDialogue != true)
        {
            if (coachDialogs[8].active == false)
            {
                tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = true;

                coachDialogs[8].SetActive(true);
            }
        }


        if (shadyDealer.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 1)
        {
            if(shadyDialogs[0].GetComponentInChildren<Dialogue>()._isEndOfDialogue == false)
            {
                shadyDealer.GetComponent<Enemy_WaypointMovement>().stopMovment = true;
                shadyDialogs[0].SetActive(true);
            }

           else if (shadyDialogs[0].GetComponentInChildren<Dialogue>()._isEndOfDialogue == true)
            {
                shadyDialogs[0].SetActive(false);
            }
        }

        if (shadyDialogs[1].GetComponentInChildren<Dialogue>()._isEndOfDialogue == true)
        {
            shadyDialogs[1].SetActive(false);
            shadyDealer.GetComponent<Enemy_WaypointMovement>().stopMovment = false;

            tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = false;
        }

        if (shadyDealer.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 3)
        {
            shadyDealer.GetComponent<Enemy_WaypointMovement>().stopMovment = true;
        }

        if (tutorialNpc.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 6)
        {
            if (coachDialogs[9].GetComponentInChildren<Dialogue>()._isEndOfDialogue != true)
            {
                tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = true;

                coachDialogs[9].SetActive(true);
            }
        }

        if (tutorialNpc.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 7)
        {
            if (coachDialogs[10].GetComponentInChildren<Dialogue>()._isEndOfDialogue != true)
            {
                tutorialNpc.GetComponent<Enemy_WaypointMovement>().stopMovment = true;
                coachDialogs[10].SetActive(true);
                portal.GetComponent<BoxCollider>().enabled = true;
            }

            else
            {
                coachDialogs[10].SetActive(false);
            }
        }


        //if (shadyDealer.GetComponent<Enemy_WaypointMovement>().wavepointIndex == 2)
        //{
        //    shadyDealer.GetComponent<Enemy_WaypointMovement>().stopMovment = true;
        //    shadyDialogs[1].SetActive(true);

        //    if (shadyDialogs[1].GetComponentInChildren<Dialogue>()._isEndOfDialogue == true)
        //    {
        //        shadyDialogs[1].SetActive(false);
        //    }
        //}


        //if(shadyWardDialog.GetComponentInChildren<Dialogue>()._isEndOfDialogue == true)
        //{
        //    startBattleDialog.SetActive(true);

        //    if(startBattleDialog.GetComponentInChildren<Dialogue>()._isEndOfDialogue)
        //        startBattleDialog.SetActive(false);
        //}

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ward")
        {

            if (shadyDialogs[1].GetComponentInChildren<Dialogue>()._isEndOfDialogue == false)
            {
                shadyDialogs[1].SetActive(true);
                print("blabla");
            }  
        }
    }
    
}
