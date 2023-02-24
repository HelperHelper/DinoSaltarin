using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; protected set; }

    [HideInInspector] public Rigidbody playerRb;
    public float jumpForce;
    //public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    void Awake()
    {
        Instance = this; 
    }


    private void Start()
    {

        playerRb = GetComponent<Rigidbody>();

        //Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    public void Jump()
    {
        if (isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    public void Crouch()
    {
        if(isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
            isOnGround = true;
            playerAnim.SetTrigger("CrounchTrig_b");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //isOnGround = true;
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            GameOver.Instance.GameOverPlayer();
            StopCoroutine(GameManager.Instance.PlayIntro());
            explosionParticle.Play();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);

        }
    }
}
