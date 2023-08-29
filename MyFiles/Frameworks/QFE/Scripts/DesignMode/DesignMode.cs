using System;
using System.Collections.Generic;

namespace CallPalCatGames.QFrameworkExtension
{
    #region 简单工厂

    public interface ISimpleFactory<T>
    {
        T GetThing(string className);
    }

    #endregion

    #region 工厂方法

    public interface IFactoryMethod<T> where T : class
    {
        T GetThing();
    }

    #endregion

    #region 抽象工厂

    public interface IAbstractFactory<T1, T2> where T1 : class where T2 : class
    {
        T1 GetThing1();
        T2 GetThing2();
    }

    #endregion

    #region 建造者

    public interface IBuilderBuilder<T>
    {
        IBuilderThing<T> _BuilderThing { get; set; }
        void BuildStep1();
        void BuildStep2();
        IBuilderThing<T> GetBuild();
    }

    public interface IBuilderDirector<T> where T : class
    {
        void DirectBuild(T builder);
    }

    public interface IBuilderThing<T>
    {
        IList<T> _parts { get; set; }
        void AddPart(T partValue);
        void ShowParts();
    }

    #endregion

    #region 原型模式

    public interface IPrototype<T> where T : class
    {
        T GetCopy();
    }

    #endregion

    #region 适配器模式

    public interface IAdaptiveTarget
    {
        IAdaptiveOffer _AdaptiveOffer { get; set; }
        void Request();
    }

    public interface IAdaptiveOffer
    {
        void SpecificRequest();
    }

    #endregion

    #region 桥接

    public interface IBridgingAbstract
    {
        IBridgingRealize _BridgingRealize { get; set; }
        void AbstractFeature1();
        void AbstractFeature2();
    }

    public interface IBridgingRealize
    {
        void RealizeFeature1();
        void RealizeFeature2();
    }

    #endregion

    #region 装饰

    public interface IDecorateComponent
    {
        void ComponentFeature();
    }

    public interface IDecorate : IDecorateComponent
    {
        IDecorateComponent _DecorateComponent { get; set; }
    }

    #endregion

    #region 组合

    public interface ICombinationLimbNode<T>
    {
        IList<ICombinationNode<T>> _childs { get; set; }
        void AddNode(ICombinationNode<T> addNode);
        void RemoveNode(ICombinationNode<T> delNode);
        ICombinationNode<T> GetChildNode(int index);
    }

    public interface ICombinationNode<T>
    {
        T value { get; set; }
        void ShowNode();
    }

    public interface ICombinationLeaf<T> : ICombinationNode<T>
    {
    }

    #endregion

    #region 代理模式

    public interface IAgentTheme
    {
        void AgentRequest();
    }

    public interface IAgentRealObject : IAgentTheme
    {
    }

    public interface IAgentObject : IAgentTheme
    {
        bool Fense { get; set; }
        IAgentRealObject AgentRealObject { get; set; }
    }

    #endregion

    #region 命令模式

    public interface ICommandReceiver<T>
    {
        void Execute(T value);
    }

    public interface ICommand<T>
    {
        ICommandReceiver<T> CommandReceiver { get; set; }
        T value { get; set; }
        void Execute(T value, ICommandReceiver<T> receiver);
    }

    public interface ICommandInvoker<T>
    {
        IList<ICommand<T>> Commands { get; set; }
        void AddCommand(ICommand<T> command);
        void RemoveCommand(ICommand<T> command);
        void ExecuteCommands();
    }

    #endregion

    #region 状态模式

    public interface IStateUpdateSubject
    {
        void StateUpdateRequest();
    }

    public interface IState : IStateUpdateSubject
    {
        IStateOwner StateOwner { get; set; }
        void Constructor(IStateOwner stateOwner);
    }

    public interface IStateOwner : IStateUpdateSubject
    {
        IState CurrentState { get; set; }
        void SetState(IState state);
    }

    #endregion

    #region 策略

    public interface IstrategySubject<T>
    {
        T GetCalculateResult(T handleValue);
    }

    public interface Istrategy<T> : IstrategySubject<T>
    {
    }

    public interface IstrategyOwner<T> : IstrategySubject<T>
    {
        Istrategy<T> CurrentStragety { get; set; }
        void SetStragety(Istrategy<T> strategy);
    }

    #endregion

    #region 责任链

    public interface ICOR<Trange, Tquest>
    {
        Trange Range { get; set; }
        ICOR<Trange, Tquest> Next { get; set; }
        void Init(Trange range, ICOR<Trange, Tquest> next);
        void CORRequst(Tquest quest);
        void CORRequstSelf(Tquest quest);
    }

    #endregion

    #region 访问者

    public interface IIVisitorElement
    {
        void Accept(IVisitorVisitor visitor);
        void ShowSelf();
    }

    public interface IVisitorElements
    {
        IList<IIVisitorElement> Elements { get; set; }
    }
    public interface IVisitorVisitor
    {
        void Visit(IIVisitorElement element);
    }

    #endregion

    #region 备忘录模式

    public interface IMementoInitiator<T> : IPrototype<List<T>>
    {
        IList<T> Data { get; set; }
        void SetMementoData(List<T> data);
        IMementoMemento<T> CreateMemento(); //创建备忘录，要深拷贝
        void RestoreMemento(IMementoMemento<T> mementoMemento); //恢复数据
    }

    public interface IMementoMemento<T> //如果备忘录需要管理多个，用字典
    {
        IList<T> Data { get; set; }
        void SetMementoData(List<T> data); //data是最底层数据，备忘录只是持有
    }

    public interface IMementoManager<T>
    {
         Dictionary<string, IMementoMemento<T>> DataDic { get; set; }
    }

    #endregion

    #region 观察者模式

    public interface IObserverSubject<T>
    {
        T SubjectState { get; set; }
        List<IObserverObserver<T>> Observers { get; set; }
        void AddObserver(IObserverObserver<T> observerObserver);
        void RemoveObserver(IObserverObserver<T> observerObserver);
        void Publish();
        void SetDataState();
        T GetDataState();
    }

    public interface IObserverObserver<T>
    {
        IObserverSubject<T> Subject { get; set; }
        void SetSubject(IObserverSubject<T> observerSubject);
        void Receive();
    }

    #endregion

    #region 享元模式

    public interface IFlyweight<in T>
    {
        void Operation(T value);
    }

    public interface IFlyweightShare<T>:IFlyweight<T>
    {
    }

    public interface IFlyweightUnShare<T> : IFlyweight<T>
    {
        
    }

    public interface IFlyweightFactory<T> : ISimpleFactory<T>
    {
        IFlyweightUnShare<T> GetUnshareThing();
    }

    #endregion
}