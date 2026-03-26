using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lilBroDialogue : MonoBehaviour
{
    public bool enter;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enter)
        {
            text.SetActive(true);
        }
        else
        {
            text.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        enter = true;
    }

    private void OnTriggerExit(Collider other)
    {
        enter = false;
    }
}
