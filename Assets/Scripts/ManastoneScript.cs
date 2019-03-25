using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManastoneScript : MonoBehaviour {

    public string stoneColor;
   



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (stoneColor)
            {
                case "blue":
                    other.gameObject.GetComponent<CountManaStones>().blueCount += 1;
                    other.gameObject.GetComponent<CountManaStones>().UpdateManastonesText();
                    Destroy(gameObject);
                    break;

                case "red":
                    other.gameObject.GetComponent<CountManaStones>().redCount += 1;
                    other.gameObject.GetComponent<CountManaStones>().UpdateManastonesText();
                    Destroy(gameObject);
                    break;

                case "purple":
                    other.gameObject.GetComponent<CountManaStones>().purpleCount += 1;
                    other.gameObject.GetComponent<CountManaStones>().UpdateManastonesText();
                    Destroy(gameObject);
                    break;
            }

            PlayerPrefs.SetInt("blueStonesPref", other.gameObject.GetComponent<CountManaStones>().blueCount);
            PlayerPrefs.SetInt("redStonesPref", other.gameObject.GetComponent<CountManaStones>().redCount);
            PlayerPrefs.SetInt("purpleStonesPref", other.gameObject.GetComponent<CountManaStones>().purpleCount);

            PlayerPrefs.Save();
        }
    }
}
