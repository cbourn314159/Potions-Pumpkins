using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : InventoryManager   
{
    ////Add pickup items script here
    public Sprite apple;
    public Sprite a;

    void Start()
    {
        addNewItem(a);
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
        int children = transform.GetChild(0).childCount;    //currently have # of inventory slots hardcoded

        //accessing children of itemsParent (all inventory slots)
        for (int i = 0; i < children - 1; ++i)
        {

            GameObject ItemsParent = transform.GetChild(0).gameObject;
            GameObject InventorySlot = ItemsParent.gameObject.transform.GetChild(i).gameObject;
            GameObject ItemButton = InventorySlot.gameObject.transform.GetChild(0).gameObject;
            GameObject placeHolderImage = ItemButton.gameObject.transform.GetChild(0).gameObject;

            placeHolderImage.SetActive(true);   //enables image component
            placeHolderImage.GetComponent<Image>().enabled = true;  //activating image component
            placeHolderImage.GetComponent<Image>().sprite = image;  //assigns new source image

            if (placeHolderImage.GetComponent<Image>().sprite = null)
            {
                placeHolderImage.GetComponent<Image>().sprite = image;  //assigns new source image
                print("ADDED");
            }
            else
            {
                placeHolderImage.GetComponent<Image>().sprite = image;  //assigns new source image

                print("NO IMAGE ADDED");
            }

        }
    }
}
