using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonMinion : MonoBehaviour
{
    public GameObject player;
    public GameObject spiceUI;
    public GameObject cream;
    public GameObject skeleton;
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

    [Header("Damage Taken")]
    public int wandDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spiceUI = GameObject.FindGameObjectWithTag("SpiceUI");
        cream = GameObject.FindGameObjectWithTag("CreamLarge");
        skeleton = GameObject.FindGameObjectWithTag("SkeletonMinion");
        cream.SetActive(false);
        walkSpeed = 3;
        attackRange = 1.75f;
        attackArea = 2;
        aggressiveArea = 10;
        health = 9;
        death = false;

        audio = player.GetComponent<AudioSource>();
        clip = (AudioClip)Resources.Load("grunt");

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
            if (clip != null)
            {
                audio.PlayOneShot(clip, 0.1f);
            }

        }
    }

    public void SkeletonFunctionality()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (animator.GetBool("AttackHit") && distanceToPlayer < 2)
        {
            spiceUI.GetComponent<SpiceUI>().damaged = true;
        }

        ResetAnimationStates();

        if (health <= 0 && death == false)
        {
            death = true;
            cream.transform.position = transform.position;
            cream.SetActive(true);
            spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Wow. Surprised that you won. Congrats, I guess... The End. Go home. Seriously, you can leave now.");
            animator.SetBool("Defeat", true);
        }

        if (distanceToPlayer < attackArea && health != 0 && death == false)
        {
            Aggressive();
            animator.SetBool("Attack", true);
        }
        else if (distanceToPlayer < aggressiveArea && death == false)
        {
            Aggressive();
            animator.SetBool("Run", true);
        }
        else if (health != 0)
        {
            animator.SetBool("Idle", true);
        }
    }

    public void Aggressive()
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
