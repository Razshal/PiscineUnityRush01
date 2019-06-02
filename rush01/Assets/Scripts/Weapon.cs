using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	public Rarity rarity;
	public int damage = 5;
	public float attackSpeed = 1.0f;

	public void setRarity(Rarity pRarity)
	{
		rarity = pRarity;
		GetComponent<Outline>().OutlineColor = rarity.color;
	}

	public int getDamage()
	{
		return (int) Random.Range(damage, damage * rarity.factorPower);
	}

	public float getAttackSpeed()
	{
		return Random.Range(attackSpeed, attackSpeed * rarity.factorPower);
	}
}
