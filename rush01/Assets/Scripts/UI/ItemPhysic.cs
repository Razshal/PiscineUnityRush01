using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPhysic : MonoBehaviour
{
	public Sprite itemSprite;
	public GameObject objectPhysic;
	public Rarity rarity;

	private void Start()
	{
		rarity = RarityManager.Instance.getRandomRarity();
		GetComponent<Outline>().OutlineColor = rarity.color;
	}
}
