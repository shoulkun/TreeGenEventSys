using System;


public class XEventGame<T> : XEventInfo
{
    private Action<T> m_Action;
    public Action<T> action 
    {
        get { return m_Action; }
        set { m_Action = value; }
    }
    public XEventGame(int cid, Action<T> action): base(cid)
    {
        m_Action = action;
    }
    public virtual void Update() {    }
}
