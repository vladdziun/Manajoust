using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPC_Trainer : MonoBehaviour {

   public GridLayoutGroup canvas;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                canvas.GetComponent<CanvasGroup>().alpha = 1;
                canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.GetComponent<CanvasGroup>().alpha = 0;
            canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

  
}
