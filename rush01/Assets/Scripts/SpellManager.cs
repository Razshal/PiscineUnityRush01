using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instance { get; private set; }
    private Dictionary<string, GameObject> _spellDictionary = new Dictionary<string, GameObject>();
    [SerializeField] List<GameObject> spellValues = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        foreach (GameObject spell in spellValues)
            _spellDictionary.Add(spell.GetComponent<SpellScript>().displayName, spell);
    }

    public GameObject getSpell(string name)
    {
        Debug.Log(name);
        Debug.Log(_spellDictionary.ContainsKey("fire").ToString());
        if (_spellDictionary.ContainsKey(name))
            return _spellDictionary[name];
        return null;
    }
}
