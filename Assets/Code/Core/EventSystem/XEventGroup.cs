using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

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

    public void AddEventListener<T>(int eventId, UnityAction<T> action, Span<int> subGroupIds)
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
                m_EventDic.Add(t_GroupIdNext, t_EventGroupNext);
                m_EventDicFlaten.Add(t_GroupIdNext, t_EventGroupNext);

                GLog.Log("XEventGroup::AddEventListener add group {0}", t_GroupIdNext);
            }

            #region 递归到下一层，直到最后一层添加Event
            t_EventGroupNext.AddEventListener<T>(eventId, action, t_subGroupIds);
            #endregion

            #region 把下一层的所有EventInfo添加到本层的Flaten
            foreach (var subGroupItem in t_EventGroupNext.m_EventDicFlaten)
            {
                m_EventDicFlaten.TryAdd(subGroupItem.Key, subGroupItem.Value);
            }
            #endregion
        }
    }

    public void RemoveEventListener(int eventId)
    {

    }
}
