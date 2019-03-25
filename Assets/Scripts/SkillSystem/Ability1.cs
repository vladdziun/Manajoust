using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ability1 : MonoBehaviour
{

    public GameObject fireballPrefab;
    public GameObject trapPrefab;
    public GameObject frostBoltPrefab;
    public GameObject blizzardPrefab;

    public GameObject blinkPrefab;




    //public Slider castSlider;

    SkillUsing skillUsing;
    //PlayerMovement3D playerMov;


    //public bool isCasting = false;

    public float fireballSpeed = 5;
    public float blinkDistance = 5;

    void Start()
    {
        
        //castSlider.gameObject.SetActive(false);
        //playerMov = GetComponent<PlayerMovement3D>();
    }


    public void FireballAbilityUse()
    {

        Vector3 offset = new Vector3(0, 1.5f, 0);

        GameObject newAbility = Instantiate(fireballPrefab, transform.position + offset, transform.rotation) as GameObject;

        if (!newAbility.GetComponent<Rigidbody>())
        {
            newAbility.AddComponent<Rigidbody>();
        }

        newAbility.GetComponent<Rigidbody>().AddForce(transform.forward * fireballSpeed, ForceMode.VelocityChange);


    }

    public void BlinkAbilityUse()
    {

        StartCoroutine("Blink");

       


    }

    public void arcaneBallAbilityUse()
    {


        Vector3 offset = new Vector3(0, 1.5f, 0);

        GameObject newAbility = Instantiate(fireballPrefab, transform.position + offset, transform.rotation) as GameObject;

        if (!newAbility.GetComponent<Rigidbody>())
        {
            newAbility.AddComponent<Rigidbody>();
        }

        newAbility.GetComponent<Rigidbody>().AddForce(transform.forward * fireballSpeed, ForceMode.VelocityChange);
    }

    public void frostBallAbilityUse()
    {


        Vector3 offset = new Vector3(0, 1.5f, 0);

        GameObject newAbility = Instantiate(frostBoltPrefab, transform.position + offset, transform.rotation) as GameObject;

        if (!newAbility.GetComponent<Rigidbody>())
        {
            newAbility.AddComponent<Rigidbody>();
        }

        newAbility.GetComponent<Rigidbody>().AddForce(transform.forward * fireballSpeed, ForceMode.VelocityChange);
    }

    public void TrapAbilityUse(float distance)
    {

        Vector3 offset = new Vector3(0, 0.5f, 0);
        //distance = Vector3.Distance(gameObject.GetComponent<PlayerMovement3D>().mousePos, transform.position);
        //if (distance <= 5f)
        //{
        //    GameObject newAbility = Instantiate(trapPrefab, gameObject.GetComponent<PlayerMovement3D>().mousePos + offset, Quaternion.identity) as GameObject;
        //} 

        GameObject newAbility = Instantiate(trapPrefab ,transform.position, Quaternion.identity) as GameObject;
    }

    public void BlizzardAbilityUse(float distance)
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        distance = Vector3.Distance(gameObject.GetComponent<PlayerMovement3D>().mousePos, transform.position);
        if (distance <= 5f)
        {
            GameObject newAbility = Instantiate(blizzardPrefab, gameObject.GetComponent<PlayerMovement3D>().mousePos + offset, Quaternion.identity) as GameObject;
        }
    }

    IEnumerator Blink()
    {
       
        yield return new WaitForSeconds(0.2f);
        Instantiate(blinkPrefab, transform.position + new Vector3 (0,5,0), new Quaternion(0,0,0,0));
        this.gameObject.transform.position += transform.forward * blinkDistance;
    }
   
}















//////////by Vladyslav Dziun (C) All rights reserved 2016