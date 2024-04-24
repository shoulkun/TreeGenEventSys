using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public XEventResult<T> AddEventListener<T>(int eventId, UnityAction<T> action, Span<int> subGroupIds)
    {
        if(action == null)
        {
            GLog.LogError("XEventGroup::AddEventListener action is null");
            return null;
        }
        
        XEventResult<T> result = null;

        if(subGroupIds == null || subGroupIds.IsEmpty)
        {
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
            }

            result = new XEventResult<T>(t_Event, new List<XEventGroup>(){this});
        }
        else
        {
            int t_DeepGroup = subGroupIds.Length;
            int t_GroupIdNext = subGroupIds[0];
            XEventGroup t_EventGroupNext;
            Span<int> t_subGroupIds = subGroupIds[1..t_DeepGroup];

            if(m_EventDic.TryGetValue(t_GroupIdNext, out XEventInfo t_EventInEventGroup))
            {
                t_EventGroupNext = t_EventInEventGroup as XEventGroup;
            }
            else
            {
                t_EventGroupNext = new XEventGroup(t_GroupIdNext);

            }
            m_EventDic.TryAdd(t_GroupIdNext, t_EventGroupNext);

            result = t_EventGroupNext.AddEventListener<T>(eventId, action, t_subGroupIds);
            result.m_groups.Add(this);

            foreach (var item in result.m_groups)
            {
                if(item != this)
                    m_EventDicFlaten.TryAdd(item.id, item);
            }
            m_EventDicFlaten.Add(eventId, result.m_gameEvent);
        }

        return result;
    }
}
