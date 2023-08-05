using UnityEngine;

public class GuideEventPenetrate : MonoBehaviour, ICanvasRaycastFilter
{
    private RectTransform _targetImage;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (_targetImage == null)
            return true;

        return !RectTransformUtility.RectangleContainsScreenPoint(_targetImage, sp, eventCamera);
    }

    public void SetTargetImage(RectTransform target)
    {
        _targetImage = target;
    }

    public Canvas GetCanvas()
    {
        return Game.CanvasTrans.GetComponent<Canvas>();
    }

    public static Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            world, canvas.GetComponent<Camera>(), out var pos);
        return pos;
    }
}