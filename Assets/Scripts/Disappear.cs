using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    [SerializeField] bool isActive = true;
    [SerializeField] float period = 2f;

    MeshRenderer mesh;
    Collider coll;

    int lastQuotient = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();

        mesh.enabled = isActive;
        coll.enabled = isActive;

        lastQuotient = (int) (Time.time / period);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time / period) > lastQuotient) {
            isActive = !isActive;
            mesh.enabled = isActive;
            coll.enabled = isActive;
            
            lastQuotient++;
        } else {
            mesh.enabled = isActive;
            coll.enabled = isActive;
        }
    }
}
