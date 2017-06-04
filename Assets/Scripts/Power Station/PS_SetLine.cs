using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_SetLine : MonoBehaviour {

	public float m_MaxDistance;

	public Material m_WrongMaterial;

	public GameObject m_TempleLine;
	public Material m_TempleMaterial;

	private PS_Console _console;
	private Camera _mainCamera;


	private void Start()
	{
		SetInitReference ();

		// events
		_console.SelectEvent += Selected;
	}


	private void Selected(Color color)
	{
		StartCoroutine(StartSetLine(color));
	}


	IEnumerator StartSetLine(Color32 color)
	{
		Vector3 offset = Vector3.forward;

		Vector3 endPoint = transform.position;
		float distance = 0f;

		GameObject line = Instantiate (m_TempleLine, transform);
		line.name = "PS_Line";
		LineRenderer lineRender = line.GetComponent<LineRenderer> ();

		Material rightMaterial = new Material (m_TempleMaterial);
		rightMaterial.color = color;

		while (Input.GetMouseButton (0)) 
		{
			endPoint = _mainCamera.ScreenToWorldPoint (Input.mousePosition);
			endPoint.z = transform.position.z;

			distance = Vector3.Distance (transform.position, endPoint);

			if (distance < m_MaxDistance) {
				lineRender.material = rightMaterial;
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

		if (hit2D.collider != null && hit2D.collider.tag == _console.GetCorrBuildingTag) {
			if (distance < m_MaxDistance && distance > 0.001f) {
				if (TryConnected (hit2D.transform)) {
					lineRender.SetPosition (0, transform.position + offset);
					lineRender.SetPosition (1, hit2D.collider.transform.position + offset);

					hit2D.transform.GetComponent<Bd_Console> ().CallSelectEvent (hit2D.transform.GetComponent<Bd_Press>().m_pressColor);
					_console.m_lines.Add (lineRender);
					yield break;
				}
			} 

		} 

		Destroy (line);
	}


	#region Toggles
	private void SetInitReference()
	{
		_console = GetComponent<PS_Console> ();
		_mainCamera = _console.m_MainCamera;
	}


	// it is called when a building will be connected
	private bool TryConnected(Transform hitTrans)
	{
		Bd_Console bdConsole = hitTrans.GetComponent<Bd_Console> ();

		if (bdConsole.m_Needed <= bdConsole.m_CurConnected) {
			return false;
		}

		bdConsole.CallConnectedEvent ();
		bdConsole.m_CurConnected++;

		return true;


	}


	#endregion



}
