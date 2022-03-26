using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerAnimations : MonoBehaviour
{
    private Animator _animator;

    private bool _jump, _fall, _open;

    public void SetJump(bool input)
    {
        _jump = input;
    }
    
    public void SetFall(bool input)
    {
        _fall = input;
    }
    
    public void SetOpen(bool input)
    {
        _open = input;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (_open)
            _animator.SetTrigger("Open");
        else if (_fall)
            _animator.SetTrigger("Fall");
        else if (_jump)
            _animator.SetTrigger("Jump");
        else
            _animator.SetTrigger("Idle");
    }
}
