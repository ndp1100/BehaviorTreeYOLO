using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTrees
{
    [BehaviorNode("MoveToPosition", BehaviorNodeAttribute.NodeType.Leaf)]
    public class MoveToPosition: BehaviorTreeNode
    {
        [BehaviorNodeParameter("Target Position")]
        public Vector3 m_TargetPosition;

        private NavMeshAgent _navMeshAgent;

        public MoveToPosition(BehaviorTree owner) : base(owner)
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
                _navMeshAgent.SetDestination(m_TargetPosition);

                if (_navMeshAgent.remainingDistance < 0.5f)
                {
                    return BehaviorTree.ResultCode.Success;
                }
                else
                    return BehaviorTree.ResultCode.Running;
            }else
                return BehaviorTree.ResultCode.Error;
        }

        protected override void OnExit(Actor actor, Blackboard local)
        {
            
        }
    }
}