using System.Collections.Generic;


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

    private Queue<int> m_Path;
    public Queue<int> path 
    { 
        get { m_Path ??= new Queue<int>();return m_Path; }
        set { m_Path = value; } 
    }

    public static Queue<int> ClonePath(Queue<int> originalQueue)
    {
        // 将原始栈转换为数组
        int[] array = originalQueue.ToArray();
        // 倒序数组
        //Array.Reverse(array);
        // 使用数组创建一个新的栈
        return new Queue<int>(array);
    }
}