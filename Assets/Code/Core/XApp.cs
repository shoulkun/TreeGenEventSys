using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XApp : MonoBehaviour
{
    private void Start() 
    {
        XEventManager.GetSingleton().AddEventListener(XEvent.Event_001_Id, ()=>{GLog.Log("1");}, XGroup.Group_Floor002_Id);
        XEventManager.GetSingleton().AddEventListener<C2>(XEvent.Event_002_Id, (c2)=>{GLog.Log("2");}, XGroup.Group_Floor003_Id);
        XEventManager.GetSingleton().AddEventListener<C3>(XEvent.Event_003_Id, (c3)=>{GLog.Log("3");}, XGroup.Group_Floor002_Id, XGroup.Group_Floor004_Id);
        XEventManager.GetSingleton().AddEventListener<C4>(XEvent.Event_004_Id, (c3)=>{GLog.Log("3");}, XGroup.Group_Floor002_Id, XGroup.Group_Floor004_Id,XGroup.Group_Floor005_Id);
    }
}
