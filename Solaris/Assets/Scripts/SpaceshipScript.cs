using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipScript : MonoBehaviour
{
    // yaw/pitch/rollTorque = how fast the spaceship rotates on all axes
    // (up/strafe)Thrust = how fast the spaceship moves on all axes
    [Header("Ship Movement Settings")]
    [SerializeField]
    private float yawTorque = 500f;
    [SerializeField]
    private float pitchTorque = 1000f;
    [SerializeField]
    private float rollTorque = 1000f;
    [SerializeField]
    private float thrust = 100f;
    [SerializeField]
    private float upThrust = 50f;
    [SerializeField]
    private float strafeThrust = 50f;

    // maxBoostAmount = how much boosting energy the spaceship can have at a time
    // boostDeprecationRate = how quickly the boosting energy depletes during boosting
    // boostRechargeRate = how quickly the boosting energy recharges when the spaceship is not using it
    // boostMultiplier = how quickly the spaceship moves during boosting
    [Header("Boost Settings")]
    [SerializeField]
    private float maxBoostAmount = 2f;
    [SerializeField]
    private float boostDeprecationRate = 0.25f;
    [SerializeField]
    private float boostRechargeRate = 0.5f;
    [SerializeField]
    private float boostMultiplier = 5f;
    bool boost = false;
    float currentBoostAmount;

    // The glide reduction variables control how quickly the spaceship slows down when the respective buttons are no longer pressed
    [SerializeField, Range(0.001f, 0.999f)]
    private float thrustGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float upDownGlideReduction = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float leftRightGlideReduction = 0.111f;
    float glide = 0f;
    float verticalGlide = 0f;
    float horizontalGlide = 0f;

    Rigidbody rb;

    // Input values (from PlayerControls)
    private float thrust1D;
    private float strafe1D;
    private float upDown1D;
    private float roll1D;
    private Vector2 pitchYaw;
    // Flag to indicate if the game has started
    public bool isStarted = false;

    // Reference to the TextManager game object
    public GameObject textManagerObject;
    // Reference to the TextManager component
    private TextManager textManager;

    // Reference to the GameManager game object
    public GameObject gameManagerObject;
    // Reference to the GameManager component
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // The spaceship starts with full boosting energy
        currentBoostAmount = maxBoostAmount;

        // Get the TextManager component from the TextManager game object
        textManager = textManagerObject.GetComponent<TextManager>();
        // Get the GameManager component from the GameManager game object
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Call the StopGame method in the GameManager
            gameManager.StopGame();
        }
    }

    void FixedUpdate()
    {
        HandleBoosting();
        HandleMovement();
    }

    void HandleBoosting()
    {
        if (!isStarted)
            return;

        // When boosting is activated, the boosting energy the spaceship currently has depletes until it reaches 0. When this happens, boosting
        // automatically deactivates
        // While boosting is deactivated, the boosting energy recharges
        if (boost && currentBoostAmount > 0f)
        {
            currentBoostAmount -= boostDeprecationRate;
            if (currentBoostAmount <= 0f)
            {
                boost = false;
            }
        }
        else
        {
            if (currentBoostAmount < maxBoostAmount)
            {
                currentBoostAmount += boostRechargeRate;
            }
        }
    }

    void HandleMovement()
    {
        if (!isStarted)
            return;

        // Roll (rotation around the Z axis)
        rb.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.deltaTime);
        // Pitch (rotation around the X axis)
        // pitchYaw.y is multiplied with -1 so that the spaceship rotates in a non-inverted way
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-pitchYaw.y, -1f, 1f) * pitchTorque * Time.deltaTime);
        // Yaw (rotation around the Y axis)
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1f, 1f) * yawTorque * Time.deltaTime);
        // Thrust (movement on the Z axis)
        if (thrust1D != 0f)
        {
            // Boosting increases the speed of the spaceship when it moves forward
            float currentThrust;
            if (boost)
            {
                currentThrust = thrust * boostMultiplier;
            }
            else
            {
                currentThrust = thrust;
            }
            rb.AddRelativeForce(Vector3.forward * thrust1D * currentThrust * Time.deltaTime);
            glide = thrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide *= thrustGlideReduction;
        }
        // Up/Down (movement on the Y axis)
        if (upDown1D != 0f)
        {
            rb.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.deltaTime);
            verticalGlide = upDown1D * upThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.up * verticalGlide * Time.deltaTime);
            verticalGlide *= upDownGlideReduction;
        }
        // Strafe (movement on the X axis)
        if (strafe1D != 0f)
        {
            rb.AddRelativeForce(Vector3.right * strafe1D * strafeThrust * Time.deltaTime);
            horizontalGlide = strafe1D * strafeThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.right * horizontalGlide * Time.deltaTime);
            horizontalGlide *= leftRightGlideReduction;
        }
    }

    // This region takes all inputs from PlayerControls to be processed in this script
    #region Input Methods
    public void OnThrust(InputAction.CallbackContext context)
    {
        thrust1D = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }

    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        boost = context.performed;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (!isStarted)
            return;
        if (other.gameObject.tag == "Asteroid")
        {
            textManager.IncreaseAsteroidsDestroyedCount();
            Destroy(other.gameObject);
            return;
        }

    }
}
