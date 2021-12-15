using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class InventoryManager : Inventory
{
    bool inventoryOpen = false;
    private Inventory inv;
    public GameObject Honey_Jar_01;
    public GameObject MilkSm_Choc_Open;
    public GameObject Coffee_Bags_v1_01;
    public GameObject MilkLrg_Whole_Closed;
    public GameObject Cream_Sm_Open;
    public GameObject TeaTin_Raspberry;
    public GameObject Coffee_Bags_v5_03;
    public GameObject testPotion;


    public GameObject inventoryPrefab;
    bool honey, choco, coffeeBag1, milkLarge, creamSmall, teaTin, coffeeBag2 = false;

    //fire potion, skull potion, healing, wind

    // Start is called before the first frame update
    void Start()
    {
        inventoryPrefab = GameObject.FindGameObjectWithTag("Inventory");
        inventoryPrefab.gameObject.SetActive(false);
    }

    private void Awake()
    {
        inv = GetComponent<Inventory>();
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


                inventoryOpen = true;
                //print("inventory opened");
            }
            else
            {
                inventoryPrefab.gameObject.SetActive(!inventoryPrefab.gameObject.activeSelf);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                inventoryOpen = false;
                //print("inventory closed");
            }
        }


    }


    //Item finds and pickups
    public void OnCollisionEnter(Collision col)
    {
        //Pick up items and remove from scene
        switch (col.gameObject.tag)
        {
            case "Honey_Jar_01":
                //print("HONEY found");
                inv.addNewItem(col.gameObject);
                honey = true;
                break;
            case "MilkSm_Choc_Open":
                //print("CHOCOLATE found");
                inv.addNewItem(col.gameObject);
                choco = true;
                break;
            case "Coffee_Bags_v1_01":
                //print("COFFEE BEANS found");
                inv.addNewItem(col.gameObject);
                coffeeBag1 = true;
                break;
            case "MilkLrg_Whole_Closed":
                //print("HONEY found");
                inv.addNewItem(col.gameObject);
                milkLarge = true;
                break;
            case "Cream_Sm_Open":
                //print("CHOCOLATE found");
                inv.addNewItem(col.gameObject);
                creamSmall = true;
                break;
            case "TeaTin_Raspberry":
                //print("COFFEE BEANS found");
                inv.addNewItem(col.gameObject);
                teaTin = true;
                break;
            case "Coffee_Bags_v5_03":
                //print("COFFEE BEANS found");
                inv.addNewItem(col.gameObject);
                coffeeBag2 = true;
                break;
            default:
                //print("NOTHING FOUND");
                break;
        }

        //check potion combination
        if (choco == true && creamSmall == true)
        {
            //add potion in recipe slot here
            inv.AddNewPotion(Instantiate(testPotion));         //testing with honey
        }

    }
}