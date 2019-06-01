using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class LootRate
{
	public float rate;
	public GameObject item;

	LootRate(float r, GameObject g)
	{
		rate = r;
		item = g;
	}
}

public class LootSpawner : MonoBehaviour
{
	public LootRate[] loots;

	void generateLoot(Transform inPlaceTransform, float factor = 1.0f)
	{
		foreach (var lot in loots)
		{
			if (Random.Range(0.0f, 1.0f) < lot.rate * factor)
				Instantiate(lot.item, inPlaceTransform);
		}
	}
}
