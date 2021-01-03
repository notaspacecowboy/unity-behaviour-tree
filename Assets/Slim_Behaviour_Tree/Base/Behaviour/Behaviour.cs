//=============================
//Author: Zack Yang 
//Created Date: 12/29/2020 11:47
//=============================

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace SlimBehaviourTree
{
    public abstract class Behaviour
    {
        #region Private Instance Variables
        public string Name { get; private set;}
        public string Type { get; private set;}
        public BehaviourStatus CurrentStatus { get; set;}
        public BehaviourTree ParentTree { get; set;}
        public Blackboard Data { get; set;}

        #endregion

        #region Public Methods

        public Behaviour(string name, string type)
        {
            Name = name;
            Type = type;
            CurrentStatus = BehaviourStatus.Ready;
        }

        public BehaviourStatus Tick(BaseInput input)
        {
            Enter(input);

            CurrentStatus = Execute(input);

            Exit(input);

            return CurrentStatus;
        }

        #endregion

        #region Virtual Methods

        protected virtual void OnEnter(BaseInput input)
        {
        }

        protected virtual BehaviourStatus Execute(BaseInput input)
        {
            return BehaviourStatus.Success;
        }

        protected virtual void OnExit(BaseInput input)
        {
        }

        #endregion

        #region Private Methods

        private void Enter(BaseInput input)
        {
            if (CurrentStatus == BehaviourStatus.Ready)
            {
                OnEnter(input);
                CurrentStatus = BehaviourStatus.Running;
            }
        }

        private void Exit(BaseInput input)
        {
            if (CurrentStatus == BehaviourStatus.Ready)
            {
                throw new InvalidOperationException("After execute a behaviour, the status of the node cannot be status_ready");
            }

            if (CurrentStatus != BehaviourStatus.Running)
            {
                OnExit(input);
                CurrentStatus = BehaviourStatus.Ready;
            }
        }

        #endregion

    }
}
