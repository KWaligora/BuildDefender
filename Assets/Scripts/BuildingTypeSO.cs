﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCostArray;
    public int HealthAmountMax;

    public string GetConstructionResourceCostString()
    {
        string str = "";
        foreach(ResourceAmount resourceAmount in constructionResourceCostArray)
        {
            str += "<color=#" + resourceAmount.ResourceType.colorHex + ">" + resourceAmount.ResourceType.nameShort + resourceAmount.amount + "</color> ";
        }

        return str;
    }
}
