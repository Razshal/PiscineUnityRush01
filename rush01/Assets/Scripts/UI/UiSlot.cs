using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;

public class UiSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
       private bool isEmpty = true;
       public ItemIcon ItemIcon;
       public Type slotType;

       public delegate void ClickWeapon( GameObject item, Rarity rarity);
       public static event ClickWeapon OnWeaponEquip;
       public delegate void ClickUnWeapon( GameObject item);
       public static event ClickUnWeapon OnWeaponUnEquip;
       
       public delegate void ClickConsumable( Consumable item);
       public static event ClickConsumable OnConsumableUse;
       private PlayerScript player;

       private void Start()
       {
              if (transform.childCount > 0)
                     ItemIcon = transform.GetChild(0).GetComponent<ItemIcon>();
              player = PlayerScript.ActivePlayer();
       }

       public bool canAdd(ItemIcon newItemIcon)
       {
              Debug.Log((newItemIcon != null).ToString() + (transform.childCount <= 0).ToString() +
                        (slotType == Type.eAll || newItemIcon.type == slotType).ToString());
              return newItemIcon != null && transform.childCount <= 0
                     && (slotType == Type.eAll || newItemIcon.type == slotType);
       }

       public bool canAdd()
       {
              return transform.childCount <= 0 && (slotType == Type.eAll);
       }
       
       public void add(ItemIcon newItemIcon)
       {
              if (canAdd(newItemIcon))
              {
                     newItemIcon.transform.SetParent(transform);
                     ItemIcon = newItemIcon;
              }
       }
       
       public void OnDrop(PointerEventData eventData)
       {
              add(ItemIcon.ItemIconDrag);
       }
       
       public void OnPointerClick(PointerEventData eventData)
       {
              if (!ItemIcon)
                     return;
              if (Input.GetKey(KeyCode.LeftShift))
              {
                     if (player.skillPoints > 0 && SpellManager.Instance.GetSpellLevel(ItemIcon.title) < 5)
                     {
                            SpellManager.Instance.IncreaseSpellLevel(ItemIcon.title, player);
                     }
              }
              
              if (ItemIcon.GetComponent<ItemIcon>().type == Type.eWeapon)
              {
                     if (transform.parent.parent != CharacterPannel.Instance.transform)
                     {
                            if (CharacterPannel.Instance.addItem(ItemIcon))
                            {
                                   if (OnWeaponEquip != null)
                                          OnWeaponEquip(ItemIcon.itemToEquip, ItemIcon.transform.GetChild(0).GetComponent<ItemPhysic>().rarity);
                                   ItemIcon = null;       
                            }
                     }
                     else if (transform.parent.parent != Inventory.Instance.transform)
                     {
                            if (Inventory.Instance.addItem(ItemIcon))
                            {
                                   if (OnWeaponUnEquip != null)
                                          OnWeaponUnEquip(ItemIcon.itemToEquip);
                                   ItemIcon = null;       
                            }
                     }
              }
              else if (ItemIcon.GetComponent<ItemIcon>().type == Type.eConsumable)
              {
                     if (OnConsumableUse != null)
                            OnConsumableUse(ItemIcon.transform.GetChild(0).GetComponent<Consumable>());
                     Destroy(ItemIcon.gameObject);
                     ItemIcon = null;
              }
       }
}
