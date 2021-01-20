﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;

    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("ResourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        float offsetAmount = -160f;

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {            
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
                   
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            index++;
        }
    }

    private void Start()
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];

            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
