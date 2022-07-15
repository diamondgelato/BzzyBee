using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust = 5f;
    [SerializeField] float angularThrust = 5f;
    [SerializeField] AudioClip buzz;

    Vector3 angularForce;
    Vector3 normalForce;

    Rigidbody rb;
    AudioSource aus;
    [SerializeField] ParticleSystem main;
    [SerializeField] ParticleSystem left;
    [SerializeField] ParticleSystem right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aus = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessAngularThrust();
        // CheckDebugCode();
    }

    void ProcessThrust () {
        normalForce = Vector3.right * thrust * Time.deltaTime;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            StartThrust();
        } else {
            StopThrust();
        }
    }

    void ProcessAngularThrust() {
        angularForce = Vector3.forward * angularThrust * Time.deltaTime;

        // freeze rotation to remove unwanted force rotation
        rb.freezeRotation = true;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {    
            RotateLeft();
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            RotateRight();
        } else {
            StopRotate();
        }

        // unfreeze rotation to continue game
        rb.freezeRotation = false;
    }

    void StartThrust() {
        // Audio and Particles
        if(!aus.isPlaying)
            aus.PlayOneShot(buzz);
        if(!main.isPlaying)
            main.Play();

        // Force
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            rb.AddRelativeForce(normalForce);
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            rb.AddRelativeForce(-normalForce);
        }
    }

    void StopThrust() {
        aus.Stop();
        main.Stop();
    }

    void RotateLeft () {
        left.Stop();
        if(!right.isPlaying)
            right.Play();
        transform.Rotate(angularForce);
    }

    void RotateRight () {
        right.Stop();
        if(!left.isPlaying)
            left.Play();
            
        transform.Rotate(-angularForce);
    }

    void StopRotate() {
        left.Stop();
        right.Stop();
    }
}
