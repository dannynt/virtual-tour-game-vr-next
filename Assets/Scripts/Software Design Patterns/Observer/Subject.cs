using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// State subject.
/// </summary>
public class Subject : MonoBehaviour
{
	protected State state;

	public Action<SubjectEvent> eventsToBeCalled;

	protected virtual void Start() {}

	public void CallEvents(SubjectEvent subjectEvent)
	{
		eventsToBeCalled?.Invoke(subjectEvent);
	}

	public void EnterState(State newState)
    {
		if (this.state != null) this.state.Exit();
		this.state = newState;
		newState.Enter();
    }

	public State GetState()
    {
		return this.state;
    }
}
