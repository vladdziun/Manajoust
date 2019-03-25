using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class Chaser : MonoBehaviour {

    public float speed = 20.0f;
    public float minDist = 5f;
    public Transform target;
    public bool isChasing = true;

    public string targetName;

    Animator _anim;

    // Use this for initialization
    void Start()
    {
        // if no target specified, assume the player
        if (target == null)
        {

            if (GameObject.Find(targetName) != null)
            {
                target = GameObject.Find(targetName).GetComponent<Transform>();
            }
        }

        _anim = Camera.main.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.active == true && gameObject.name == "Spider_Boss")
            GetComponent<Animator>().SetBool("Running", true);
        else if(gameObject.name == "Spider_Boss")
            GetComponent<Animator>().SetBool("Running", false);



        if (isChasing)
        {
            if (target == null)
                return;

            // face the target
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            //get the distance between the chaser and the target
            float distance = Vector3.Distance(transform.position, target.position);

            //so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
            if (distance > minDist)
                transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (isChasing == false)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }

    }

    // Set the target of the chaser
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    //void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.name == "Ground")                 //camera shaker
    //    {
    //        if (_anim != null)
    //            _anim.SetTrigger("Shaker");

    //    }
    //}

}
