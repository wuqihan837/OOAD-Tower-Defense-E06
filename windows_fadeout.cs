using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windows_fadeout : MonoBehaviour
{   
    public GameObject windows_exit;

    private Animator windows_Animator;
    // Start is called before the first frame update
    void Awake() {
        windows_Animator = windows_exit.GetComponent<Animator>();
        windows_Animator.Play("Panel Out");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_exit_active(){
    
    }

}
