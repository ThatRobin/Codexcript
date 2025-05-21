using System;
using TMPro;
using UnityEngine;

public class SystemClock : MonoBehaviour {

    [SerializeField] public TMP_Text clockText;
    void Update() {
        clockText.text = DateTime.Now.ToString("HH:mm");
    }
}
