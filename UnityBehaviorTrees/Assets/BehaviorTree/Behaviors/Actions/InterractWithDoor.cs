using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTrees
{
    [BehaviorNode("InterractWithDoor", BehaviorNodeAttribute.NodeType.Leaf)]
    public class InterractWithDoor : BehaviorTreeNode
    {
        [BehaviorNodeParameter("Door Target")]
        public GameObject DoorTarget;

        [BehaviorNodeParameter("Open Door ")]
        public bool OpenDoorAction = true;

        [BehaviorNodeParameter("Close Door ")]
        public bool CloseDoorAction = false;

        public InterractWithDoor(BehaviorTree owner) : base(owner)
        {
        }

        protected override void OnEnter(Actor actor, Blackboard local)
        {
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
            if (DoorTarget != null)
            {
                Door _door = DoorTarget.GetComponent<Door>();
                if (OpenDoorAction)
                {
                    _door.Open();
                }
                else
                    _door.Close();

                return BehaviorTree.ResultCode.Success;
            }
            else
                return BehaviorTree.ResultCode.Error;
        }

        protected override void OnExit(Actor actor, Blackboard local)
        {

        }
    }
}
