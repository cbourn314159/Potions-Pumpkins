using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    SkeletonMinion SkeletonMinion;

    //public GameObject childObj;
    public GameObject inventoryObject;
    public AudioSource audio;
    public AudioClip clip;


    void Start()
    {

        // addNewItem(item);
        GameObject inventoryObject = GameObject.Find("Inventory");
        audio = inventoryObject.GetComponent<AudioSource>();
        clip = (AudioClip)Resources.Load("pop_sound");
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
        //print("ADDING ITEM");
        int children =  inventoryObject.transform.GetChild(0).childCount;
        if (clip != null)
        {
            audio.PlayOneShot(clip, 0.1f);
        }

        for (int i = 0; i < children - 1; ++i)
        {
            //print("Adding item to inventory");
            GameObject ItemsParent = inventoryObject.transform.GetChild(0).gameObject;
            GameObject InventorySlot = ItemsParent.gameObject.transform.GetChild(i).gameObject;
            GameObject ItemButton = InventorySlot.gameObject.transform.GetChild(0).gameObject;
            GameObject placeHolderImage = ItemButton.gameObject.transform.GetChild(0).gameObject;
            int placeHolderImageChildren = ItemButton.gameObject.transform.GetChild(0).childCount;

            placeHolderImage.SetActive(true);   //enables image component

            //add honey prefab as chld
            if (placeHolderImageChildren == 0)
            {
                item.transform.SetParent(placeHolderImage.transform, false);
                item.transform.localScale = new Vector3(100, 100, 100);
                item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                item.transform.SetPositionAndRotation(new Vector3(placeHolderImage.transform.position.x - .01f, placeHolderImage.transform.position.y - .025f, placeHolderImage.transform.position.z), new Quaternion(0,0,0,0));
                // go.transform.SetParent(fCanvas.transform);


                //print("no children found here");
                //if empty, add item here and break
                //additem

                break;

            }
            else
            {
                //print(" child found here");

            }


        }
    }
}
