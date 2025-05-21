using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    
    public List<FightSystem> system;

}

[System.Serializable]
public class FightSystem {
    public enum fight_system {
        CARDS,
        CHESS,
        CHAT
    };

    public fight_system fightSystem;
    public string name;
}