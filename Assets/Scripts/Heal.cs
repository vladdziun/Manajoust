using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

    public int healAmount;

    public bool healOnTrigger = true;
    public bool destroySelfOnImpact = false;
    public bool destroySelfOnCol;
    public bool destroyParentOnCol;

    void Start()
    {


    }


    void OnTriggerEnter(Collider collision)
    {

        if (healOnTrigger)
        {
            if (collision.gameObject.GetComponent<Health>() != null)
            {   // if the hit object has the Health script on it, deal damage

                collision.gameObject.GetComponent<Health>().ApplyHeal(healAmount);


                if (destroySelfOnImpact)
                    Destroy(gameObject, 0.1f);

                if(destroyParentOnCol)
                {
                    transform.DetachChildren();
                    Destroy(gameObject.transform.parent.gameObject, 0.1f);
                }
                    


            }
        }
    }
}
