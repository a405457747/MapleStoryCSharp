using UnityEngine;

public abstract class PanelArgs
{
}
public class Panel : MonoBehaviour
{
    protected RectTransform rectTransform;
    protected PanelArgs args;
    protected Vector3 localPosition;

    public virtual void OnInit(PanelArgs arguments)
    {
        args = arguments;
        var transform1 = transform;
        rectTransform = transform1 as RectTransform;
        localPosition = transform1.localPosition;
    }

    public virtual void OnOpen(PanelArgs arguments)
    {
        args = arguments;
    }

    public virtual void OnClose()
    {
    }

    public virtual void OnRemove()
    {
    }
}