using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource audioSource;

    public AudioClip[] hitForEnemyClips;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(transform.position.magnitude > 1000f) {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
            PlayRandomSound();
        }

        Destroy(gameObject);
    }

    void PlayRandomSound() {
        if (!audioSource.isPlaying) {
            audioSource.clip = hitForEnemyClips[UnityEngine.Random.Range(0, hitForEnemyClips.Length - 1)];
            audioSource.Play();
        }
    }
}