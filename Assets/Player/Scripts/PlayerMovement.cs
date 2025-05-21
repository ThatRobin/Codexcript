using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    private Vector2 moveVec;
    private Rigidbody2D playerRigidbody;
    private StatHandler statHandler;
    private Animator animator;

    void Start() {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        statHandler = this.GetComponent<StatHandler>();
        animator = this.GetComponent<Animator>();
    }

    private void Update() {
        if (moveVec.x != 0 || moveVec.y != 0) {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("Vertical", moveVec.y);
            animator.SetFloat("Horizontal", moveVec.x);
        } else {
            animator.SetBool("IsMoving", false);
        }
    }

    void FixedUpdate() {
        playerRigidbody.linearVelocity = moveVec.normalized * statHandler.getStat(Stats.SPEED);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Item")) {
            if(collision.gameObject.TryGetComponent<ItemManager>(out ItemManager itemManager)) {
                InstantEffectItem item = itemManager.item as InstantEffectItem;
                if(item != null) {
                    item.activateEffect(itemManager);
                }
            }
        }
    }

    private void OnMove(InputValue inputValue) {
        moveVec = inputValue.Get<Vector2>();
    }

}
