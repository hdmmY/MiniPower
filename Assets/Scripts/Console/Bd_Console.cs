using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bd_Console : MonoBehaviour {

	public int m_CurConnected;

	public int m_Needed;


	private void Start()
	{
		m_CurConnected = 0;
		m_Needed = 1;
	}



	#region Event
	public delegate void ConnectedDelegate (); 
	public delegate void SelectDelegate (Color color);

	public ConnectedDelegate ConnectedEvent;
	public SelectDelegate SelectEvent;

	public void CallConnectedEvent()
	{
		if(ConnectedEvent != null)
		{
			ConnectedEvent();
		}
	}


	public void CallSelectEvent(Color color)
	{
		if (SelectEvent != null) 
		{
			SelectEvent (color);
		}
	}

	#endregion

}
