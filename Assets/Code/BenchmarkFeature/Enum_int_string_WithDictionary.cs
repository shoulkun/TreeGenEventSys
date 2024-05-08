using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum En
{
    K1,
    K2,
    K3
}

public class C1 { }
public class C2 { }
public class C3 { }
public class C4 { }


public class Enum_int_string_WithDictionary: MonoBehaviour
{
    static Dictionary<Type, int> T2I = new Dictionary<Type, int> 
        { { typeof(C1), 0 }, { typeof(C2), 1 }, { typeof(C3), 2 } };
    static void UseRef(Type k)
    {
        int v = T2I[k];         // operator[]
        bool b = k == typeof(C1); // operator==
    }

    static Dictionary<En, int> E2I = new Dictionary<En, int>
        { { En.K1, 0 }, { En.K2, 1 }, { En.K3, 2 } };
    static void UseEnum(En k)
    {
        int v = E2I[k];    // operator[]
        bool b = k == En.K1; // operator==
    }

    static Dictionary<int, int> I2I = new Dictionary<int, int>
        { { 1, 0 }, { 2, 1 }, { 3, 2 } };
    static void UseInt(int k)
    {
        int v = I2I[k]; // operator[]
        bool b = k == 1;  // operator==
    }

    static Dictionary<long, int> L2I = new Dictionary<long, int>
    { { 1, 0 }, { 2, 1 }, { 3, 2 } };
    static void UseLong(long k)
    {
        int v = L2I[k]; // operator[]
        bool b = k == 1;  // operator==
    }

    static Dictionary<ulong, int> UL2I = new Dictionary<ulong, int>
    { { 1, 0 }, { 2, 1 }, { 3, 2 } };
    static void UseUlong(ulong k)
    {
        int v = UL2I[k]; // operator[]
        bool b = k == 1;  // operator==
    }

    static Dictionary<string, int> Str2I = new Dictionary<string, int>
    { { "Test1", 0 }, { "Test2", 1 }, { "Test3", 2 } };
    static void UseString(string k)
    {
        int v = Str2I[k]; // operator[]
        bool b = k == "1";  // operator==
    }

    static readonly int Count = 10000000;

    static void Run()
    {
        // Type
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        stopwatch.Start();
        for (int i = 0; i < Count; ++i)
        {
            UseRef(typeof(C1));
            UseRef(typeof(C2));
            UseRef(typeof(C3));
        }
        stopwatch.Stop();
        GLog.Log("type: " + stopwatch.ElapsedMilliseconds + " ms");

        // enum
        stopwatch.Reset();

        stopwatch.Start();
        for (int i = 0; i < Count; ++i)
        {
            UseEnum(En.K1);
            UseEnum(En.K2);
            UseEnum(En.K3);
        }
        stopwatch.Stop();
        GLog.Log("enum: " + stopwatch.ElapsedMilliseconds + " ms");

        // int
        stopwatch.Reset();

        stopwatch.Start();
        for (int i = 0; i < Count; ++i)
        {
            UseInt(1);
            UseInt(2);
            UseInt(3);
        }
        stopwatch.Stop();
        GLog.Log("int: " + stopwatch.ElapsedMilliseconds + " ms");

        stopwatch.Reset();

        stopwatch.Start();
        for (int i = 0; i < Count; ++i)
        {
            UseLong(1);
            UseLong(2);
            UseLong(3);
        }
        stopwatch.Stop();
        GLog.Log("long: " + stopwatch.ElapsedMilliseconds + " ms");

        stopwatch.Reset();

        stopwatch.Start();
        for (int i = 0; i < Count; ++i)
        {
            UseUlong(1);
            UseUlong(2);
            UseUlong(3);
        }
        stopwatch.Stop();
        GLog.Log("ulong: " + stopwatch.ElapsedMilliseconds + " ms");

        stopwatch.Reset();

        stopwatch.Start();
        for (int i = 0; i < Count; ++i)
        {
            UseString("Test1");
            UseString("Test2");
            UseString("Test3");
        }
        stopwatch.Stop();
        GLog.Log("string: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    void Start()
    {
        Run();
    }
}
