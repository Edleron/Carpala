namespace Edleron.Input
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    [DefaultExecutionOrder(-1)]
    public class InputManager : Singleton<InputManager>
    {
        #region Events
        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;
        public delegate void EndTouch(Vector2 position, float time);
        public event EndTouch OnEndTouch;
        public delegate void PressTouch(Vector2 position);
        public event PressTouch OnPressTouch;
        #endregion

        private PlayerControls playerControl;
        private Camera mainCamera;


        private void Awake()
        {
            playerControl = new PlayerControls();
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            playerControl.Enable();
        }

        private void OnDisable()
        {
            playerControl.Disable();
        }

        private void Start()
        {
            playerControl.Touch.TouchPress.performed += ctx => TouchPressed(ctx);
            playerControl.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
            playerControl.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        }

        public void TouchPressed(InputAction.CallbackContext context)
        {
            if (OnPressTouch != null) OnPressTouch(Utils.ScreenToWorld(mainCamera, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>()));

            // float value = context.ReadValue<float>();
            // Debug.Log(value);
        }

        private void StartTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }

        private void EndTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }

        public Vector2 PrimaryPosition()
        {
            return Utils.ScreenToWorld(mainCamera, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>());
        }
    }
}