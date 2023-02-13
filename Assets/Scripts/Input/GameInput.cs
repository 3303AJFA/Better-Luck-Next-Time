using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance;

        public InputActions inputActions { get; private set; }

        private void Awake()
        {
            Instance = this;

            inputActions = new InputActions();
        }
        private void Start()
        {
            EnablePlayerActionMap();
        }

        public void EnablePlayerActionMap()
        {
            inputActions.Player.Enable();
        }
    }
}
