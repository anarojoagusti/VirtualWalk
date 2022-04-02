using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;

namespace OculusSampleFramework
{
    public class PanelController : MonoBehaviour
    {
        public Animator animPanel;
        bool isShown;
        private void Start()
        {
            isShown = true;
        }
        private void OpenClosePanel()
        {
            isShown = !isShown;
            animPanel.SetBool("isShown", isShown);
        }

        public void SmokeButtonStateChanged(InteractableStateArgs obj)
        {
            if (obj.NewInteractableState == InteractableState.ActionState)
            {
                OpenClosePanel();
            }
        }
    }
}
