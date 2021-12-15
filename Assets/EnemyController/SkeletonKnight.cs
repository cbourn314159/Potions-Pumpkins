using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonKnight : MonoBehaviour
{
    public GameObject player;
    public GameObject spiceUI;
    public GameObject itemDrop;
    public Animator animator;
    public float walkSpeed;
    public float attackRange;
    public float attackArea;
    public float aggressiveArea;
    public float shieldArea;
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
        //cream = GameObject.FindGameObjectWithTag("CreamLarge");
        //cream.SetActive(false);
        walkSpeed = 3.5f;
        attackRange = 4f;
        attackArea = 3f;
        aggressiveArea = 7.5f;
        shieldArea = 25;
        health = 10;
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
            if (!animator.GetBool("Shield"))
            {
                health -= wandDamage;
                if (clip != null && health > 0)
                {
                    audio.PlayOneShot(clip, 0.1f);
                }
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
            itemDrop.transform.position = transform.position;
            Instantiate(itemDrop);
            //spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Wow. Surprised that you won. Congrats, I guess... The End. Go home. Seriously, you can leave now.");
            animator.SetBool("Defeat", true);
            if (clip != null)
            {
                audio.PlayOneShot(clip2, 0.1f);
            }
        }

        if (distanceToPlayer < attackArea && death == false)
        {
            walkSpeed = 1f;
            MoveToPlayer();
            animator.SetBool("Attack", true);
        }
        else if (distanceToPlayer < aggressiveArea && death == false)
        {
            walkSpeed = 4f;
            MoveToPlayer();
            animator.SetBool("Run", true);
        }
        else if (distanceToPlayer < shieldArea && death == false)
        {
            walkSpeed = 0f;
            MoveToPlayer();
            animator.SetBool("Shield", true);
        }
        else if (health != 0)
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
        animator.SetBool("Shield", false);
        animator.SetBool("AttackHit", false);
    }
}
