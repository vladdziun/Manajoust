using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    public int stamina = 1;
    public int haste = 1;
    public int castSpeed = 1;
    public int spellSpeed = 1;
    public int critChance = 1;

    public static Stats playerStats;


    void Start()
    {
        playerStats = gameObject.GetComponent<Stats>();
    }

}
