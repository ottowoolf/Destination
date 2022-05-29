using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    public float delay = 3f;
    bool isTransitioning = false;
    public bool isCollisionDisabled = false;
    private static int lives = 5;
    Movement move;
    Rigidbody rb;
    Health health;

    [SerializeField] AudioClip Landed;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem success;
    [SerializeField] ParticleSystem crash;


    new AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        move = GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Commands();
    }
   
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || isCollisionDisabled) { return; };

        switch (other.gameObject.tag)
        {
            case "Land":
                Landing();
                break;
            case "Obstacle":
                StartCrash(other);
                break;
            case "Win":
                Win(); 
                break;
            default:
                break;
        }
    }
    private void StartCrash(Collision other)
    {
        lives--;
        isTransitioning = true;
        audio.Stop();
        crash.Play();
        rb.freezeRotation = false;

        audio.PlayOneShot(death);

        move.enabled = false;
        Invoke("Reload", delay);
        ThrowPlayer(other);
        if (lives <= 0)
        {
        Invoke("EndGame", delay);
        }
    }
    void Landing()
    {
        isTransitioning = true;
        audio.Stop();
        success.Play();
        audio.PlayOneShot(Landed);
        move.enabled = false;
        Invoke("LoadNextLevel", delay);
    }
    void Win()
    {
        isTransitioning = true;
        audio.Stop();
        success.Play();
        audio.PlayOneShot(Landed);
        move.enabled = false;
        Invoke("WinGame", delay);
    }
    public void LoadNextLevel()
    {
        int currrentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currrentIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    private void Reload()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
    private void EndGame()
    {
        SceneManager.LoadScene("End");
    }
    private void WinGame()
    {
        SceneManager.LoadScene("Win");
    }
    private void Commands()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabled = !isCollisionDisabled;
            Debug.Log(isCollisionDisabled);
        }
        else if (Input.GetKey(KeyCode.L))
        {
           LoadNextLevel();
        }
    }

    private void ThrowPlayer(Collision other)
    {
        // how much the character should be knocked back
        var magnitude = 2000;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
     rb.AddForce(force * magnitude);
    }

    public int GetLives()
    {
        return lives;
    }
}
