using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerUI : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject inventory;
    public GameObject characterSystem;

    private GameObject _weapon;
    //private Inventory mainInventory;
    //private Inventory characterSystemInventory;
    //private Tooltip toolTip;

    public GameObject HPMANACanvas;

    Text hpText;
    Text manaText;
    Image hpImage;
    Image manaImage;

    public float maxHealth = 100;
    public float maxMana = 100;
    public float maxDamage = 0;
    public float maxArmor = 0;

    public float currentHealth = 60;
    float currentMana = 100;
    float currentDamage = 0;
    float currentArmor = 0;

    int normalSize = 3;

    public void OnEnable()
    {
        UiSlot.OnWeaponEquip += EquipWeapon;
        UiSlot.OnWeaponUnEquip += UnEquipWeapon;
        //    Inventory.ItemEquip += OnGearItem;
        //    Inventory.ItemConsumed += OnConsumeItem;
        //    Inventory.UnEquipItem += OnUnEquipItem;

        //    Inventory.ItemEquip += EquipWeapon;
        //    Inventory.UnEquipItem += UnEquipWeapon;
    }

    public void OnDisable()
    {
        UiSlot.OnWeaponEquip -= EquipWeapon;
        UiSlot.OnWeaponUnEquip -= UnEquipWeapon;
     //   Inventory.ItemEquip -= OnGearItem;
     //   Inventory.ItemConsumed -= OnConsumeItem;
     //   Inventory.UnEquipItem -= OnUnEquipItem;

     //   Inventory.UnEquipItem -= UnEquipWeapon;
     //   Inventory.ItemEquip -= EquipWeapon;
    }

    void EquipWeapon(GameObject item)
    {
        Debug.Log("EquipWeapon");
        _weapon = Instantiate(item, rightHand.transform);
    }

    void UnEquipWeapon(GameObject item)
    {
        Debug.Log("UnEquipWeapon");
        if (_weapon)
            Destroy(_weapon);
    }

    void Start()
    {
        //if (GameObject.FindGameObjectWithTag("Tooltip") != null)
        //    toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        //if (inventory != null)
        //    mainInventory = inventory.GetComponent<Inventory>();
        //if (characterSystem != null)
        //    characterSystemInventory = characterSystem.GetComponent<Inventory>();
    }

    //public void OnConsumeItem(Item item)
    //{
    //    for (int i = 0; i < item.itemAttributes.Count; i++)
    //    {
    //        if (item.itemAttributes[i].attributeName == "Health")
    //        {
    //            if ((currentHealth + item.itemAttributes[i].attributeValue) > maxHealth)
    //                currentHealth = maxHealth;
    //            else
    //                currentHealth += item.itemAttributes[i].attributeValue;
    //        }
    //        if (item.itemAttributes[i].attributeName == "Mana")
    //        {
    //            if ((currentMana + item.itemAttributes[i].attributeValue) > maxMana)
    //                currentMana = maxMana;
    //            else
    //                currentMana += item.itemAttributes[i].attributeValue;
    //        }
    //        if (item.itemAttributes[i].attributeName == "Armor")
    //        {
    //            if ((currentArmor + item.itemAttributes[i].attributeValue) > maxArmor)
    //                currentArmor = maxArmor;
    //            else
    //                currentArmor += item.itemAttributes[i].attributeValue;
    //        }
    //        if (item.itemAttributes[i].attributeName == "Damage")
    //        {
    //            if ((currentDamage + item.itemAttributes[i].attributeValue) > maxDamage)
    //                currentDamage = maxDamage;
    //            else
    //                currentDamage += item.itemAttributes[i].attributeValue;
    //        }
    //    }
    //    //if (HPMANACanvas != null)
    //    //{
    //    //    UpdateManaBar();
    //    //    UpdateHPBar();
    //    //}
    //}

    //public void OnGearItem(Item item)
    //{
    //    for (int i = 0; i < item.itemAttributes.Count; i++)
    //    {
    //        if (item.itemAttributes[i].attributeName == "Health")
    //            maxHealth += item.itemAttributes[i].attributeValue;
    //        if (item.itemAttributes[i].attributeName == "Mana")
    //            maxMana += item.itemAttributes[i].attributeValue;
    //        if (item.itemAttributes[i].attributeName == "Armor")
    //            maxArmor += item.itemAttributes[i].attributeValue;
    //        if (item.itemAttributes[i].attributeName == "Damage")
    //            maxDamage += item.itemAttributes[i].attributeValue;
    //    }
    //    //if (HPMANACanvas != null)
    //    //{
    //    //    UpdateManaBar();
    //    //    UpdateHPBar();
    //    //}
    //}
//
    //public void OnUnEquipItem(Item item)
    //{
    //    for (int i = 0; i < item.itemAttributes.Count; i++)
    //    {
    //        if (item.itemAttributes[i].attributeName == "Health")
    //            maxHealth -= item.itemAttributes[i].attributeValue;
    //        if (item.itemAttributes[i].attributeName == "Mana")
    //            maxMana -= item.itemAttributes[i].attributeValue;
    //        if (item.itemAttributes[i].attributeName == "Armor")
    //            maxArmor -= item.itemAttributes[i].attributeValue;
    //        if (item.itemAttributes[i].attributeName == "Damage")
    //            maxDamage -= item.itemAttributes[i].attributeValue;
    //    }
    //    //if (HPMANACanvas != null)
    //    //{
    //    //    UpdateManaBar();
    //    //    UpdateHPBar();
    //    //}
    //}



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //if (!characterSystem.activeSelf)
            //{
            //    characterSystemInventory.openInventory();
            //}
            //else
            //{
            //    if (toolTip != null)
            //        toolTip.deactivateTooltip();
            //    characterSystemInventory.closeInventory();
            //}
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //if (!inventory.activeSelf)
            //{
            //    mainInventory.openInventory();
            //}
            //else
            //{
            //    if (toolTip != null)
            //        toolTip.deactivateTooltip();
            //    mainInventory.closeInventory();
            //}
        }

    }

}