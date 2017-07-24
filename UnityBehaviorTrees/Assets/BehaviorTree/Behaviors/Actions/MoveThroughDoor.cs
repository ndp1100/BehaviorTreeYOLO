using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTrees
{
    [BehaviorNode("MoveThroughDoor", BehaviorNodeAttribute.NodeType.Leaf)]
    public class MoveThroughDoor : BehaviorTreeNode
    {

        [BehaviorNodeParameter("Door Target")]
        public GameObject DoorTarget;

        private NavMeshAgent _navMeshAgent;

        public MoveThroughDoor(BehaviorTree owner) : base(owner)
        {
        }

        protected override void OnEnter(Actor actor, Blackboard local)
        {
            if (_navMeshAgent == null)
                _navMeshAgent = actor.GetComponent<NavMeshAgent>();
            if (_navMeshAgent) _navMeshAgent.enabled = false;


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

            _time = 0;
        }

        private float _time = 0;
        protected override BehaviorTree.ResultCode OnAct(Actor actor, Blackboard local)
        {
            if (DoorTarget != null)
            {
                Door _door = DoorTarget.GetComponent<Door>();
                _time += Time.deltaTime;
                if (_time <= 2)
                {
                    actor.transform.position = Vector3.Slerp(_door.EntryOB.transform.position, _door.ExitOB.transform.position,
                                                            _time / 2f);
                    return BehaviorTree.ResultCode.Running;
                }
                else
                {
                    actor.transform.position = _door.ExitOB.transform.position;
                    if (_navMeshAgent) _navMeshAgent.enabled = true;
                    return BehaviorTree.ResultCode.Success;
                }

                
            }
            else
                return BehaviorTree.ResultCode.Error;
        }

        protected override void OnExit(Actor actor, Blackboard local)
        {

        }
        
    }
}
