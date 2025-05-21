using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {
    public float lifetime = 2f;
    public float fadeDuration = 1.5f;
    private SpriteRenderer spriteRenderer;
    private float fadeStartTime;
    private bool fadingStarted = false;
    private float damage;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void StartFading() {
        fadingStarted = true;
        fadeStartTime = Time.time;
    }

    void Update() {
        if (fadingStarted) {
            FadeOut();
        }
    }

    public void SetLifetime(float amount) {
        lifetime = amount;
        Invoke("StartFading", lifetime);
    }

    public void SetDamage(float amount) {
        this.damage = amount;
    }

    public void AddDamage(float amount) {
        this.damage += amount;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Enemy")) {
            EnemyManager enemyManager = collision.GetComponent<EnemyManager>();
            enemyManager.DamageEnemy(damage);
            Destroy(gameObject);
        }
    }

    void FadeOut() {
        float elapsedTime = Time.time - fadeStartTime;
        float alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

        if (elapsedTime >= fadeDuration) {
            Destroy(gameObject);
        }
    }

}
