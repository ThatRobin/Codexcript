using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "heal item", menuName = "items/heal item")]
public class HealthItem : InstantEffectItem {

    public int healAmount;

    public override void activateEffect(ItemManager itemManager) {
        PlayerHealth playerHealth = PlayerManager.getHealthManager();
        playerHealth.HealPlayer(healAmount);
        Destroy(itemManager.gameObject);
    }

}
