using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float period = 2f;
    [SerializeField] float speed = 10f;
    // [SerializeField] Vector3 axis = new Vector3(1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cycles;
        cycles = Mathf.RoundToInt(Time.time / period);

        // get rotation axis
        
        // rotate around axis
        if (cycles % 2 == 0) {
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        } else {
            transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }
    }
}
