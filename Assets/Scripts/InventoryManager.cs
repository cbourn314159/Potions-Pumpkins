using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPrefab.gameObject.SetActive(!inventoryPrefab.gameObject.activeSelf);  //toggles between true/false based on active self at that moment
        }
    }
}
