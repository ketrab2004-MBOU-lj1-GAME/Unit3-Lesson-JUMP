using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    private Animator animator;
    public ParticleSystem explosionParticle;
    public ParticleSystem runParticle;
    private AudioSource playerSound;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 100f;

    public bool isOnGround = true;
    public bool gameOver = false;

    public float fallMultiplierFloat = 2.5f;
    public float lowJumpMultiplierFloat = 2f;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isOnGround && !gameOver)
        {
            runParticle.Stop();
            playerSound.PlayOneShot(jumpSound, 1f);
            animator.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        
        //faster falling
        if (playerRb.velocity.y < 0) {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
        }

//control jump height by length of time jump button held
        if (playerRb.velocity.y > 0 && !Input.GetButton("Jump")) {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            runParticle.Play();
            isOnGround = true;
        }else if (other.gameObject.CompareTag("Obstacle"))
        {
            playerSound.PlayOneShot(crashSound, 1f);
            runParticle.Stop();
            gameOver = true;
            Debug.Log("Game Over!");
            explosionParticle.Play();
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
        }
    }
}
