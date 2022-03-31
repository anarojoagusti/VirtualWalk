using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform eyeCenter;
    private Vector3 iniPosition;
    private void Start()
    {
        iniPosition = this.transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(iniPosition.x + eyeCenter.localPosition.x, iniPosition.y + eyeCenter.localPosition.y, iniPosition.z + eyeCenter.localPosition.z);
    }
}
