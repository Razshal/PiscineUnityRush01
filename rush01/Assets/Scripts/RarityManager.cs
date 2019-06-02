using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Rarity
{
    public string name;
    public Color color;
    public float factorPower;
    public float factorRarity;
}

public class RarityManager : MonoBehaviour
{
    public static RarityManager Instance { get; private set; }
    public Rarity[] rarities;

    private void Awake()
    {
        Instance = this;
    }

    public Rarity getRandomRarity(float factor = 1.0f)
    {
        float percent = Random.Range(0.0f, 1.0f) * factor;
        foreach (var rarity in rarities)
        {
            if (percent < rarity.factorRarity)
                return rarity;
        }
        return rarities[rarities.Length - 1];
    }
}
