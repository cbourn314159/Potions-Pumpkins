using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 6f;
    
    [Header("Jumping")]
    public float jumpForce = 5f;
    public float gravityForce = -25.0f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    public float movementMultiplier = 10f;
    [SerializeField] public float airMultiplier = 0.4f;

    float groundDrag = 6f;
    float airDrag = 1f;


    float horizontalMovement;
    float verticalMovement;

    bool isGrounded;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);

        MyInput();
        ControlDrag();

        print(rb.drag);

        if (Input.GetKeyDown(jumpKey) && isGrounded) 
        {
            Jump();
        }
    }

    void MyInput() 
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
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
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded) 
        {
            Physics.gravity = new Vector3(0, gravityForce, 0);
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    void Jump() 
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
