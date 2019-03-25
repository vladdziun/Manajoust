using UnityEngine;
using System.Collections;

public class ShootingWardScript : MonoBehaviour {

    // Reference to projectile prefab to shoot
    public GameObject projectile;
    public float power = 10.0f;
    public float secondsBetweenSpawning = 0.1f;
    public Vector3 offset;

    public Transform target;
    public string targetName;

    private float nextSpawnTime;

    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {

   
    }

    void LookAtTarget()
    {
      //  GameObject go = GameObject.FindGameObjectWithTag("Boss");
        //target = go.transform;
        //if (go == null)
        //    target = go.transform;

        Vector3 targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);

        // face the target
        transform.LookAt(targetPostition);
    }

    void Shoot()
    {
        // Detect if fire button is pressed
        if (Time.time >= nextSpawnTime)

        {
            // if projectile is specified
            if (projectile)
            {

                // Instantiante projectile at the camera + 1 meter forward with camera rotation


                GameObject newProjectile = Instantiate(projectile, transform.position + offset, transform.rotation) as GameObject;

                // if the projectile does not have a rigidbody component, add one
                if (!newProjectile.GetComponent<Rigidbody>())
                {
                    newProjectile.AddComponent<Rigidbody>();
                }

                // Apply force to the newProjectile's Rigidbody component if it has one
                newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);

                // play sound effect if set

            }
            nextSpawnTime = Time.time + secondsBetweenSpawning;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = other.gameObject.transform;
            LookAtTarget();
            Shoot();

        }
    }
}
