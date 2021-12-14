using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonArcher : MonoBehaviour
{
    public GameObject player;
    public GameObject spiceUI;
    public GameObject arrow;
    //public GameObject cream;
    public Animator animator;
    public float walkSpeed;
    public float fleeArea;
    public float shootArea;
    public float towardsArea;
    public float health;
    public bool death;
    public float distanceToPlayer;

    [Header("Damage Taken")]
    public int wandDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spiceUI = GameObject.FindGameObjectWithTag("SpiceUI");
        //cream = GameObject.FindGameObjectWithTag("CreamLarge");
        //cream.SetActive(false);
        walkSpeed = 1.5f;
        fleeArea = 10;
        shootArea = 25;
        towardsArea = 30;
        health = 3;
        death = false;
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
        }
    }

    public void SkeletonFunctionality()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (animator.GetBool("ShotArrow"))
        {
            arrow.transform.position = this.transform.GetChild(1).transform.position;
            Instantiate(arrow);
        }

        ResetAnimationStates();

        if (health <= 0 && death == false)
        {
            death = true;
            //cream.transform.position = transform.position;
            //cream.SetActive(true);
            spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Wow. Surprised that you won. Congrats, I guess... The End. Go home. Seriously, you can leave now.");
            animator.SetBool("Defeat", true);
        }

        if (distanceToPlayer < fleeArea && death == false)
        {
            walkSpeed = 5f;
            MoveAwayPlayer();
            animator.SetBool("Run", true);
        }
        else if (distanceToPlayer < shootArea && death == false)
        {
            walkSpeed = 0f;
            MoveToPlayer();
            animator.SetBool("Shoot", true);
        }
        else if (distanceToPlayer < towardsArea && death == false)
        {
            walkSpeed = 2.5f;
            MoveToPlayer();
            animator.SetBool("Run", true);
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

    public void MoveAwayPlayer()
    {
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookAt);
        transform.rotation = Quaternion.Inverse(transform.rotation);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -1 * walkSpeed * Time.deltaTime);
    }

    public void ResetAnimationStates()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Shoot", false);
        animator.SetBool("ShotArrow", false);
    }
}
