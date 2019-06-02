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
	public static LootSpawner Instance { get; private set; }
	public LootRate[] loots;

	private void Awake()
	{
		Instance = this;
	}

	public void generateLoot(Transform inPlaceTransform, float factor = 1.0f)
	{
		foreach (var lot in loots)
		{
			if (Random.Range(0.0f, 1.0f) < lot.rate * factor * 5.5f)
			{
				GameObject obj = Instantiate(lot.item, inPlaceTransform.position, Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 5.0f, ForceMode.Impulse);
				obj.transform.position += Random.insideUnitSphere * 2.0f;
			}
		}
	}
}
