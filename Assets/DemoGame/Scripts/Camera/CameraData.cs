using System;
using Cinemachine;
namespace DemoGame.Scripts.Camera
{
    [Serializable]
    public class CameraData
    {
        public CameraType cameraType;
        public CinemachineVirtualCamera camera;
    }
}