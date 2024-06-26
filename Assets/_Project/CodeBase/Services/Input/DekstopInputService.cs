using UnityEngine;

namespace _Project.CodeBase.Services.Input
{
    public class DekstopInputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        private const string InteractButton = "Fire1";

        public Vector3 MoveInput =>
            new Vector3(UnityEngine.Input.GetAxis(Horizontal), 0, UnityEngine.Input.GetAxis(Vertical));

        public Vector3 CameraInput =>
            new Vector3(UnityEngine.Input.GetAxis(MouseX), 0, UnityEngine.Input.GetAxis(MouseY));

        public bool IsInterractButtonDown() => UnityEngine.Input.GetButtonDown(InteractButton);
    }
}