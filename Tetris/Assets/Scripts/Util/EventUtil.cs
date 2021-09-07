using System;

public static class EventUtil
{
    public static void SafeInvoke(Action action)
    {
        if (action != null) action();
    }

    public static void SafeInvoke<T>(Action<T> action, T argument)
    {
        if (action != null) action(argument);
    }
}