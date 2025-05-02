using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    public bool doubleJump;
    public float Playerspeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerRb.velocity.y > 0f && doubleJump && !gameOver)
        {
            doubleJump = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1);
            
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.speed = 1.3f;
        }
        else
        {
            playerAnim.speed = 1.2f;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}

// dont get rid of collision stuff