using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnimation : AnimVarible
{
    // Start is called before the first frame update

    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
