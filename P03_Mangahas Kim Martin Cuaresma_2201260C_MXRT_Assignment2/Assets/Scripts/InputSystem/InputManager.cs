using System.Collections;
using UnityEngine;

namespace Assets
{ 
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(menuName = "XR/AR Foundation/Input Action References")]

    public class InputManager : ScriptableObject
    {
        [SerializeField]
        InputActionProperty m_ScreenTap;

        [SerializeField]
        InputActionProperty m_ScreenTapPosition;

        public InputActionProperty ScreenTap { get => m_ScreenTap; set => m_ScreenTap = value; }
        public InputActionProperty ScreenTapPosition { get => m_ScreenTapPosition; set => m_ScreenTapPosition = value; }
    }
}