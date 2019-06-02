using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPhysic : MonoBehaviour
{
	public Rarity rarity;
	public string displayName;

	private void Start()
	{
		rarity = RarityManager.Instance.getRandomRarity();
		Outline outline = GetComponent<Outline>();
		if (outline != null)
			outline.OutlineColor = rarity.color;
	}
}
