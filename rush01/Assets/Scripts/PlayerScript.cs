﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : CharacterScript
{
    public GameObject rightHand;
    public GameObject leftHand;
    public UiHidable inventory;
    public UiHidable characterSystem;
    private GameObject _weapon;
    private SpellManager spellManager;

    private RaycastHit clickHit;
    private Camera cameraLUL;
    private int frameCount;

    // Player ui references
    public GameObject enemyHover;
    public GameObject statsUI;
    public int statsPoints;
    public int skillPoints;
    public ParticleSystem levelup;
    public GameObject skillsAvailables;
    public float radiusDrop = 1.0f;
    public GameObject deathText;

    public static PlayerScript ActivePlayer()
    {
        return GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    //Event Function
    public void OnEnable()
    {
        UiSlot.OnWeaponEquip += EquipWeapon;
        UiSlot.OnWeaponUnEquip += UnEquipWeapon;
        UiSlot.OnConsumableUse += ConsumableUse;
    }

    public void OnDisable()
    {
        UiSlot.OnWeaponEquip -= EquipWeapon;
        UiSlot.OnWeaponUnEquip -= UnEquipWeapon;
        UiSlot.OnConsumableUse -= ConsumableUse;
    }

    void EquipWeapon(GameObject item, Rarity rarity)
    {
        Debug.Log("EquipWeapon");
        _weapon = Instantiate(item, rightHand.transform);
        _weapon.GetComponent<Weapon>().setRarity(rarity);
    }

    void UnEquipWeapon(GameObject item)
    {
        Debug.Log("UnEquipWeapon");
        if (_weapon)
            Destroy(_weapon);
    }

    void ConsumableUse(Consumable item)
    {
        if (item.regenHp > 0)
            ReceiveLife(item.regenHp);
    }
    //End Event Function

    private void Awake()
    {
        LootSpawner.Instance.generateLoot(transform, 10.0f);
    }

    new void Start()
    {
        base.Start();
        cameraLUL = Camera.main;
        displayName = "Maya";
        experience = 0;
        spellManager = GameObject.Find("GameManager").GetComponent<SpellManager>();
        
    }

    private void OnTriggerStay(Collider other)
    {
        // Enemy detection if player does not already launched an instruction
        if (state != State.ATTACKING
            && !enemyTarget
            && !other.isTrigger
            && other.gameObject.CompareTag("Enemy")
            && other.gameObject.GetComponent<CharacterScript>().state != State.DEAD
            && !prioritaryWaypoint)
        {
            enemyTarget = other.gameObject;
        }
    }

    private void Drop()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusDrop, LayerMask.GetMask("Loot"));
        if (hitColliders.Length > 0)
        {
            ItemPhysic ip = hitColliders[0].GetComponent<ItemPhysic>();
            if (Inventory.Instance.leftPlace())
                Inventory.Instance.addItem(ConvertItem.Instance.ConvertToItemIcon(ip));
        }
    }

    public void ReceiveExperience(int newXp)
    {
        experience += newXp;
        if (experience >= requieredXp)
        {
            experience -= requieredXp;
            requieredXp += 150;
            level += 1;
            life = maxLife;
            statsPoints += 5;
            skillPoints += 1;
            skillsAvailables.SetActive(true);

            // LevelUp particle
            levelup.time = 0;
            levelup.Play();
        }
    }

    public void OpenStats()
    {
        statsUI.SetActive(!statsUI.activeSelf);
    }

    private void AddStat()
    {
        statsPoints--;
        ComputeStats();
    }

    // Public setters for UI buttons
    public void AddAgility()
    {
        agility++;
        AddStat();
    }

    public void AddStrength()
    {
        strength++;
        AddStat();
    }

    public void AddConst()
    {
        constitution++;
        AddStat();
    }

    new void Update()
    {
        base.Update();


        if (state != State.DEAD)
        {
            // Sets player click movement instructions
            if (Input.GetMouseButtonDown(0)
                && Physics.Raycast(cameraLUL.ScreenPointToRay(Input.mousePosition), out clickHit)
                && !clickHit.collider.gameObject.CompareTag("Enemy")
                && !statsUI.activeSelf)
            {
                
                navMeshAgent.SetDestination(clickHit.point);
                prioritaryWaypoint = true;
                enemyTarget = null;
            }
            if (Input.GetKeyDown(KeyCode.C))
                OpenStats();
            if (Input.GetKeyDown(KeyCode.P))
                ReceiveExperience(level * 150);
            if (Input.GetKeyDown(KeyCode.E))
                Drop();
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventory.isHided())
                    inventory.Show();
                else
                    inventory.Hide();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (characterSystem.isHided())
                    characterSystem.Show();
                else
                    characterSystem.Hide();
            }
        }
        else
            deathText.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            LaunchSpell(SkillBar.Instance.getSpell(0));
        if (Input.GetKeyDown(KeyCode.Alpha2))
            LaunchSpell(SkillBar.Instance.getSpell(1));
        if (Input.GetKeyDown(KeyCode.Alpha3))
            LaunchSpell(SkillBar.Instance.getSpell(2));
        if (Input.GetKeyDown(KeyCode.Alpha4))
            LaunchSpell(SkillBar.Instance.getSpell(3));
    }

    private void LaunchDirectSpell(GameObject spell)
    {
        SpellScript launchedSpell = Instantiate(spell, transform.position, transform.rotation).GetComponent<SpellScript>();
        if (launchedSpell.isHeal)
            launchedSpell.target = gameObject;
        else
            launchedSpell.target = enemyTarget;
        launchedSpell.spellLevel = spellManager.GetSpellLevel(spell.GetComponent<SpellScript>().displayName);
        launchedSpell.Start();
    }

    private void LaunchCloseSpell(GameObject spell)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, navMeshAgent.stoppingDistance * 10.0f, LayerMask.GetMask("Enemy"));
        if (hitColliders.Length > 0)
        {
            foreach (var collider in hitColliders)
            {
                SpellScript launchedSpell = Instantiate(spell, transform.position, transform.rotation).GetComponent<SpellScript>();
                launchedSpell.target = collider.gameObject;
                launchedSpell.spellLevel = spellManager.GetSpellLevel(spell.GetComponent<SpellScript>().displayName);
                launchedSpell.Start();
            }    
        }
    }
    
    private void LaunchSelectZoneSpell(GameObject spell)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            SpellScript launchedSpell = Instantiate(spell, transform.position, transform.rotation).GetComponent<SpellScript>();
            launchedSpell.transform.position = hit.point + Vector3.up;
            launchedSpell.spellLevel = spellManager.GetSpellLevel(spell.GetComponent<SpellScript>().displayName);
            launchedSpell.Start();
        }
    }

    public void LaunchSpell(GameObject spell)
    {
        SpellScript spellScript = spell.GetComponent<SpellScript>();
        if (((spellScript.isDirect && (enemyTarget || spellScript.isHeal)))
            && spellManager.GetSpellLevel(spellScript.displayName) > 0
            && spellManager.CanLaunchSpell(spellScript.displayName)) 
        {
            LaunchDirectSpell(spell);
        }
        else if (spellScript.isDirect && !enemyTarget && spellManager.GetSpellLevel(spellScript.displayName) > 0
                                                        && spellManager.CanLaunchSpell(spellScript.displayName))
            LaunchCloseSpell(spell);
        else if (spellScript.isZone && spellManager.GetSpellLevel(spellScript.displayName) > 0
                 && spellManager.CanLaunchSpell(spellScript.displayName))
            LaunchSelectZoneSpell(spell);
        else
            Debug.Log("Cannot launch " + spellScript.displayName);
    }

    //ANIMATION EVENT || TEST
    public void TestInstanciateParticle()
    {
        Instantiate(SkillBar.Instance.getSpell(0), rightHand.transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (navMeshAgent)
            Gizmos.DrawSphere(navMeshAgent.destination, 1);
    }
}
