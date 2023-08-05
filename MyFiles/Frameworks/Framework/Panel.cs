using UnityEngine;

public abstract class PanelArgs
{
}

public abstract class Panel : MonoBehaviour
{
    protected PanelArgs args;

    public virtual void OnInit(PanelArgs arguments)
    {
        args = arguments;
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