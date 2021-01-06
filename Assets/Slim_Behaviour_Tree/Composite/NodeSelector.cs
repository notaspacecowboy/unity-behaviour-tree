//=============================
//Author: Zack Yang 
//Created Date: 12/31/2020 12:22
//=============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class NodeSelector : NodeComposite
    {
        /// <summary>
        /// index of the child node to execute first
        /// </summary>
        private int _lastIndex;
        public NodeSelector() : base("Selector")
        {
            _lastIndex = 0;
        }

        protected override BehaviourStatus Execute(BaseInput input)
        {
            BehaviourStatus result;
            for (int i = _lastIndex; i < base.ChildrenCount; i++)
            {
                result = base._children[i].Tick(input);
                //Debug.LogFormat("Current node: {0}, result: {1}", _children[i].Name, _children[i].CurrentStatus);
                
                if (result == BehaviourStatus.Failure)
                    continue;

                if (result == BehaviourStatus.Success)
                {
                    CurrentStatus = BehaviourStatus.Success;
                    return CurrentStatus;
                }

                if (result == BehaviourStatus.Running)
                {
                    _lastIndex = i;
                    base.CurrentStatus = BehaviourStatus.Running;
                    return CurrentStatus;
                }
            }

            CurrentStatus = BehaviourStatus.Failure;
            return CurrentStatus;
        }
    }
}
