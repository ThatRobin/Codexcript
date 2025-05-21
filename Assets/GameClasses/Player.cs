using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController), typeof(PlayerStats))]
public class Player : GameScript {
    
    [Header("Movement Settings")]
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    [Header("References")]
    public Transform cameraTransform;
    
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private CharacterController characterController;
    private Vector3 velocity;

    private bool isGrounded;
    
    private PlayerStats playerStats;
    private IInteractable interactableObject;
    private void Awake() {
        playerControls = new PlayerControls();
        characterController = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
    }
    
    private void OnEnable() {
        playerControls.Player.Enable();

        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        playerControls.Player.Attack.performed += _ => Attack(playerStats.GetStat(Stats.DAMAGE));
        playerControls.Player.Defend.performed += _ => Defend();
        
        playerControls.Player.Interact.performed += _ => Interact(interactableObject);
    }

    private void Interact(IInteractable interactable) {
        interactable.Interact();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Interactable")) {
            interactableObject = other.GetComponent<IInteractable>();
        }
    }
    
    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Interactable")) {
            interactableObject = null;
        }
    }

    private void OnDisable() {
        playerControls.Player.Disable();
    }

    private void Update() {
        if(!DesktopInteraction.GetInstance().IsSelectedWindowGame()) return;
        
        if (cameraTransform == null) {
            Debug.LogWarning("Camera Transform not assigned!");
            return;
        }

        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundLayer);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float speed = playerStats.GetStat(Stats.SPEED);

        Vector3 totalMovement = Move(speed);

        characterController.Move(totalMovement * Time.deltaTime);
    }

    public Vector3 Move(float speed) {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        RotateTowardsMovementDirection(moveDirection);

        velocity.y += gravity * Time.deltaTime;

        Tuple<float> parameters = Tuple.Create(speed);
        speed = ExecuteMethod("Move", ref parameters).Item1;

        return moveDirection * speed + velocity;
    }

    private void RotateTowardsMovementDirection(Vector3 moveDirection) {
        if (moveDirection.sqrMagnitude > 0.01f) {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    
    private void Attack(float damage) {
        Debug.Log("Attack performed!");
        
        Tuple<float> parameters = Tuple.Create(damage);
        damage = ExecuteMethod("Attack", ref parameters).Item1;
    }

    private void Defend() {
        Debug.Log("Defend performed!");
    }
    
    public void Defend(string name) {
        Tuple<string> parameters = Tuple.Create(name);
        name = ExecuteMethod("Defend", ref parameters).Item1;
    }

    
}
