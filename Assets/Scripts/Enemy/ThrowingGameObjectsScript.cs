using UnityEngine;
using System.Collections;

public class ThrowingGameObjectsScript : MonoBehaviour {

    // Reference to projectile prefab to shoot
    public GameObject projectile;

    public float secondsBetweenSpawning = 0.1f;
    public float projectile_Velocity;//velocity to reach a target
    public float veloCalibrate = 1;
    public float angle;

    

    public Vector3 offset; //adjust projectile's starting position

    public Transform Target;
    public string target;//position where projectile will be thrown
    public Transform Thrower; //first position of projectile

    private float nextSpawnTime;

    GameObject go;

    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
        go = GameObject.Find(target);
        if (go)
        {
            Target = go.transform;

            Vector3 targetPostition = new Vector3(go.transform.position.x,
                                 this.transform.position.y,
                                 go.transform.position.z);

            // face the target
            transform.LookAt(targetPostition);
        }

            



  

        // Detect if fire button is pressed
        if (Time.time >= nextSpawnTime)

        {
            // if projectile is specified
            if (projectile && Time.timeSinceLevelLoad > 2)
            {

                // Instantiante the projectile at current GO position + offset


                GameObject newProjectile = Instantiate(projectile, transform.position + offset, transform.rotation) as GameObject;

                // if the projectile does not have a rigidbody component, add one
                if (!newProjectile.GetComponent<Rigidbody>())
                {
                    newProjectile.AddComponent<Rigidbody>();
                }
                
                //calculate distance between an object which throw the projectile and target
                float target_Distance = Vector3.Distance(Thrower.position, Target.position);
                //calculate the projectile velocity that needed to reach a target  (adjust 0.5f to regulate velocity)
                projectile_Velocity = target_Distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / 0.5f * veloCalibrate);
                
                // Apply force to the newProjectile's Rigidbody component if it has one
                newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectile_Velocity, ForceMode.VelocityChange);

                // play sound effect if set

            }
            nextSpawnTime = Time.time + secondsBetweenSpawning;
        }
    }
}
