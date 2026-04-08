using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public UIManager ui;
    private CharacterController cc;

    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    private bool isGrounded;

    private Vector3 velocity;

    private float maxStamina = 100f;
    private float currentStamina;
    private float emptyRate = 10f;
    private float refillRate = 5f;
    private float speedBoost = 10f;

    private bool isRunning;

    private bool staminaOnCooldown = false;
    public float cooldownTime = 3f;
    private float cooldownTimer = 0f;

    void Start()
    {
        currentStamina = maxStamina;
        cc = GetComponent<CharacterController>();

        ui.SetupPlayerStamina(currentStamina);
    }

    void Update()
    {
        HandleMovement();
        HandleCooldown();
    }
    void HandleCooldown()
    {
        if (staminaOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
                staminaOnCooldown = false;
        }
    }

    void HandleMovement()
    {
        isGrounded = cc.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        float currentSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !staminaOnCooldown)
        {
            isRunning = true;
            currentStamina -= emptyRate * Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isRunning = false;
                staminaOnCooldown = true;
                cooldownTimer = cooldownTime;
            }
        }
        else
        {
            isRunning = false;

            if (!staminaOnCooldown && currentStamina < maxStamina)
            {
                currentStamina += refillRate * Time.deltaTime;
            }
        }
        
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        ui.UpdatePlayerStamina(currentStamina);

        if (isRunning)
        {

            currentSpeed = speed + speedBoost;
        }
        else
        {
            currentSpeed = speed;
        }

        if (staminaOnCooldown)
        {
            currentSpeed = speed * 0.5f;
        }

        cc.Move((move * currentSpeed + velocity) * Time.deltaTime);
    }
}

