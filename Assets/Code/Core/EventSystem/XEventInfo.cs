using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// public interface IEvent{}

public abstract class XEventInfo
{
    private int m_EventId;
    public int id 
    {
        get { return m_EventId; }
        // 构造中设置事件的cid
        protected set { m_EventId = value;}
    }

    private bool m_Enable;
    public bool enable 
    {
        get { return m_Enable; }
        set { m_Enable = value; }
    }

    public XEventInfo(int cid)
    {
        m_EventId = cid;
        m_Enable = true;
    }
}