using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine.Events;

public class XEventManager : XMonoSingleton<XEventManager>
{
    private readonly XEventGroup m_EventGroupRoot = new((int)XGroup.Group_Root_Id);
    public bool isDebug = false;
    public readonly XSignEvent EmptyCallBack = new XSignEvent();

    public void AddEventListener<T>(XEvent eventId, UnityAction<T> action, params XGroup[] subGroupIds)
    {
        List<int> subGroupIdsList = new List<int>();
        foreach (var subGroupId in subGroupIds)
            subGroupIdsList.Add((int)subGroupId);

        m_EventGroupRoot.AddEventListener<T>((int)eventId, action, subGroupIdsList.ToArray());
    }

    public void AddEventListener(XEvent eventId, UnityAction action, params XGroup[] subGroupIds)
    {
        // 将UnityAction转换为UnityAction<T>的逻辑
        UnityAction<XSignEvent> actionT = (XSignEvent _) => action();
        AddEventListener<XSignEvent>(eventId, actionT, subGroupIds);
    }


    public void EventTrigger<T>(ushort eventId, T info, List<ulong> subGroupIds = null)
    {
        if(subGroupIds == null ||subGroupIds.Count == 0)
        {
            if (m_EventGroupRoot.m_EventDicFlaten.ContainsKey(eventId))
                (m_EventGroupRoot.m_EventDicFlaten[eventId] as XEventGame<T>).action?.Invoke(info);
        }
    }

    public void EventTrigger(ushort eventId, List<ulong> subGroupIds = null)
    {
        EventTrigger<XSignEvent>(eventId, EmptyCallBack, subGroupIds);
    }

    public void RemoveEventListener<T>(ushort eventId, UnityAction<T> action, List<ulong> subGroupIds = null)
    {
        if(subGroupIds == null ||subGroupIds.Count == 0)
        {
            if (m_EventGroupRoot.m_EventDicFlaten.ContainsKey(eventId))
                (m_EventGroupRoot.m_EventDicFlaten[eventId] as XEventGame<T>).action -= action;
        }
    }
}
