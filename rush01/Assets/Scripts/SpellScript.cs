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
    public float SpellCoolDown = 1f;
    public float lifeTime = 10;
    public float damagesCooldown = 1;

    private List<GameObject> enemies = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerExit(Collider other)
    {
        enemies.Remove(other.gameObject);
    }

    private IEnumerator ZoneCoroutine()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != startEntity)
                enemy.GetComponent<CharacterScript>().ReceiveDirectDamages(damages);
        }
        yield return new WaitForSeconds(damagesCooldown);
    }

    private void Start()
    {
        // Calculate spell stats
        damages *= spellLevel;
        lifeTime *= spellLevel;
        SpellCoolDown = SpellCoolDown / spellLevel;

        if (startEntity)
            transform.position = startEntity.transform.position;

        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        if (isDirect)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + target.transform.up, 0.1f);
            transform.LookAt(target.transform.position);
        }
        if (isPersonal)
            transform.parent = startEntity.transform;
    }
}
