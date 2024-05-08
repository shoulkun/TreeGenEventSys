using System;
using System.Collections.Generic;


public class XEventManager : XSimpleSingleton<XEventManager>
{
    private readonly XEventGroup m_EventGroupRoot = new(XGroupID.Group_Root);
    public readonly XSignEvent EmptyCallBack = new XSignEvent();

    /// <summary>
    /// 添加 有参 事件监听器
    /// </summary>
    /// <typeparam name="T">事件参数类型</typeparam>
    /// <param name="eventId">事件ID</param>
    /// <param name="action">事件回调函数</param>
    /// <param name="subGroupIds">事件所在分组ID路径</param>
    public void AddEventListener<T>(int eventId, Action<T> action, params int[] subGroupIds)
    {
        m_EventGroupRoot.AddEventListener<T>((int)eventId, action, subGroupIds);
    }

    /// <summary>
    /// 添加 无参 事件监听器
    /// </summary>
    /// <param name="eventId">事件ID</param>
    /// <param name="action">事件回调函数</param>
    /// <param name="subGroupIds">事件所在分组ID路径</param>
    public void AddEventListener(int eventId, Action action, params int[] subGroupIds)
    {
        // 将Action转换为Action<T>的逻辑
        Action<XSignEvent> actionT = (XSignEvent _) => action();
        AddEventListener<XSignEvent>(eventId, actionT, subGroupIds);
    }

    /// <summary>
    /// 触发事件，发送参数类型
    /// </summary>
    /// <typeparam name="T">事件参数类型</typeparam>
    /// <param name="eventId">事件ID</param>
    /// <param name="info">参数</param>
    public void TriggerEvent<T>(int eventId, T info)
    {
        m_EventGroupRoot.TriggerEvent<T>((int)eventId, info);
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventId">事件ID</param>
    public void TriggerEvent(int eventId)
    {
        TriggerEvent<XSignEvent>(eventId, EmptyCallBack);
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="eventId">事件ID</param>
    public void RemoveEventListener(int eventId)
    {
        m_EventGroupRoot.RemoveEventListener(eventId);
    }
}
