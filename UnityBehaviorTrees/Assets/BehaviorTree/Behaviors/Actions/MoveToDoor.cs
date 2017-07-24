using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTrees
{
    [BehaviorNode("MoveToDoor", BehaviorNodeAttribute.NodeType.Leaf)]
    public class MoveToDoor : BehaviorTreeNode
    {

        [BehaviorNodeParameter("Door Target")]
        public GameObject DoorTarget;

        private NavMeshAgent _navMeshAgent;

        public MoveToDoor(BehaviorTree owner) : base(owner)
        {
        }

        protected override void OnEnter(Actor actor, Blackboard local)
        {
            if (_navMeshAgent == null)
                _navMeshAgent = actor.GetComponent<NavMeshAgent>();

            if (_navMeshAgent == null)
                _navMeshAgent = actor.gameObject.AddComponent<NavMeshAgent>();

            Blackboard treeBlackboard = actor.Blackboards.GetBlackboard(actor.Behavior);
            if (treeBlackboard != null)
            {
                GameObject _door;
                treeBlackboard.GetValue("DoorOB", out _door);
                if (_door != null)
                    DoorTarget = _door;
                else
                    Debug.Log("Cant Find DoorTarget");
            }
            else
                Debug.Log("Cant Find Tree Blackboard");
        }

        protected override BehaviorTree.ResultCode OnAct(Actor actor, Blackboard local)
        {
            if (_navMeshAgent && DoorTarget != null)
            {
                Vector3 _pos = DoorTarget.GetComponent<Door>().EntryOB.transform.position;
                _navMeshAgent.SetDestination(_pos);

                Vector3 _actorPos = new Vector3(actor.gameObject.transform.position.x, _pos.y, actor.gameObject.transform.position.z);
                if(Vector3.SqrMagnitude(_actorPos - _pos) < (0.5f * 0.5f))
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
