using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenuScript : MonoBehaviour
{
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
    public Text pointsToSpend;
    public GameObject skillsAvailables;
    public GameObject[] increaseStats;

    public void RefreshButtons()
    {
        foreach (GameObject button in increaseStats)
            button.SetActive(playerScript.statsPoints > 0);
        skillsAvailables.SetActive(playerScript.statsPoints > 0);
    }

    private void OnEnable()
    {
        RefreshButtons();
    }

    // Use this for initialization
    void Update()
    {
        playerScript = GameObject.FindWithTag("Player")
                                 .GetComponent<PlayerScript>();
        playerName.text = playerScript.displayName;
        strength.text = "" + playerScript.strength;
        agility.text = "" + playerScript.agility;
        constitution.text = "" + playerScript.constitution;
        minAttack.text = "" + playerScript.minDamage;
        maxAttack.text = "" + playerScript.maxDamage;
        armor.text = "" + playerScript.armor;
        credits.text = "" + playerScript.money;
        remainingXP.text = "" + (playerScript.requieredXp - playerScript.experience);
        pointsToSpend.text = playerScript.statsPoints + " Points available";
    }
}
