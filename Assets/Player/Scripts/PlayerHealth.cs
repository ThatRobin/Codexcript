using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Color damageColour;
    SpriteRenderer spriteRenderer;
    Color cacheColour;

    [SerializeField] private StatHandler statHandler;
    [SerializeField] private int health;

    void Start() {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        cacheColour = spriteRenderer.color;
        health = Mathf.RoundToInt(statHandler.getStat(Stats.MAX_HEALTH));
    }

    private void Update() {
        if(health > Mathf.RoundToInt(statHandler.getStat(Stats.MAX_HEALTH))) {
            health = Mathf.RoundToInt(statHandler.getStat(Stats.MAX_HEALTH));
        }
    }

    public void DamagePlayer(int amount) {
        StartCoroutine(ChangeColour());
        if (health - amount <= 0) {
            Destroy(gameObject);
        } else {
            health -= amount;
        }
    }

    public void HealPlayer(int amount) {
        if (health + amount > statHandler.getStat(Stats.MAX_HEALTH)) {
            health = Mathf.RoundToInt(statHandler.getStat(Stats.MAX_HEALTH));
        } else {
            health += amount;
        }
    }

    public int getHealth() {
        return this.health;
    }

    IEnumerator ChangeColour() {
        spriteRenderer.color = damageColour;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = cacheColour;
    }
}
