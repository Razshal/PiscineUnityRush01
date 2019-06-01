using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
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

    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public GameObject hit;
    public GameObject flash;

    void Start ()
    {
        // Calculate spell stats
        damages *= spellLevel;
        lifeTime *= spellLevel;
        SpellCoolDown = SpellCoolDown / spellLevel;

        if (startEntity)
            transform.position = startEntity.transform.position;
        
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs == null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
	}

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

        speed = 0;
        Quaternion rot = transform.rotation;
        Vector3 pos = transform.position;

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            if (UseFirePointRotation)
            { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs == null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
        Destroy(gameObject);
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

    void FixedUpdate ()
    {
        if (isDirect)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + target.transform.up, speed);
            transform.LookAt(target.transform.position);
        }
        if (isPersonal)
            transform.parent = startEntity.transform;
	}

    // ----------- Original collision script
    ////https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    //void OnCollisionEnter(Collision collision)
    //{
    //    speed = 0;
    //    ContactPoint contact = collision.contacts[0];
    //    Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //    Vector3 pos = contact.point + contact.normal * hitOffset;

    //    if (hit != null)
    //    {
    //        var hitInstance = Instantiate(hit, pos, rot);
    //        if (UseFirePointRotation)
    //        { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
    //        else
    //        { hitInstance.transform.LookAt(contact.point + contact.normal); }

    //        var hitPs = hitInstance.GetComponent<ParticleSystem>();
    //        if (hitPs == null)
    //        {
    //            Destroy(hitInstance, hitPs.main.duration);
    //        }
    //        else
    //        {
    //            var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
    //            Destroy(hitInstance, hitPsParts.main.duration);
    //        }
    //    }
    //    Destroy(gameObject);
    //}
}
