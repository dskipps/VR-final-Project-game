using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class Hand : XRDirectInteractor
    {

        public GameObject LHand;
        public GameObject RHand;

        private SkinnedMeshRenderer meshRenderer = null;

        protected override void Awake()
        {
            base.Awake();
            meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public void SetVisibility(bool value)
        {
            //LHand.SetActive(value);
            //RHand.SetActive(value);

            // meshRenderer.enabled = value;
        }
    }
}