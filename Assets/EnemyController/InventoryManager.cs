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
    //public GameObject Cream_Lrg_Open;
    GameObject honeyJar;


    //Inventory.GetComponent<Image>().sprite = image;  //assigns new source image

    ////GameObject milkLarge = new GameObject("milkLarge");
    //public GameObject honey;
    //public GameObject milk;
    GameObject item;
    //GameObject honeyJar01Prefab;

    public List<GameObject> targetList;
    GameObject honeyJar01Prefab;


    public GameObject inventoryPrefab;
    // Start is called before the first frame update
    void Start()
    {

        targetList = new List<GameObject>(Resources.LoadAll<GameObject>("Resources"));
        //item = Instantiate(Honey_Jar_01, new Vector3(0, 0, 0), Quaternion.identity);

        //GameObject honey = Instantiate(Honey_Jar_01, new Vector3(0, 0, 0), Quaternion.identity);
        //GameObject milk = Instantiate(Cream_Lrg_Open, new Vector3(0, 0, 0), Quaternion.identity);

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
                //playerCam.transform.rotation = Quaternion.Euler(-xRotation, yRotation, 0);
                //playerOrientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

                inventoryOpen = true;
                print("inventory opened");
            }
            else
            {
                inventoryPrefab.gameObject.SetActive(!inventoryPrefab.gameObject.activeSelf);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                inventoryOpen = false;
                print("inventory closed");
            }
        }


    }

    //Item finds and pickups
    public void OnTriggerEnter(Collider col)
    {
        //Pick up items and remove from scene
        switch (col.tag)
        {
            case "Honey_Jar_01":
                print("HONEY found");
                inv.addNewItem(honeyJar01Prefab);
                Destroy(col.gameObject);
                break;
            case "MilkSm_Choc_Open":
                print("CHOCOLATE found");
                inv.addNewItem(MilkSm_Choc_Open);
                Destroy(col.gameObject);
                break;
            default:
                print("NOTHING FOUND");
                break;
        }
    }
}
