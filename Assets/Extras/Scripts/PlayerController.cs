using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbplayer;
    public ParticleSystem characterExplosion;
    public ParticleSystem dirtSplatter;
    private Animator playerAnime;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource audioSource;
    public float jumpforce = 100.0f;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;

    private void Start()
    {
        rbplayer = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnime = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            rbplayer.AddForce(Vector3.up *jumpforce, ForceMode.Impulse);
            isOnGround = false;
            playerAnime.SetTrigger("Jump_trig");
            dirtSplatter.Stop();
            audioSource.PlayOneShot(jumpSound, 1.0f);
           
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtSplatter.Play();
        }

        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            playerAnime.SetBool("Death_b", true);
            playerAnime.SetInteger("DeathType", 2);
            characterExplosion.Play();
            dirtSplatter.Stop();
            audioSource.PlayOneShot(crashSound, 1.0f);
        }
        
    }
}
