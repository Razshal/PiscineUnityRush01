using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{

	public static ItemIcon ItemIconDrag;
	private UiSlot _parentSlot;
	private ItemIcon _itemIcon;
	private Vector3 _startPosition;
	private Transform _startParent;

	private void Start()
	{
		_parentSlot = transform.parent.GetComponent<UiSlot>();
		_itemIcon = _parentSlot.ItemIcon;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		ItemIconDrag = _itemIcon;
		_startPosition = transform.position;
		_startParent = transform;
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		ItemIconDrag = null;
		if (_startParent == transform.parent)
		{
			
		}
			//transform.position = _startPosition;
	}
}
