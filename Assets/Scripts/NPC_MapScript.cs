using UnityEngine;
using System.Collections;

public class NPC_MapScript : MonoBehaviour
{
    public GameObject map;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                map.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            map.SetActive(false);
        }
    }

    public void CloseMap()
    {
        map.SetActive(false);
    }
}