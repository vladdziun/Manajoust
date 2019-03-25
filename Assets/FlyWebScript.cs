using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWebScript : MonoBehaviour {

    public bool inWeb;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(gameObject.transform.childCount ==0)
            inWeb = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FlyOne(Clone)" || other.gameObject.name == "FlyTwo(Clone)")
        {
            inWeb = true;
            other.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.transform.parent = gameObject.transform;
            
        }
    }
}
