using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;public class PlayerScript : CharacterScript{    private RaycastHit clickHit;    new private Camera camera;    private int frameCount;

    // Player ui references
    public Slider lifeSlider;    public Slider XpSlider;    public Text lifeText;    public Text xpText;    public Text lvlText;    public GameObject enemyInfosPanel;    public Slider enemyLifeSlider;    public Text enemyName;    public Text enemyLevel;    public GameObject enemyHover;    public GameObject statsUI;    public int skillPoints;    public ParticleSystem levelup;    new void Start()    {        base.Start();        camera = Camera.main;        displayName = "Maya";        experience = 0;    }    private void OnTriggerStay(Collider other)
    {
        if (state != State.ATTACKING            && !enemyTarget            && !other.isTrigger            && other.gameObject.CompareTag("Enemy")
            && other.gameObject.GetComponent<CharacterScript>().state != State.DEAD            && !prioritaryWaypoint)        {
            enemyTarget = other.gameObject;
        }
    }    public void ReceiveExperience(int newXp)    {        experience += newXp;        if (experience >= requieredXp)        {            experience -= requieredXp;            requieredXp += 150;            level += 1;            life = maxLife;            skillPoints += 5;            levelup.time = 0;            levelup.Play();        }    }    private void UpdateUi()    {        CharacterScript enemyToDisplay = null;

        // Determine wich value needs to be displayed
        if (enemyTarget)            enemyToDisplay = enemyTarget.GetComponent<CharacterScript>();        else if (enemyHover)            enemyToDisplay = enemyHover.GetComponent<CharacterScript>();                // Then if enemy, display his infos        if (enemyToDisplay)        {
            enemyInfosPanel.SetActive(true);
            enemyLifeSlider.maxValue = enemyToDisplay.maxLife;            enemyLifeSlider.value = enemyToDisplay.life;            enemyName.text = enemyToDisplay.displayName;            enemyLevel.text = "LVL " + enemyToDisplay.level;
        }        else            enemyInfosPanel.SetActive(false);        // Update the player ones        XpSlider.value = experience;		XpSlider.maxValue = requieredXp;        xpText.text = experience + "/" + requieredXp;        lvlText.text = "LVL " + level;        lifeSlider.value = life;        lifeText.text = life + "/" + lifeSlider.maxValue;        lifeSlider.maxValue = maxLife;    }    public void OpenStats()    {        statsUI.SetActive(!statsUI.activeSelf);    }    public void AddAgility()    {        agility++;    }    public void AddStrength()    {        strength++;        minDamage = strength / 2;        maxDamage = minDamage + 4;    }    public void AddConst()    {        strength++;        maxLife = constitution * 5;    }    new void Update()    {        base.Update();

        // Updating UI
        UpdateUi();

        // Sets player click movement instructions
        if (Input.GetMouseButtonDown(0)
            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit)
            && !clickHit.collider.gameObject.CompareTag("Enemy"))
        {
            navMeshAgent.SetDestination(clickHit.point);
            prioritaryWaypoint = true;
            enemyTarget = null;
        }        if (Input.GetKeyDown(KeyCode.C))            OpenStats();    }}