using System.Collections;
using System.Collections.Generic;
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
	public string title;
    private SpellManager spellManager;
	
	public static ItemIcon ItemIconDrag;
	private UiSlot _parentSlot;
	private Vector3 _startPosition;
	private Transform _startParent;
    private PlayerScript player;

	private void Start()
	{
        spellManager = SpellManager.Manager();
        player = PlayerScript.ActivePlayer();
	}

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

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (Input.GetKey(KeyCode.LeftShift) && player.skillPoints > 0 && spellManager.GetSpellLevel(title) < 5)
    //    {
    //        spellManager.IncreaseSpellLevel(title);
    //        player.skillPoints--;
    //    }
    //}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ItemPhysic itemPhysic = null;
		if (transform.childCount > 0)
			itemPhysic = transform.GetChild(0).GetComponent<ItemPhysic>();
		if (type == Type.eSkill)
		{
			ProjectileMover spell = SpellManager.Instance.getSpell(title).GetComponent<ProjectileMover>();
			if (spell != null)
			{
				title = spell.displayName;
				ToolTips.Instance.setTitleColor();
				info = "Level:" + spell.spellLevel.ToString() + "\nDamage:" + spell.damages.ToString() + "\n CoolDown:" + spell.SpellCoolDown.ToString();
			}
		}

		if (type == Type.eWeapon)
		{
			Rarity rarity = itemPhysic.rarity;
			Weapon weapon = itemToEquip.GetComponent<Weapon>();
			 
			if (itemPhysic != null && rarity != null)
			{
				title = itemPhysic.displayName;
				ToolTips.Instance.setTitleColor(rarity.color);
				info = rarity.displayName + "\nDamage: " + weapon.getMinDamage() + "-" + weapon.getMaxDamage(rarity.factorPower);
				info += "\nAttackSpeed :";
			}
		}

		if (type == Type.eConsumable)
		{
			Consumable consumable = itemPhysic.GetComponent<Consumable>();
			title = itemPhysic.displayName;
			info = "";
			if (consumable.regenHp >= 0)
				info += "Regeneration HP: " + consumable.regenHp.ToString();
		}

		ToolTips.Instance.setToolTips(title, info, transform.position);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ToolTips.Instance.unsetToolTips();
	}
}

