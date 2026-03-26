using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caught : MonoBehaviour
{
    public GameObject text;
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
        text.SetActive(true);
        Invoke("caught", 2f);
    }

    void caught()
    {
        text.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
