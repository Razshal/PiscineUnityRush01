using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    public GameObject target;
    public GameObject startEntity;

    public int spellLevel = 1;

    public bool isZone = false;
    public bool isPersonal = false;
    public bool isDirect = false;

    public int damages = 10;
    public float SpellCoolDown = 10f;
    public float lifeTime = 10f;
    public float zoneDamagesCooldown = 1f;
    private float _timerZoneDamagesCooldown = 0.0f;
    public float speed = 15f;
    public string displayName;
    public string description = "A spell";

    private bool canDealDamages = true;
    private bool _canDoZoneDamage = true; 
    

    [SerializeField]
    protected List<GameObject> enemies = new List<GameObject>();

    protected void OnTriggerStay(Collider other)
    {
        //Debug.Log("Spell: " + name + "OnTriggerStay");
        if (!enemies.Contains(other.gameObject)) //Patch ntoniolo
            enemies.Add(other.gameObject);
        if (canDealDamages && isDirect && target && other.gameObject == target)
        {
            Debug.Log("Do Damage");
            target.GetComponent<CharacterScript>().ReceiveDirectDamages(damages);
            canDealDamages = false;
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, lifeTime);
        }
        else if (isZone)
        {
            if (_canDoZoneDamage)
            {
                Debug.Log("DoDamageZone");
                foreach (var enemy in enemies)
                    enemy.GetComponent<CharacterScript>().ReceiveDirectDamages(damages);
                StartCoroutine(ZoneCoroutine());    
            }
        }
            
    }

    protected void OnTriggerExit(Collider other)
    {
        //Debug.Log("Spell: " + name + "OnTriggerExit");
        enemies.Remove(other.gameObject);
    }

    protected IEnumerator ZoneCoroutine()
    {
        _canDoZoneDamage = false;
        yield return new WaitForSeconds(zoneDamagesCooldown);
        _canDoZoneDamage = true;
    }

    public void Start()
    {
        // Calculate spell stats
        damages *= spellLevel;
        lifeTime *= spellLevel;
        SpellCoolDown = SpellCoolDown / spellLevel;

        if (startEntity)
            transform.position = startEntity.transform.position;
        Destroy(gameObject, lifeTime);
    }

    protected void FixedUpdate()
    {
        if (isDirect && target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + target.transform.up, speed);
            transform.LookAt(target.transform.position);
        }
        if (isPersonal)
            transform.parent = startEntity.transform;
    }
}
