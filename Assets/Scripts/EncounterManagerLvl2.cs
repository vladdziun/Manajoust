using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class EncounterManagerLvl2 : MonoBehaviour {

    public GameObject player;
    public GameObject Boss;//boss gameobject
    public GameObject shooterPivot;
    public GameObject tail;

    private bool isBreathing;

    private CameraShakeInstance myShake;


    public int phaseTwoTransitionHp; //amount of the boss hp when starts phase 2
    public float phaseTwoTime; //duration pf phase two in seconds
    public Vector3 tarPos; //using to store boss Chaser script target
    public GameObject tarObj; //empy object where boss charge
    public GameObject spawner; //spawner go script
    public GameObject teleport; //teleport go
    public GameObject bossDialog;

    public GameObject[] exlosiveSpider; //exlosive spiders prefabs
    public GameObject dummyTarget; //empty gameoject target for boss to place an explosive egg
    public GameObject dummyTarget2; //boss target to go outside the map
    public GameObject cocoonObj; //explosive cocoon (egg)
    GameObject spider0; //explosive spider 1
    GameObject spider1; //explosive spider 2
    public int _childCount; //counting eggs on map under encountermanager

    public float xMinRangeSpawn = -25.0f; //min and max coords to spawn an egg for spider
    public float xMaxRangeSpawn = 25.0f;
    public float yMinRangeSpawn = 8.0f;
    public float yMaxRangeSpawn = 25.0f;
    public float zMinRangeSpawn = -25.0f;
    public float zMaxRangeSpawn = 25.0f;


    public float bossShootCd; //time between boss' skills Shoot/Charge/Egg
    private float bossShootTime; //time before boss use skill after scene load (внутрниий кд)
    public float speedUpDuration; //speed up duration in seconds in Phase 2
    private float speedUpTime; //inside cooldown for speedup

    private bool isCharging; //idicate if boss using charge or not
    private bool isShooting; //idicate if boss shooting or not
    private bool isPhaseTwo = false; //is phase 2 active
    private bool isPhaseThree = false; //is phase 3 active
    private float phaseTwoCd; //phase 2 inside cd
    private bool isDead = false; //is death animation player 
    bool instatiated = false; // is spider instantiated alreadt

    Animator _anim;
    Shooter_Boss _shooterBoss;
    Chaser _chaserBoss;
    LookAt _lookAtBoss;

    void Start ()
    {
        _anim = Boss.GetComponent<Animator>();
        _shooterBoss = Boss.GetComponent<Shooter_Boss>();
        _chaserBoss = Boss.GetComponent<Chaser>();
        _lookAtBoss = Boss.GetComponent<LookAt>();


        myShake = CameraShaker.Instance.StartShake(15,25,2);

        //If don't want your shake to be active immediately, have these lines:
        myShake.StartFadeOut(0);
        myShake.DeleteOnInactive = false;

        bossShootTime = 1f; //delay for dialog in the beginning of level

        StartCoroutine("DialogDelay"); 

}

    // Update is called once per frame
    void Update()
    {
        UseBossSkills();
            
    }

    IEnumerator Breathe()
    {
        print("breathe");
        bossShootTime = Time.timeSinceLevelLoad + bossShootCd;
        shooterPivot.SetActive(true);
        // CameraShaker.Instance.ShakeOnce(15, 25, 2, 20);

        myShake.StartFadeIn(2);
        yield return new WaitForSeconds(7);
        myShake.StartFadeOut(3);
        shooterPivot.SetActive(false);
    }

    IEnumerator TailAttack()
    {
        bossShootTime = Time.timeSinceLevelLoad + bossShootCd;

        tarObj = new GameObject("tarObj");
        tarObj.transform.position = player.transform.position;
        tail.transform.position = new Vector3 (tarObj.transform.position.x, tail.transform.position.y, tarObj.transform.position.z);
        tail.GetComponent<Rigidbody>().isKinematic = false;
        tail.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
        print("tailattack");
        return null;
    }

    void UseBossSkills()
    {
        if (bossShootTime <= Time.timeSinceLevelLoad)
        {
            int ran = Random.Range(0, 100);

            if (ran >= 99 && ran <= 100)
            {
                StopAllCoroutines();
                StartCoroutine("Breathe");
            }
            else if (ran >= 1 && ran <= 100)
                {
                    StopAllCoroutines();
                    StartCoroutine("TailAttack");
                }
        }
    }
}

