using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    public GameObject head;

    [Header("Movement")]
    [SerializeField] Transform orientation;
    public bool wallRunning = false;

    [Header("Detection")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private Rigidbody rb;
    private Transform initialRotation;

    Animator anim;
    PlayerMovement pMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = head.GetComponent<Animator>();
        pMove = GetComponent<PlayerMovement>(); 
    }

    bool CanWallRun() 
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void CheckWall() 
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);

        if (!wallLeft || !wallRight) 
        {
            anim.SetTrigger("Idle");
        }
    }

    void Update()
    {
        CheckWall();

        if (CanWallRun()) 
        {
            if (wallLeft)
            {
                wallRunning = true;
                StartWallRun();
                anim.SetBool("isLWallRun", true);
            }

            else if (wallRight)
            {
                wallRunning = true;
                StartWallRun();
                anim.SetBool("isRWallRun", true);
            }
            else 
            {
                StopWallRun();
                anim.SetBool("isLWallRun", false);
                anim.SetBool("isRWallRun", false);
                anim.SetTrigger("Idle");
            }
        }
        if (pMove.isGrounded == true) 
        {
            anim.SetBool("isLWallRun", false);
            anim.SetBool("isRWallRun", false);
        }
    }

    void StartWallRun() 
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if (wallRight) 
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
        }
    }
    void StopWallRun() 
    {
        rb.useGravity = true;
    }
}
