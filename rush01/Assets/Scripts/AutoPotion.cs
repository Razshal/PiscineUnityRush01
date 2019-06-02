using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPotion : MonoBehaviour
{
    public int baseHeal = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().ReceiveLife(other.gameObject.GetComponent<PlayerScript>().level * baseHeal);
            Destroy(gameObject);
        }
    }
}
