using UnityEngine;

public static class RectTransformExpand
{
    public static RectTransform SetWidth(this RectTransform rect, float x)
    {
        var sizeDelta = rect.sizeDelta;
        sizeDelta = new Vector2(x, sizeDelta.y);
        rect.sizeDelta = sizeDelta;
        return rect;
    }

    public static RectTransform SetHeight(this RectTransform rect, float y)
    {
        var sizeDelta = rect.sizeDelta;
        sizeDelta = new Vector2(sizeDelta.x, y);
        rect.sizeDelta = sizeDelta;
        return rect;
    }

    public static RectTransform SetPosX(this RectTransform rect, float val)
    {
        var anchoredPosition3D = rect.anchoredPosition3D;
        anchoredPosition3D = new Vector3(val, anchoredPosition3D.y, anchoredPosition3D.z);
        rect.anchoredPosition3D = anchoredPosition3D;
        return rect;
    }

    public static RectTransform SetPosY(this RectTransform rect, float val)
    {
        var anchoredPosition3D = rect.anchoredPosition3D;
        anchoredPosition3D = new Vector3(anchoredPosition3D.x, val, anchoredPosition3D.z);
        rect.anchoredPosition3D = anchoredPosition3D;
        return rect;
    }
}