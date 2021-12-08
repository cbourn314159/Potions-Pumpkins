using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    ////Add pickup items script here
    //  public GameObject _apple;
    //public GameObject _honey;
    // public GameObject Honey_Jar_01;
    public GameObject childObj;
    GameObject childObject;
    //public GameObject item;

    //public List<GameObject> targetList;

    void Start()
    {
        //targetList = new List<GameObject>(Resources.LoadAll<GameObject>("Resources"));
        //Instantiate(Honey_Jar_01, new Vector3(0, 0, 0), Quaternion.identity);
        // GameObject item = Instantiate(Honey_Jar_01, new Vector3(0, 0, 0), Quaternion.identity);
        //addNewItem(Honey_Jar_01);


        // addNewItem(item);

    }

    private void Update()
    {

    }

    void WithForeachLoop()
    {
        foreach (Transform child in transform)
            print("Foreach loop: " + child);
    }


    public void addNewItem(GameObject item)
    {
        int children = transform.GetChild(0).childCount;    //currently have # of inventory slots hardcoded
        //accessing children of itemsParent (all inventory slots)
        for (int i = 0; i < children - 1; ++i)
        {

            GameObject ItemsParent = transform.GetChild(0).gameObject;
            GameObject InventorySlot = ItemsParent.gameObject.transform.GetChild(i).gameObject;
            GameObject ItemButton = InventorySlot.gameObject.transform.GetChild(0).gameObject;
            GameObject placeHolderImage = ItemButton.gameObject.transform.GetChild(0).gameObject;
            int placeHolderImageChildren = ItemButton.gameObject.transform.GetChild(0).childCount;

            placeHolderImage.SetActive(true);   //enables image component

            //add honey prefab as chld
            if (placeHolderImageChildren == 0)
            {
                childObject = item;

                childObject.transform.SetParent(placeHolderImage.transform, false);
                childObject.transform.localScale = new Vector3(100, 100, 100);
                // go.transform.SetParent(fCanvas.transform);

                print("no children found here");
                //if empty, add item here and break
                //additem

                break;

            }
            else
            {
                print(" child found here");
                //keep counting up until empty slot found
            }


        }
    }
}
