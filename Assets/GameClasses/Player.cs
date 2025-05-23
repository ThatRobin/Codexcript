using System;
using System.Collections;
using System.Collections.Generic;
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
    private Interactable interactableObject;
    private Animator animator;
    private void Awake() {
        playerControls = new PlayerControls();
        characterController = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }
    
    private void OnEnable() {
        playerControls.Player.Enable();

        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        playerControls.Player.Attack.performed += _ => Attack();
        playerControls.Player.Defend.performed += _ => {
            Defend();
            OnDefendStart();
        };
        playerControls.Player.Defend.canceled += _ => OnDefendEnd();
        
        playerControls.Player.Interact.performed += _ => Interact(interactableObject);
    }

    private void Interact(Interactable interactable) {
        if(interactable != null) 
            interactable.Interact();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Interactable")) {
            interactableObject = other.GetComponent<Interactable>();
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
        
        
        velocity.y += gravity * Time.deltaTime;
        
        Tuple<float> gravityParameters = Tuple.Create(velocity.y);
        velocity.y = ExecuteMethod("GetGravity", ref gravityParameters).Item1;
        
        float speed = playerStats.GetStat(Stats.SPEED);

        Vector3 totalMovement = Move(speed);

        Vector3 horizontalMovement = new Vector3(totalMovement.x, 0, totalMovement.z);
        animator.SetBool("IsMoving", horizontalMovement.magnitude > 0);
        
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
        
        Tuple<float> speedParameter = Tuple.Create(speed);
        speed = ExecuteMethod("Move", ref speedParameter).Item1;

        return moveDirection * speed + velocity;
    }

    private void RotateTowardsMovementDirection(Vector3 moveDirection) {
        if (moveDirection.sqrMagnitude > 0.01f) {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    
    private void Attack() {
        if(!DesktopInteraction.GetInstance().IsSelectedWindowGame()) return;
        if (!animator.GetBool("IsAttacking")) {

            animator.SetBool("IsAttacking", true);



            StartCoroutine(CancelAttack());
        }
    }

    IEnumerator CancelAttack() {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("IsAttacking", false);
    }

    private void OnDefendStart() {
        if(!DesktopInteraction.GetInstance().IsSelectedWindowGame()) return;
        animator.SetBool("IsDefending", true);
    }

    private void OnDefendEnd() {
        if(!DesktopInteraction.GetInstance().IsSelectedWindowGame()) return;
        animator.SetBool("IsDefending", false);
    }
    
    public void Defend() {
        if(!DesktopInteraction.GetInstance().IsSelectedWindowGame()) return;
        //Tuple<string> parameters = Tuple.Create(name);
        //name = ExecuteMethod("Defend", ref parameters).Item1;
    }


    public void SwordTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy") && animator.GetBool("IsAttacking")) {
            float damage = playerStats.GetStat(Stats.DAMAGE);
            
            Tuple<float> parameters = Tuple.Create(damage);
            damage = ExecuteMethod("Attack", ref parameters).Item1;
            
            other.GetComponent<EnemyHandler>().Hit(damage);
        }
    }
    
    /*
using ModdingTools;
using UnityEngine;

[Injector("Player")]
public class Test {
	[Inject("GetGravity(float)")]
	public static void Jump(ref float amount) {
		amount = Mathf.Sin(Time.time) * 9.81f;
	}
}
     */
}
