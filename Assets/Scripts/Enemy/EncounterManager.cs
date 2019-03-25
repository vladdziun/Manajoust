using UnityEngine;
using System.Collections;

public class EncounterManager : MonoBehaviour {

    public GameObject Boss;
    public GameObject addOne;
    public GameObject Player;
    public GameObject Spawner;

    private float phaseOneTime = 60; //duration of the first Fase

    Chaser _chaser;
    GameObject _bossHeal;
    Animator _anim;
    SpawnGameObjects[] _spawner;


    private float secondsBetweenSpawning;
    private float nextSpawnTime;
    // Use this for initialization
    void Start ()
    {
        _chaser = addOne.GetComponent<Chaser>();
        _anim = addOne.GetComponent<Animator>();
        _spawner = Spawner.GetComponents<SpawnGameObjects>();

        secondsBetweenSpawning = Random.Range(15, 25);
        StartCoroutine("Spray");
    }
	
	// Update is called once per frame
	void Update () {

        _bossHeal = GameObject.FindGameObjectWithTag("BossHeal");
        if (_bossHeal == null)
        {
            _chaser.target = Player.transform;
        }
        else
        {
            StartCoroutine("ChangeTarget");

        }

        if (addOne.GetComponentInChildren<Shooter_Boss>().enabled == true)
        {
            addOne.GetComponent<Chaser>().enabled = false;
            addOne.GetComponent<LookAt>().enabled = true;

            //addOne.GetComponentInChildren<Shooter_Boss>().enabled = true;   
        }
        else
        {
            addOne.GetComponent<LookAt>().enabled = false;
        }


        //////Phase1
        
        if(phaseOneTime > 0)
        {
            phaseOneTime -= Time.deltaTime;

            _spawner[0].enabled = false;
            _spawner[1].enabled = false;
            Boss.transform.GetChild(1).GetComponent<ThrowingGameObjectsScript>().enabled = false;
            Boss.transform.GetChild(2).gameObject.active = false;
        }
        

        ///Phase2
        if (phaseOneTime < 0)
        {
            _spawner[0].enabled = true;
            _spawner[1].enabled = true;
            _spawner[2].enabled = false;
            _spawner[3].enabled = false;
            Boss.transform.GetChild(1).GetComponent<ThrowingGameObjectsScript>().enabled = true;

            addOne.SetActive(true);
        }

        ///Phase3
        if(addOne.GetComponent<Health>().healthPoints <= 0)
        {
            addOne.SetActive(false);
            Boss.transform.GetChild(1).GetComponent<ThrowingGameObjectsScript>().enabled = false;
            Boss.transform.GetChild(2).gameObject.active = true;
            Boss.GetComponent<Enemy_WaypointMovement>().enabled = true;
        }
            
    }

    IEnumerator Spray()
    {
        _anim.SetTrigger("Spray");
        yield return new WaitForSeconds(2);
        addOne.GetComponent<SphereCollider>().material.bounciness = 0;
        addOne.GetComponentInChildren<Shooter_Boss>().enabled = true;
        yield return new WaitForSeconds(8);
        addOne.GetComponentInChildren<Shooter_Boss>().enabled = false;
        addOne.GetComponent<SphereCollider>().material.bounciness = 1;
        addOne.GetComponent<Rigidbody>().AddForce(transform.up * 10, ForceMode.VelocityChange);
        addOne.GetComponent<Chaser>().enabled = true;
        yield return new WaitForSeconds(15);
        yield return StartCoroutine("Spray");
    }

    IEnumerator ChangeTarget()
    {
        yield return new WaitForSeconds(4);
      if(_bossHeal != null)
        _chaser.target = _bossHeal.transform;
    }
}

