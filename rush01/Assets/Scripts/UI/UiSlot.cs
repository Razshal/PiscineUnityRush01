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
       
       private void Start()
       {
              if (transform.childCount > 0)
                     ItemIcon = transform.GetChild(0).GetComponent<ItemIcon>();
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
              Debug.Log("Right Click");
              if (ItemIcon.GetComponent<ItemIcon>().type == Type.eWeapon)
              {
                     Debug.Log("IsWeapon");
                     if (transform.parent.parent != CharacterPannel.Instance.transform)
                     {
                            Debug.Log("CharacterPannel");
                            if (CharacterPannel.Instance.addItem(ItemIcon))
                            {
                                   Debug.Log("Equiped");
                                   if (OnWeaponEquip != null)
                                   {
                                          Debug.Log("Call OnWeqponEquipEvent");
                                          OnWeaponEquip(ItemIcon.itemToEquip, ItemIcon.transform.GetChild(0).GetComponent<ItemPhysic>().rarity);
                                   }
                                   ItemIcon = null;       
                            }
                     }
                     else if (transform.parent.parent != Inventory.Instance.transform)
                     {
                            Debug.Log("Inventory");
                            if (Inventory.Instance.addItem(ItemIcon))
                            {
                                   Debug.Log("UnEquiped");
                                   if (OnWeaponUnEquip != null)
                                   {
                                          Debug.Log("Call OnWeqponUnEquipEvent");
                                          OnWeaponUnEquip(ItemIcon.itemToEquip);
                                   }
                                   ItemIcon = null;       
                            }
                     }
              }
              else if (ItemIcon.GetComponent<ItemIcon>().type == Type.eConsumable)
              {
                     Debug.Log("Consumable");
                     if (OnConsumableUse != null)
                     {
                            Debug.Log("Call OnWeqponEquipEvent");
                            OnConsumableUse(ItemIcon.transform.GetChild(0).GetComponent<Consumable>());
                     }

                     Destroy(ItemIcon.gameObject);
                     ItemIcon = null;
              }
       }
}
