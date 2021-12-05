using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform playerCam;
    public Transform playerOrientation;

    bool inventoryOpen = false;

    public GameObject inventoryPrefab;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = playerCam.GetComponent<Transform>();
        playerOrientation = playerOrientation.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (inventoryOpen == false)
            {
                inventoryPrefab.gameObject.SetActive(!inventoryPrefab.gameObject.activeSelf);  //toggles between true/false based on active self at that moment
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //playerCam.transform.rotation = Quaternion.Euler(-xRotation, yRotation, 0);
                //playerOrientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

                inventoryOpen = true;
                print("hi");
            }
            else
            {
                inventoryPrefab.gameObject.SetActive(!inventoryPrefab.gameObject.activeSelf);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                inventoryOpen = false;
                print("bye");
            }
        }
        

    }
}
