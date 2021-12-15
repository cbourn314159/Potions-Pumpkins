using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonMinion : MonoBehaviour
{
    public GameObject player;
    public GameObject spiceUI;
    public GameObject honey;
    public Animator animator;
    public float walkSpeed;
    public float attackRange;
    public float attackArea;
    public float aggressiveArea;
    public float health;
    public bool death;
    public float distanceToPlayer;
    public AudioSource audio;
    public AudioClip clip;
    public AudioClip clip2;

    [Header("Damage Taken")]
    public int wandDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spiceUI = GameObject.FindGameObjectWithTag("SpiceUI");
        walkSpeed = 2.5f;
        attackRange = 2f;
        attackArea = 2f;
        aggressiveArea = 20;
        health = 5;
        death = false;

        audio = player.GetComponent<AudioSource>();
        clip = (AudioClip)Resources.Load("grunt");
        clip2 = (AudioClip)Resources.Load("dying");
    }

    // Update is called once per frame
    void Update()
    {
        SkeletonFunctionality();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet") 
        {
            health -= wandDamage;
            if (clip != null && health > 0)
            {
                audio.PlayOneShot(clip, 0.1f);
            }

        }
    }

    public void SkeletonFunctionality()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (animator.GetBool("AttackHit") && distanceToPlayer < attackRange)
        {
            spiceUI.GetComponent<SpiceUI>().damaged = true;
        }

        ResetAnimationStates();

        if (health <= 0 && death == false)
        {
            death = true;
            honey.transform.position = transform.position;
            Instantiate(honey);
            animator.SetBool("Defeat", true);
            if (clip != null)
            {
                audio.PlayOneShot(clip2, 0.1f);
            }
        }

        if (distanceToPlayer < attackArea && death == false)
        {
            walkSpeed = 2.5f;
            MoveToPlayer();
            animator.SetBool("Attack", true);
        }
        else if (distanceToPlayer < aggressiveArea && death == false)
        {
            walkSpeed = 5f;
            MoveToPlayer();
            animator.SetBool("Run", true);
        }
        else if (death == false)
        {
            animator.SetBool("Idle", true);
        }
    }

    public void MoveToPlayer()
    {
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookAt);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, walkSpeed * Time.deltaTime);
    }

    public void ResetAnimationStates()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
        animator.SetBool("Idle", false);
        animator.SetBool("AttackHit", false);
    }
}
