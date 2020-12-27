using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAnimator : MonoBehaviour
{
    private Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        ani.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}
