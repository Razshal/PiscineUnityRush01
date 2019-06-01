using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    private GameObject player;

    new void Start()
    {
		base.Start();
        level = playerScript.level;
        player = GameObject.FindWithTag("Player");
        agility += (int)(agility * level * 0.15);
        strength += (int)(agility * level * 0.15);
        constitution += (int)(agility * level * 0.15);
    }

	private void OnMouseDown()
	{
        playerScript.enemyTarget = gameObject;
	}

	private void OnMouseOver()
	{
        playerScript.enemyHover = gameObject;
	}

	private void OnMouseExit()
	{
        playerScript.enemyHover = null;
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            enemyTarget = other.gameObject;
    }

    new void Update()
    {
        base.Update();
    }
}
