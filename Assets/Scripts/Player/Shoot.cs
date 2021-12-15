using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public Transform LHfirePoint, RHfirePoint;

    enum WEAPON { WAND, FIREWAND, SKULL}
    WEAPON currentWeapon = WEAPON.WAND;

    [Header("WAND")]
    public GameObject projectile;
    public GameObject muzzleFX;
    public GameObject wand;
    public float wand_fireRate = 4;

    [Header("FIRE WAND")]
    public GameObject fwand_projectile;
    public GameObject fwand_MuzzleFX;
    public GameObject fWand;
    public float fwand_fireRate = 4;

    [Header("SKULL WAND")]
    public GameObject skull_projectile;
    public GameObject skull_muzzleFX;
    public GameObject skull_wand;
    public float skull_fireRate = 4;

    [Header("Projectile Settings")]
    public float projectileSpeed = 30;
    public float arcRange = 1;

    private Vector3 destination;
    private bool leftHand;
    private float timeToFire;
    private Animator anim;
    public AudioSource audio;
    public AudioClip clip;
    float scrollInput = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = wand.GetComponent<Animator>();
        audio = player.GetComponent<AudioSource>();
        clip = (AudioClip)Resources.Load("blast");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            if (currentWeapon == WEAPON.WAND)
            {
                anim.SetTrigger("Firing");
                timeToFire = Time.time + 1 / wand_fireRate;
                ShootProjectile(projectile, muzzleFX);
            }
            else if (currentWeapon == WEAPON.FIREWAND) 
            {
                timeToFire = Time.time + 1 / fwand_fireRate;
                ShootProjectile(fwand_projectile, fwand_MuzzleFX);
            }
            else if (currentWeapon == WEAPON.SKULL)
            {
                //anim.SetTrigger("Firing");
                timeToFire = Time.time + 1 / skull_fireRate;
                ShootTwoProjectiles(skull_projectile, skull_muzzleFX);
            }
            
        }
        else 
        {
            anim.SetTrigger("Idling");
        }

        InventoryManager inventoryManager = player.GetComponent<InventoryManager>();
        scrollInput += Input.GetAxis("Mouse ScrollWheel") * 10;
        switch (scrollInput % 3) 
        {
            case 0:
                currentWeapon = WEAPON.WAND;
                wand.SetActive(true);
                fWand.SetActive(false);
                skull_wand.SetActive(false);
                break;
            case 1:
                if(inventoryManager.latte)
                {
                    currentWeapon = WEAPON.FIREWAND;
                    wand.SetActive(false);
                    fWand.SetActive(true);
                    skull_wand.SetActive(false);
                }
                else
                {
                    scrollInput++;
                }
                break;
            case 2:
                if (inventoryManager.boba)
                {
                    currentWeapon = WEAPON.SKULL;
                    wand.SetActive(false);
                    fWand.SetActive(false);
                    skull_wand.SetActive(true);
                }
                else
                {
                    scrollInput++;
                }
                break;
            default:
                currentWeapon = WEAPON.WAND;
                wand.SetActive(true);
                fWand.SetActive(false);
                skull_wand.SetActive(false);
                break;
        }
    }

    void ShootTwoProjectiles(GameObject projectileType, GameObject muzzleType) 
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else 
        {
            destination = ray.GetPoint(1000);
        }

        if (leftHand)
        { 
            leftHand = false;
            InstantiateProjectile(LHfirePoint, projectileType, muzzleType);
        }
        else 
        {
            leftHand = true;
            InstantiateProjectile(RHfirePoint, projectileType, muzzleType);
        }
    }

    void ShootProjectile(GameObject projectileType, GameObject muzzleType)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(RHfirePoint, projectileType, muzzleType);

    }
    
    void InstantiateProjectile(Transform firePoint, GameObject projectileType, GameObject muzzleType) 
    {
        var projectileObj = Instantiate(projectileType, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0),Random.Range(0.5f, 2));
        var muzzle = Instantiate(muzzleType, firePoint.position, Quaternion.identity) as GameObject;
        Destroy(muzzle, 0.15f);

        if (clip != null)
        {
            audio.PlayOneShot(clip, 0.1f);
        }

    }

}
