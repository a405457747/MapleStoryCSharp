using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetBy2D : MonoBehaviour
{
    public void LookAtTarget(Vector3 targetPos)
    {
        Vector2 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
