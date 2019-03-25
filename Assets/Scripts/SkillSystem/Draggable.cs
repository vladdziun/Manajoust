using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public Transform parentToReturnTo = null;

    public GameObject skillBar;
    public GameObject actionSkillBar;
    public GameObject skillBookBar;
    public string curParentName;
    public bool dragged;

    public Transform placeholderParent = null;

	GameObject placeholder = null;
    void Start()
    {
        
        curParentName = this.transform.parent.name;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.DeleteAll();
        
    }

    public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("OnBeginDrag");

        placeholder = new GameObject();
        placeholder.AddComponent<UseSkills>();
        placeholder.AddComponent<SwitchSkills>();
        placeholder.GetComponent<SwitchSkills>().spellButton = "InactiveButton";
        placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex( this.transform.GetSiblingIndex() );
		
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		this.transform.SetParent( this.transform.parent.parent );
		
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	
	public void OnDrag(PointerEventData eventData) {
        //Debug.Log ("OnDrag");

        skillBar.GetComponent<SwitchAbilitiesScript>().countChildren();
		
		this.transform.position = eventData.position;

		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);

		int newSiblingIndex = placeholderParent.childCount;

		for(int i=0; i < placeholderParent.childCount; i++) {
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x) {

				newSiblingIndex = i;

				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;

				break;
			}
		}

		placeholder.transform.SetSiblingIndex(newSiblingIndex);

	}
	
	public void OnEndDrag(PointerEventData eventData) {
        skillBar.GetComponent<SwitchAbilitiesScript>().countChildren();
        Debug.Log ("OnEndDrag");
        this.transform.SetParent( parentToReturnTo );
		this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
		GetComponent<CanvasGroup>().blocksRaycasts = true;
      // curParentName = this.transform.parent.name;
        dragged = true;
        // skillBar.GetComponent<MainSkillUsing>().AssignButtons();
      
       // skillBar.GetComponent<MainSkillUsing>().SaveSkillbar();
        actionSkillBar.GetComponent<ActionSkillUsing>().SaveSkillbar();

        Destroy(placeholder);
        //PlayerPrefs.SetString(curParentName, this.transform.parent.name);
       // Debug.Log(this.transform.parent.name);
       // PlayerPrefs.Save();
    }
	
	
	
}
