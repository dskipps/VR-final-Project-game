using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    //makes the input usable and customisable from unity for pinch
    public InputActionProperty pinchAnimationAction;
    //makes the input usable and customisable from unity for grip
    public InputActionProperty gripAnimationAction;

    //
    public Animator handAnimator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //will read input of trigger button
        float triggerval = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerval);
        //debug log for trigger button
        //Debug.log(triggerval);
        float gripVal = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripVal);

    }
}
