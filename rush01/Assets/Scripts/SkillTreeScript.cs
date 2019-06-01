using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeScript : MonoBehaviour {
    private PlayerScript player;
    public GameObject[] skills;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
