//=============================
//Author: Zack Yang 
//Created Date: 01/02/2021 21:42
//=============================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class Blackboard
    {
        public class BlackboardAsset
        {
            private object _value;

            public void SetValue(object value)
            {
                _value = value;
            }

            public object GetValue()
            {
                return _value;
            }

            public T GetValue<T>()
            {
                return (T)_value;
            }
        }

        private Dictionary<string, BlackboardAsset> _boardAssets;

        public void SetValue(string key, object value)
        {
            if (!_boardAssets.ContainsKey(key))
            {
                BlackboardAsset tmp =  new BlackboardAsset();
                tmp.SetValue(value);
                _boardAssets[key] = tmp;
            }
            else
            {
                _boardAssets[key].SetValue(value);
            }
        }

        public object GetValue(string key)
        {
            if (!_boardAssets.ContainsKey(key))
                return null;

            return _boardAssets[key].GetValue();
        }

        public T GetValue<T>(string key)
        {
            if (!_boardAssets.ContainsKey(key))
                return default(T);

            else
                return _boardAssets[key].GetValue<T>();
        }

        public bool ContainsKey(string key)
        {
            return _boardAssets.ContainsKey(key);
        }
    }
}
