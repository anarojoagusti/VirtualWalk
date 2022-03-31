using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandsAnimator : MonoBehaviour
{
    public Animator handAnimator;
    public XRNode handType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool grip = false;
        bool trigger = false;
        bool primaryAxisTouch = false; // joystick
        bool primaryTouch = false; // A button
        bool secondaryTouch = false; // B button
        float triggerDown = 0f;

        InputDevice hand = InputDevices.GetDeviceAtXRNode(handType);

        hand.TryGetFeatureValue(CommonUsages.trigger, out triggerDown);
        hand.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        hand.TryGetFeatureValue(CommonUsages.gripButton, out grip);
        hand.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out primaryAxisTouch);
        hand.TryGetFeatureValue(CommonUsages.primaryTouch, out primaryTouch);
        hand.TryGetFeatureValue(CommonUsages.secondary2DAxisTouch, out secondaryTouch);

        bool fingerDown = primaryAxisTouch | primaryTouch | secondaryTouch;

        float triggerTotal = 0f;

        if(trigger)
        {
            triggerTotal = 0.1f;
        }

        if (triggerDown > triggerTotal)
        {
            triggerTotal = triggerDown;
        }

        handAnimator.SetBool("GrabbingGrip", grip);
        handAnimator.SetBool("ThumbUp", fingerDown);
        handAnimator.SetFloat("TriggerDown", triggerDown);
    }
}
