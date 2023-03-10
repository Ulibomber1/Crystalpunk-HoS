using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    public float jumpHeight = 0;
    public float playerSpeed;
    public float maxVelocity;

    //private bool isGrounded = false;
    private Rigidbody rb;
    private Vector3 movementVector;

    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    void OnJump()
    {
        if (GroundCheck())
        {
            Debug.Log("Boing");
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }

    void OnFire()
    {
        Debug.Log("Pew"); // Projectile-based shooting
        // Disable projectile when they hit something
        // Here, we only instantiate the projectile
    }

    void OnLook(InputValue inputValue)
    {
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 readVector = movementValue.Get<Vector2>();
        Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
        movementVector = toConvert;
    }

    void MovePlayerRelativeToCamera()
    {
        if (movementVector.magnitude == 0.0f)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY |
                             RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationZ;
            return;
        }
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;

        movementVector = movementVector.normalized * playerSpeed;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = movementVector.z * forward;
        Vector3 rightRelativeHorizontalInput = movementVector.x * right;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
        rb.velocity = new Vector3(cameraRelativeMovement.x, rb.velocity.y, cameraRelativeMovement.z);
        transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        /* // May be deprecated
        if (rb.velocity.sqrMagnitude > maxVelocity * maxVelocity) // Using sqrMagnitude for efficiency
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }*/
    }

    bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayerRelativeToCamera();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Void"))
        {
            transform.position = new Vector3(0f, 0.5f, 0f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
}
