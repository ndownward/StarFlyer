using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //parameters
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip thrustSound;

    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;

    //cache variables
    Rigidbody rbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool toThrust = ProcessThrust();
        bool toRotate = ProcessRotation();
        if (!toThrust && !toRotate){
            audioSource.Stop();
        }
    }

    bool ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)){
            rbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(thrustSound);
            }
            if (!mainParticles.isPlaying){
                mainParticles.Play();
            }
            return true;
        }
        else{
            mainParticles.Stop();
        }
        return false;
    }

    bool ProcessRotation() {
        if (Input.GetKey(KeyCode.A)){
           ApplyRotation(rotationThrust);

           if(!audioSource.isPlaying){
                audioSource.PlayOneShot(thrustSound);
            }

           if (!rightParticles.isPlaying){
            rightParticles.Play();
           }
           return true;
        }

        else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotationThrust);

            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(thrustSound);
            }

            if (!leftParticles.isPlaying){
                leftParticles.Play();
            }
            return true;
        }
        else{
                rightParticles.Stop();
                leftParticles.Stop();
            }
        return false;
    }

    void ApplyRotation(float rotationThisFrame){
        //freeze physics for manual rotation
        rbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        //unfreeze physics
        rbody.freezeRotation = false;
    }

    //this is a version control test
}