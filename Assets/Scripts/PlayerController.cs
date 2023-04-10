using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public HealthBar healthBar;

    public float jumpHeight = 0;
    public float playerSpeed;
    public float maxVelocity;

    //private bool isGrounded = false;
    private Rigidbody rb;
    private Vector3 movementVector;
    private Quaternion rotationResult;

    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    //Player Stat Variables
    private static int gears = 0;
    private static int maxAmmo = 5;
    private static int ammo = maxAmmo;
    private int maxHealth = 10;
    private int currentHealth;
    private static int lives = 3;
    private static bool doubleJump = false;
    private static float cd = 5;
    private static float nextCast;

    void OnJump()
    {
        if (GroundCheck())
        {
            if (doubleJump != false)
            {
                doubleJump = false;
            }
            Debug.Log("Boing");
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
        else if (GroundCheck() == false && doubleJump == false)
        {
            Debug.Log("Double Boing");
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            doubleJump = true;
        }
    }

    void OnFire()
    {
        if (ammo > 0)
        {
            Debug.Log("Pew"); // Projectile-based shooting
            ammo = ammo - 1;
            // Disable projectile when they hit something
            // Here, we only instantiate the projectile
        }
        else
        {
            Reload();
        }
    }

    void Reload()
    {
        //Code for reloading
        ammo = maxAmmo;
        Debug.Log("Reloaded Successfully!");
    }

    //Ability function, checks to see if ability is on cooldown
    void Ability()
    {
        if(Time.time < nextCast)
        {
            Debug.Log("Ability in cooldown!");
            return;
        }
        else
        {
            Debug.Log("Ability casted!");
            nextCast = Time.time + cd;
        }
    }

    //Should return current HP
    public int DisplayHP()
    {
        return currentHealth;
    }

    //Should return current amount of gears
    public int DisplayGears()
    {
        return gears;
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

    private Quaternion VectorToQuaternion(Vector3 movementVector)
    {
        Vector3 relative = (transform.position + movementVector) - transform.position;
        return Quaternion.LookRotation(relative, Vector3.up);
    }

    void MovePlayerRelativeToCamera()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;

        if (movementVector.magnitude == 0.0f)
            return;

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
        rotationResult = VectorToQuaternion(cameraRelativeMovement);

        //rb.velocity = new Vector3(cameraRelativeMovement.x, rb.velocity.y, cameraRelativeMovement.z);
        rb.AddForce(cameraRelativeMovement);
        //transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotationResult, Time.deltaTime * 10);//smooths rotation

        // May be deprecated
        if (rb.velocity.sqrMagnitude > maxVelocity * maxVelocity) // Using sqrMagnitude for efficiency
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
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

        //Sets the current health to the max health
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
            gears += 1;
            Debug.Log(gears + " gears collected");
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

    //Reduces the player's health by the parameter value
    //Function should be called whenever the player takes damage from an enemy
    void PlayerDamage(int damage)
    {
        //The player's health is subtracted by the damage value
        currentHealth = currentHealth - damage;

        //The new player health that was subtracted is set
        healthBar.SetHealth(currentHealth);

        //When the player loses all health their position will be reset and the death count will increase by +1
        //The player's health is reset back to it's max value
        //The health slider is also reset to a full bar
        if (currentHealth == 0)
        {
            transform.position = new Vector3(0f, 0.5f, 0f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            lives = lives - 1;
            Debug.Log("Lives left: " + lives);
            //deathccount = deathcount + 1;
            //SetCountText();

            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    void Update()
    {
        //This is used as a test to ensure the health bar works properly
        //Press the "V" key to reduce the player health
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerDamage(2);
        }

        //Tests Ability Cooldown
        //Press the "E" key to use ability to cast and see if its on cooldown or not
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ability();
        }
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
