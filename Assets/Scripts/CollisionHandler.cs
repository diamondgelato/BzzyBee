using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] float delayTime = 0.5f;

    private int currSceneInd;
    private int nextSceneInd;
    AudioSource aus;
    Collider coll;

    bool isTransition = false;
    bool collisionDisabled = false;

    void Start() {
        aus = GetComponent<AudioSource>();
    }

    void Update() {
        CheckDebugCode();
    }

    void OnCollisionEnter(Collision other) {
        if (isTransition || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("touched friendly");
                break;

            case "Finish":
                Debug.Log("course finished yay");
                SuccessHandler();
                break;

            case "Pollen":
                Debug.Log("power up!");
                break;

            case "Obstacle":
                Debug.Log("lmao ded");
                CrashHandler();
                break;

            default:
                break;
        }
    } 

    void CheckDebugCode() {
        coll = GetComponent<Collider>();

        if (Input.GetKey(KeyCode.L)) {
            // Level up
            Debug.Log("CHEAT: Level up");
            LoadNextLevel();
        } else if (Input.GetKey(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
            Debug.Log("CHEAT: Disable Collision");            
        }
    }

    void CrashHandler () {
        isTransition = true;

        // TODO: add sound fx
        aus.PlayOneShot(death);

        // TODO: add particle effect
        deathParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void SuccessHandler () {
        isTransition = true;

        // TODO: add sound fx
        aus.PlayOneShot(success);

        // TODO: add particle effect
        successParticles.Play();
        
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    void ReloadLevel () {
        currSceneInd = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneInd);
    }

    void LoadNextLevel() {
        nextSceneInd = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneInd == SceneManager.sceneCountInBuildSettings) {
            nextSceneInd = 0;
        }

        SceneManager.LoadScene(nextSceneInd);
    }
}
