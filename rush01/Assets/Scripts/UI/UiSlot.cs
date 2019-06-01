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

       public delegate void ClickWeapon( GameObject item);
       public static event ClickWeapon OnWeaponEquip;
       public delegate void ClickUnWeapon( GameObject item);
       public static event ClickUnWeapon OnWeaponUnEquip;
       
       private void Start()
       {
              if (transform.childCount > 0)
                     ItemIcon = transform.GetChild(0).GetComponent<ItemIcon>();
       }

       public bool canAdd(ItemIcon newItemIcon)
       {
              Debug.Log((newItemIcon != null ).ToString() + (transform.childCount <= 0).ToString() +
                        (slotType == Type.eAll || newItemIcon.type == slotType).ToString());
              return newItemIcon != null && transform.childCount <= 0
                     && (slotType == Type.eAll || newItemIcon.type == slotType);
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
              Debug.Log("Right Click");
              if (ItemIcon && ItemIcon.GetComponent<ItemIcon>().type == Type.eWeapon)
              {
                     Debug.Log("IsWeapon");
                     if (transform.parent != CharacterPannel.Instance.transform &&
                         CharacterPannel.Instance.addItem(ItemIcon))
                     {
                            Debug.Log("Equiped");
                            if (OnWeaponEquip != null)
                            {
                                   Debug.Log("Call OnWeqponEquipEvent");
                                   OnWeaponEquip(ItemIcon.itemToEquip);
                            }
                            ItemIcon = null;
                     }
                     else if (transform.parent != Inventory.Instance.transform &&
                              Inventory.Instance.addItem(ItemIcon))
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
}
