using UnityEngine.Events;


public class XEventGame<T> : XEventInfo
{
    private UnityAction<T> m_Action;
    public UnityAction<T> action 
    {
        get { return m_Action; }
        set { m_Action = value; }
    }
    public XEventGame(int cid, UnityAction<T> action): base(cid)
    {
        m_Action = action;
    }
    public virtual void Update() {    }
}
