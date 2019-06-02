using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Type
{ eAll, eWeapon, eSkill, eConsumable };

public class ItemIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

	Quaternion rotation;
	public GameObject itemToEquip;
	public Type type = Type.eAll;
	public string info;
	public string name;

	
	public static ItemIcon ItemIconDrag;
	private UiSlot _parentSlot;
	private Vector3 _startPosition;
	private Transform _startParent;

	private void Awake()
	{
		rotation = transform.rotation;
	}
	void LateUpdate()
	{
		transform.rotation = rotation;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log(name + "OnBeginDrag");

		ItemIconDrag = this;
		_startPosition = transform.position;
		_startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		ItemIconDrag.transform.SetParent(transform.root);
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		ItemIconDrag.transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		ItemIconDrag = null;
		if (_startParent == transform.parent || transform.parent == transform.root)
		{
			
			
			if (type == Type.eConsumable || type == Type.eWeapon)
			{
				ItemPhysic ret = ConvertItem.Instance.ConvertToItemPhysic(GetComponent<ItemIcon>());
				ret.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);	
			}
			else
			{
				transform.position = _startPosition;
				transform.SetParent (_startParent);
			}
		}
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (type == Type.eSkill)
		{
			ProjectileMover spell = SpellManager.Instance.getSpell(name).GetComponent<ProjectileMover>();
			if (spell != null)
			{
				info = "Level:" + spell.spellLevel.ToString() + "\nDamage:" + spell.damages.ToString() + "\n CoolDown:" + spell.SpellCoolDown.ToString();
			}
		}
		ToolTips.Instance.setToolTips(name, info, transform.position);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ToolTips.Instance.unsetToolTips();
	}
}

