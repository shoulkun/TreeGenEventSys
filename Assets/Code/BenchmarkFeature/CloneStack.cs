using System;
using System.Collections.Generic;
using UnityEngine;

public class CloneStack : MonoBehaviour 
{
    Stack<int> m_Path1;
    Stack<int> m_Path2;
    Stack<int> m_Path3;

    private void Awake()
    {
        m_Path1 = new Stack<int>();
        m_Path2 = new Stack<int>();
        m_Path3 = new Stack<int>();

        for (int i = 0; i < 100000000; i++) 
        {
            m_Path1.Push(i);
            m_Path2.Push(i);
            m_Path3.Push(i);
        }
    }

    private void Start() 
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ClonePath(m_Path1);
        stopwatch.Stop();
        Debug.Log("ClonePath: " + stopwatch.ElapsedMilliseconds + "ms");
        stopwatch.Reset();

        stopwatch.Start();
        ClonePath2(m_Path2);
        stopwatch.Stop();
        Debug.Log("ClonePath2: " + stopwatch.ElapsedMilliseconds + "ms");
        stopwatch.Reset();

        stopwatch.Start();
        ClonePath3(m_Path3);
        stopwatch.Stop();
        Debug.Log("ClonePath3: " + stopwatch.ElapsedMilliseconds + "ms");
        stopwatch.Reset();
    }


    public void ClonePath(Stack<int> originalStack)
    {
        // 将原始栈转换为数组
        int[] array = originalStack.ToArray();
        // 倒序数组
        Array.Reverse(array);
        // 使用数组创建一个新的栈
        m_Path1 = new Stack<int>(array);
    }

    public void ClonePath2(Stack<int> originalStack)
    {
        // 创建一个新的栈
        m_Path2 = new Stack<int>();

        // 使用迭代器从原始栈中倒序生成新栈
        foreach (int item in ReverseStack(originalStack))
        {
            m_Path2.Push(item);
        }
    }

    // 倒序生成原始栈的迭代器
    private IEnumerable<int> ReverseStack(Stack<int> stack)
    {
        // 创建一个临时栈
        Stack<int> tempStack = new Stack<int>(stack);

        // 从临时栈中依次取出元素，实现倒序
        while (tempStack.Count > 0)
        {
            yield return tempStack.Pop();
        }
    }

    public void ClonePath3(Stack<int> originalStack)
    {
        // 创建一个临时栈来保存倒序的元素
        Stack<int> tempStack = new Stack<int>();

        // 将原始栈中的元素依次弹出并压入临时栈中，实现倒序
        while (originalStack.Count > 0)
        {
            tempStack.Push(originalStack.Pop());
        }

        // 将倒序后的元素重新压入原始栈中，并赋值给 m_Path
        originalStack = tempStack;
        m_Path3 = tempStack;
    }
}