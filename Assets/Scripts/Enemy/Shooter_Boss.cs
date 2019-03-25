using UnityEngine;
using System.Collections;

public class Shooter_Boss : MonoBehaviour {

	// Reference to projectile prefab to shoot
	public GameObject projectile;
	public float power = 10.0f;
	public float secondsBetweenSpawning = 0.1f;
    public Vector3 offset;

	private float nextSpawnTime;

    void Start()
	{


	}
	// Update is called once per frame
	void Update () {
	
		// Detect if fire button is pressed
		if (Time.time >= nextSpawnTime)

		{	
			// if projectile is specified
			if (projectile && Time.timeSinceLevelLoad > 2)
			{

                // Instantiante projectile at the camera + 1 meter forward with camera rotation
                

					GameObject newProjectile = Instantiate(projectile, transform.position + offset, transform.rotation) as GameObject;
                    GetComponent<Animator>().SetTrigger("Shoot");
                

                // if the projectile does not have a rigidbody component, add one
                if (!newProjectile.GetComponent<Rigidbody>()) 
				{
					newProjectile.AddComponent<Rigidbody>();
				}

                // Apply force to the newProjectile's Rigidbody component if it has one
                newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);
				
				// play sound effect if set
				
			}
			nextSpawnTime = Time.time+secondsBetweenSpawning;
        }
	}
}
