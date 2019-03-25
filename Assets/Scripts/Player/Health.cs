using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    [HideInInspector]
    public Stats stats; //stats script 
    public int healthPoints; //go health
    public bool isAlive = true; //checking if go is alive
    public bool ableDestroy = false;

   
	void Start ()
    {
      
       
        //if GO has stats script add stamina value to HP value
        if (!stats == false)
        {
            stats = gameObject.GetComponent<Stats>();

            healthPoints = healthPoints + stats.stamina; 
        }
	}
	
	void Update () {
        //if HP <0 GO died
        if (healthPoints <= 0) 
        {
            healthPoints = 0;
            isAlive = false;

            if (ableDestroy)
                Destroy(gameObject);
        }    
    }
    
    public void ApplyDamage(int damageAmount)
    {
        healthPoints = healthPoints -damageAmount;
       
    }

    public void ApplyHeal(int healAmount)
    {
        healthPoints = healthPoints + healAmount;
    }

}
