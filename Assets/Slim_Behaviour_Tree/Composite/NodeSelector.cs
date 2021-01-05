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
        public NodeSelector(string name, List<Behaviour> nodes) : base("Selector", nodes)
        {
            _lastIndex = 0;
        }

        protected override BehaviourStatus Execute(BaseInput input)
        {


            BehaviourStatus result;
            for (int i = _lastIndex; i < base.ChildrenCount; i++)
            {
                result = base._children[i].Tick(input);

                if (result == BehaviourStatus.Success)
                    continue;

                if (result == BehaviourStatus.Failure)
                {
                    CurrentStatus = BehaviourStatus.Failure;
                    return CurrentStatus;
                }

                if (result == BehaviourStatus.Running)
                {
                    _lastIndex = i;
                    base.CurrentStatus = BehaviourStatus.Running;
                    return CurrentStatus;
                }
            }

            CurrentStatus = BehaviourStatus.Success;
            return CurrentStatus;
        }
    }
}
