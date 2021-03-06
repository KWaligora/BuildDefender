﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOvelay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;

    private Transform barTransform;

    private void Start()
    {
        barTransform = transform.Find("Bar");
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;       
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerScound().ToString("F2"));
    }

    private void Update()
    {
       barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
