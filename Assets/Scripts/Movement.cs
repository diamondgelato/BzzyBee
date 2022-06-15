using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrust = 5f;
    [SerializeField] float angularThrust = 5f;
    Vector3 angularForce;
    Vector3 normalForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        angularForce = Vector3.forward * angularThrust * Time.deltaTime;
        normalForce = Vector3.right * thrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            // Debug.Log("Going up");
            rb.AddRelativeForce(normalForce);
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            rb.AddRelativeForce(-normalForce);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            // freeze rotation to remove unwanted force rotation
            rb.freezeRotation = true;
            transform.Rotate(angularForce);
            // unfreeze rotation to continue game
            rb.freezeRotation = false;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            // transform.Translate(2, 0, 0);
            rb.freezeRotation = true;
            transform.Rotate(-angularForce);
            rb.freezeRotation = false;
        }
    }
}
