using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpScript : MonoBehaviour
{

    EncounterManagerLvl1 _encManager;

    GameObject _Player;
    PlayerMovement3D _playerMov;


    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _playerMov = _Player.GetComponent<PlayerMovement3D>();

       _encManager =  GameObject.Find("EncounterManager").GetComponent<EncounterManagerLvl1>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            print("YOYOYO");

            _encManager.StartSpeedUpCd();

            Destroy(gameObject);

        }
    }
}



