using DG.Tweening;
using UnityEngine;


public enum PanelTier
{
    Default,
    PopUp,
    AlwaysInFront,
    Guide,
    Effect,
    Curtain
}
public abstract class PanelArgs
{
}

[RequireComponent(typeof(HaveEvents))]
public abstract class Panel : MonoBehaviour
{
    protected Tween openTween;
    protected Game game;
    protected PanelTier tier;

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    
    public virtual void Initialize()
    {
        game = FindObjectOfType<Game>();
        transform.SetParent(Game.CanvasTrans.Find(tier.ToString()), false);
        Hide();
    }
    
    public virtual void Release()
    {
    }

    public virtual void EachFrame()
    {
    }
}