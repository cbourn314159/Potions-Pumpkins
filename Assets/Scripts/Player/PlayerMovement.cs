using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float gravityForce = -25.0f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    public float movementMultiplier = 10f;
    [SerializeField] public float airMultiplier = 0.4f;

    float groundDrag = 6f;
    float airDrag = 1f;


    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;
    public bool isGrounded;
    float groundDistance = 0.4f;
    
    [Header("Sound")]
    public AudioSource sound;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    //public Animator animator;
    Rigidbody rb;
    Animator anim;
    [Header("Animator")]
    public GameObject head;


    RaycastHit slopeHit;
    private bool OnSlope() 
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f)) 
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
                return false;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        sound = GetComponent<AudioSource>();
        anim = head.GetComponent<Animator>();
        float groundDrag = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        //print(rb.drag);

        if (Input.GetKeyDown(jumpKey) && isGrounded) 
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

    }

    void MyInput() 
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

        if (horizontalMovement == 0 && verticalMovement == 0)
        {
            anim.SetTrigger("Idle");
        }
        else
        {
            anim.SetTrigger("Walk");
        }
    }

    void ControlSpeed() 
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else 
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag() 
    {
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = airDrag;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer() 
    {

        if (isGrounded && !OnSlope())
        {
            rb.useGravity = false;
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            //print("grounded not on slope");

        }
        else if (isGrounded && OnSlope()) 
        {
            rb.useGravity = false;
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            //print("grounded and on slope");

        }
        else if (!isGrounded)
        {
            //rb.useGravity = true;
            //Physics.gravity = new Vector3(0, gravityForce, 0);
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            //print("not grounded");

            //sound.Play();
        }

    }

    void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
