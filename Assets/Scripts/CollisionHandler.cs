using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;

    //test merge reset
    //yet another merge test
    

    void OnCollisionEnter(Collision other) {
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
        //todo: add crash sound affect
        //todo: add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayInSeconds);
    }

    void StartReloadSequence() {
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
