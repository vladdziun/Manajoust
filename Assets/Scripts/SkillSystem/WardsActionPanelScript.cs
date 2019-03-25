using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardsActionPanelScript : MonoBehaviour
{

    private GameObject _slotOne;
    private GameObject _slotTwo;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("WardOne") && gameObject.transform.childCount >= 1)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Slot>().UseItem();
        }

       else if (Input.GetButtonDown("WardTwo") && gameObject.transform.childCount >= 2)
        {
            gameObject.transform.GetChild(1).gameObject.GetComponent<Slot>().UseItem();
        }
    }
}
