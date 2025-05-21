using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private static PlayerManager playerManager;
    public PlayerHealth playerHealth;
    public StatHandler statHandler;

    private void Start() {
        if (playerManager == null) playerManager = this;
    }

    public static bool isReady() {
        return playerManager != null;
    }

    public static PlayerHealth getHealthManager() {
        return playerManager.playerHealth;
    }

    public static StatHandler getStatHandler() {
        return playerManager.statHandler;
    }

}
