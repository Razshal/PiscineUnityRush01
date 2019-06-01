using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUiScript : MonoBehaviour {
    public Slider lifeSlider;
    public Slider XpSlider;
    public Text lifeText;
    public Text xpText;
    public Text lvlText;

    // Enemy UI
    public Slider enemyLifeSlider;
    public Text enemyLifeText;
    public Text enemyName;
    public Text enemyLevel;
    private PlayerScript player;
    public GameObject enemyInfosPanel;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
	}
	
	void Update () {
        CharacterScript enemyToDisplay = null;

        // Determine if and wich enemy needs to be displayed
        if (player.enemyTarget)
            enemyToDisplay = player.enemyTarget.GetComponent<CharacterScript>();
        else if (player.enemyHover)
            enemyToDisplay = player.enemyHover.GetComponent<CharacterScript>();

        // Then if enemy, display his infos
        if (enemyToDisplay)
        {
            enemyInfosPanel.SetActive(true);
            enemyLifeSlider.maxValue = enemyToDisplay.maxLife;
            enemyLifeSlider.value = enemyToDisplay.life;
            enemyName.text = enemyToDisplay.displayName;
            lifeText.text = enemyToDisplay.life + "/" + enemyToDisplay.maxLife;
            enemyLevel.text = "LVL " + enemyToDisplay.level;
        }
        else
            enemyInfosPanel.SetActive(false);

        // Update player main UI
        XpSlider.value = player.experience;
        XpSlider.maxValue = player.requieredXp;
        xpText.text = player.experience + "/" + player.requieredXp;
        lifeSlider.value = player.life;
        lifeText.text = player.life + "/" + player.maxLife;
        lifeSlider.maxValue = player.maxLife;
        lvlText.text = "LVL " + player.level;
	}
}
