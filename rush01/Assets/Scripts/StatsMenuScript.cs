using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenuScript : MonoBehaviour {
    public PlayerScript playerScript;
    public Text playerName;
    public Text strength;
    public Text agility;
    public Text constitution;
    public Text minAttack;
    public Text maxAttack;
    public Text armor;
    public Text credits;
    public Text remainingXP;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindWithTag("Player")
                                 .GetComponent<PlayerScript>();
        playerName.text = playerScript.name;
        strength.text = "" + playerScript.strength;
        agility.text = "" + playerScript.agility;
        constitution.text = "" + playerScript.constitution;
        minAttack.text = "" + playerScript.minDamage;
        maxAttack.text = "" + playerScript.maxDamage;
        armor.text = "" + playerScript.armor;
        credits.text = "" + playerScript.money;
        remainingXP.text = "" + (playerScript.requieredXp - playerScript.experience);
	}
}
