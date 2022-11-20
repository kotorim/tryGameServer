using System;
using System.Collections.Generic;

namespace Baccarat_Server.Tools
{
    /// <summary>
    /// 事件控制器
    /// </summary>
    public class eventController
    {
        private Dictionary<string, Delegate> eventDic = new Dictionary<string, Delegate>();

        #region 注入事件
        /// <summary>
        /// 注入事件(无参)
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void AddEvent(string eventName, Action action)
        {
            if (!eventDic.ContainsKey(eventName))
            {
                eventDic.Add(eventName, action);
            }
            else
            {
                eventDic[eventName] = (Action)eventDic[eventName] + action;
            }
        }
        /// <summary>
        /// 注入事件(1个参数)
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void AddEvent<T>(string eventName, Action<T> action)
        {
            if (!eventDic.ContainsKey(eventName))
            {
                eventDic.Add(eventName, action);
            }
            else
            {
                eventDic[eventName] = (Action<T>)eventDic[eventName] + action;
            }
        }
        /// <summary>
        /// 注入事件(2个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void AddEvent<T, X>(string eventName, Action<T, X> action)
        {
            if (!eventDic.ContainsKey(eventName))
            {
                eventDic.Add(eventName, action);
            }
            else
            {
                eventDic[eventName] = (Action<T, X>)eventDic[eventName] + action;
            }
        }
        /// <summary>
        /// 注入事件(3个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <typeparam name="Z">事件3类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void AddEvent<T, X, Z>(string eventName, Action<T, X, Z> action)
        {
            if (!eventDic.ContainsKey(eventName))
            {
                eventDic.Add(eventName, action);
            }
            else
            {
                eventDic[eventName] = (Action<T, X, Z>)eventDic[eventName] + action;
            }
        }

        #endregion

        #region 移除事件
        /// <summary>
        /// 移除事件(无参)
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void RemoveEvent(string eventName, Action action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                eventDic[eventName] = (Action)eventDic[eventName] - action;
            }
        }
        /// <summary>
        /// 移除事件(1个参数)
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void RemoveEvent<T>(string eventName, Action<T> action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                eventDic[eventName] = (Action<T>)eventDic[eventName] - action;
            }
        }
        /// <summary>
        /// 移除事件(2个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void RemoveEvent<T, X>(string eventName, Action<T, X> action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                eventDic[eventName] = (Action<T, X>)eventDic[eventName] - action;
            }
        }
        /// <summary>
        /// 移除事件(3个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <typeparam name="Z">事件3类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void RemoveEvent<T, X, Z>(string eventName, Action<T, X, Z> action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                eventDic[eventName] = (Action<T, X, Z>)eventDic[eventName] - action;
            }
        }

        #endregion

        #region 触发事件
        /// <summary>
        /// 触发事件(无参)
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void TriggerEvent(string eventName)
        {
            if (eventDic.TryGetValue(eventName, out Delegate del))
            {
                Delegate[] dels = del.GetInvocationList();
                for (int i = 0; i < dels.Length; i++)
                {
                    Action action = dels[i] as Action;
                    if (action == null)
                    {
                        Console.WriteLine(eventName + "---该事件不存在!");
                        return;
                    }
                    try
                    {
                        action.Invoke();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
        /// <summary>
        /// 触发事件(1个参数)
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void TriggerEvent<T>(string eventName, T arg1)
        {
            if (eventDic.TryGetValue(eventName, out Delegate del))
            {
                Delegate[] dels = del.GetInvocationList();
                for (int i = 0; i < dels.Length; i++)
                {
                    Action<T> action = dels[i] as Action<T>;
                    if (action == null)
                    {
                        Console.WriteLine(eventName + "---该事件不存在!");
                        return;
                    }
                    try
                    {
                        action.Invoke(arg1);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
        /// <summary>
        /// 触发事件(2个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void TriggerEvent<T, X>(string eventName, T arg1, X arg2)
        {
            if (eventDic.TryGetValue(eventName, out Delegate del))
            {
                Delegate[] dels = del.GetInvocationList();
                for (int i = 0; i < dels.Length; i++)
                {
                    Action<T, X> action = dels[i] as Action<T, X>;
                    if (action == null)
                    {
                        Console.WriteLine(eventName + "---该事件不存在!");
                        return;
                    }
                    try
                    {
                        action.Invoke(arg1, arg2);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
        /// <summary>
        /// 触发事件(3个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <typeparam name="Z">事件3类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public void TriggerEvent<T, X, Z>(string eventName, T arg1, X arg2, Z arg3)
        {
            if (eventDic.TryGetValue(eventName, out Delegate del))
            {
                Delegate[] dels = del.GetInvocationList();
                for (int i = 0; i < dels.Length; i++)
                {
                    Action<T, X, Z> action = dels[i] as Action<T, X, Z>;
                    if (action == null)
                    {
                        Console.WriteLine(eventName + "---该事件不存在!");
                        return;
                    }
                    try
                    {
                        action.Invoke(arg1, arg2, arg3);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        #endregion
    }
}
