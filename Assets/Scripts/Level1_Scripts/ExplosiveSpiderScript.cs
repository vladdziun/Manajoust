using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveSpiderScript : MonoBehaviour {

    GameObject secondSpider;

    // Use this for initialization
    void Awake()
    {
        secondSpider = GameObject.FindGameObjectWithTag("ExplosiveSpider"); //using this to find second spider

    }

    void Start () {

        secondSpider.tag = "Untagged";
        gameObject.tag = "Untagged";

    }

	
	// Update is called once per frame
	void Update () {
        if(gameObject != null && secondSpider !=null)
        {
            RaycastHit hit;

            Vector3 offset = new Vector3(0, 0.8f, 0);

            Ray ray = new Ray(transform.position + offset, (secondSpider.transform.position - transform.position));
            Debug.DrawRay(transform.position + offset, (secondSpider.transform.position - transform.position), Color.red, 1.0f);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    gameObject.GetComponent<Chaser>().target.gameObject.GetComponent<ExplosiveCocoon>().Explode();
                    secondSpider.GetComponent<Chaser>().target.gameObject.GetComponent<ExplosiveCocoon>().Explode();
                    Destroy(gameObject);
                    Destroy(secondSpider);
                }

            }
        }
    
    }
}
