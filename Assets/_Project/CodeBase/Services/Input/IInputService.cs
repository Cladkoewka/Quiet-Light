
using UnityEngine;

namespace _Project.CodeBase.Services.Input
{
    public interface IInputService
    {
        Vector3 MoveInput { get; }
        Vector3 CameraInput { get; }
        bool IsInterractButtonDown();
    }
}