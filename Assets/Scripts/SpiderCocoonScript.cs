using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCocoonScript : MonoBehaviour {

    public GameObject spider;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject newProjectile = Instantiate(spider, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }
}
