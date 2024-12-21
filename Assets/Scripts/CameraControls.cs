//MIT License
//Copyright (c) 2023 DA LAB (https://www.youtube.com/@DA-LAB)
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControls : MonoBehaviour {
    [SerializeField] private float _rotationSpeed = 500.0f;
    [SerializeField] private float _zoomMin = 0.5f;
    [SerializeField] private float _zoomMax = 100.0f;
    [SerializeField] private bool _enableOrbit = false;
    [SerializeField] private bool _enablePan = true;
    [SerializeField] private bool _enableZoom = true;


    private Vector3 _mouseWorldPosStart;
    private float _zoomScale = 5.0f;
    private Camera _camera;


    private void Start() {
        _camera = Camera.main;
    }

    private void Update() {
        // Check for Orbit, Pan, Zoom
        if (_enableOrbit && (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Mouse2)) &&
            !EventSystem.current.IsPointerOverGameObject()) //Check for orbit
        {
            CamOrbit();
        }

        if (_enablePan) {
            if ((Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(0)) &&
                !Input.GetKey(KeyCode.LeftShift) &&
                !EventSystem.current.IsPointerOverGameObject()) //Check for Pan
            {
                _mouseWorldPosStart = _camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if ((Input.GetMouseButton(2) || Input.GetMouseButton(0)) &&
                !Input.GetKey(KeyCode.LeftShift) &&
                !EventSystem.current.IsPointerOverGameObject()) {
                Pan();
            }
        }

        if (_enableZoom && !EventSystem.current.IsPointerOverGameObject()) {
            Zoom(Input.GetAxis("Mouse ScrollWheel")); //Check for Zoom
        }
    }


    private void CamOrbit() {
        if (Input.GetAxis("Mouse Y") == 0 && Input.GetAxis("Mouse X") == 0) {
            return;
        }

        float verticalInput = Input.GetAxis("Mouse Y") * _rotationSpeed * Time.deltaTime;
        float horizontalInput = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right, -verticalInput);
        transform.Rotate(Vector3.up, horizontalInput, Space.World);
    }

    private void Pan() {
        if (Input.GetAxis("Mouse Y") == 0 && Input.GetAxis("Mouse X") == 0) {
            return;
        }

        Vector3 mouseWorldPosDiff = _mouseWorldPosStart - _camera.ScreenToWorldPoint(Input.mousePosition);
        _camera.transform.position += mouseWorldPosDiff;
    }

    private void Zoom(float zoomDiff) {
        if (zoomDiff == 0) {
            return;
        }

        _mouseWorldPosStart = _camera.ScreenToWorldPoint(Input.mousePosition);
        _camera.orthographicSize =
            Mathf.Clamp(_camera.orthographicSize - zoomDiff * _zoomScale, _zoomMin, _zoomMax);
        Vector3 mouseWorldPosDiff = _mouseWorldPosStart - _camera.ScreenToWorldPoint(Input.mousePosition);
        _camera.transform.position += mouseWorldPosDiff;
    }
}