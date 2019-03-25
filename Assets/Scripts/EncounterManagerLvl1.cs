using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class EncounterManagerLvl1 : MonoBehaviour {

    public GameObject Boss; //boss gameobject
    public GameObject player;
    public GameObject cinemaCamera;
    public int phaseTwoTransitionHp; //amount of the boss hp when starts phase 2
    public float phaseTwoTime; //duration pf phase two in seconds
    public Vector3 tarPos; //using to store boss Chaser script target
    public GameObject tarObj; //empy object where boss charge
    public GameObject spawner; //spawner go script
    public GameObject flySpawner; //spawner go script
    public Transform[] flies;
    public GameObject[] flyWeb;
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

    public bool isCharging; //idicate if boss using charge or not
    public bool isShooting; //idicate if boss shooting or not
    public bool isExplodeSpiders;
    public bool ateFly;
    public bool flySpawned;
    public bool eating = false;

    private bool isUsingSkill;

    private bool isPhaseTwo = false; //is phase 2 active
    private bool isPhaseThree = false; //is phase 3 active
    private float phaseTwoCd; //phase 2 inside cd
    private bool isDead = false; //is death animation player 
    bool instatiated = false;
    // is spider instantiated alreadt

    public int ran;

    private float distWebOne;
    private float distWebTwo;

    Animator _anim;
    Shooter_Boss _shooterBoss;
    Chaser _chaserBoss;
    LookAt _lookAtBoss;

    //private CameraShakeInstance myShake;
    private CinemachineCamShake cinemachineCameraShake;

    void Start ()
    {
        _anim = Boss.GetComponent<Animator>();
        _shooterBoss = Boss.GetComponent<Shooter_Boss>();
        _chaserBoss = Boss.GetComponent<Chaser>();
        _lookAtBoss = Boss.GetComponent<LookAt>();
        cinemachineCameraShake = cinemaCamera.GetComponent<CinemachineCamShake>();

        bossShootTime =3f; //delay for dialog in the beginning of level
        StartCoroutine("DialogDelay");

        //myShake = CameraShaker.Instance.StartShake(30, 10, 2);

        //If don't want your shake to be active immediately, have these lines:
        //myShake.StartFadeOut(0);
        //myShake.DeleteOnInactive = false;


    }

    // Update is called once per frame
    void Update ()
    {
        //print(ran);

        //*************************************************************************************************************
        //if victory disable everything
        if (GameManager.gm.isVictory == true && teleport.active == false)
        {
            StopAllCoroutines();
            bossShootTime = 999999;
            _chaserBoss.enabled = false;
            _shooterBoss.enabled = false;
            _lookAtBoss.enabled = false;
            Boss.GetComponent<Damage>().enabled = false;
            Boss.GetComponent<BossMeleeAttack>().enabled = false;
            Boss.transform.GetChild(1).gameObject.active = false;
            Boss.GetComponent<BoxCollider>().enabled = false;

            teleport.SetActive(true);

            if (isDead == false)
            {
                _anim.SetTrigger("Die");
                isDead = true;
            }          
        }

        if (GameManager.gm.isVictory == true)
        {
            _chaserBoss.enabled = false;
            _lookAtBoss.enabled = false;
        }

        //*************************************************************************************************************

        SpeedUpCd(); //run speedup cd function
        ChangeDummyTarget(); //change boss target to dummy to place an egg and change back to the player after egg has been placed
        SpawnExlosiveSpiders(); // if 2 eggs instantiated already spawn 2 spiders and make them run towards each egg

        //if there are two spiders has been spawned draw the line between them
        if (spider0 && spider1)
        SpiderWebLine();

        BossRunAnimation(); //If boss chasing something run running animation

        if (flyWeb[0].GetComponent<FlyWebScript>().inWeb == true || flyWeb[1].GetComponent<FlyWebScript>().inWeb == true && eating == false)
        {
            
            if (!isUsingSkill && !isShooting && !isCharging)
            {
                StartCoroutine("EatFly");
            }
        }

        distWebOne = Vector3.Distance(Boss.transform.position, flyWeb[0].transform.position);
        distWebTwo = Vector3.Distance(Boss.transform.position, flyWeb[1].transform.position);
        if (distWebOne <= 1.5f || distWebTwo <= 1.5f && eating == true)
        {
            
            _chaserBoss.enabled = true;
            _chaserBoss.target = player.transform;
            _chaserBoss.speed = 4;
            _lookAtBoss.enabled = true;
            _lookAtBoss.target = player.transform;
            bossShootTime = Time.timeSinceLevelLoad + bossShootCd;
            eating = false;
        }


        //if (distWebOne < 1 || distWebTwo < 1 && ateFly==false)
        //{
        //    ateFly = true;
        //    eating = false;
        //    _chaserBoss.enabled = true;
        //    _chaserBoss.target = GameObject.FindGameObjectWithTag("Player").transform;
        //    _chaserBoss.speed = 4;
        //    _lookAtBoss.enabled = true;

        //}


        ///PHASE 1**********************************************************************************
        if (tarObj != null) //if charge target exitst and distance between boss and target less than one - start stun courtine
        {
            if (Vector3.Distance(Boss.transform.position, tarPos) <= 1.3f)
            {
                _chaserBoss.enabled = false;
                StartCoroutine("Stun");
            }
        }

        UseBossSkills(); //use boss skills Shoot/Charge/PlaceEgg

        //if boss going to place an explosive egg - increase boss speed
        if (_chaserBoss.target == dummyTarget.transform)
            _chaserBoss.speed = 10;

        if (_chaserBoss.target == GameObject.Find("Player").transform)
        {
            if (!isCharging)
                _chaserBoss.speed = 4;
        }

        ///PHASE2**************************************************************************************

        //if boss' health less than pahseTwoTransistionHp - start phase 2
        if (Boss.GetComponent<Health>().healthPoints <= phaseTwoTransitionHp && isPhaseTwo== false)
        {
            //if (!isCharging)
            //{
            //    spawner.SetActive(true);//activate spawner

            //    if (_chaserBoss.enabled == false)
            //        _chaserBoss.enabled = true;

            //    if (dummyTarget2.active == false)
            //        dummyTarget2.active = true;

            //    dummyTarget2.transform.position = new Vector3(100, 0, 100); //place target outside the battlemap (boss gonna chase this target to dissaper in phase2)

            //    _chaserBoss.target = dummyTarget2.transform; //select targer dummy target
            //    _chaserBoss.speed = 7;
            //    Boss.GetComponent<LookAt>().target = dummyTarget2.transform;
            //    // Boss.transform.GetChild(2).gameObject.active = false; //disable throwing spider web
            //    _shooterBoss.enabled = false;
            //    bossShootTime = Time.timeSinceLevelLoad + 99999; //use this line to stop using Shoot/Charge/ And placing Egg while phase 2
            //    isPhaseTwo = true;
            //    print("phase 2");
            //}
        }

        //if (isPhaseTwo && !isPhaseThree) //counting phase 2 duration
        //    phaseTwoCd += 1 * Time.deltaTime;


        ///PHASE 3

        if (phaseTwoCd >= phaseTwoTime && isPhaseThree == false)
        {
            
               isPhaseThree = true;


            spawner.SetActive(false);

            bossShootTime = Time.timeSinceLevelLoad + 10; //start using Shoot/Charge/PlaceEgg again

            _chaserBoss.target = GameObject.Find("Player").transform; //select the Player as a target
            _lookAtBoss.target = GameObject.Find("Player").transform;
            _chaserBoss.speed = 3; //use standard speed
            Boss.transform.GetChild(1).gameObject.active = true; //activate throwing spider web
        }


    }

    IEnumerator Shoot()
    {
       
        if (!isCharging && !isUsingSkill)
        {
            //myShake.StartFadeIn(0.5f);
            //cinemachineCameraShaker.ShakeCamera(0.5f);
            cinemachineCameraShake.ShakeCinemachineCam(0.3f, 3f,2f);

            print("shoot");
            isShooting = true;
            isUsingSkill = true;
            bossShootTime = Time.timeSinceLevelLoad + bossShootCd;
            Boss.transform.GetChild(0).gameObject.active = false;
            _shooterBoss.enabled = true;
            _lookAtBoss.enabled = true;
            _lookAtBoss.target = player.transform;
            _chaserBoss.enabled = false;
            yield return new WaitForSeconds(6);
            Boss.transform.GetChild(0).gameObject.active = true;
            _shooterBoss.enabled = false;
            _chaserBoss.enabled = true;
            //myShake.StartFadeOut(0.5f);
            cinemachineCameraShake.ShakeCinemachineCam(0f, 0f, 0f);
            isShooting = false;
            isUsingSkill = false;
        }
    }

    IEnumerator SpawnFly()
    {
        bossShootTime = Time.timeSinceLevelLoad + bossShootCd;
            print("spawnfly");

            flySpawner.GetComponent<SpawnGameObjects>().enabled = true;
            yield return new WaitForSeconds(2.1f);
            flySpawner.GetComponent<SpawnGameObjects>().enabled = false;
            ateFly = false;
       
    }

    IEnumerator EatFly()
    {
        if(!isUsingSkill && !isCharging && !isShooting )
        {
            bossShootTime = Time.timeSinceLevelLoad + 99999999;
            eating = true;
            print("eatfly");        

            if (flyWeb[0].GetComponent<FlyWebScript>().inWeb == true)
            {
                _chaserBoss.target = flyWeb[0].transform;
                _lookAtBoss.target = flyWeb[0].transform;
            }
            else if (flyWeb[1].GetComponent<FlyWebScript>().inWeb == true)
            {
                _chaserBoss.target = flyWeb[1].transform;
                _lookAtBoss.target = flyWeb[1].transform;
            }
            if (flyWeb[0].GetComponent<FlyWebScript>().inWeb == true && flyWeb[1].GetComponent<FlyWebScript>().inWeb == true)
            {

                if (distWebOne < distWebTwo)
                {
                    _chaserBoss.target = flyWeb[0].transform;
                    _lookAtBoss.target = flyWeb[0].transform;
                }
                else if (distWebOne > distWebTwo)
                {
                    _chaserBoss.target = flyWeb[1].transform;
                    _lookAtBoss.target = flyWeb[1].transform;
                }
        }
        }
        yield return null;
    }

    IEnumerator Charge()
    {
        if(!isUsingSkill)
        { 
        print("charge");
        isCharging = true;
        isUsingSkill = true;

        bossShootTime = Time.timeSinceLevelLoad + bossShootCd;

                _shooterBoss.enabled = false;
                _chaserBoss.enabled = false;
                _lookAtBoss.enabled = false;
                _anim.SetTrigger("Charge");
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
                tarPos = player.transform.position;
                tarObj = new GameObject("tarObj");
                tarObj.transform.position = tarPos;
                _chaserBoss.target = tarObj.transform;
                 _chaserBoss.enabled = true;
            //myShake.StartFadeIn(0.5f);
            cinemachineCameraShake.ShakeCinemachineCam(0.3f, 1f, 2.0f);
            _chaserBoss.speed = 35;
                _anim.speed = 4f;
        }
    }

    IEnumerator Stun()
    {
        cinemachineCameraShake.ShakeCinemachineCam(0f, 0f, 0f);
        _anim.speed = 0.9f;
            _anim.SetBool("Stunned", true);
        Boss.transform.GetChild(0).gameObject.active = false;
            yield return new WaitForSeconds(2);
            Boss.transform.GetChild(0).gameObject.active = true;
            _anim.SetBool("Stunned", false);
            Destroy(tarObj);
        _chaserBoss.enabled = true;
       
        _chaserBoss.speed = 4;
        _lookAtBoss.enabled = true;
        if(!eating)
        {
            _lookAtBoss.target = player.transform;
            _chaserBoss.target = player.transform;
        }
        isUsingSkill = false;
        isCharging = false;
    }

    IEnumerator ExplodeSpiders()
    {
        if (!isShooting && !isCharging && !isUsingSkill)
        {

            bossShootTime = Time.timeSinceLevelLoad + bossShootCd+3;
            print("explode spiders");
            Vector3 spawnPosition;

            // get a random position between the specified ranges
            spawnPosition.x = Random.Range(xMinRangeSpawn, xMaxRangeSpawn);
            spawnPosition.y = Random.Range(yMinRangeSpawn, yMaxRangeSpawn);
            spawnPosition.z = Random.Range(zMinRangeSpawn, zMaxRangeSpawn);

            dummyTarget.active = true;
            dummyTarget.transform.position = spawnPosition;

            if (dummyTarget.active == true)
            {
                _chaserBoss.enabled = true;
               _chaserBoss.target = dummyTarget.transform;
                Boss.GetComponent<LookAt>().target = dummyTarget.transform;
            }

        }
        yield return null;
    }


    IEnumerator AddCollider()
    {
        yield return new WaitForSeconds(1.5f);
        if(spider0 && spider1)
        {
            spider0.GetComponent<BoxCollider>().enabled = true;
            spider1.GetComponent<BoxCollider>().enabled = true;
        }
    }

    IEnumerator DialogDelay()

    {
        yield return new WaitForSeconds(31);

            _chaserBoss.enabled = true;
            Boss.GetComponent<BoxCollider>().enabled = true;
            _lookAtBoss.enabled = true;
        Destroy(bossDialog, 1f);
    }


    void SpiderWebLine()
    {
        float distance = Vector3.Distance(exlosiveSpider[0].transform.position, exlosiveSpider[1].transform.position);

        spider0.GetComponent<LineRenderer>().SetPosition(0, spider0.transform.position);
        spider0.GetComponent<LineRenderer>().SetPosition(1, spider1.transform.position);

    }

    void SpeedUpCd()
    {
        if (speedUpTime > Time.timeSinceLevelLoad && isPhaseTwo && !isPhaseThree)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement3D>().speed = GameObject.Find("Player").GetComponent<PlayerMovement3D>().defaultSpeed + 2;
        }
        else if (speedUpTime < Time.timeSinceLevelLoad && isPhaseTwo && !isPhaseThree)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement3D>().speed = GameObject.Find("Player").GetComponent<PlayerMovement3D>().defaultSpeed;
        }
    }

    void ChangeDummyTarget()
    {
        if (dummyTarget.active == true)
        {
            if (Vector3.Distance(Boss.transform.position, dummyTarget.transform.position) <= 2)
            {
                GameObject cocoonObject = Instantiate(cocoonObj, Boss.transform.position, Boss.transform.rotation) as GameObject;
                cocoonObject.transform.parent = gameObject.transform;

                dummyTarget.active = false;
                _chaserBoss.target = GameObject.Find("Player").transform;
                _lookAtBoss.target = GameObject.Find("Player").transform;

            }
        }
    }

    void SpawnExlosiveSpiders()
    {
        _childCount = gameObject.transform.childCount; //counting explosive eggs

        if (_childCount != 0)
        {
            if ((_childCount) % 2 == 0 && instatiated == false)
            {
                spider0 = Instantiate(exlosiveSpider[0], gameObject.transform.GetChild(_childCount - 2).transform.position, gameObject.transform.GetChild(_childCount - 2).transform.rotation) as GameObject;
                spider1 = Instantiate(exlosiveSpider[1], gameObject.transform.GetChild(_childCount - 1).transform.position, gameObject.transform.GetChild(_childCount - 1).transform.rotation) as GameObject;

                StartCoroutine("AddCollider");

                spider0.GetComponent<Chaser>().target = gameObject.transform.GetChild(_childCount - 1);
                spider1.GetComponent<Chaser>().target = gameObject.transform.GetChild(_childCount - 2);

                instatiated = true;
            }

            else if (_childCount % 2 != 0)
            {
                instatiated = false;
            }
        }
    }

    void UseBossSkills()
    {
        if (bossShootTime <= Time.timeSinceLevelLoad)
        {
            print("random");
            ran = Random.Range(0, 100);

            if (ran >= 1 && ran <= 20)
            {
               // StopAllCoroutines();
                StartCoroutine("Shoot");
            }
            else if (ran >= 60)
            {
               // StopAllCoroutines();
                StartCoroutine("ExplodeSpiders");
            }
            else if (ran > 20 && ran < 60)
            {
               // StopAllCoroutines();
                StartCoroutine("Charge");
            }
           
            if (ran > 90 && flyWeb[0].transform.childCount == 0 && flyWeb[1].transform.childCount == 0)
            {
                StartCoroutine("SpawnFly");
            }
        }
    }

    void BossRunAnimation()
    {
        if (_chaserBoss.enabled == true)
        {
            _anim.SetBool("Running", true);
            //CameraShaker.Instance.ShakeOnce(2, 2, 0, 0.5f);
            //cinemachineCameraShake.ShakeCinemachineCam(0.3f, 1f, 2.0f);

}
        else
        {
            _anim.SetBool("Running", false);
        }
            
    }

    public void StartSpeedUpCd()
    {
        speedUpTime = 5 + Time.timeSinceLevelLoad;
    }
}

