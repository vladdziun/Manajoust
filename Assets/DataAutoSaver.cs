using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAutoSaver : MonoBehaviour {

    public float autoSaveFrequency;

    private float autoSaveCD;
    private Inventory _inventory;

    void Start () {
      _inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
        autoSaveCD -= Time.deltaTime;

        if(autoSaveCD <= 0)
        {
            _inventory.SaveDataOnline();

            autoSaveCD = autoSaveFrequency;
        }
	}
}
