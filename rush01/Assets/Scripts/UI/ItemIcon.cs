using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Type
{ eAll, eWeapon, eSkill };

public class ItemIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	
	public bool isDragable;
	public GameObject itemPhysic;
	public GameObject itemToEquip;
	public Type type = Type.eAll;

	
	public static ItemIcon ItemIconDrag;
	private UiSlot _parentSlot;
	private Vector3 _startPosition;
	private Transform _startParent;
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		ItemIconDrag = this;
		_startPosition = transform.position;
		_startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		transform.SetParent(transform.root);
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		ItemIconDrag = null;
		if (_startParent == transform.parent || transform.parent == transform.root)
		{
			//transform.position = _startPosition;
			//transform.SetParent (_startParent);
			ItemPhysic ret = ConvertItem.Instance.ConvertToItemPhysic(GetComponent<ItemIcon>());
			ret.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

}

