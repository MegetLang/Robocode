using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PG4500_2016_Exam1.Robocode;


namespace PG4500_2016_Exam1
{
	class FiniteStateMachine
	{
		private const int SpinCount = 10;  // Max number of state transitions in one round before we trigger spin.
		private readonly State[] _possibleStates;
		private readonly Queue<State> _transitionQueue;
		private State _currentState;

		public FiniteStateMachine(State[] statesToUse)
		{
			_possibleStates=statesToUse;
			_transitionQueue=new Queue<State>();
		}

		public void Init(frelas14_DeadBotRIP ourRobot)
		{
			foreach(State state in _possibleStates)
			{
				state.Init(ourRobot);
			}
			_currentState=_possibleStates[0];
			_currentState.EnterState();
		}

		/// <summary>
		/// Queue up a new state (as long as it is registered as a possible state for this FSM).
		/// </summary>
		public void Queue(string stateId)
		{
			State newState = null;
			foreach(State element in _possibleStates)
			{
				if(stateId==element.stateName)
				{
					newState=element;
					break;
				}
			}

			if(null!=newState)
			{
				_transitionQueue.Enqueue(newState);
			}
		}

		public void Update()
		{
			State nextState;
			string queueStateName;
			int loopCounter = 0;

			// Process any queued states (in order). If no new states are queued, process the current one.
			do
			{
				loopCounter++;
				if(SpinCount<loopCounter)
				{
					break;
				}

				// Swap to next state, if any are queued and current state allows a swap.
				if(0<_transitionQueue.Count)
				{
					nextState=_transitionQueue.Dequeue();
					if(_currentState.DoTransition(nextState.stateName))
					{
						_currentState.ExitState();
						_currentState=nextState;
						_currentState.EnterState();
					}
				}

				// Process the AI action for the current state.
				queueStateName=_currentState.ProcessState();

				// If current AI action triggered a transition, queue it up.
				if(null!=queueStateName)
				{
					Queue(queueStateName);
				}

			} while(0<_transitionQueue.Count);
		}
	}
}
