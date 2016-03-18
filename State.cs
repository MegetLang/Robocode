using Robocode;

namespace PG4500_2016_Exam1.Robocode
{
	public abstract class State
	{

		protected AdvancedRobot Robot;

		public string stateName { get; private set; }

		protected State (string name)
		{
			stateName=name;
		}

		public virtual void Init(AdvancedRobot ourRobot)
		{
			Robot=ourRobot;
		}
		public virtual bool DoTransition(string nextStateName)
		{
			if(stateName!=nextStateName)
			{
				return true;
			}
			return false;
		}
		public virtual void EnterState()
		{
			Robot.Out.WriteLine("{0,6} [{1}] entered.", Robot.Time, stateName);
		}

		public virtual void ExitState()
		{
			// Intentionally left blank.
		}


		/// <summary>
		/// Called once for every Update() in the "owning" StateMachine, as long as this state is queued or it is the active one with an empty queue.
		/// </summary>
		public abstract string ProcessState();

	}
}
