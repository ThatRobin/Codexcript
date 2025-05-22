using System;
using UnityEngine;

public class SwordCollision : MonoBehaviour {
    public void OnTriggerEnter(Collider other) {
        transform.root.gameObject.GetComponent<Player>().SwordTriggerEnter(other);
    }
}
