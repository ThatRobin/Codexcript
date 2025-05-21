using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	private Rigidbody rb;
    public float moveSpeed, jumpSpeed, fallSpeed;

    public LayerMask ground;
    public Transform groundPoint;
    public bool isGrounded;

    public Animator anim;
    public SpriteRenderer sr;

    private void Awake() {
        GameData.controls.Enable();
    }

    void Start() {
		rb = this.GetComponent<Rigidbody>();
	}
	
	void Update() {
        if (!GameData.isConversing) {
            float x = GameData.controls.movement.X_Axis.ReadValue<float>();
            float y = GameData.controls.movement.Y_Axis.ReadValue<float>() * -1;
            float jump = GameData.controls.movement.Jump.ReadValue<float>();
            Vector2 inputVector = new Vector2(x, y);
            inputVector.Normalize();
            rb.linearVelocity = new Vector3(inputVector.x * moveSpeed, rb.linearVelocity.y, inputVector.y * moveSpeed);

            anim.SetBool("isMoving", new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude > 0);


            if (!sr.flipX && inputVector.x > 0) {
                sr.flipX = true;
            } else if (sr.flipX && inputVector.x < 0) {
                sr.flipX = false;
            }

            RaycastHit hit;
            isGrounded = Physics.Raycast(groundPoint.position, Vector3.down, out hit, 0.3f, ground);

            // TODO: add jumping (not floaty)
            //if(jump == 1 && isGrounded) {
            //    rb.velocity += new Vector3(0.0f, jumpSpeed, 0.0f);
            //}
        }
	}
}
