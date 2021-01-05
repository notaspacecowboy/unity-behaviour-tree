//=============================
//Author: Zack Yang 
//Created Date: 01/02/2021 21:20
//=============================
using System.Collections;
using System.Collections.Generic;
using LitJson;
using NUnit.Framework;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class BehaviourTree
    {
        public string TreeName { get; set;}

        public BehaviourTree(string name)
        {
            TreeName = name;
            _root = null;   
            _data = new Blackboard();
        }

        public void ReadData(string jsonTxt)
        {
            JsonData data = JsonMapper.ToObject(jsonTxt);

            int i = 0;
            foreach (var key in data.Keys)
            {
                _data.SetValue(key, data[key]);
            }
            
            _data.print();
        }

        public void SetRoot(Behaviour root)
        {
            _root = root;
            _root.Data = this._data;
        }

        public Behaviour GetRoot()
        {
            return _root;
        }

        public void Run(BaseInput input)
        {
            _root.Tick(input);
        }

        private Behaviour _root;
        private Blackboard _data;
    }
}
