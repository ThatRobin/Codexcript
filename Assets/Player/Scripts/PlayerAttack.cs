using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {

    public GameObject projectilePrefab;
    private Rigidbody2D playerRigidbody;
    private StatHandler statHandler;
    public Transform firePoint;
    public Transform weaponHolder;
    public Transform weapon;
    public float projectileSpeed = 1f;

    private float fireTimer; // Timer to control the firing rate


    public float rotationSpeed = 5f;
    private Vector2 rotVec;

    void Start() {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        statHandler = this.GetComponent<StatHandler>();
    }

    private void Update() {
        fireTimer += Time.deltaTime;

        if (rotVec != Vector2.zero) {
            float targetAngle = Mathf.Atan2(rotVec.y, rotVec.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            if (fireTimer >= 1f / statHandler.getStat(Stats.CAST_TIME) && Mathf.Abs(weaponHolder.rotation.eulerAngles.z - targetRotation.eulerAngles.z) < 10) {
                FireProjectile();
                fireTimer = 0f;
            }
            weaponHolder.rotation = Quaternion.Slerp(weaponHolder.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FireProjectile() {
        float targetAngle = Mathf.Atan2(rotVec.y, rotVec.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, targetRotation);

        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        ProjectileManager projManager = newProjectile.GetComponent<ProjectileManager>();
        
        projManager.SetDamage(statHandler.getStat(Stats.DAMAGE));
        projManager.SetLifetime(statHandler.getStat(Stats.RANGE));

        if (rb != null) {
            Vector2 projectileDirection = new Vector2(newProjectile.transform.right.x, newProjectile.transform.right.y);
            Vector2 playerVelocity = playerRigidbody != null ? playerRigidbody.linearVelocity : Vector2.zero;

            float dotProduct = Vector2.Dot(playerVelocity.normalized, projectileDirection.normalized);

            // You can adjust these values as per your requirement to control the projectile speed
            float minSpeed = 5f; // Minimum speed
            float maxSpeed = 10f; // Maximum speed

            // Interpolate between minSpeed and maxSpeed based on the dot product
            float adjustedSpeed = Mathf.Lerp(minSpeed, maxSpeed, (dotProduct + 1) / 2);

            Vector2 playerSidewaysVelocity = playerVelocity - (Vector2.Dot(playerVelocity, projectileDirection.normalized) * projectileDirection.normalized);


            // Calculate adjusted velocity
            Vector2 adjustedVelocity = projectileDirection * adjustedSpeed + playerSidewaysVelocity;


            rb.linearVelocity = adjustedVelocity;
        }
    }

    private void OnAttack(InputValue inputValue) {
        rotVec = inputValue.Get<Vector2>();
    }
}
