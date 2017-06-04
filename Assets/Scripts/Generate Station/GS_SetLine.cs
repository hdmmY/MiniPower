using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_SetLine : MonoBehaviour {

	public float m_MaxDistance;

	public Material m_WrongMaterial;
	public Material m_RightMaterial;

	public GameObject m_TempleLine;

	private GS_Console _console;
	private Camera _mainCamera;

	private LineRenderer _lineRender;
	private LineRenderer _testLineRender;

	private void Start()
	{
		SetInitReference ();


		// event
		_console.ConnectPsEvent += Connect;
		_console.SelectEvent += Selected;
	}


	// it is observed by GS_SelectEvent
	private void Selected()
	{
		if (_console.m_CanDrag)
			return;

		StartCoroutine (StartSetLine ());
	}

	IEnumerator StartSetLine()
	{
		Vector3 offset = Vector3.forward;

		Vector3 endPoint = transform.position;
		float distance = 0f;

		GameObject line = Instantiate (m_TempleLine, transform);
		line.name = "GS_Line";
		LineRenderer lineRender = line.GetComponent<LineRenderer> ();

		while (Input.GetMouseButton (0)) 
		{
			endPoint = _mainCamera.ScreenToWorldPoint (Input.mousePosition);
			endPoint.z = transform.position.z;

			distance = Vector3.Distance (transform.position, endPoint);

			if (distance < m_MaxDistance) {
				lineRender.material = m_RightMaterial;
				lineRender.SetPosition (0, transform.position + offset);
				lineRender.SetPosition (1, endPoint + offset);
			} 
			else {
				lineRender.material = m_WrongMaterial;
				lineRender.SetPosition (0, transform.position + offset);
				lineRender.SetPosition (1, endPoint + offset);
			}
			yield return null;
		}

		Ray ray = _mainCamera.ScreenPointToRay (_mainCamera.WorldToScreenPoint (endPoint));
		RaycastHit2D hit2D = Physics2D.Raycast (ray.origin, ray.direction, 100f);

		if (hit2D.collider != null && 
			(hit2D.collider.tag == _console.m_IndustoryPowerStationTag ||
				hit2D.collider.tag == _console.m_NormalPowerStationTag)) {
			if (distance < m_MaxDistance && distance > 0.001f) {
				lineRender.SetPosition (0, transform.position + offset);
				lineRender.SetPosition (1, hit2D.collider.transform.position + offset);

				_console.CallConnectPs (hit2D.transform.GetComponent<PS_Console> ());
				hit2D.transform.GetComponent<PS_Console> ().CallAddElectEvent (_console, lineRender);
				hit2D.transform.GetComponent<PS_Console> ().CallSelect (Color.white);

				yield break;
			} 
		} 

		Destroy (line);
	}


	#region Toggles
	private void SetInitReference()
	{
		_console = GetComponent<GS_Console> ();
		_mainCamera = _console.m_MainCamera;
	}
		

	// it is observed by GS_Console
	private void Connect(PS_Console psConsole)
	{
		_console.m_psConsoles.Add(psConsole);

		//_console.m_UsedElect += 
	}

	#endregion
}
