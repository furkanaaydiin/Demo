using System.Linq;
using UnityEngine;

namespace DemoGame.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        [SerializeField] private CameraData[] cameraData;
        private CameraData _currentCam;
        private bool HasCam => _currentCam != null;

        private void Awake()
        {
            Instance = this;
            ChangeCamera(CameraType.StartCamera); //Oyun başında default olarak start kameraya geçer.
        } 
   
        // Camera Type'a göre kamera değiştirmeye yarar.
        public void ChangeCamera(CameraType cameraType)
        {
            if (HasCam)
                _currentCam.camera.enabled = false;
            _currentCam = cameraData.FirstOrDefault(x => x.cameraType == cameraType);
            _currentCam!.camera.enabled = true;
        }
    }
}
