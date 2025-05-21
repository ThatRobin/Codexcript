using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour {

    [SerializeField] private float max_health;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float cast_time;

    public void modifyStat(Stats stat, float amount) {
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
            case Stats.CAST_TIME:
                cast_time += amount;
                break;
            default:
                break;
        }
    }

    public float getStat(Stats stat) {
        switch (stat) {
            case Stats.MAX_HEALTH:
                return max_health;
            case Stats.DAMAGE:
                return damage;
            case Stats.SPEED:
                return speed;
            case Stats.RANGE:
                return range;
            case Stats.CAST_TIME:
                return cast_time;
            default:
                return 0f;
        }
    }

}
