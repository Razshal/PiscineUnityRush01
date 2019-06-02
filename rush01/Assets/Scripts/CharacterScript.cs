﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
	protected PlayerScript playerScript;
    public GameObject enemyTarget;
    public GameObject autoPotion;

    public string displayName = "Zombie";

    // Stats
    public int life;
    [HideInInspector]
    public int maxLife;
    public int strength = 10;
    public int agility = 10;
    public int constitution = 10;
    public int armor = 10;

    // Substats
    public int minDamage;
    public int maxDamage;

    // Level
    public int level;
    public int requieredXp = 100;
    public int experience = 50;

    // Coin
    public int money = 10;

    // Attack conditions
    public bool isInContact;
    private bool isPlayer;
    private bool deadTrigger = false;
    private bool deathCoroutine = false;

    // Allows the player to get out of fight
    protected bool prioritaryWaypoint = false;

    public enum State
    {
        RUN,
        ATTACKING,
        IDLE,
        DEAD
    }
    public State state;

    protected void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        isPlayer = gameObject.CompareTag("Player");
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        ComputeStats();
        life = maxLife;
    }

    public void ComputeStats()
    {
        minDamage = strength / 2;
        maxDamage = minDamage + 4;
        maxLife = constitution * 5;
    }

    protected void UpdateAnimation()
    {
        string attackVar = isPlayer ? "Attack" : "ZombieAttack";

        switch (state)
        {
            case State.DEAD:
                animator.SetBool(attackVar, false);
                animator.SetBool("Run", false);
                if (!deadTrigger)
                {
                    animator.SetTrigger("Dead");
                    deadTrigger = true;
                }
                break;
            case State.RUN:
                animator.SetBool(attackVar, false);
                animator.SetBool("Run", true);
                break;
            case State.ATTACKING:
                animator.SetBool("Run", false);
                animator.SetBool(attackVar, true);
                break;
            default:
                animator.SetBool("Run", false);
                animator.SetBool(attackVar, false);
                break;
        }
    }
    
    public void AttackEnnemyWithAnimation()
    {
        if (state == State.ATTACKING && enemyTarget)
        {
            enemyTarget.GetComponent<CharacterScript>()
                       .ReceiveDamages(agility, minDamage, maxDamage);
        }
    }

    private void CalculateIfDead()
    {
        if (life <= 0 && state != State.DEAD)
        {
            state = State.DEAD;
            if (!isPlayer)
            {
                playerScript.ReceiveExperience(experience);
                playerScript.money += money;
                if (Random.value < 0.1)
                    Instantiate(autoPotion, transform.position, transform.rotation);
                LootSpawner.Instance.generateLoot(transform);
            }
        }
    }

    public void ReceiveDamages(int attackerAgility, int attackerMinDamage, int attackerMaxDamage)
    {
        float random = Random.value;
        int hitChance = 75 + attackerAgility - agility;
        int baseDamage = random < 0.5 ? attackerMinDamage : attackerMaxDamage;

        //Debug.Log(name + " receive attack " + (random * 100 <= hitChance ? "success" : "miss") + random + "/" + hitChance);

        if (random * 100 <= hitChance)
            life -= (baseDamage * (1 - armor / 200));

        CalculateIfDead();
    }

    public void ReceiveDirectDamages(int damages)
    {
        life -= damages;
        CalculateIfDead();
    }

    public void ReceiveLife(int healAmmount)
    {
        if (life + healAmmount > maxLife)
            life = maxLife;
        else
            life += healAmmount;
    }

    private IEnumerator DeadDisapearing()
    {
        deathCoroutine = true;
        Destroy(gameObject.GetComponent<CharacterController>());
        Destroy(gameObject.GetComponent<NavMeshAgent>());
        yield return new WaitForSeconds(4);
        Destroy(gameObject, 2f);
        while (gameObject)
        {
            transform.Translate(Vector3.down * 0.005f);
            yield return new WaitForFixedUpdate();
        }
    }

    protected void Update()
    {
        if (state != State.DEAD)
        {
            
            if (enemyTarget)
                navMeshAgent.SetDestination(enemyTarget.transform.position);
            // Defines if target is reached
            isInContact = navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
            
            // Sets states for animations and attack
            if (!isInContact)
                state = State.RUN;
            else if (!prioritaryWaypoint && enemyTarget && isInContact && enemyTarget.GetComponent<CharacterScript>().state != State.DEAD)
            {
                transform.LookAt(enemyTarget.transform.position);
                state = State.ATTACKING;
            }
            else
            {
                state = State.IDLE;
                prioritaryWaypoint = false;
                if (isPlayer)
                    enemyTarget = null;
            }
        }
        else if (!deathCoroutine && !isPlayer)
            StartCoroutine("DeadDisapearing");
        UpdateAnimation();
    }
}
