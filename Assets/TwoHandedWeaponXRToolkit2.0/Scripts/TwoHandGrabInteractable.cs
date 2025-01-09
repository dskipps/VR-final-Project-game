using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class TwoHandGrabInteractable : XRGrabInteractable
    {
        public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
        private IXRSelectInteractor firstInteractor, secondInteractor;


        private void Start()
        {
            foreach (var item in secondHandGrabPoints)
            {
                item.selectEntered.AddListener(OnSecondHandGrab);
                item.selectExited.AddListener(OnSecondHandRelease);
            }
        }

        public void GrabPointsDeactivate()
        {
            foreach (var grabPoint in secondHandGrabPoints)
            {
                grabPoint.transform.gameObject.SetActive(false);
            }
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            // compute rotation
            if (firstInteractor != null && secondInteractor != null)
            {
                firstInteractor.transform.rotation = Quaternion.LookRotation(secondInteractor.transform.position - firstInteractor.transform.position, firstInteractor.transform.up);

            }
            base.ProcessInteractable(updatePhase);
        }
        /*---------first interactor-----------*/
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            firstInteractor = args.interactorObject;

            base.OnSelectEntered(args);
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {

            firstInteractor = null;
            secondInteractor = null;
            base.OnSelectExited(args);
        }
        /*-----------second intercator-----------------*/
        public void OnSecondHandGrab(SelectEnterEventArgs args) => secondInteractor = args.interactorObject;
        public void OnSecondHandRelease(SelectExitEventArgs args) => secondInteractor = null;
        // have an error, grab and realse at same time
        /*
        public override bool IsSelectableBy(IXRSelectInteractor interactor)
        {
            bool isalreadygrabbed = isSelected && !interactor.Equals(isSelected);
            return base.IsSelectableBy(interactor) &&!isalreadygrabbed;
        }*/
    }
}
