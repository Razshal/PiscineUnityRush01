using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBar : UiHidable {

	public static SkillBar Instance { get; private set; }

	private List<UiSlot> _slots = new List<UiSlot>();

	
	protected override void Awake()
	{
		base.Awake();
		Instance = this;
		for (int i = 0; i < transform.GetChild(0).childCount; i++)
		{
			if (transform.GetChild(0).GetChild(i).tag == "slot")
				_slots.Add(transform.GetChild(0).GetChild(i).GetComponent<UiSlot>());
		}
	}

	public GameObject getSpell(int index)
	{
		if (_slots[index].transform.childCount > 0)
		{
			return SpellManager.Instance.getSpell(_slots[index].transform.GetChild(0).GetComponent<ItemIcon>().name);
		}

		return null;
	}
	
}
