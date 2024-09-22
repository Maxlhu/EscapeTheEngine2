using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputHandler : MonoBehaviour
{
    public CoalDropper dropper;
    public Animator animator;
    private void OnMouseDown()
    {
        animator.SetBool("ClickLever", true);
        dropper.drop();
        animator.SetBool("ClickLever", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
