using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    AudioSource audioSource;
    //test for patrick
    
    bool isTransitioning = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }    

    void OnCollisionEnter(Collision other) {

        if (isTransitioning){
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
        //todo: add particle effect
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        Debug.Log(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayInSeconds);
    }

    void StartReloadSequence() {
        //todo: add particle effect
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
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
