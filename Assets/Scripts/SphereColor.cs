using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereColor : MonoBehaviour
{

    public int color;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (color)
        {
            case 1:
                gameObject.transform.GetComponent<Image>().color = Color.red;
                break;
            case 2:
                gameObject.transform.GetComponent<Image>().color = Color.blue;
                break;
            case 3:
                gameObject.transform.GetComponent<Image>().color = Color.cyan;
                break;

        }

        if (color > 3)
            color = 1;
    }
}
