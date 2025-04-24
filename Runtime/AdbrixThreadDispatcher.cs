using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AdbrixPlugin
{
    /// <summary>
    /// Unity 메인 스레드에서 작업을 실행하기 위한 디스패처입니다.
    /// 다른 스레드에서 Unity API를 호출해야 할 때 사용합니다.
    /// </summary>
    public class AdbrixThreadDispatcher : MonoBehaviour
    {
        private static AdbrixThreadDispatcher _instance;
        private readonly Queue<Action> _executionQueue = new Queue<Action>();
        private readonly object _lock = new object();

        /// <summary>
        /// 싱글턴 인스턴스에 접근합니다. 인스턴스가 없으면 자동으로 생성합니다.
        /// </summary>
        public static AdbrixThreadDispatcher Instance()
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("AdbrixThreadDispatcher");
                _instance = go.AddComponent<AdbrixThreadDispatcher>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }

        /// <summary>
        /// 메인 스레드 큐에 작업을 추가합니다.
        /// </summary>
        /// <param name="action">실행할 작업</param>
        public void Enqueue(Action action)
        {
            lock (_lock)
            {
                _executionQueue.Enqueue(action);
            }
        }

        void Update()
        {
            lock (_lock)
            {
                while (_executionQueue.Count > 0)
                {
                    try
                    {
                        _executionQueue.Dequeue().Invoke();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"[Adbrix] Error in thread dispatcher: {e.Message}\n{e.StackTrace}");
                    }
                }
            }
        }

        void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
} 