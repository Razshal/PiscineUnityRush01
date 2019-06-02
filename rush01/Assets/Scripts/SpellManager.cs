﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instance { get; private set; }
    private Dictionary<string, GameObject> _spellDictionary = new Dictionary<string, GameObject>();
    public Dictionary<string, int> _spellLevelDictionary = new Dictionary<string, int>();
    public Dictionary<string, float> _spellCoolDownDictionary = new Dictionary<string, float>();
    [SerializeField] List<GameObject> spellValues = new List<GameObject>();

    public static SpellManager Manager()
    {
        return GameObject.Find("GameManager").GetComponent<SpellManager>();
    }

    private void Awake()
    {
        Instance = this;
        Debug.Log("PLSLSS");
        foreach (GameObject spell in spellValues)
        {
            Debug.Log(spell.GetComponent<SpellScript>().displayName);
            _spellDictionary.Add(spell.GetComponent<SpellScript>().displayName, spell);
        }
        foreach (GameObject spell in spellValues) 
            _spellLevelDictionary.Add(spell.GetComponent<SpellScript>().displayName, 0);
        foreach (GameObject spell in spellValues)
            _spellCoolDownDictionary.Add(spell.GetComponent<SpellScript>().displayName, 
                                         spell.GetComponent<SpellScript>().spellCoolDown);
    }

    public GameObject getSpell(string t)
    {
        Debug.Log("getSpell | MAnager  = " + t);
        if (_spellDictionary.ContainsKey(t))
            return _spellDictionary[t];
        return null;
    }

    public int GetSpellLevel(string name)
    {
        if (_spellLevelDictionary.ContainsKey(name))
            return _spellLevelDictionary[name];
        return 1;
    }

    public void IncreaseSpellLevel(string name, PlayerScript player)
    {
        if (player.level >= _spellDictionary[name].GetComponent<SpellScript>().minLevel
            && _spellLevelDictionary.ContainsKey(name))
        {
            _spellLevelDictionary[name]++;
            player.skillPoints--;
        }
    }

    public bool CanLaunchSpell(string name)
    {
        if (_spellCoolDownDictionary.ContainsKey(name) && _spellCoolDownDictionary[name] <= 0)
        {
            _spellCoolDownDictionary[name] = _spellDictionary[name].GetComponent<SpellScript>().spellCoolDown / _spellLevelDictionary[name];
            return true;
        }
        return false;
    }

    public void Update()
    {
        foreach (GameObject spell in spellValues)
        {
            if (_spellCoolDownDictionary[spell.GetComponent<SpellScript>().displayName] > 0)
                _spellCoolDownDictionary[spell.GetComponent<SpellScript>().displayName] -= Time.deltaTime;
        }
    }
}
