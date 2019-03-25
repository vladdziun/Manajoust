using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountManaStones : MonoBehaviour {


    public int blueCount;
    public int redCount;
    public int purpleCount;

    public Text blueText;
    public Text redText;
    public Text purpleText;

    void Start () {
        blueCount = PlayerPrefs.GetInt("blueStonesPref");
        redCount = PlayerPrefs.GetInt("redStonesPref");
        purpleCount = PlayerPrefs.GetInt("purpleStonesPref");
        UpdateManastonesText();

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void UpdateManastonesText()
    {
        blueText.text = blueCount.ToString();
        redText.text = redCount.ToString();
        purpleText.text = purpleCount.ToString();

    }
}
