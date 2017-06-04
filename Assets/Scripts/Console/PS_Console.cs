using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Console : MonoBehaviour {

	public int m_ConnectedNumber;

	public bool m_Selected;

	public bool m_CanDrag;

	public string m_IndustoryPSTag = "PS_Industory";
	public string m_NormalPSTag = "PS_Normal";
	public string m_IndustoryBdTag = "Bd_Industory";
	public string m_NormalBdTag = "Bd_Normal";

	public string m_GsNodeTag = "Gs_Node";

	public Camera m_MainCamera;

	public List<GS_Console> m_gsConsole;

	public List<LineRenderer> m_gsLines;

	public List<LineRenderer> m_lines;

	public string GetCorrBuildingTag
	{
		get
		{
			if (this.tag == m_IndustoryPSTag)
				return m_IndustoryBdTag;

			return m_NormalBdTag;
		}
	}


	private void Start()
	{
		SetInitReference ();

		m_ConnectedNumber = 0;

		m_Selected = false;
	}


	private void Update()
	{
		// make sure that m_Selected is true when it first seleced, and not true when hold the mouse 
		if (m_Selected && !Input.GetMouseButtonDown (0)) 
		{
			m_Selected = false;
		}
	}


	#region Event

	public delegate void ConnectDelegate(Bd_Console bdConsole);
	public delegate void AddElectDelegate(GS_Console gsConsole, LineRenderer gsLineRender);
	public delegate void SelectDelegate (Color color);
	public delegate void GsDisConnectDelegate(int index);
	public delegate void BdDisConnectDelegate(Bd_Console bdConsole);

	public ConnectDelegate ConnectEvent;
	public AddElectDelegate AddElectEvent;
	public SelectDelegate SelectEvent;
	public GsDisConnectDelegate GsDisConnectedEvent;
	public BdDisConnectDelegate BdDisConnectEvent;


	public void CallConnectedEvent(Bd_Console bdConsole)
	{
		if(ConnectEvent != null)
		{
			ConnectEvent(bdConsole);
		}
	}

	public void CallAddElectEvent(GS_Console gsConsole, LineRenderer gsLineRender)
	{
		if (AddElectEvent != null) 
		{
			AddElectEvent (gsConsole, gsLineRender);
		}
	}

	public void CallSelect(Color color)
	{
		if (SelectEvent != null) 
		{
			SelectEvent (color);
		}
	}

	public void CallGsDisConnectEvent(int index)
	{
		if (GsDisConnectedEvent != null) 
		{
			GsDisConnectedEvent (index);
		}
	}

	public void CallBdDisConnectEvent(Bd_Console bdConsole)
	{
		if (BdDisConnectEvent != null) 
		{
			BdDisConnectEvent (bdConsole);
		}
	}

	#endregion


	void SetInitReference()
	{
		m_gsConsole = new List<GS_Console> ();

		m_lines = new List<LineRenderer> ();

		m_gsLines = new List<LineRenderer> ();
	}
}
