using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public HealthBar healthBar;
    public TextMeshProUGUI gearText;
    public TextMeshProUGUI abilityText;
    public TextMeshProUGUI ammoText;
    private CapsuleCollider capColl;
    public PlayerInput playerInput;
    public MenuManager menuManager;
    public CooldownBar cooldownBar;
    public AmmoBar ammoBar;
    public ThirdPersonShooterController shooterController;
    [SerializeField] private Animator Anim;

    public float jumpHeight = 0;
    public float acceleration;
    public float maxVelocity;
    public float slopeLimit;
    private const float walkSpeed = 10.0f;
    private const float runSpeed = 15.0f;

    private Rigidbody rb;
    private Vector3 movementVector;
    private Quaternion rotationResult;

    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    //Player Stat Variables
    public static int gears = 0;
    [SerializeField] private static int maxAmmo = 20;
    private static int ammo = maxAmmo;
    private int maxHealth = 10;
    private int currentHealth;
    private static int lives = 3;

    private static bool canDoubleJump = false;
    public bool doubleJumpUnlocked;
    private static bool isGrounded;
    public float groundAngle;
    [Range(0, 1)] public float midAirForceScale;
    public Vector3 groundNormal;
    public float sphercastOffset;

    private static float cd = 5;
    private static float nextCast;
    private static int abilityCDText = 0;
    private static float fireRate = 0.2f;
    private static float nextFire;
    private static float reloadTime = 2;
    private static float nextReload;
    private static bool allowFire = false;
    private static bool isReloading = false;


    public void OnPause(InputValue context)
    {
        if (menuManager.IsShop() || menuManager.IsDialogue())
            return;
        if (menuManager.IsPaused())
        {
            menuManager.Unpause();
            // pause animation?
        }
        else
        {
            menuManager.Pause();
            // unpause animation?
        }
    }
    public void OnJump(InputValue context)
    {
        if (menuManager.IsPaused() || menuManager.IsShop() || menuManager.IsDialogue())
            return;
        if (isGrounded)
        {
            if (canDoubleJump != false)
            {
                canDoubleJump = false;
            }
            Debug.Log("Boing");
            rb.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
        }
        else if (!isGrounded && !canDoubleJump && doubleJumpUnlocked)
        {
            Debug.Log("Double Boing");
            rb.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
            canDoubleJump = true;
        }
        Anim.SetBool("Is Jumping", true);
    }

    public void OnFire(InputValue context)
    {
        if (menuManager.IsPaused() || menuManager.IsShop())
            return;
        if (menuManager.IsDialogue())
        {
            menuManager.NextDialogue();
            return;
        }
        if (ammo > 0)
        {
            Debug.Log("Pew"); // Projectile-based shooting
            shooterController.Shoot();
            ammo = ammo - 1;
            Fired();
            // Disable projectile when they hit something
            // Here, we only instantiate the projectile
        }
        else
        {
            Reload();
        }
    }

    void OnSprint(InputValue context)
    {
        if (menuManager.IsPaused() || menuManager.IsShop() || menuManager.IsDialogue())
            return;
        if (context.isPressed)
        {
            Debug.Log("Add + 5 Speed!");
            Anim.SetBool("Is Sprinting", true);
            maxVelocity = runSpeed;
        }
        else
        {
            Debug.Log("Speed Reset");
            Anim.SetBool("Is Sprinting", false);
            maxVelocity = walkSpeed;
        }
    }

    void Reload()
    {
        //Code for reloading
        isReloading = false;
        ammo = maxAmmo;
        SetAmmoText();
        Debug.Log("Reloaded Successfully!");
        ammoBar.SetMaxAmmo(ammo);
    }

    public void ReloadTime()
    {
        isReloading = true;
        nextReload = Time.time + reloadTime;
        Debug.Log("Reloading...");
    }

    void Fired()
    {
        SetAmmoText();
        // Disable projectile when they hit something
        // Here, we only instantiate the projectile
    }

    //Ability function, checks to see if ability is on cooldown
    void OnAbility(InputValue context)
    {
        if (menuManager.IsPaused() || menuManager.IsShop() || menuManager.IsDialogue())
            return;
        if (Time.time < nextCast)
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

    void OnReload(InputValue context)
    {
        if (menuManager.IsPaused() || menuManager.IsShop() || menuManager.IsDialogue())
            return;
        allowFire = false;
        ReloadTime();
    }

    //Should return current HP
    public int GetHP()
    {
        return currentHealth;
    }

    //Should return current amount of gears
    public int GetGearTotal()
    {
        return gears;
    }

    public void SubtractGears(int price)
    {
        gears = gears - price;
        SetGearText();
    }

    public void OnLook(InputValue context)
    {

    }

    public delegate void InteractionHandler();
    public static event InteractionHandler OnInteraction;
    public void OnInteract(InputValue value)
    {
        if (menuManager.IsPaused() || menuManager.IsShop() || menuManager.IsDialogue())
            return;
        OnInteraction?.Invoke();
    }

    public void OnMove(InputValue movementValue)
    {
        if (menuManager.IsPaused() || menuManager.IsShop() || menuManager.IsDialogue())
        {
            movementVector = Vector3.zero;
            return;
        }
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
        Anim.SetFloat("Input Magnitude", movementVector.magnitude);
        if (movementVector.magnitude == 0.0f)
            return;

        movementVector = movementVector.normalized * acceleration;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeInput = movementVector.z * forward;
        Vector3 rightRelativeInput = movementVector.x * right;

        Vector3 cameraRelativeMovement = forwardRelativeInput + rightRelativeInput;
        rotationResult = VectorToQuaternion(cameraRelativeMovement);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationResult, Time.deltaTime * 10);//smooths rotation

        Vector3 projectedRelativeMovement = Vector3.ProjectOnPlane(cameraRelativeMovement, groundNormal);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

        if (isGrounded)
        {
            rb.AddForce(projectedRelativeMovement);
        }
        else
        {
            rb.AddForce(projectedRelativeMovement * midAirForceScale); 
        }
        if (rb.velocity.sqrMagnitude > maxVelocity * maxVelocity) // Using sqrMagnitude for efficiency
        {
            float yValue = rb.velocity.y;
            Vector3 clampedVelocity = rb.velocity.normalized * maxVelocity;
            rb.velocity = new Vector3(clampedVelocity.x, yValue, clampedVelocity.z);
        }
    }

    void GroundCheck()
    {
        if (Physics.SphereCast(transform.position, capColl.radius, Vector3.down, out RaycastHit hit, capColl.height / 2 - capColl.radius + sphercastOffset))
        {
            isGrounded = true;
            groundAngle = Vector3.Angle(Vector3.up, hit.normal);
            groundNormal = hit.normal;

            if (Physics.BoxCast(transform.position, new Vector3(capColl.radius / 2.5f, capColl.radius / 3f, capColl.radius / 2.5f), Vector3.down, out RaycastHit helpHit, transform.rotation, capColl.height / 2 - capColl.radius / 2))
            {
                groundAngle = Vector3.Angle(Vector3.up, helpHit.normal);
            }
            Anim.SetBool("Is Jumping", false);
        }
        else
        {
            isGrounded = false;
            groundAngle = 0;
            groundNormal = Vector3.up;
        }
        Anim.SetBool("Is Grounded", isGrounded);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponentInParent<Rigidbody>();
        capColl = GetComponent<CapsuleCollider>();

        //Sets the current health to the max health
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        //Sets HUD stats
        SetGearText();
        SetAbilityText();
        SetAmmoText();
        cooldownBar.SetMaxCooldown(cd);
        ammoBar.SetMaxAmmo(ammo);
    }

    void FixedUpdate()
    {
        GroundCheck();
        MovePlayerRelativeToCamera();

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            gears += 1;
            Debug.Log(gears + " gears collected");
            SetGearText();
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
    public void PlayerDamage(int damage)
    {
        //The player's health is subtracted by the damage value
        currentHealth = currentHealth - damage;
        Anim.SetTrigger("Health Lost");
        //The new player health that was subtracted is set
        healthBar.SetHealth(currentHealth);
        Anim.SetInteger("Health", currentHealth);
        //When the player loses all health their position will be reset and the death count will increase by +1
        //The player's health is reset back to it's max value
        //The health slider is also reset to a full bar
        if (currentHealth == 0)
        {
            Anim.SetTrigger("Player Dead");
            playerInput.DeactivateInput();
            this.CallWithDelay(Respawn, 5f);
            
        }
    }

    private void Respawn()
    {
        transform.position = new Vector3(0f, 0.5f, 0f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        lives = lives - 1;
        Debug.Log("Lives left: " + lives);
        Anim.StopPlayback();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Anim.SetTrigger("Respawned");
        playerInput.ActivateInput();
    }

    public void SetHealthFull()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void SetGearText()
    {
        gearText.text = gears.ToString();

    }
    void SetAbilityText()
    {
        if (abilityCDText > 0)
        {
            abilityText.text = abilityCDText.ToString();
        }
        else
        {
            abilityText.text = "Ability Available";
        }
    }

    void SetAmmoText()
    {
        ammoText.text = ammo.ToString() + "/" + maxAmmo;
        ammoBar.SetAmmo(ammo);
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

    void Update()
    {
        if (nextCast > Time.time)
        {

            abilityCDText = (int)nextCast - (int)Time.time;
            cooldownBar.SetCooldown(abilityCDText);
            SetAbilityText();
        }

        if (ammo == 0 && isReloading == false)
        {
            ReloadTime();
        }

        if (nextReload < Time.time && isReloading == true)
        {
            Reload();
        }

        if (Time.time > nextFire && ammo > 0 && allowFire == true)
        {
            nextFire = Time.time + fireRate;
            Fired();
        }
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }


}
