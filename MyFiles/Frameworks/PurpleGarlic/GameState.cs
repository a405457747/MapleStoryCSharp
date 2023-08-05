namespace PurpleGarlic
{
    public abstract class GameState //只需要负责UI，和通知游戏开始，清理之类的工作，其余都交给Manager和事件处理，其实就是SceneManager，如果不需要加载场景，就是游戏状态呢
    {
        public string EnterMsg { get; protected set; }
        public string LeaveMsg { get; protected set; }

        public virtual void Enter()
        {
        }

        public virtual void Leave()
        {
        }

        public virtual void Tick()
        {
        }
    }
}