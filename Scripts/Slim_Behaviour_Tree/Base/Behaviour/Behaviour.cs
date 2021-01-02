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
    public abstract class Behaviour : IBehaviour
    {
        #region Public Properties

        public string Name { get; set; }
        public string Type { get; protected set; }
        public Behaviour Parent { get; set; }
        public BehaviourStatus CurrentStatus { get; protected set; }

        #endregion

        #region Public Methods

        public Behaviour(string name)
        {
            Name = "Node";
            Type = "Node";
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
