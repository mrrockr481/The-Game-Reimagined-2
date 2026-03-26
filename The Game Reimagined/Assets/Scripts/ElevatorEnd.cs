using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnd : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
    }
}
