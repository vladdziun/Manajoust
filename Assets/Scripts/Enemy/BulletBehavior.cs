using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

 
    public float speed = 3.0f;

    void Start()
    {
       
        
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;

        transform.position = pos;
    }
}
