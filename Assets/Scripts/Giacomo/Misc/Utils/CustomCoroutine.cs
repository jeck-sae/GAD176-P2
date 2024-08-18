using System;
using System.Collections;
using UnityEngine;

//List of methods to quickly create commonly used coroutines
public class CustomCoroutine : MonoBehaviour
{
    private static CustomCoroutine instance;

    public static CustomCoroutine Instance
    {
        get
        {
            TryCreateInstance();
            return instance;
        }
    }

    private static void TryCreateInstance()
    {
        if (instance == null)
        {
            instance = new GameObject("CustomCoroutineRunner").AddComponent<CustomCoroutine>();
        }
    }

    //invokes the delegate once the condition returns true
    public static void WaitOnConditionThenExecute(Func<bool> condition, Action action)
    {
        TryCreateInstance();
        instance.StartWaitOnConditionThenExecute(condition, action);
    }
    public void StartWaitOnConditionThenExecute(Func<bool> condition, Action action)
    {
        StartCoroutine(DoWaitOnConditionThenExecute(condition, action));
    }
    private IEnumerator DoWaitOnConditionThenExecute(Func<bool> condition, Action action)
    {
        yield return new WaitUntil(() => condition());
        action();
    }


    //waits N seconds and then invokes the delegate
    public static void WaitThenExecute(float wait, Action action, bool unscaledTime = false)
    {
        TryCreateInstance();
        instance.StartWaitThenExecute(wait, action, unscaledTime);
    }
    public void StartWaitThenExecute(float wait, Action action, bool unscaledTime = false)
    {
        StartCoroutine(DoWaitThenExecute(wait, action, unscaledTime));
    }
    private IEnumerator DoWaitThenExecute(float wait, Action action, bool unscaledTime = false)
    {
        if (wait <= 0f)
            yield return new WaitForEndOfFrame();
        else if (unscaledTime)
            yield return new WaitForSecondsRealtime(wait);
        else
            yield return new WaitForSeconds(wait);
        action();
    }

}
