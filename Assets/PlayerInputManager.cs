using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public float speed = 20f;
    public float rotationSpeed = 10f;
    public float jumpForce = 190.0f;

    public bool isGrounded;
    private Rigidbody rb;
    private float yaw = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
   private void FixedUpdate ()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hInput, rb.velocity.y, vInput);
        movement = rb.rotation * movement;
        rb.MovePosition(rb.position + movement * Time.deltaTime * speed);

        yaw = (yaw + Input.GetAxis("Mouse X") * rotationSpeed) % 360f;
        rb.MoveRotation(Quaternion.Euler(0, yaw, 0));
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(jumpForce * transform.up, ForceMode.Impulse);

        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
}