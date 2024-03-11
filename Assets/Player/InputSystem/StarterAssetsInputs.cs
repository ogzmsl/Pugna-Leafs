using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool Orbball;
        public bool Project_Tile;
        public bool fire;
        public bool Block;
        public bool mouseLeft;
        public bool Tab;
        public bool Intraction; 


        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

        public void TabInput(bool newTabState)
        {
            Tab = newTabState;
        }

        public void OnTab(InputValue value)
        {
            TabInput(value.isPressed);
        }

        public void IntractionInput(bool newIntractionState)
        {
            Intraction = newIntractionState;
        }

        public void OnIntraction(InputValue value)
        {
            IntractionInput(value.isPressed);
        }

        public void OrbballInput(bool newFireStateorball)
        {
            Orbball = newFireStateorball;
        }


        public void OnOrbball(InputValue value)
        {
            OrbballInput(value.isPressed);
        }

        public void Project_TileInput(bool newFireStateProject_Tile)
        {
            Project_Tile = newFireStateProject_Tile;
        }

        public void OnProject_Tile(InputValue value)
        {
            Project_TileInput(value.isPressed);
        }

        public void FireInput(bool newFireState)
        {
            fire = newFireState;
        }

        public void OnFire(InputValue value)
        {
            FireInput(value.isPressed);
        }

        public void OnBlock(InputValue value)
        {
            Block = value.isPressed;
        }

        public void OnMouseLeft(InputValue value)
        {
            MouseLeftInput(value.isPressed);
        }

        public void MouseLeftInput(bool newState)
        {
            mouseLeft = newState;
        }

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
