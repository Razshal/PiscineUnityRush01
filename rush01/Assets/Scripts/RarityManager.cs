using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Rarity[] rarities;
}
