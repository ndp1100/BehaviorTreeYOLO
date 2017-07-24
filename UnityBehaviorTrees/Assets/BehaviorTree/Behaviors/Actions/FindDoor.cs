using System.Collections;
using System.Collections.Generic;
using BehaviorTrees;
using UnityEngine;

namespace BehaviorTrees
{

    [BehaviorNode("FindDoor", BehaviorNodeAttribute.NodeType.Leaf)]
    public class FindDoor : BehaviorTreeNode
    {
        private GameObject m_DoorOB;

        // Use this for initialization
        public FindDoor(BehaviorTree owner) : base(owner)
        {
        }

        protected override void OnEnter(Actor actor, Blackboard local)
        {

            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
                if (go.activeInHierarchy)
                {
                    Door _door = go.GetComponent<Door>();
                    if (_door)
                    {
                        m_DoorOB = _door.gameObject;
                        return;
                    }
                }

            Debug.Log("Cant Find any Door");
        }

        protected override BehaviorTree.ResultCode OnAct(Actor actor, Blackboard local)
        {
            if (m_DoorOB != null)
            {
                Blackboard treeBlackboard = actor.Blackboards.GetBlackboard(actor.Behavior);
                treeBlackboard.SetValue("DoorOB", m_DoorOB);
                return BehaviorTree.ResultCode.Success;

            }
            else
                return BehaviorTree.ResultCode.Failure;
        }

        protected override void OnExit(Actor actor, Blackboard local)
        {

        }
    }
}
