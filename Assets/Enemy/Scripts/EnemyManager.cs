using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField] private Color damageColour;
    SpriteRenderer spriteRenderer;
    Color cacheColour;


    [SerializeField] private StatHandler statHandler;
    [SerializeField] private float health;


    void Start() {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        cacheColour = spriteRenderer.color;
        health = statHandler.getStat(Stats.MAX_HEALTH);
    }

    public void DamageEnemy(float damage) {
        StartCoroutine(ChangeColour());
        if (health - damage <= 0) {
            Destroy(gameObject);
        } else {
            health -= damage;
        }
    }

    IEnumerator ChangeColour() {
        spriteRenderer.color = damageColour;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = cacheColour;
    }


}
