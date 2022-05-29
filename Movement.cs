using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed, mainThrust = 50, LRThrust = 50;
    [SerializeField]
    AudioClip engine;
    [SerializeField] ParticleSystem thrust;
    [SerializeField] ParticleSystem thrust1;
    [SerializeField] ParticleSystem thrust2;



    Rigidbody rb;
    new AudioSource audio;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }
    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Fly();
        }
        else
        {
            StopThrust();
        }
    }

    private void Fly()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(engine);
            thrust.Play();
        }
    }

    private void StopThrust()
    {
        thrust.Stop();
        audio.Stop();
    }


    private void ProcessRotation()
    {
        MoveLeftRight();
    }

    private void MoveLeftRight()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(LRThrust);
            thrust2.Play();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

            ApplyRotation(-LRThrust);
            thrust1.Play();
        }
        else
        {
            thrust1.Stop();
            thrust2.Stop();
        }
    }

    private void ApplyRotation(float rotationThrust)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Mathf.Clamp(rotationThrust, -25f, 25f) * Time.deltaTime);
    }
}
