using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private float bounce = 5.0f;
    public float waitTime = .50f;

    
    private Rigidbody playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    public ParticleSystem rocketTrace;


    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {

            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            rocketTrace.Play();
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            rocketTrace.Stop();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
           
            StartCoroutine("GameOver");

            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * bounce , ForceMode.Impulse);
        }
                

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

    IEnumerator GameOver()
    {
        
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("GameOver");
       
    }

}
