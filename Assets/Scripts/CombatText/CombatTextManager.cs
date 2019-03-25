using UnityEngine;
using System.Collections;

public class CombatTextManager : MonoBehaviour
{

    private static CombatTextManager instance;
   // private static Damage damage;
   // public GameObject playerBullet;

    public GameObject textPrefab;
    public float speed;
    public Vector3 direction;
    public float fadeTime;


    void Start()
    {
        //damage = playerBullet.GetComponent<Damage>();
        
    }

  

    public static CombatTextManager Instance
    {
        get
        { 
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
                
            }
            return instance;
        }
    }

    //using to spawn combat text
    public void CreateText(Vector3 position, bool crit)
    {
        GameObject sct = (GameObject)Instantiate(textPrefab, position, Quaternion.identity);
        sct.GetComponent<CombatText>().Initialize(speed, direction, fadeTime,crit);
       
        //textPrefab.GetComponent<TextMesh>().text = damage.damageAmount.ToString();
        

    }

    public void DamageToString(string damageAmount)
    {
        textPrefab.GetComponent<TextMesh>().text = damageAmount;
    }
	
	
}
