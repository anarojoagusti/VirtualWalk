using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEnter : MonoBehaviour
{
    public UnityEvent call2TCP;
    [HideInInspector] public bool isCeroTriggered;
    [HideInInspector] public float timer;
    private void Start()
    {
        isCeroTriggered = false;
        timer = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isCeroTriggered)
        {
            call2TCP.Invoke();
            isCeroTriggered = true;
        }
    }
    private void Update()
    {
        if (isCeroTriggered)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                isCeroTriggered = false;
                timer = 0;
            }
        }
    }
}
