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
        //get components because you can't just slide them in the inspector ¯\_(ツ)_/¯
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isOnGround && !gameOver) //when press jumpButton, is on ground and not gameover
        {
            runParticle.Stop(); //stop run particle
            playerSound.PlayOneShot(jumpSound, 1f); //play jump sound
            animator.SetTrigger("Jump_trig"); //play jump anim
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //add jumpForce upwards
            isOnGround = false; //no longer on ground
        }
        
        //faster falling
        if (playerRb.velocity.y < 0) {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
        }

        //while you are holding jump button gravity is lower than normal so you can control how high you jump
        if (playerRb.velocity.y > 0 && !Input.GetButton("Jump")) {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) //collide with ground
        {
            runParticle.Play(); //start run particle again
            isOnGround = true; //you are on ground again
        }else if (other.gameObject.CompareTag("Obstacle")) //collide with obstacle
        {
            playerSound.PlayOneShot(crashSound, 1f); //play death sound
            runParticle.Stop(); //stop run particle
            gameOver = true; //set gameover
            Debug.Log("Game Over!"); //gameover message
            explosionParticle.Play(); //explode
            animator.SetBool("Death_b", true); //play death animation
            animator.SetInteger("DeathType_int", 1); //set death animation type to 1
        }
    }
}
