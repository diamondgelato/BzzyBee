using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Vector3 movementVec = Vector3.left;
    [SerializeField] float movementFactor = 0f;
    [SerializeField] int period = 2;

    const float Tau = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles;

        if (period > Mathf.Epsilon) {
            cycles = Time.time / period;              // continually increasing
        } else {
            cycles = 0;
        }
        
        float rawSinWave = Mathf.Sin(cycles * Tau);     // [-1, 1]
        movementFactor = (rawSinWave + 1) / 2;          // [0, 1]

        Vector3 offset = movementFactor * movementVec;
        transform.position = startPos + offset;
    }
}
