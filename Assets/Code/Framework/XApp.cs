using UnityEngine;

public class XApp : MonoBehaviour
{
    private void Start() 
    {
        XEventManager.GetSingleton().AddEventListener(XEventID.Event_001, ()=>{GLog.Log("1");}, XGroupID.Group_Floor002);
        XEventManager.GetSingleton().AddEventListener<C2>(XEventID.Event_002, (c2)=>{GLog.Log("2");}, XGroupID.Group_Floor003);
        XEventManager.GetSingleton().AddEventListener<C3>(XEventID.Event_003, (c3)=>{GLog.Log("3");}, XGroupID.Group_Floor002, XGroupID.Group_Floor004);
        XEventManager.GetSingleton().AddEventListener<C4>(XEventID.Event_004, (c3)=>{GLog.Log("4");}, XGroupID.Group_Floor002, XGroupID.Group_Floor004,XGroupID.Group_Floor005);
    
        XEventManager.GetSingleton().TriggerEvent(XEventID.Event_001);
        XEventManager.GetSingleton().TriggerEvent<C3>(XEventID.Event_003, new C3());

        XEventManager.GetSingleton().RemoveEventListener(XEventID.Event_003);
    }
}
