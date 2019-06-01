using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    private GameObject player;

    new void Start()
    {
		base.Start();
        player = GameObject.FindWithTag("Player");
    }

	private void OnMouseDown()
	{
        player.GetComponent<PlayerScript>().enemyTarget = gameObject;
	}

	private void OnMouseOver()
	{
        player.GetComponent<PlayerScript>().enemyHover = gameObject;
	}

	private void OnMouseExit()
	{
        player.GetComponent<PlayerScript>().enemyHover = null;
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
