using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour {

	public static SpellManager Instance { get; private set; }
	private Dictionary< string, GameObject > _spellDictionary = new Dictionary<string, GameObject>();
	[SerializeField] List<string> SpellKeys = new List<string>();
	[SerializeField] List<GameObject> SpellValues = new List<GameObject>();

	private void Awake()
	{
		Instance = this;
		for (int i = 0; i < SpellKeys.Count; i++)
		{
			_spellDictionary[SpellKeys[i]] = SpellValues[i];
		}
	}

	public GameObject getSpell(string name)
	{
		Debug.Log(name);
		Debug.Log(_spellDictionary.ContainsKey("fire").ToString());
		return _spellDictionary[name];
	}
}
