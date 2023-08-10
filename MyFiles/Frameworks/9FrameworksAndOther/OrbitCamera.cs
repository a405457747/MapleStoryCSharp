using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
    [SerializeField] private Transform verticalAxis;
    [SerializeField] private Transform horizontalAxis;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float hSpeed = 10;
    [SerializeField] private float vSpeed = 10;
    [SerializeField] private float mobileZoomSpeed = 40;
    [SerializeField] private float desktopZoomSpeed = 200;
    [SerializeField] private float maxVerticalAngle = 80;
    [SerializeField] private float minVerticalAngle = -30;

    //Desktop
    protected bool buttonDown = false;
    protected Vector3 lastPos = Vector3.zero;

    protected virtual void Start()
    {
        mainCam.transform.LookAt(transform.position);
        float distance = Vector3.Distance(mainCam.transform.position, transform.position);
        if (distance > maxDistance) {
            mainCam.transform.position = transform.position - mainCam.transform.forward.normalized * maxDistance;
        } else if (distance < minDistance) {
            mainCam.transform.position = transform.position - mainCam.transform.forward.normalized * minDistance;
        }

    }

    protected virtual void LateUpdate()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            InputMobile();
        } else {
            InputDesktop();
        }
    }

    protected virtual void InputDesktop()
    {
        if (!buttonDown && Input.GetMouseButtonDown(0)) {
            buttonDown = true;
            lastPos = Input.mousePosition;
        } else if (buttonDown && !Input.GetMouseButtonUp(0)) {
            Vector3 currentPos = Input.mousePosition;
            float deltaV = currentPos.x - lastPos.x;
            float deltaH = currentPos.y - lastPos.y;
            horizontalAxis.Rotate(Vector3.up, deltaV * hSpeed * Time.deltaTime);
            verticalAxis.Rotate(Vector3.right, -deltaH * Time.deltaTime * vSpeed);
            Vector3 vAngles = verticalAxis.localRotation.eulerAngles;
            vAngles.y = vAngles.z = 0;
            if (vAngles.x > 180) {
                vAngles.x -= 360;
            } else if (vAngles.x < -180) {
                vAngles.x += 360;
            }
            if (vAngles.x < minVerticalAngle) {
                vAngles.x = minVerticalAngle;
            } else if (vAngles.x > maxVerticalAngle) {
                vAngles.x = maxVerticalAngle;
            }
            verticalAxis.localRotation = Quaternion.Euler(vAngles);
            lastPos = currentPos;
        } else if (buttonDown && Input.GetMouseButtonUp(0)) {
            buttonDown = false;
            Debug.Log("buttonDown set to false");
        }
        if (Input.GetMouseButtonUp(0)){
            Debug.Log("button up");
        }
        Zoom(Input.mouseScrollDelta.y, desktopZoomSpeed);

    }

    protected virtual void InputMobile()
    {
        if (Input.touchCount == 1) {
            Touch finger1 = Input.GetTouch(0);
            horizontalAxis.Rotate(Vector3.up, finger1.deltaPosition.x * hSpeed * finger1.deltaTime);
            verticalAxis.Rotate(Vector3.right, -finger1.deltaPosition.y * vSpeed * finger1.deltaTime);
            Debug.Log("delta V = " + (-finger1.deltaPosition.y * vSpeed));
            Vector3 vAngles = verticalAxis.localRotation.eulerAngles;
            vAngles.z = 0;
            if (vAngles.x > 180) {
                vAngles.x -= 360;
            } else if (vAngles.x < -180) {
                vAngles.x += 360;
            }
            if (vAngles.x < minVerticalAngle) {
                vAngles.x = minVerticalAngle;
            } else if (vAngles.x > maxVerticalAngle) {
                vAngles.x = maxVerticalAngle;
            }
            Debug.Log("rotating on mobile");
            verticalAxis.localRotation = Quaternion.Euler(vAngles);
        } else if (Input.touchCount >= 2) {
            Touch finger = Input.GetTouch(0);
            float delta = Mathf.Max(Mathf.Abs(finger.deltaPosition.x), Mathf.Abs(finger.deltaPosition.y));
            if (IsZoomingIn()) {
                Zoom(delta, mobileZoomSpeed);
            } else if (IsZoomingOut()) {
                Zoom(-delta, mobileZoomSpeed);
            }
        }
    }

    private void Zoom(float rawDelta, float zoomSpeed)
    {
        float distance, delta;
        if (rawDelta > 0) {
            distance = Vector3.Distance(mainCam.transform.position, transform.position);
            delta = rawDelta * zoomSpeed * Time.deltaTime;
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, transform.position, Mathf.Min(delta, distance));
        } else if (rawDelta < 0) {
            distance = maxDistance - Vector3.Distance(mainCam.transform.position, transform.position);
            delta = rawDelta * zoomSpeed * Time.deltaTime;
            float minDelta = Mathf.Min(distance, Mathf.Abs(delta));
            if (minDelta > 0) {
                mainCam.transform.position -= mainCam.transform.forward.normalized * minDelta;
            }
        }
    }

    private bool IsZoomingIn()
    {
        Touch finger1 = Input.GetTouch(0);
        Touch finger2 = Input.GetTouch(1);
        if (finger1.deltaPosition.x < 0 && finger2.deltaPosition.x > 0 && finger1.position.x < finger2.position.x) {
            return true;
        }

        if (finger1.deltaPosition.x > 0 && finger2.deltaPosition.x < 0 && finger1.position.x > finger2.position.x) {
            return true;
        }

        if (finger1.deltaPosition.y > 0 && finger2.deltaPosition.y < 0 && finger1.position.y > finger2.position.y) {
            return true;
        }

        if (finger1.deltaPosition.y < 0 && finger2.deltaPosition.y > 0 && finger2.position.y > finger1.position.y) {
            return true;
        }
        return false;
    }

    private bool IsZoomingOut()
    {
        Touch finger1 = Input.GetTouch(0);
        Touch finger2 = Input.GetTouch(1);

        if (finger1.deltaPosition.x > 0 && finger2.deltaPosition.x < 0 && finger1.position.x < finger2.position.x) {
            return true;
        }
        if (finger1.deltaPosition.x < 0 && finger2.deltaPosition.x > 0 && finger2.position.x < finger1.position.x) {
            return true;
        }

        if (finger1.deltaPosition.y < 0 && finger2.deltaPosition.y > 0 && finger2.position.y < finger1.position.y) {
            return true;
        }
        if (finger1.deltaPosition.y > 0 && finger2.deltaPosition.y < 0 && finger1.position.y < finger2.position.y) {
            return true;
        }
        return false;
    }
}
