using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;
    private Camera mainCamera;


    private void Awake()
    {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    // On game begin
    private void Start()
    {
       mainCamera = Camera.main;       
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(activeBuildingType != null)
                Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    // returning world mouse position
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0.0f;
        return mouseWorldPosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}
