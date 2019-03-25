using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HpSliderScript : MonoBehaviour {

    Slider _slider;

	void Start ()
    {
        //rederence to ui element
        _slider = GetComponent<Slider>();

        //looking for health script on the gameobject where hpbar attached and set max slider value to hp value of the gameobject
        if (gameObject.transform.root.GetComponent<Health>() != null)
        {
            _slider.maxValue = gameObject.transform.root.GetComponent<Health>().healthPoints;
        }
        else if(gameObject.transform.parent.parent.GetComponent<Health>() != null)
        {
            _slider.maxValue = gameObject.transform.parent.parent.GetComponent<Health>().healthPoints;
        }
        else if(gameObject.GetComponent<Health>() != null)
        {
            _slider.maxValue = gameObject.GetComponent<Health>().healthPoints;
        }
        //call update hp bar function to update health when a scene loads
        UpdateHealthBar();

    }
	
	// Update is called once per frame
	void Update ()
    {
       

       
    }

    //update healthbar ui value depends on current gameobject's hp. We call this function in Damage script
    public void UpdateHealthBar()
    {
        if (gameObject.transform.root.GetComponent<Health>() != null)
        {
            _slider.value = gameObject.transform.root.GetComponent<Health>().healthPoints;

        }
        else if (gameObject.transform.parent.parent.GetComponent<Health>() != null)
        {
            _slider.value = gameObject.transform.parent.parent.GetComponent<Health>().healthPoints;

        }
        else if (gameObject.GetComponent<Health>() != null)
        {
            _slider.value = gameObject.GetComponent<Health>().healthPoints;
        }
    }
}
