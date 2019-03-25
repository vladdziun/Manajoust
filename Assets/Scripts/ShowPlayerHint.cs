using UnityEngine;
using System.Collections;

public class ShowPlayerHint : MonoBehaviour {

    public GameObject hint;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Vendor" || other.tag == "NPC" || other.tag == "Chest")
            hint.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vendor" || other.tag == "NPC" || other.tag == "Chest" )
            hint.SetActive(false);
    }
}
