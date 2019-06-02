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

	public int getMaxDamage(float factor = 1.0f)
	{
		return (int) (damage * factor);
	}
	
	public int getMaxDamage()
	{
		return (int) (damage * rarity.factorPower);
	}
	
	public int getMinDamage()
	{
		return (damage);
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
