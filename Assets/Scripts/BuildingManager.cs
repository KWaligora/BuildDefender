using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private Transform PFWoodHarvester;

    private Camera mainCamera;   

    // On game begin
    private void Start()
    {
        mainCamera = Camera.main;   
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(PFWoodHarvester, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    // returning world mouse position
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0.0f;
        return mouseWorldPosition;
    }
}
