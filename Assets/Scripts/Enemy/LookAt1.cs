using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class LookAt1 : MonoBehaviour {

	public Transform target;
	
	// Use this for initialization
	void Start () 
	{
		// if no target specified, assume the player
		if (target == null) {
			
			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null)
			return;

        //Vector3 targetPostition = new Vector3(target.position.x,
                                       //this.transform.position.y,
                                       //target.position.z);

        // face the target
        transform.LookAt(target);
		
		//get the distance between the chaser and the target
	}
	
	// Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}
	
}