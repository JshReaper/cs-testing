using System;
using System.Collections;

namespace Game1
{
    /// <summary>
    /// Base class for pathfinding nodes, it holds no actual information about the map. 
    /// An inherited class must be constructed from this class and all virtual methods must be 
    /// implemented. Note, that calling base() in the overridden methods is not needed.
    /// </summary>
    public class AStarNode : IComparable
    {
        #region Properties

        private AStarNode FParent;
        /// <summary>
        /// The parent of the node.
        /// </summary>
        public AStarNode Parent
        {
            get
            {
                return FParent;
            }
            set
            {
                FParent = value;
            }
        }

        /// <summary>
        /// The accumulative cost of the path until now.
        /// </summary>
        public double Cost 
        {
            set
            {
                FCost = value;
            }
            get
            {
                return FCost;
            }
        }
        private double FCost;

        /// <summary>
        /// The estimated cost to the goal from here.
        /// </summary>
        public double GoalEstimate 
        {
            set
            {
                FGoalEstimate = value;
            }
            get 
            {
                Calculate();
                return FGoalEstimate;
            }
        }
        private double FGoalEstimate;

        /// <summary>
        /// The cost plus the estimated cost to the goal from here.
        /// </summary>
        public double TotalCost
        {
            get 
            {
                return Cost + GoalEstimate;
            }
        }

        /// <summary>
        /// The goal node.
        /// </summary>
        public AStarNode GoalNode 
        {
            set 
            {
                FGoalNode = value;
                Calculate();
            }
            get
            {
                return FGoalNode;
            }
        }
        private AStarNode FGoalNode;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="AParent">The node's parent</param>
        /// <param name="AGoalNode">The goal node</param>
        /// <param name="ACost">The accumulative cost until now</param>
        public AStarNode(AStarNode AParent,AStarNode AGoalNode,double ACost)
        {
            FParent = AParent;
            FCost = ACost;
            GoalNode = AGoalNode;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Determines wheather the current node is the goal.
        /// </summary>
        /// <returns>Returns true if current node is the goal</returns>
        public bool IsGoal()
        {
            return IsSameState(FGoalNode);
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Determines wheather the current node is the same state as the on passed.
        /// </summary>
        /// <param name="ANode">AStarNode to compare the current node to</param>
        /// <returns>Returns true if they are the same state</returns>
        public virtual bool IsSameState(AStarNode ANode)
        {
            return false;
        }

        /// <summary>
        /// Calculates the estimated cost for the remaining trip to the goal.
        /// </summary>
        public virtual void Calculate()
        {
            FGoalEstimate = 0.0f;
        }

        /// <summary>
        /// Gets all successors nodes from the current node and adds them to the successor list
        /// </summary>
        /// <param name="ASuccessors">List in which the successors will be added</param>
        public virtual void GetSuccessors(ArrayList ASuccessors)
        {
        }

        /// <summary>
        /// Prints information about the current node
        /// </summary>
        public virtual void PrintNodeInfo()
        {
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            return IsSameState((AStarNode)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return-TotalCost.CompareTo(((AStarNode)obj).TotalCost);
        }

        #endregion
    }
}