using System;
using System.Collections.Generic;


public class XEventResult<T>
{
    public XEventGame<T> m_gameEvent;
    public List<XEventGroup> m_groups;

    public XEventResult(XEventGame<T> gameEvent, List<XEventGroup> groups)
    {
        m_gameEvent = gameEvent;
        m_groups = groups;
    }
}

public class XEventGroup : XEventInfo
{
    private readonly Dictionary<int, XEventInfo> m_EventDic = new();
    public readonly Dictionary<int, XEventInfo> m_EventDicFlaten = new();

    public XEventGroup(int infoId)  : base(infoId)
    {

    }

    /// <summary>
    /// 添加 无参 事件监听器
    /// </summary>
    /// <param name="eventId">事件ID</param>
    /// <param name="action">事件回调函数</param>
    /// <param name="subGroupIds">事件所在分组ID路径</param>
    public void AddEventListener<T>(int eventId, Action<T> action, Span<int> subGroupIds)
    {
        if(action == null)
        {
            GLog.LogError("XEventGroup::AddEventListener action is null");
            return;
        }
        
        if(subGroupIds == null || subGroupIds.IsEmpty)
        {
            #region 在最后一层添加Event

            XEventGame<T> t_Event;
            if (m_EventDicFlaten.TryGetValue(eventId, out XEventInfo t_EventInFlaten))
            {
                t_Event = t_EventInFlaten as XEventGame<T>;
                t_Event.action += action;
            }
            else
            {
                t_Event = new XEventGame<T>(eventId, action);
                t_Event.path = XEventInfo.ClonePath(this.path);
                t_Event.path.Enqueue(this.id);

                m_EventDic.Add(eventId, t_Event);
                m_EventDicFlaten.Add(eventId, t_Event);

                GLog.Log("XEventGroup::AddEventListener add event {0}", eventId);
            }

            #endregion
        }
        else
        {
            int t_GroupIdNext = subGroupIds[0];
            Span<int> t_subGroupIds = subGroupIds[1..subGroupIds.Length];
            XEventGroup t_EventGroupNext;
            
            if(m_EventDic.TryGetValue(t_GroupIdNext, out XEventInfo t_EventInEventGroup))
            {
                t_EventGroupNext = t_EventInEventGroup as XEventGroup;
            }
            else
            {
                t_EventGroupNext = new XEventGroup(t_GroupIdNext);
                t_EventGroupNext.path = XEventInfo.ClonePath(this.path);
                t_EventGroupNext.path.Enqueue(this.id);

                m_EventDic.Add(t_GroupIdNext, t_EventGroupNext);
                m_EventDicFlaten.Add(t_GroupIdNext, t_EventGroupNext);


                GLog.Log("XEventGroup::AddEventListener add group {0}", t_GroupIdNext);
            }

            #region 递归到下一层，直到最后一层添加Event
            t_EventGroupNext.AddEventListener<T>(eventId, action, t_subGroupIds);
            #endregion

            #region 把下一层的所有EventInfo添加到本层的Flaten
            foreach (var subInfo in t_EventGroupNext.m_EventDicFlaten)
            {
                m_EventDicFlaten.TryAdd(subInfo.Key, subInfo.Value);
            }
            #endregion
        }
    }

    /// <summary>
    /// 触发事件，发送参数类型
    /// </summary>
    /// <typeparam name="T">事件参数类型</typeparam>
    /// <param name="eventId">事件ID</param>
    /// <param name="info">参数</param>
    public void TriggerEvent<T>(int eventId, T info)
    {
        if(!this.enable)
            return;

        if (m_EventDicFlaten.TryGetValue(eventId, out XEventInfo t_EventInfo))
        {
            XEventGame<T> t_Event = t_EventInfo as XEventGame<T>;
            if(!t_Event.enable)
                return;
            t_Event.action?.Invoke(info);
        }
    }

    public void SetGroupEventActive(bool enable)
    {
        foreach (var subInfo in m_EventDicFlaten)
        {
            subInfo.Value.enable = enable;
        }
    }

    public void SetEventActive(int eventId, bool enable)
    {
        if (m_EventDicFlaten.TryGetValue(eventId, out XEventInfo t_EventInfo))
        {
            t_EventInfo.enable = enable;
        }
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="eventId">事件ID</param>
    public void RemoveEventListener(int eventId)
    {
        if (m_EventDicFlaten.TryGetValue(eventId, out XEventInfo t_EventInfo))
        {
            while(t_EventInfo.path.Count > 1)
            {
                int t_GroupId = t_EventInfo.path.Dequeue();
                XEventGroup t_EventGroup;
                if(t_GroupId == this.id)
                {
                    t_EventGroup = this;
                }
                else
                {
                    t_EventGroup = m_EventDicFlaten[t_GroupId] as XEventGroup;
                }
                t_EventGroup.m_EventDicFlaten.Remove(eventId);
            }
            int last_GroupId = t_EventInfo.path.Dequeue();
            XEventGroup last_EventGroup = m_EventDicFlaten[last_GroupId] as XEventGroup;
            last_EventGroup.m_EventDicFlaten.Remove(eventId);
            last_EventGroup.m_EventDic.Remove(eventId);

            GLog.Log("XEventGroup::RemoveEventListener remove event {0}", eventId);
        }
    }
}
