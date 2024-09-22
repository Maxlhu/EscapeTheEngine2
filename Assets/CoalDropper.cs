using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalDropper : MonoBehaviour
{
    public GameObject m_Coal;
    public GameObject m_BuggedCoal1;
    public GameObject m_BuggedCoal2;
    public GameObject m_BuggedCoal3;
    public GameObject m_ultraBuggedCoal;
    public Crasher m_Crasher;
    public int m_CoalCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void drop()
    {
        switch (m_CoalCounter)
        {
            case < 10:
                Instantiate(m_Coal);
                m_CoalCounter++;
                break;
            case < 11:
                Instantiate(m_BuggedCoal1);
                m_CoalCounter++;
                break;
            case < 12:
                Instantiate(m_BuggedCoal2);
                m_CoalCounter++;
                break;
            case < 13:
                Instantiate(m_BuggedCoal3);
                m_CoalCounter++;
                break;
            case < 14:
                Instantiate(m_ultraBuggedCoal);
                m_CoalCounter++;
                break;
            case < 15:
                Debug.Log("Crashed");
                StartCoroutine(m_Crasher.crashGame());
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
