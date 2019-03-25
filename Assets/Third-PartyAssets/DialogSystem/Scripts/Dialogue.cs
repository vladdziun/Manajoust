using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Dialogue : MonoBehaviour
{
    private Text _textComponent;

    public string[] DialogueStrings;

    public float SecondsBetweenCharacters = 0.15f;
    public float CharacterRateMultiplier = 0.5f;

    public KeyCode DialogueInput;

    public bool isPlayOnTrigger;
    public bool isAutoPlay;
    public bool isPlayOnStart;
    public float autoPlayDelay = 1.5f;

    private bool _isStringBeingRevealed = false;
    public bool _isDialoguePlaying = false;
    public bool _isEndOfDialogue = false;


	// Use this for initialization
	void Start ()
	{
	    _textComponent = GetComponent<Text>();
	    _textComponent.text = "";

        if(isPlayOnStart)
        {
            StartCoroutine(StartDialogue());
        }

        
	}
	
	// Update is called once per frame
	void Update () 
	{
	    if (Input.GetKeyDown(DialogueInput) && gameObject.GetComponentInParent<CanvasGroup>().alpha == 1)
	    {
	        if (!_isDialoguePlaying)
	        {
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
            }
	        
	    }
      
	}


    private IEnumerator StartDialogue()
    {
        int dialogueLength = DialogueStrings.Length;
        int currentDialogueIndex = 0;

        while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
        {
            if (!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueStrings[currentDialogueIndex++]));

                if (currentDialogueIndex >= dialogueLength)
                {
                    _isEndOfDialogue = true;
                    gameObject.GetComponentInParent<CanvasGroup>().alpha = 0; //close dialog panel after the last string
                   
                }
            }

            yield return 0;
        }

        while (true && gameObject.GetComponentInParent<CanvasGroup>().alpha != 0)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

       
       // _isEndOfDialogue = false;
        _isDialoguePlaying = false;

        //if full dialog displayed disable dialog GameObject
        if (currentDialogueIndex >= dialogueLength)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        

        _textComponent.text = "";

        while (currentCharacterIndex < stringLength)
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;

            if (currentCharacterIndex < stringLength)
            {
                if (Input.GetKey(DialogueInput))
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters*0.1f); //how fast dialog appers when hold LMB
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);
                }
            }
            else
            {
                break;
            }
        }

       
        while (true)
        {
            if (Input.GetKeyDown(DialogueInput) && gameObject.GetComponentInParent<CanvasGroup>().alpha != 0) //go to next replic
            {
                break;
            }
            else if(isAutoPlay)
            {
                yield return new WaitForSeconds(autoPlayDelay);
                break;
            }

            yield return 0;
        }

     

        _isStringBeingRevealed = false;
        _textComponent.text = "";
    }

    //if idsplayontrigger on, show dialog windown when player step in trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isPlayOnTrigger)
        {
           // gameObject.GetComponentInParent<CanvasGroup>().alpha = 1;
            if (!_isDialoguePlaying)
            {
               gameObject.GetComponentInParent<CanvasGroup>().alpha = 1;
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
            }
            else if(_isDialoguePlaying)
            {
                gameObject.GetComponentInParent<CanvasGroup>().alpha = 1;
            }
        }
    }

    //disable dialog cloud if player head out from the NPC
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isPlayOnTrigger)
        {

                gameObject.GetComponentInParent<CanvasGroup>().alpha = 0;
           

            
        }
    }

}
