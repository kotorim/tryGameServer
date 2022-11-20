﻿using System;

/// <summary>
/// 事件派发类
/// </summary>
namespace Baccarat_Server.Tools
{
    public class eventDispatcher
    {
        private static eventController ec = new eventController();

        #region 注入事件
        /// <summary>
        /// 注入事件(无参)
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void AddEvent(eventType _eventName, Action _action)
        {
            ec.AddEvent(_eventName.ToString(), _action);
        }
        /// <summary>
        /// 注入事件(1个参数)
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void AddEvent<T>(eventType _eventName, Action<T> _action)
        {
            ec.AddEvent(_eventName.ToString(), _action);
        }
        /// <summary>
        /// 注入事件(2个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void AddEvent<T, X>(eventType _eventName, Action<T, X> _action)
        {
            ec.AddEvent(_eventName.ToString(), _action);
        }
        /// <summary>
        /// 注入事件(3个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <typeparam name="Z">事件3类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void AddEvent<T, X, Z>(eventType _eventName, Action<T, X, Z> _action)
        {
            ec.AddEvent(_eventName.ToString(), _action);
        }

        #endregion

        #region 移除事件
        /// <summary>
        /// 移除事件(无参)
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void RemoveEvent(eventType _eventName, Action _action)
        {
            ec.RemoveEvent(_eventName.ToString(), _action);
        }
        /// <summary>
        /// 移除事件(1个参数)
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void RemoveEvent<T>(eventType _eventName, Action<T> _action)
        {
            ec.RemoveEvent(_eventName.ToString(), _action);
        }
        /// <summary>
        /// 移除事件(2个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void RemoveEvent<T, X>(eventType _eventName, Action<T, X> _action)
        {
            ec.RemoveEvent(_eventName.ToString(), _action);
        }
        /// <summary>
        /// 移除事件(3个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <typeparam name="Z">事件3类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void RemoveEvent<T, X, Z>(eventType _eventName, Action<T, X, Z> _action)
        {
            ec.RemoveEvent(_eventName.ToString(), _action);
        }
        #endregion

        #region 触发事件
        /// <summary>
        /// 触发事件(无参)
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void dispatchEvent(eventType _eventName)
        {
            ec.TriggerEvent(_eventName.ToString());
        }
        /// <summary>
        /// 触发事件(1个参数)
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void dispatchEvent<T>(eventType _eventName, T arg1)
        {
            ec.TriggerEvent(_eventName.ToString(), arg1);
        }
        /// <summary>
        /// 触发事件(2个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void dispatchEvent<T, X>(eventType _eventName, T arg1, X arg2)
        {
            ec.TriggerEvent(_eventName.ToString(), arg1, arg2);
        }
        /// <summary>
        /// 触发事件(3个参数)
        /// </summary>
        /// <typeparam name="T">事件1类型</typeparam>
        /// <typeparam name="X">事件2类型</typeparam>
        /// <typeparam name="Z">事件3类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">事件</param>
        public static void dispatchEvent<T, X, Z>(eventType _eventName, T arg1, X arg2, Z arg3)
        {
            ec.TriggerEvent(_eventName.ToString(), arg1, arg2, arg3);
        }
        #endregion
    }
    public enum eventType
    {
        onPost,
        onGet,
        onTimeTick,
        onConnected,
        onDisconnected,
    }
}



