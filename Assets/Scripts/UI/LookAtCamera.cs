using UnityEngine;

namespace UI {
public sealed class LookAtCamera : MonoBehaviour {
    [SerializeField] private Mode _mode;

    private enum Mode {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    private Camera _camera;

    private void Awake() {
        _camera = Camera.main;
    }

    private void LateUpdate() {
        Transform cameraTransform = _camera.transform;
        switch (_mode) {
            case Mode.LookAt:
                transform.LookAt(cameraTransform);
                break;
            case Mode.LookAtInverted:
                Vector3 position = transform.position;
                Vector3 directionFromCamera = position - cameraTransform.position;
                transform.LookAt(position + directionFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = cameraTransform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -cameraTransform.forward;
                break;
            default:
                Debug.LogError("Unimplemented LookAtCamera Mode! Should not be called.");
                break;
        }
    }
}
}