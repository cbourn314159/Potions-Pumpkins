using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventoryConsole;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryConsole.gameObject.SetActive(inventoryConsole.gameObject.activeSelf);  //toggles between true/false based on active self at that moment
        }
    }
}
