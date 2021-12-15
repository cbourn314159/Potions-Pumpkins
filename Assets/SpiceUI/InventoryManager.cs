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

    //list of potions
    public GameObject lattePotion;
    public GameObject icedCoffeePotion;
    public GameObject bobaPotion;
    public GameObject frappePotion;


    public GameObject inventoryPrefab;
    public bool honey, choco, coffeeBag1, milkLarge, creamSmall, teaTin, coffeeBag2, latte, iced, boba, frappe = false;

    public GameObject spiceUI;

    //fire potion, skull potion, healing, wind

    // Start is called before the first frame update
    void Start()
    {
        inventoryPrefab = GameObject.FindGameObjectWithTag("Inventory");
        spiceUI = GameObject.FindGameObjectWithTag("SpiceUI");
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
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;


                inventoryOpen = true;
                //print("inventory opened");
            }
            else
            {
                inventoryPrefab.gameObject.SetActive(!inventoryPrefab.gameObject.activeSelf);
                //Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;
                //inventoryOpen = false;
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
        if (coffeeBag1 == true && milkLarge == true && creamSmall == true && !latte)
        {
            spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Cinnamon Latte Brewed: Sweet! New staff er wand er whatever. [Scroll Wheel] to toggle between the different wands.");
            print("ADDING POTION");
            //add potion in recipe slot here
            inv.AddNewPotion(Instantiate(lattePotion));
            latte = true;
        }
        //check potion combination
        if (coffeeBag2 == true && milkLarge == true && honey == true && !iced)
        {
            spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Iced Cappuccino Brewed: Ooooo aaaahh. Looks like you picked up a regeneration buff. Now, that protection aura won't be so flimsy.");
            print("Ice Latte");
            //add potion in recipe slot here
            inv.AddNewPotion(Instantiate(icedCoffeePotion));
            iced = true;
        }
        //check potion combination
        if (choco == true && teaTin == true && honey == true && !boba)
        {
            //add potion in recipe slot here
            spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Firey Boba Brewed: THE NEWEST RAPID-FIRE MODEL OF WAND! I gotta try this out. [Scroll Wheel] to see what this baby can do.");
            inv.AddNewPotion(Instantiate(bobaPotion));
            boba = true;
        }

        if (coffeeBag1 && honey && choco && milkLarge && creamSmall && teaTin && coffeeBag2 && !frappe)    //most difficult potion achievement
        {
            spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Legendary Chocolate Frappe Brewed: Aaaannndddd done! Perfecto. We've (mostly me) made the Legendary Frappe. Should be able to hit that door and bust outta here.");
            print("ADDING POTION");
            //add potion in recipe slot here
            inv.AddNewPotion(Instantiate(frappePotion));
            frappe = true;
        }
    }
}

//MAKE A ZIP FOLDER with all the potion prefabs