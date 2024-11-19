using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip nextLevel;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem nextLevelParticles;
    
    AudioSource audioSource;
    ParticleSystem particleSystem;

    private bool isTransitioning = false;
    private bool overPower = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatNextLevel();
        CheatOverPower();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return; 
        }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Other");
                if (!overPower)
                    StartCrashSequence();
                break;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Collision Detected, Reload Level");
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = ++currentSceneIndex;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        explosionParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(nextLevel);
        nextLevelParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void CheatNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Cheat Code Detected. NextLevel");
            nextLevelParticles.Play();
            Invoke("LoadNextLevel", levelLoadDelay);
        }
    }

    void CheatOverPower()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (overPower)
            {
                Debug.Log("OverPower Disabled");
                overPower = false;
            }
            else
            {
                Debug.Log("Cheat Code Detected. OverPower");
                overPower = true;
            }
        }
    }
}