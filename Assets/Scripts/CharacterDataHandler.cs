using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataHandler : MonoBehaviour
{
    public CharacterData characterData;
    public DSDialogueSO conversation;

    public float talkCooldown = 0.4f;
    public float talkTimer;

    public SpriteRenderer sr;

    void Start() {
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(talkTimer < talkCooldown) {
            talkTimer += Time.deltaTime;
		}
    }
}
