using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float height = 6;
    public float decal = 3;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z - decal);
    }
}
