using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crasher : MonoBehaviour
{
    public GameObject m_crashBG;
    public GameObject m_crashWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator crashGame()
    {
        yield return new WaitForSeconds(1);
        Instantiate(m_crashBG);
        yield return new WaitForSeconds(3);
        Debug.Log("crashBG waited");
        Instantiate(m_crashWindow);
    }
}
