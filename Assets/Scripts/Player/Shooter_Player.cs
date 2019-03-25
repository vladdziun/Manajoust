using UnityEngine;
using System.Collections;

public class Shooter_Player : MonoBehaviour {

    // Reference to projectile prefab to shoot
    public GameObject projectile;
    public float power = 10.0f;
    public float secondsBetweenSpawning = 0.1f;
    public Vector3 offset;
    private float nextSpawnTime;
    public static Damage damage;
    AudioSource audioSource;

    public GameObject skillBar;
    SkillUsing skillUsing;

    Animator _anim;

   
    // Reference to AudioClip to play

    void Start()
    {
        //damage = projectile.GetComponent<Damage>();
        audioSource = GetComponent<AudioSource>();

        _anim = GetComponent<Animator>();

       // skillUsing = skillBar.GetComponent<SkillUsing>();

    }
    // Update is called once per frame
    void Update() {


        // Detect if fire button is pressed
        if (Input.GetMouseButtonDown(0) && Time.time >= nextSpawnTime)

        {
            // damage.DamageRandomize();




            // Instantiante projectile at the camera + 1 meter forward with camera rotation

            _anim.SetTrigger("Attack");

            GameObject newProjectile = Instantiate(projectile, transform.position + offset,transform.rotation) as GameObject;

            // if the projectile does not have a rigidbody component, add one
            if (!newProjectile.GetComponent<Rigidbody>())
            {
                newProjectile.AddComponent<Rigidbody>();
            }
            // Apply force to the newProjectile's Rigidbody component if it has one
            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);

                // play sound effect if set

                audioSource.Play();
                

        
        nextSpawnTime = Time.time + secondsBetweenSpawning;
    }
    }
}

