using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : CharacterScript
{
    public GameObject rightHand;
    public GameObject leftHand;
    public UiHidable inventory;
    public UiHidable characterSystem;
    private GameObject _weapon;
    
    private RaycastHit clickHit;
    private Camera camera;
    private int frameCount;

    // Player ui references
    public GameObject enemyHover;
    public GameObject statsUI;
    public int statsPoints;
    public int skillPoints;
    public ParticleSystem levelup;
    public GameObject skillsAvailables;
    public float radiusDrop = 1.0f;
    public GameObject deathText;

    //Event Function
    public void OnEnable()
    {
        UiSlot.OnWeaponEquip += EquipWeapon;
        UiSlot.OnWeaponUnEquip += UnEquipWeapon;
        UiSlot.OnConsumableUse += ConsumableUse;
    }

    public void OnDisable()
    {
        UiSlot.OnWeaponEquip -= EquipWeapon;
        UiSlot.OnWeaponUnEquip -= UnEquipWeapon;
        UiSlot.OnConsumableUse -= ConsumableUse;
    }
    
    void EquipWeapon(GameObject item, Rarity rarity)
    {
        Debug.Log("EquipWeapon");
        _weapon = Instantiate(item, rightHand.transform);
        _weapon.GetComponent<Weapon>().setRarity(rarity);
    }

    void UnEquipWeapon(GameObject item)
    {
        Debug.Log("UnEquipWeapon");
        if (_weapon)
            Destroy(_weapon);
    }

    void ConsumableUse(Consumable item)
    {
        if (item.regenHp > 0)
            ReceiveLife(item.regenHp);
    }
    //End Event Function
    
    new void Start()
    {
        base.Start();
        camera = Camera.main;
        displayName = "Maya";
        experience = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        // Enemy detection if player does not already launched an instruction
        if (state != State.ATTACKING
            && !enemyTarget
            && !other.isTrigger
            && other.gameObject.CompareTag("Enemy")
            && other.gameObject.GetComponent<CharacterScript>().state != State.DEAD
            && !prioritaryWaypoint)
        {
            enemyTarget = other.gameObject;
        }
    }

    private void Drop()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusDrop, LayerMask.GetMask("Loot"));
        if (hitColliders.Length > 0)
        {
            ItemPhysic ip = hitColliders[0].GetComponent<ItemPhysic>();
            if (Inventory.Instance.leftPlace())
                Inventory.Instance.addItem(ConvertItem.Instance.ConvertToItemIcon(ip));
        }
    }
    
    public void ReceiveExperience(int newXp)
    {
        experience += newXp;
        if (experience >= requieredXp)
        {
            experience -= requieredXp;
            requieredXp += 150;
            level += 1;
            life = maxLife;
            statsPoints += 5;
            skillPoints += 1;
            skillsAvailables.SetActive(true);

            // LevelUp particle
            levelup.time = 0;
            levelup.Play();
        }
    }

    public void ReceiveLife(int healAmmount)
    {
        if (life + healAmmount > maxLife)
            life = maxLife;
        else
            life += healAmmount;
    }

    public void OpenStats()
    {
        statsUI.SetActive(!statsUI.activeSelf);
    }

    private void AddStat()
    {
        statsPoints--;
        ComputeStats();
    }

    // Public setters for UI buttons
    public void AddAgility()
    {
        agility++;
        AddStat();
    }

    public void AddStrength()
    {
        strength++;
        AddStat();
    }

    public void AddConst()
    {
        constitution++;
        AddStat();
    }

    new void Update()
    {
        base.Update();


        if (state != State.DEAD)
        {
            // Sets player click movement instructions
            if (Input.GetMouseButtonDown(0)
                && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit)
                && !clickHit.collider.gameObject.CompareTag("Enemy")
                && !statsUI.activeSelf)
            {
                navMeshAgent.SetDestination(clickHit.point);
                prioritaryWaypoint = true;
                enemyTarget = null;
            }
            if (Input.GetKeyDown(KeyCode.C))
                OpenStats();
            if (Input.GetKeyDown(KeyCode.P))
                ReceiveExperience(level * 150);
            if (Input.GetKeyDown(KeyCode.E))
                Drop();
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventory.isHided())
                    inventory.Show();
                else
                    inventory.Hide();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (characterSystem.isHided())
                    characterSystem.Show();
                else
                    characterSystem.Hide();
            }
        }
        else
            deathText.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Instantiate(SkillBar.Instance.getSpell(0), rightHand.transform.position, Quaternion.identity);
    }
    
    
    
    //ANIMATION EVENT || TEST
    public void TestInstanciateParticle()
    {
        Instantiate(SkillBar.Instance.getSpell(0), rightHand.transform.position, Quaternion.identity);
    }
}
