using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour {

    public GameObject bossBullet;
    public float shootingDelay = 0.5f;
    public float smoothRotation = 90;

    float shootingCD = 0;

    public Transform Target;

    private bool dirRight = true;
    public float speed = 2.0f;



    void Start ()
    {

        if (Target == null)
        {

            if (GameObject.FindWithTag("Player") != null)
            {
                Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {

        Movement();
        Shooting();

        Vector3 dir = Target.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRotation = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, smoothRotation * Time.deltaTime);
        
        //v_diff = (Target.position - transform.position);
        //atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg), smoothRotation * Time.deltaTime);

    }

    void Movement()
    {


        if (dirRight)
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
        else
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;

        if (transform.position.x >= Random.Range(2, 5))
            
        {
            dirRight = false;
        }

        if (transform.position.x <= Random.Range(-2, -5))
        {
            dirRight = true;

        }
    }

    void Shooting()
    {
        shootingCD -= Time.deltaTime; //starting shooting cooldown

        if (shootingCD <= 0) //shooting with delay
        {

            Vector3 offset = transform.rotation * new Vector3(0, 1, 0);

            Instantiate(bossBullet, transform.position + offset, transform.rotation); 

            shootingCD = shootingDelay;
        }

    }
}
