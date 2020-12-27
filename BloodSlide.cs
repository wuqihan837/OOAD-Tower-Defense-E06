using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSlide : MonoBehaviour
{
    private GameObject myCamera;

    private void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void Update()
    {
        //transform.LookAt(myCamera.transform.position);
        Vector3 dir = myCamera.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotateAngle = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotateAngle.x,0.0f, 0.0f);
    }
}
