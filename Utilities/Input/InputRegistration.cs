using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
namespace Input { 
public class InputRegistration
{

#if USE_NEW_INPUT


        private Actions controls;

        // Track which instance of this script registered the inputs, to prevent
        // another instance from accidentally unregistering them.
        protected static bool isRegistered = false;
        private bool didIRegister = false;

        void Awake()
        {
            controls = new Actions();
        }

        void OnEnable()
        {
            if (!isRegistered)
            {
                isRegistered = true;
                didIRegister = true;
                controls.Enable();
                 
                InputDeviceManager.RegisterInputAction("Enter", controls.Player.Enter);
            }
        }

        void OnDisable()
        {
            if (didIRegister)
            {
                isRegistered = false;
                didIRegister = false;
                controls.Disable();
                InputDeviceManager.UnregisterInputAction("Enter");
            }
        }

#endif

    }


}
