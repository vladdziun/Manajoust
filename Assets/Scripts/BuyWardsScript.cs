using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyWardsScript : MonoBehaviour {

    public int blueStonePrice;
    public int redStonePrice;
    public int purpleStonePrice;

    public GameObject parentObject;
    public GameObject anotherObject;
    public GameObject skillBokk;

    GameObject _player;

    void Awake ()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        //if (gameObject.transform.parent.name == "SkillBookBar")
        //{
        //    gameObject.GetComponent<Draggable>().enabled = false;
        //}
        //else
        //{
        //    gameObject.GetComponent<Draggable>().enabled = true;
        //}

    }
	
	// Update is called once per frame
	void Update () {
        //if (gameObject.transform.parent.name != "SkillBookBar")
        //{
        //    gameObject.GetComponent<Draggable>().enabled = true;
        //}
    }

    public void BuyWard()
    {
        //if(gameObject.transform.parent.name == "SkillBookBar" && gameObject.transform.GetChild(0).GetComponent<Image>().enabled == false)
        // {
        //     if (blueStonePrice <= _player.GetComponent<CountManaStones>().blueCount &&
        //        redStonePrice <= _player.GetComponent<CountManaStones>().redCount &&
        //        purpleStonePrice <= _player.GetComponent<CountManaStones>().purpleCount)
        //     {
        //         gameObject.GetComponent<Draggable>().enabled = true;
        //         gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
        //         _player.GetComponent<CountManaStones>().blueCount = _player.GetComponent<CountManaStones>().blueCount - blueStonePrice;
        //         _player.GetComponent<CountManaStones>().redCount = _player.GetComponent<CountManaStones>().redCount - redStonePrice;
        //         _player.GetComponent<CountManaStones>().purpleCount = _player.GetComponent<CountManaStones>().purpleCount - purpleStonePrice;
        //         _player.GetComponent<CountManaStones>().UpdateManastonesText();
        //     }

        // }
        //else
        // {
        //     gameObject.GetComponent<Draggable>().enabled = true;
        // }

        anotherObject.transform.Find(gameObject.name).gameObject.transform.SetParent(parentObject.transform);
        skillBokk.GetComponent<MainSkillUsing>().SaveSkillbar();
        //actionSkillBar.GetComponent<ActionSkillUsing>().SaveSkillbar();
    }
}
