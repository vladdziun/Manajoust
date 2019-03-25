using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebZone : MonoBehaviour {


    // Use this for initialization
    void Awake ()
    {

    }
	
	// Update is called once per frame
	void LateUpdate ()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement3D>().speed = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement3D>().speed = other.gameObject.GetComponent<PlayerMovement3D>().defaultSpeed;
        }
    }
}
