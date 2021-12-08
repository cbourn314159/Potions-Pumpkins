using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : InventoryManager   
{
    ////Add pickup items script here
    public Sprite apple;
    public Image myImageComponent;

    void Start()
    {
        apple = Resources.Load<Sprite>("Assets/Inventory/RPG_inventory_icons/apple.png"); //need to load the apple asset inside path
        addNewItem(apple);

        //x = Resources.Load<Sprite>("close.png");
        //addNewItem(x);

    }

    private void Update()
    {


    }

    void WithForeachLoop()
    {
        foreach (Transform child in transform)
            print("Foreach loop: " + child);
    }




    void addNewItem(Sprite image)
    {
        int children = transform.childCount;


        //accessing children of itemsParent (all inventory slots)
        for (int i = 0; i < 5; ++i)
        {
            //print("For loop: " + transform.GetChild(i));
            // GameObject InventorySlot = ItemsParent.transform.GetChild(i).gameObject;
            GameObject ItemsParent = transform.GetChild(0).gameObject;
            GameObject InventorySlot = ItemsParent.gameObject.transform.GetChild(i).gameObject;
            GameObject ItemButton = InventorySlot.gameObject.transform.GetChild(0).gameObject;
            GameObject placeHolderImage = ItemButton.gameObject.transform.GetChild(0).gameObject;
            myImageComponent = GetComponent<Image>();
            

            //GameObject ItemsParent = transform.GetChild(0).gameObject;
            //GameObject InventorySlot = ItemsParent.transform.GetChild(i).gameObject;
            //GameObject ItemButton = InventorySlot.transform.GetChild(0).gameObject;
            //GameObject placeHolderImage = ItemButton.transform.GetChild(0).gameObject;
            print("Items parent?: " + transform.GetChild(0).gameObject);
            print("InventorySlot?: " + ItemsParent.transform.GetChild(i).gameObject);
            print("ItemButton?: " + InventorySlot.transform.GetChild(0).gameObject);
            print("placeHolderImage?: " + ItemButton.transform.GetChild(0).gameObject);

           // placeHolderImage.SetActive(true);   //enables image component
            placeHolderImage.GetComponent<Image>().enabled = true;  //activating image component
            //Destroy(placeHolderImage);    //removes image component
            placeHolderImage.GetComponent<Image>().sprite = image;  //assigns new source image


            //gameObject.GetComponent<Image>().sprite = apple;
            //inventoryImage.GetComponent<Image>().sprite = image;

        }
    }
}
