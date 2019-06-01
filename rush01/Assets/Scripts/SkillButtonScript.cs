using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonScript : MonoBehaviour {
    public GameObject Spell;
    private GameObject launchedSpell;

    public void LaunchSpell()
    {
        launchedSpell = Instantiate(Spell);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
