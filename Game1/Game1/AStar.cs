using System;
using System.Collections;

namespace Game1 {
    /// <summary>
	/// Class for performing A* pathfinding
	/// </summary>
	public sealed class AStar
	{
		#region Private Fields

		private AStarNode FStartNode;
		private AStarNode FGoalNode;
		private Heap FOpenList;
		private Heap FClosedList;
		private ArrayList FSuccessors;

		#endregion

		#region Properties

		/// <summary>
		/// Holds the solution after pathfinding is done. <see>FindPath()</see>
		/// </summary>
		public ArrayList Solution
		{
			get 
			{
				return FSolution;
			}
		}
		private ArrayList FSolution;

		#endregion
		
		#region Constructors

		public AStar()
		{
			FOpenList = new Heap();
			FClosedList = new Heap();
			FSuccessors = new ArrayList();
			FSolution = new ArrayList();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Prints all the nodes in a list
		/// </summary>
		/// <param name="ANodeList">List to print</param>
		private void PrintNodeList(object ANodeList)
		{
			Console.WriteLine("Node list:");
			foreach(AStarNode n in ANodeList as IEnumerable) 
			{
				n.PrintNodeInfo();
			}
			Console.WriteLine("=====");
		}

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds the shortest path from the start node to the goal node
        /// </summary>
        /// <param name="AStartNode">Start node</param>
        /// <param name="AGoalNode">Goal node</param>
        public void FindPath(AStarNode AStartNode, AStarNode AGoalNode)
        {
            FStartNode = AStartNode;
            FGoalNode = AGoalNode;

            FOpenList.Add(FStartNode);
            while (FOpenList.Count > 0)
            {
                // Get the node with the lowest TotalCost
                AStarNode NodeCurrent = (AStarNode)FOpenList.Pop();

                // If the node is the goal copy the path to the solution array
                if (NodeCurrent.IsGoal())
                {
                    while (NodeCurrent != null)
                    {
                        FSolution.Insert(0, NodeCurrent);
                        NodeCurrent = NodeCurrent.Parent;
                    }
                    break;
                }

                // Get successors to the current node
                NodeCurrent.GetSuccessors(FSuccessors);
                foreach (AStarNode NodeSuccessor in FSuccessors)
                {
                    // Test if the currect successor node is on the open list, if it is and
                    // the TotalCost is higher, we will throw away the current successor.
                    AStarNode NodeOpen = null;
                    foreach (AStarNode Node in FOpenList)
                    {
                        if (NodeSuccessor.IsSameState(Node))
                        {
                            NodeOpen = Node;
                            break;
                        }
                    }
                    if (NodeOpen != null && NodeSuccessor.TotalCost > NodeOpen.TotalCost)
                        continue;

                    // Test if the currect successor node is on the closed list, if it is and
                    // the TotalCost is higher, we will throw away the current successor.
                    AStarNode NodeClosed = null;
                    foreach (AStarNode Node in FClosedList)
                    {
                        if (NodeSuccessor.IsSameState(Node))
                        {
                            NodeClosed = Node;
                            break;
                        }
                    }
                    if (NodeClosed != null && NodeSuccessor.TotalCost > NodeClosed.TotalCost)
                        continue;

                    // Remove the old successor from the open list
                    FOpenList.Remove(NodeOpen);

                    // Remove the old successor from the closed list
                    FClosedList.Remove(NodeClosed);

                    // Add the current successor to the open list
                    FOpenList.Push(NodeSuccessor);
                }
                // Add the current node to the closed list
                FClosedList.Add(NodeCurrent);
            }
        }
        #endregion
    }
}