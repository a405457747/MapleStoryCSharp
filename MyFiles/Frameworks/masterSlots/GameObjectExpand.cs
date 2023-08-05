using UnityEngine;

public static class GameObjectExpand
{
    public static int Number(this GameObject go)
    {
        return StringHelper.GetPureNumber(go.name);
    }

    public static T AddOrGetComponent<T>(this GameObject go) where T : Component
    {
        var temp = go.GetComponent<T>();

        if (temp == null) temp = go.AddComponent<T>();

        return temp;
    }

    public static GameObject Show(this GameObject go)
    {
        go.SetActive(true);
        return go;
    }

    public static GameObject Hide(this GameObject go)
    {
        go.SetActive(false);
        return go;
    }

    public static GameObject Name(this GameObject go, string name)
    {
        go.name = name;
        return go;
    }

    public static GameObject Layer(this GameObject go, string name)
    {
        go.layer = LayerMask.NameToLayer(name);
        return go;
    }
}