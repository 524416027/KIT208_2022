using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller controller;
    [SerializeField] private OVRInput.Button actionButton;

    [SerializeField] private Transform inGameMenuStartPoint = null;
    [SerializeField] private GameObject canvasObj = null;
    [SerializeField] private bool isShowMenu = false;

    private void Update()
    {
        if (OVRInput.GetDown(actionButton, controller))
        {
            if(isShowMenu)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }

            isShowMenu = !isShowMenu;
        }
    }

    private void ShowMenu()
    {
        canvasObj.SetActive(true);
        this.gameObject.transform.position = inGameMenuStartPoint.position;

        Vector3 eulerRotation = new Vector3(30, inGameMenuStartPoint.rotation.eulerAngles.y, inGameMenuStartPoint.rotation.eulerAngles.z);
        this.gameObject.transform.rotation = Quaternion.Euler(eulerRotation);
    }

    public void HideMenu()
    {
        canvasObj.SetActive(false);
    }
}
