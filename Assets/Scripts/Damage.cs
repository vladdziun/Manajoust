using UnityEngine;
using System.Collections;


public class Damage : MonoBehaviour
{

    public int damageAmountMin;
    public int damageAmountMax;
    public int damageAmount;
    public int critChance; //chance to get double damage from 0 to 100
    private int critRange;
    private bool isCrit = false;
 
    public bool damageOnTrigger = true;
    public bool destroySelfOnImpact = false;
    public bool destroySelfOnCol;
    static HpSliderScript hpslider;

    void Start()
    {

    }


    void OnTriggerEnter(Collider collision)                     
    {
        damageAmount = Random.Range(damageAmountMin, damageAmountMax); //randomize damage

        critRange = Random.Range(1, 100);

        //initialize crit mechanic
        if (critChance >= critRange)
        {
            isCrit = true;
            damageAmount = damageAmount * 2;
        }
        else
        {
            isCrit = false;
        }
      
        CombatTextManager.Instance.DamageToString(damageAmount.ToString());//using to show the damage

       if (collision.gameObject.CompareTag("Environment"))
        {
            if(destroySelfOnCol)
                Destroy(gameObject, 0.1f);

        }
            
        //create combat text on collison position. Creating text only if go colliding with Boss
        if (collision.gameObject.CompareTag("Boss")|| collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("BossHeal"))
        {
            if (!isCrit)
            { 
                CombatTextManager.Instance.CreateText(transform.position, false);
            }
            else
            {
                CombatTextManager.Instance.CreateText(transform.position, true);
            }
        }

        if (damageOnTrigger)
        {   
            if (collision.gameObject.GetComponent<Health>() != null)
            {   // if the hit object has the Health script on it, deal damage
                
                collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);

                //call updatehelthbar fuction from HpSliderScript
                if(collision.gameObject.GetComponentInChildren<HpSliderScript>() != null)
                collision.gameObject.GetComponentInChildren<HpSliderScript>().UpdateHealthBar(); 

               
                if (destroySelfOnImpact)
              Destroy(gameObject, 0.1f);

            }
        }
    }

    private void OnParticleTrigger(Collider collision)
    {
        damageAmount = Random.Range(damageAmountMin, damageAmountMax); //randomize damage

        critRange = Random.Range(1, 100);

        //initialize crit mechanic
        if (critChance >= critRange)
        {
            isCrit = true;
            damageAmount = damageAmount * 2;
        }
        else
        {
            isCrit = false;
        }

        CombatTextManager.Instance.DamageToString(damageAmount.ToString());//using to show the damage

        if (collision.gameObject.CompareTag("Environment"))
        {
            if (destroySelfOnCol)
                Destroy(gameObject, 0.1f);

        }

        //create combat text on collison position. Creating text only if go colliding with Boss
        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BossHeal"))
        {
            if (!isCrit)
            {
                CombatTextManager.Instance.CreateText(transform.position, false);
            }
            else
            {
                CombatTextManager.Instance.CreateText(transform.position, true);
            }
        }

        if (damageOnTrigger)
        {
            if (collision.gameObject.GetComponent<Health>() != null)
            {   // if the hit object has the Health script on it, deal damage

                collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);

                //call updatehelthbar fuction from HpSliderScript
                if (collision.gameObject.GetComponentInChildren<HpSliderScript>() != null)
                    collision.gameObject.GetComponentInChildren<HpSliderScript>().UpdateHealthBar();


                if (destroySelfOnImpact)
                    Destroy(gameObject, 0.1f);

            }
        }
    }

}























//////all scripts in the project are done by Vladyslav Dziun (c) All rights reserved 2016