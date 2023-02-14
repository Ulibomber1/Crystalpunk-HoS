using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    public TextMeshProUGUI countText;
    public TextMeshProUGUI deathCountText;
    public float jumpHeight = 0;
    public float haltingDrag;
    public float accelerationForce;
    public float maxVelocity;

    //private bool isGrounded = false;
    private Rigidbody rb;
    private int count;
    private int deathcount;
    private float movementX;
    private float movementY;
    public GameObject winTextObject;

    private Vector2 movementVector;

    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        count = 0;
        deathcount = 0;

        SetCountText();
        winTextObject.SetActive(false);

        
    }

    void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
    }
    void MovePlayerRelativeToCamera()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = movementVector.normalized.y * forward;
        Vector3 rightRelativeHorizontalInput = movementVector.normalized.x * right;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
        rb.AddForce(cameraRelativeMovement.normalized * accelerationForce);
        if (rb.velocity.sqrMagnitude > maxVelocity * maxVelocity) // Using sqrMagnitude for efficiency
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

        transform.rotation = Quaternion.LookRotation(cameraRelativeMovement); //a little bit better way of rotating player
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position-transform.up*maxDistance, boxSize);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        deathCountText.text = "Deaths: " + deathcount.ToString();

        if (count >= 6)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        MovePlayerRelativeToCamera();
    }

    bool GroundCheck()
    {
        if(Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

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
        Debug.Log("Pew");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        if (other.gameObject.CompareTag("Void"))
        {
            transform.position = new Vector3(0f, 0.5f, 0f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            deathcount = deathcount + 1;
            SetCountText();
        }
    }
}
