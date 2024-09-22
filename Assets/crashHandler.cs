using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class crashHandler : MonoBehaviour
{

    // Start is called before the first frame update

    private void OnMouseDown()
    {
        Debug.Log("Changing scene");
        SceneManager.LoadScene("FinalScene");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
