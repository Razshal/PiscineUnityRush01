using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPannel : MonoBehaviour {

	public static CharacterPannel Instance { get; private set; }

	[SerializeField]
	private List<UiSlot> _slots = new List<UiSlot>();
    
	private void Awake()
	{
		Instance = this;
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(0).GetChild(i).tag == "slot")
				_slots.Add(transform.GetChild(0).GetChild(i).GetComponent<UiSlot>());
		}
	}

	public bool addItem(ItemIcon item)
	{
		int i = 0;
		foreach (var slot in _slots)
		{
			if (slot.canAdd(item.GetComponent<ItemIcon>()))
			{
				Debug.Log("CharacterPannel place [" + i + "]");
				slot.add(item.GetComponent<ItemIcon>());
				return true;
			}

			i++;
		}
		return false;
	}
}
