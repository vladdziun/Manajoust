using UnityEngine;
using System.Collections;
using EZCameraShake;

public class BossBulletScript : MonoBehaviour {

    public GameObject voidZone;

    Animator _anim;
    private CameraShakeInstance myShake;

    void Start()
    {
        _anim = Camera.main.GetComponent<Animator>();

        //myShake = CameraShaker.Instance.StartShake(30, 10, 2);

        //If don't want your shake to be active immediately, have these lines:
        //myShake.StartFadeOut(0);
        //myShake.DeleteOnInactive = false;
    }

	// Update is called once per frame
	void Update () {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            //CameraShaker.Instance.ShakeOnce(30, 10, 0, 1);

            Instantiate(voidZone, transform.position, transform.rotation);
        }
    }
}
