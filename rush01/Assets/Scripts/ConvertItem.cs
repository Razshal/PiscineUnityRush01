using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertItem : MonoBehaviour
{
	public static ConvertItem Instance { get; private set; }
	public GameObject gItemIcon;

	private void Awake()
	{
		Instance = this;
	}

	public ItemIcon ConvertToItemIcon(ItemPhysic lItemPhysic)
	{
		GameObject ret = lItemPhysic.transform.GetChild(0).gameObject; 
		ret.SetActive(true);
		ret.transform.parent = null;
		lItemPhysic.gameObject.SetActive(false);
		lItemPhysic.transform.parent = ret.transform;
		return ret.GetComponent<ItemIcon>();
	}
	
	public ItemPhysic ConvertToItemPhysic(ItemIcon lItemIcon)
	{
		GameObject ret = lItemIcon.transform.GetChild(0).gameObject;
		ret.SetActive(true);
		ret.transform.parent = null;
		lItemIcon.gameObject.SetActive(false);
		lItemIcon.transform.parent = ret.transform;
		//re Rotate
		return ret.GetComponent<ItemPhysic>();
	}
}
