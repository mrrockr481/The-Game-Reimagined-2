using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBegin : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 20;
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
        rb.velocity = new Vector3(0f, speed, 0);
    }
}
