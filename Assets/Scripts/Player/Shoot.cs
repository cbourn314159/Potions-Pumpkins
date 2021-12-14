using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Assign GameObjects")]
    public Camera cam;
    public GameObject projectile;
    public GameObject muzzleFX;
    public GameObject player;
    public Transform LHfirePoint, RHfirePoint;
    public GameObject wand;

    [Header("Projectile Settings")]
    public float projectileSpeed = 30;
    public float fireRate = 4;
    public float arcRange = 1;

    private Vector3 destination;
    private bool leftHand;
    private float timeToFire;
    private Animator anim;
    public AudioSource audio;
    public AudioClip clip;

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
            anim.SetTrigger("Firing");
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile();
        }
        else 
        {
            anim.SetTrigger("Idling");
        }
    }

    /*void ShootTwoProjectiles() 
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
            InstantiateProjectile(LHfirePoint);
        }
        else 
        {
            leftHand = true;
            InstantiateProjectile(RHfirePoint);
        }
    }*/
    void ShootProjectile()
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

        InstantiateProjectile(LHfirePoint);

    }
    
    void InstantiateProjectile(Transform firePoint) 
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0),Random.Range(0.5f, 2));
        var muzzle = Instantiate(muzzleFX, firePoint.position, Quaternion.identity) as GameObject;
        Destroy(muzzle, 2);

        if (clip != null)
        {
            audio.PlayOneShot(clip, 0.1f);
        }

    }

}
