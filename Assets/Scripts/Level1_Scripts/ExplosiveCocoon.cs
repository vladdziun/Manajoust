using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCocoon : MonoBehaviour {

    public ParticleSystem explodeParticle;

    Animator _anim;
	// Use this for initialization
	void Start ()
    {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Spider_Explosive(Clone)")
        {
            _anim.SetTrigger("Explode");
            
            int _childNumber;

            _childNumber = other.gameObject.transform.childCount;

            for(int i = 0; i <= _childNumber-1; i++)
            {
                other.gameObject.transform.GetChild(i).gameObject.active = false;
            }
        }
    }

    public void Explode()
    {
        Instantiate(explodeParticle, transform.position, transform.rotation);
        gameObject.transform.GetChild(0).gameObject.active = true;
        //Camera.main.GetComponent<Animator>().SetTrigger("Shaker");
        Destroy(gameObject,0.2f);
    }

}
