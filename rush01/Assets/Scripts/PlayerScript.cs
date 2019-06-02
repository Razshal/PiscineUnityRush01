﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : CharacterScript
{
    private RaycastHit clickHit;
    private Camera camera;
    private int frameCount;

    // Player ui references
    public GameObject enemyHover;
    public GameObject statsUI;
    public int statsPoints;
    public int skillPoints;
    public ParticleSystem levelup;
    public GameObject skillsAvailables;
    public GameObject deathText;

    new void Start()
    {
        base.Start();
        camera = Camera.main;
        displayName = "Maya";
        experience = 0;
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
                && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit)
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
        }
        else
            deathText.SetActive(true);
    }
}
