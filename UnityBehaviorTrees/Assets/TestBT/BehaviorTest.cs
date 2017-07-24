using UnityEngine;
using BehaviorTrees;

/*
*   Behavior Tree: Test                         
*   This class has been auto generated on 7/24/2017 3:03:51 PM  
*   DO NOT MODIFY MANUALLY!                    
*/

public static class BehaviorTreeTest 
{
    [BehaviorGenerator( "Test")]
    public static BehaviorTree Generate()
    {
        BehaviorTree t = new BehaviorTree();
        
		BehaviorTrees.Sequencer n1 = new BehaviorTrees.Sequencer( t );
		n1.m_continueOnFail = false;

		BehaviorTrees.FindDoor n2 = new BehaviorTrees.FindDoor( t );

		BehaviorTrees.MoveToDoor n3 = new BehaviorTrees.MoveToDoor( t );

		BehaviorTrees.InterractWithDoor n4 = new BehaviorTrees.InterractWithDoor( t );
		n4.OpenDoorAction = true;
		n4.CloseDoorAction = false;

		BehaviorTrees.MoveThroughDoor n5 = new BehaviorTrees.MoveThroughDoor( t );

		BehaviorTrees.InterractWithDoor n6 = new BehaviorTrees.InterractWithDoor( t );
		n6.OpenDoorAction = false;
		n6.CloseDoorAction = true;

		n1.Children.Add(n2);
		n1.Children.Add(n3);
		n1.Children.Add(n4);
		n1.Children.Add(n5);
		n1.Children.Add(n6);

        t.Root = n1;
        return t;
    }
}
