using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHandler : GameScript {

    public float maxHealth = 20;
    public float health = 20;
    public float stunTime = 2;
    public float stunTimeMax = 2;
    public Boolean stunned;
    public List<BasicBehaviour> behaviours = new();
    private Material matInst;
    public Material material;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public GameObject deathParticle;

    AudioSource audioData;

    void Start() {
        OnStart();
    }
    
    public void OnStart() {
        audioData = GetComponent<AudioSource>(); // cache the audio data
        navMeshAgent = GetComponent<NavMeshAgent>(); // cache the navmesh agent
        animator = GetComponent<Animator>(); // cache the animator
        matInst = new Material(material); // create a new instance of material based on this enemies default material
        foreach(SkinnedMeshRenderer meshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>()) {
            meshRenderer.sharedMaterial = matInst;
        }
        
        foreach (BasicBehaviour behaviour in behaviours) {
            behaviour.OnStart(gameObject);
        }
        health = maxHealth;
    }

    private void FixedUpdate() {
        if (!stunned) {
            foreach (BasicBehaviour behaviour in behaviours) {
                behaviour.OnUpdate(gameObject, animator);
            }
        } else {
            if (stunTime <= 0) {
                navMeshAgent.angularSpeed = 360f;
                stunned = false;
                stunTime = stunTimeMax;
            } else {
                navMeshAgent.angularSpeed = 0f;
                matInst.color = Color.Lerp(Color.white, Color.red, stunTime / stunTimeMax);
                stunTime -= Time.deltaTime;
            }
        }
    }

    public Boolean IsStunned() {
        return stunned; // returns in the enemy is stunned
    }

    public void Hit(float amount) {
        if (audioData != null) {
            if (!audioData.isPlaying) {
                audioData.Play(0);
            }
        }

        stunned = true; // set the enemy to be stunned
        stunTime = stunTimeMax; // set the stun time to be max
        health -= amount; // remove the damage dealt
        if (health <= 0) { // if the health is less than or equal to 0
            Invoke("SelfTerminate", 0f); // kill this object
            if (deathParticle != null) {
                GameObject smokePuff =
                    Instantiate(deathParticle, transform.position,
                        transform.rotation) as GameObject; // create a death particle
                ParticleSystem parts = smokePuff.GetComponent<ParticleSystem>(); // get the parts of the particle
                float totalDuration = parts.main.duration + parts.main.startLifetime.constant; // get the duration of the particle in total
                Destroy(smokePuff, totalDuration); // destroy the particle after its duration is complete.
            }
        }
    }

    void SelfTerminate() {
        Destroy(gameObject); // destroy this object.
    }
}
