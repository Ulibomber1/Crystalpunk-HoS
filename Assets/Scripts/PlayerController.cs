using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI deathCountText;
    public float jumpHeight = 0;

    private bool isGrounded = false;
    private Rigidbody rb;
    private int count;
    private int deathcount;
    private float movementX;
    private float movementY;
    public GameObject winTextObject;



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
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
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
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (rb.velocity.y == 0)
        {
            isGrounded = true;
        }
        else
            isGrounded = false;

    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
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
