using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    SkeletonMinion SkeletonMinion;
    //public GameObject droppedCream;

    
    ////Add pickup items script here
    //  public GameObject _apple;
    //public GameObject _honey;
    // public GameObject Honey_Jar_01;
    public GameObject childObj;
    public GameObject inventoryObject;
    GameObject childObject;
    //public GameObject item;
   // GameObject honeyJar01Prefab;

    //public List<GameObject> targetList;


    void Start()
    {
        //droppedCream = SkeletonMinion.cream;
        //droppedCream = droppedCream.transform.name;
        //honeyJar01Prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Resources/Honey_Jar_01.prefab"));


        //targetList = new List<GameObject>(Resources.LoadAll<GameObject>("Resources"));
        //Instantiate(Honey_Jar_01, new Vector3(0, 0, 0), Quaternion.identity);
        // GameObject item = Instantiate(Honey_Jar_01, new Vector3(0, 0, 0), Quaternion.identity);
        //addNewItem(Honey_Jar_01);


        // addNewItem(item);
        GameObject inventoryObject = GameObject.Find("Inventory");
    }

    private void Update()
    {

    }

    void WithForeachLoop()
    {
        foreach (Transform child in transform)
            print("Foreach loop: " + child);
    }


    //Add found items to inventory slots
    public void addNewItem(GameObject item)
    {
        print("ADDING ITEM");
        int children = transform.GetChild(0).childCount;    //currently have # of inventory slots hardcoded
        //accessing children of itemsParent (all inventory slots)
        for (int i = 0; i < 3 - 1; ++i)
        {
            print("Adding item to inventory");
            //GameObject inv = inventoryObject.transform.GetChild(0).gameObject;
            GameObject ItemsParent = inventoryObject.transform.GetChild(0).gameObject;
            GameObject InventorySlot = ItemsParent.gameObject.transform.GetChild(0).gameObject;
            GameObject ItemButton = InventorySlot.gameObject.transform.GetChild(0).gameObject;
            GameObject placeHolderImage = ItemButton.gameObject.transform.GetChild(0).gameObject;
            int placeHolderImageChildren = ItemButton.gameObject.transform.GetChild(0).childCount;

            placeHolderImage.SetActive(true);   //enables image component

            //add honey prefab as chld
            if (placeHolderImageChildren == 0)
            {
                //childObject = item;
                //FIXME setparent not working.. need to set childobject correctly
                //childObject.transform.SetParent(placeHolderImage.transform, false);
                //childObject.transform.localScale = new Vector3(100, 100, 100);
                item.transform.SetParent(placeHolderImage.transform, false);
                item.transform.localScale = new Vector3(100, 100, 100);
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
