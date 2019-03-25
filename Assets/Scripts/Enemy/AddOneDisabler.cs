using UnityEngine;
using System.Collections;

public class AddOneDisabler : MonoBehaviour {
    public GameObject addOne;
    public int additionalDamage;
	// Use this for initializatione
	void Start () {

        addOne = GameObject.Find("AddOne");
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "AddOne")
        StartCoroutine("Disabler");
    }
    void OnTriggerStay(Collider col)
    {
        //increase minimum damage value if GO in the trigger
        if (col.gameObject.name == "AddOne"){
            if (GameObject.FindGameObjectWithTag("PlayerBullet") != null)
                GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<Damage>().damageAmountMin =additionalDamage;
        }
        
    }

    IEnumerator Disabler()
    {
        addOne.GetComponentInChildren<Shooter_Boss>().enabled = false;
        addOne.GetComponent<Chaser>().enabled = false;
        addOne.GetComponent<SphereCollider>().material.bounciness = 0;
        yield return new WaitForSeconds(10);
        addOne.GetComponentInChildren<Shooter_Boss>().enabled = true;
        addOne.GetComponent<SphereCollider>().material.bounciness = 1;
        addOne.GetComponent<Chaser>().enabled = true;
    }
}
