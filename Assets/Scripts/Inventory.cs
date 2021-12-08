using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : InventoryManager   
{
    ////Add pickup items script here

    //public void addItem()
    //{

    //}        
    //GameObject originalGameObject = GameObject.Find("ItemsParent");


    void Start()
    {
        addNewItem();
        Sprite apple = Resources.Load<Sprite>("apple"); //need to load the apple asset inside path
       //path =  Assets / Inventory / RPG_inventory_icons / apple.png
    }

    private void Update()
    {
        
    }

    void WithForeachLoop()
    {
        foreach (Transform child in transform)
            print("Foreach loop: " + child);
    }




    void addNewItem()
    {
        int children = transform.childCount;

        //accessing children of itemsParent (all inventory slots)
        for (int i = 0; i < children; ++i)
        {
            print("For loop: " + transform.GetChild(i));
            GameObject inventorySlot = transform.GetChild(i).gameObject;
            gameObject.GetComponent<Image>().sprite = apple;

            //inventorySlot.GetComponent<Image>().sprite = "apple";


        }
    }
}
