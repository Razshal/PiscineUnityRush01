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
    public float speed = 15f;
    public string displayName;
    public string description = "A spell";

    protected List<GameObject> enemies = new List<GameObject>();

    protected void OnTriggerEnter(Collider other)
    {
        enemies.Add(other.gameObject);
        if (isDirect && target && other.gameObject == target)
        {
            target.GetComponent<CharacterScript>().ReceiveDirectDamages(damages);
            Destroy(gameObject);
        }
        else if (isZone)
            StartCoroutine(ZoneCoroutine());
    }

    protected void OnTriggerExit(Collider other)
    {
        enemies.Remove(other.gameObject);
    }

    protected IEnumerator ZoneCoroutine()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != startEntity)
                enemy.GetComponent<CharacterScript>().ReceiveDirectDamages(damages);
        }
        yield return new WaitForSeconds(zoneDamagesCooldown);
    }

    protected void Start()
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
        if (isDirect)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + target.transform.up, speed);
            transform.LookAt(target.transform.position);
        }
        if (isPersonal)
            transform.parent = startEntity.transform;
    }
}
