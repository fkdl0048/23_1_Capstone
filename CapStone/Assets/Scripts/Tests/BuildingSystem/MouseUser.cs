using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public enum MouseButton
    {
        Left, Right
    }
    public class MouseUser : MonoBehaviour
    {
        private InputActions _inputActions;

        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseInWorldPosition => Camera.main.ScreenToWorldPoint(MousePosition);

        private bool _isLeftButtonPressed;
        private bool _isRightButtonPressed;

        private void OnEnable()
        {
            _inputActions = InputActions.Instance;
            _inputActions.Player.MousePosition.performed += OnMousePositionPerformed;
            _inputActions.Player.PerformAction.performed += OnPerformActionPerformed;
            _inputActions.Player.PerformAction.canceled += OnPerformActionCanceled;
            _inputActions.Player.CancelAction.performed += OnCancelActionPerformed;
            _inputActions.Player.CancelAction.canceled += OnCancelActionCanceled;

        }

        private void OnDisable()
        {
            _inputActions.Player.MousePosition.performed -= OnMousePositionPerformed;
            _inputActions.Player.PerformAction.performed -= OnPerformActionPerformed;
            _inputActions.Player.PerformAction.canceled -= OnPerformActionCanceled;
            _inputActions.Player.CancelAction.performed -= OnCancelActionPerformed;
            _inputActions.Player.CancelAction.canceled -= OnCancelActionCanceled;
            _inputActions.Player.CancelAction.canceled -= OnCancelActionCanceled;
        }

        private void OnMousePositionPerformed(InputAction.CallbackContext ctx)
        {
            MousePosition = ctx.ReadValue<Vector2>();
        }

        private void OnPerformActionPerformed(InputAction.CallbackContext ctx)
        {
            _isLeftButtonPressed = true;
        }

        private void OnPerformActionCanceled(InputAction.CallbackContext ctx)
        {
            _isLeftButtonPressed = false;
        }

        private void OnCancelActionPerformed(InputAction.CallbackContext ctx)
        {
            _isRightButtonPressed = true;
        }

        private void OnCancelActionCanceled(InputAction.CallbackContext ctx)
        {
            _isRightButtonPressed = false;
        }

        public bool IsMouseButtonPressed(MouseButton button)
        {
            return button == MouseButton.Left ? _isLeftButtonPressed : _isRightButtonPressed;
        }
    }
}
