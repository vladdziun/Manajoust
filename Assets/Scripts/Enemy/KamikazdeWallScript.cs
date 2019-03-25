using UnityEngine;
using System.Collections;

public class KamikazdeWallScript : MonoBehaviour {

    public float speed = 10f;

   
    void Start()
    {
        
    }

    void Update()
    {
     transform.position -= transform.forward * speed*Time.deltaTime;
    }


}
