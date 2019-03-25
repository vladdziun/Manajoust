using UnityEngine;
using System.Collections;

public class TimeDestructor : MonoBehaviour {

    public float timeOut;

	void Awake ()
    {
        Invoke("DestroyNow", timeOut);

	}
	
	
	void DestroyNow ()
    {
        Destroy(gameObject);
	}
}
