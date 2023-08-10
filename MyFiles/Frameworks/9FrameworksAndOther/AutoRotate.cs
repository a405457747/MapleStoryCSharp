using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xmaolol.com;

public class AutoRotate : MonoBehaviour
{
    public bool IsSpaceWorld;
    public float RotateSpeed = 600f;
    public AxisDirection axisDirection;
    private Vector3 axisVector3;

    void Awake()
    {
        axisDirection = AxisDirection.Up;
    }

    public AxisDirection AxisDirection
    {
        get { return axisDirection; }
        set
        {
            axisDirection = value;
            switch (axisDirection)
            {
                case AxisDirection.Forward:
                    axisVector3 = Vector3.forward;
                    break;

                case AxisDirection.Right:
                    axisVector3 = Vector3.right;
                    break;

                case AxisDirection.Up:
                    axisVector3 = Vector3.up;
                    break;
            }
        }
    }

    void Update()
    {
        if (IsSpaceWorld)
        {
            transform.Rotate(axisVector3, RotateSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Rotate(axisVector3, RotateSpeed * Time.deltaTime, Space.Self);
        }
    }
}
