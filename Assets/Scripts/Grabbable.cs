using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grabbable : MonoBehaviour
{
    public XRNode handType;
    bool grip = false;
    public bool isNotGrabbedYet = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice hand = InputDevices.GetDeviceAtXRNode(handType);
        hand.TryGetFeatureValue(CommonUsages.gripButton, out grip);

        if(grip)
        {
            Collider[] overlapCollisions = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (Collider o in overlapCollisions)
            {
                if (o.gameObject.GetComponent<Grabbable>() != null && !isNotGrabbedYet)
                {
                    o.gameObject.transform.SetParent(transform);
                    GameManager.instance.gameStarted = true;
                    isNotGrabbedYet = true;
                }
            }
        }
    }
}
