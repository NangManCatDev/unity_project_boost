using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float RotationThrust = 10f;
    [SerializeField] AudioClip MainEngine;
    [SerializeField] private ParticleSystem mainBoosterParticle;
    [SerializeField] private ParticleSystem leftBoosterParticle;
    [SerializeField] private ParticleSystem rightBoosterParticle;
    
    Rigidbody rocketRigidBody;
    AudioSource audioSource;
    ParticleSystem particleSystem;
    
    // Start is called before the first frame update
    void Start(){
        rocketRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngine);
        }

        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticle.Stop();
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void StartLeftRotation()
    {
        ApplyRotation(RotationThrust);
        rightBoosterParticle.Play();
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
    }
    
    void StartRightRotation()
    {
        ApplyRotation(-RotationThrust);
        leftBoosterParticle.Play();
        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
    }
    
    void StopRotation()
    {
        leftBoosterParticle.Stop();
        rightBoosterParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame) 
    {
        rocketRigidBody.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidBody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}