using UnityEngine;

// 遊戲子系統共用界面
[RequireComponent(typeof(HaveEvents))]
public abstract class GameSystem : MonoBehaviour
{
    protected Game game;

    public GameSystem(Game game)
    {
        this.game = game;
    }

    public virtual void Initialize()
    {
        game = FindObjectOfType<Game>();
    }

    public virtual void Release()
    {
    }

    public virtual void EachFrame()
    {
    }
}