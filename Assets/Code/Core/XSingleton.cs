using UnityEngine;

/// <summary>
/// 简单单例.
/// -必须是 引用类型(非int float等)
/// -必须至少具备无参的构造函数
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class XSimpleSingleton<T> where T : class, new()
{
    /// <summary>
    /// The instance.
    /// </summary>
    protected static T instance;

    /// <summary>
    /// Gets the singleton.
    /// </summary>
    /// <returns>The singleton.</returns>
    public static T GetSingleton()
    {
        if(instance == null)
        {
            instance = new T();
        }

        return instance;
    }
}

/// <summary>
/// Mono behaviour 单例.
/// </summary>
public class XMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;

    /// <summary>
    /// Gets the singleton.
    /// </summary>
    /// <returns>The singleton.</returns>
    public static T GetSingleton()
    {
        if (!instance)
        {
            GameObject singleton = new GameObject(typeof(T).Name);
            if (!singleton)
                throw new System.NullReferenceException();

            instance = singleton.AddComponent<T>();
            GameObject.DontDestroyOnLoad(singleton);
        }

        return instance;
    }

    public virtual void Init() { }
    protected virtual void OnDestroy()
    {
        Destroy(gameObject);
    }
}