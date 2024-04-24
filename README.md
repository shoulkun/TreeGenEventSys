# 项目名称: TreeGenEventSys
## 介绍
TreeGenEventSys 是一个灵活且高效的树状泛型事件系统，为游戏开发者提供了一个强大的工具，用于处理复杂的事件流。通过这个系统，开发者可以轻松地创建、管理和分发各种类型的事件，从简单的用户输入到复杂的游戏逻辑事件。

## 特点
灵活性: TreeGenEventSys 使用树状结构组织事件，使得事件的触发和处理变得灵活而高效。
可扩展性: 开发者可以轻松地扩展系统，添加新的事件类型和处理器，以满足不同的需求。
高性能: TreeGenEventSys 使用高效的算法和数据结构，保证在高负载情况下的性能表现。
易用性: 提供了简洁而直观的 API，使得开发者可以快速上手并且轻松集成到他们的项目中。

## 使用示例
```csharp
XEventManager.GetSingleton().AddEventListener
    (
        XEvent.Event_001_Id, 
        ()=>{/*do somthing.*/}, 
        p.Group_Floor001_Id
    );
XEventManager.GetSingleton().AddEventListener<Test>
    (
        XEvent.Event_002_Id, 
        (test)=>{/*do somthing.*/},
        XGroup.Group_Floor001_Id, XGroup.Group_Floor002_Id
    );
```
```
可能的大致结构：
 m_EventGroupRoot
 ├─ Event 1 -> Action: onEvent1
 ├─ Event 9 -> Action: onEvent9
 ├─ Event 10 -> Action: onEvent10
 ├─ EventGroup 1
 ├─  └─ Event 3 -> Action: onEvent3
 └─ EventGroup 2
     ├─ Event 2 -> Action: onEvent2
     └─ Event 4 -> Action: onEvent4

```
## 贡献
欢迎贡献代码，提交 bug 报告或者提出改进建议。