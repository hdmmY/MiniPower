using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Console : MonoBehaviour {

	public int m_TotalElect;
	public int m_UsedElect;

	public bool m_CanDrag;

	public bool m_Selected;

	public Camera m_MainCamera;

	public Color32 m_gsColor;

	public string m_IndustoryPowerStationTag = "PS_Industory";
	public string m_NormalPowerStationTag = "PS_Normal";

	public List<PS_Console> m_psConsoles;


	private void Start()
	{
		SetInitReference ();

		m_Selected = false;
	}

	private void Update()
	{
		if (m_Selected) 
		{
			CallSelect ();

			m_Selected = false;
		}


	}


	private void SetInitReference()
	{
		m_psConsoles = new List<PS_Console> ();
	}


	#region Event
	public delegate void ConnectPsDelegate(PS_Console psConsole);
	public delegate void ConnectGdDelegate(Bd_Console bdConsole);
	public delegate void SelectDelegate();
	public delegate void DisConnectDelegate ();


	public ConnectPsDelegate ConnectPsEvent;
	public ConnectGdDelegate ConnectBdEvent;
	public SelectDelegate SelectEvent;
	public DisConnectDelegate DisConnectEvent;

	public void CallConnectPs(PS_Console psConsole)
	{
		if (ConnectPsEvent != null) {
			ConnectPsEvent (psConsole);
		}
	}

	public void CallConnectBd(Bd_Console bdConsole)
	{
		if (ConnectBdEvent != null) {
			ConnectBdEvent (bdConsole);
		}
	}

	public void CallSelect()
	{
		if (SelectEvent != null) 
		{
			SelectEvent ();
		}
	}

	public void CallDisConnectEvent()
	{
		if (DisConnectEvent != null) 
		{
			DisConnectEvent ();
		}
	}

	#endregion
}
