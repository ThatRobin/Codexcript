using System;
using UnityEngine;

public class PlayerStats : GameScript {
    
    [SerializeField] private float max_health;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;

    public void ModifyStat(Stats stat, float amount) {
        switch (stat) {
            case Stats.MAX_HEALTH:
                max_health += amount;
                break;
            case Stats.DAMAGE:
                damage += amount;
                break;
            case Stats.SPEED:
                speed += amount;
                break;
            case Stats.RANGE:
                range += amount;
                break;
        }
    }

    public float GetStat(Stats stat) {
        switch (stat) {
            case Stats.MAX_HEALTH:
                return max_health;
            case Stats.DAMAGE:
                return damage;
            case Stats.SPEED:
                return speed;
            case Stats.RANGE:
                return range;
            default:
                return 0f;
        }
    }
}
