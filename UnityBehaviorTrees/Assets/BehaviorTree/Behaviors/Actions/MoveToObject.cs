using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTrees
{
    [BehaviorNode("MoveToObject", BehaviorNodeAttribute.NodeType.Leaf)]
    public class MoveToObject : BehaviorTreeNode
    {
        [BehaviorNodeParameter("Object Target")]
        public GameObject ObjectTarget;

        private NavMeshAgent _navMeshAgent;

        public MoveToObject(BehaviorTree owner) : base(owner)
        {
        }

        protected override void OnEnter(Actor actor, Blackboard local)
        {
            if (_navMeshAgent == null)
                _navMeshAgent = actor.GetComponent<NavMeshAgent>();

            if (_navMeshAgent == null)
                _navMeshAgent = actor.gameObject.AddComponent<NavMeshAgent>();
        }

        protected override BehaviorTree.ResultCode OnAct(Actor actor, Blackboard local)
        {
            if (_navMeshAgent)
            {
                _navMeshAgent.SetDestination(ObjectTarget.transform.position);

                if (_navMeshAgent.remainingDistance < 0.5f)
                {
                    return BehaviorTree.ResultCode.Success;
                }
                else
                    return BehaviorTree.ResultCode.Running;
            }
            else
                return BehaviorTree.ResultCode.Error;
        }

        protected override void OnExit(Actor actor, Blackboard local)
        {

        }
    }
}
