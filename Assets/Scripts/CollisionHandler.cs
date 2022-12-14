using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    GameObject[] virtualCameras;
    GameObject followCamera;
    //test for patrick
    
    bool isTransitioning = false;
    bool collisionsDisabled = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        virtualCameras = GameObject.FindGameObjectsWithTag("Follow");
        if (virtualCameras.Length != 0){
            followCamera = virtualCameras[0];
        }
    }    

    void Update() {
        // RespondToDebugKeys();
    }

    /*
    void RespondToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)){
            //toggle
            collisionsDisabled = !collisionsDisabled;
        }
    }
    */

    void OnCollisionEnter(Collision other) {

        if (isTransitioning || collisionsDisabled){
            return;
        }

        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                //Load next level
                StartReloadSequence();
                break;

            default:
                //respawn rocket by reloading the level
                StartCrashSequence();
                break;
            
        }
    }

    void StartCrashSequence() {
        isTransitioning = true;
        followCamera.SetActive(false);
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayInSeconds);
    }

    void StartReloadSequence() {
        //todo: add particle effect
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayInSeconds);
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
