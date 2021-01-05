//=============================
//Author: Zack Yang 
//Created Date: 01/01/2021 16:56
//=============================
using System.Collections;
using System.Collections.Generic;
using SlimBehaviourTree;
using UnityEngine;
using Behaviour = UnityEngine.Behaviour;

namespace SlimBehaviourTree
{
    public class NodeRandomSequence : NodeComposite
    {
        // Start is called before the first frame update
        private int _lastIndex;

        public NodeRandomSequence(string name, List<Behaviour> nodes) : base("RandomSequence", nodes)
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

        protected override void OnEnter(BaseInput input)
        {
            Shuffle();
        }

        private void Shuffle()
        {
            Behaviour lastRunningNode = null;
            if (CurrentStatus == BehaviourStatus.Running)
            {
                lastRunningNode = _children[_lastIndex];
            }

            for (int i = base.ChildrenCount; i >= 0; i--)
            {
                int toSwitch = Random.Range(0, i);
                Behaviour tmp = _children[i - 1];
                _children[i - 1] = _children[toSwitch];
                _children[toSwitch] = tmp;
            }

            if (lastRunningNode == null)
                return;

            for (int i = 0; i < base.ChildrenCount; i++)
            {
                if (_children[i] == lastRunningNode)
                {
                    Behaviour tmp = _children[i];
                    _children[i] = _children[0];
                    _children[0] = tmp;

                    break;
                }
            }
        }
    }
}
