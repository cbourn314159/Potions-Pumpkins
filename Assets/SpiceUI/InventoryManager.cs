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
    GameObject honeyJar;

    GameObject item;

    //public List<GameObject> targetList;
    GameObject honeyJar01Prefab;


    public GameObject inventoryPrefab;
    // Start is called before the first frame update
    void Start()
    {

        //targetList = new List<GameObject>(Resources.LoadAll<GameObject>("Resources"));
        inventoryPrefab = GameObject.FindGameObjectWithTag("Inventory");
        inventoryPrefab.gameObject.SetActive(false);
        honeyJar01Prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("Assets/Resources/Honey_Jar_01.prefab"));
        MilkSm_Choc_Open = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("Assets/Resources/MilkSm_Choc_Open.prefab"));

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
                break;
            case "MilkSm_Choc_Open":
                //print("CHOCOLATE found");
                inv.addNewItem(col.gameObject);
                break;
            default:
                //print("NOTHING FOUND");
                break;
        }
    }
}
