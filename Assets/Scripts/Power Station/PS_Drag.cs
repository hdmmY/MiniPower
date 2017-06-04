using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Drag : MonoBehaviour {

	public float m_DeathSpeed = 20f;

	private PS_Console _console;

	private Camera _mainCamera;

	void Start()
	{
		SetInitReference ();

		// event
		_console.GsDisConnectedEvent += GsDisConnect;
		_console.BdDisConnectEvent += BdDisConnect;
	}

	private void Update()
	{
		if (_console.m_Selected && _console.m_CanDrag) 
		{
			StartCoroutine (Drag ());
		}

	}


	IEnumerator Drag()
	{
		while (Input.GetMouseButton (0)) 
		{
			Vector3 position = _mainCamera.ScreenToWorldPoint (Input.mousePosition);
			position.z = transform.position.z;

			BdLineCheckAndFollow (position);

			GsCheckAndFollow (position);

			transform.position = position;

			yield return null;
		}

	}

	IEnumerator Death(LineRenderer lineRender)
	{
		Material matrial = lineRender.material;
		matrial.color = transform.GetComponent<PS_SetLine> ().m_WrongMaterial.color;

		Color curColor = matrial.color;

		while (curColor.a >= 0.01f) 
		{
			curColor.a -= m_DeathSpeed * Time.deltaTime / 100f;
			matrial.color = curColor;
			yield return null;
		}

		Destroy (lineRender.gameObject);
	}


	void BdLineCheckAndFollow(Vector3 newPosition)
	{
		List<LineRenderer> newLines = new List<LineRenderer> ();
		List<LineRenderer> deleteLines = new List<LineRenderer> ();

		foreach (LineRenderer lineRender in _console.m_lines) 
		{
			lineRender.SetPosition (0, newPosition);

			float distance = Vector3.Distance (lineRender.GetPosition (0), lineRender.GetPosition (1));
			float maxDistance = GetComponent<PS_SetLine> ().m_MaxDistance;

			if (distance > maxDistance) {
				deleteLines.Add (lineRender);
			} 
			else {
				lineRender.SetPosition (0, newPosition);
				newLines.Add (lineRender);
			}
		}

		_console.m_lines = newLines;

		for (int i = 0; i < deleteLines.Count; i++) 
		{
			Ray ray = _mainCamera.ScreenPointToRay (_mainCamera.WorldToScreenPoint(deleteLines [i].GetPosition (1)));
			RaycastHit2D hit2D = Physics2D.Raycast (ray.origin, ray.direction, 100f);
			Bd_Console bdConsole = hit2D.transform.GetComponent<Bd_Console> ();

			_console.CallBdDisConnectEvent (bdConsole);
			StartCoroutine (Death(deleteLines [i]));
		}

	}



	void GsCheckAndFollow(Vector3 newPosition)
	{
		for (int i = 0; i < _console.m_gsConsole.Count; i++)
		{
			LineRenderer line = _console.m_gsLines[i];
			GS_Console gsConsole = _console.m_gsConsole [i];

			float distances = Vector3.Distance (line.GetPosition (0), line.GetPosition (1));
			float maxDistance = gsConsole.GetComponent<GS_SetLine> ().m_MaxDistance;

			if (distances >= maxDistance) {
				gsConsole.CallDisConnectEvent ();
				_console.CallGsDisConnectEvent (i);
				StartCoroutine (Fade (gsConsole, line));
				i--;
			} 
			else {
				line.SetPosition (1, newPosition);
			}
		}

	}


	IEnumerator Fade(GS_Console gsConsole, LineRenderer lineRender)
	{
		Material matrial = lineRender.material;
		matrial.color = gsConsole.GetComponent<GS_SetLine> ().m_WrongMaterial.color;

		Color curColor = matrial.color;

		while (curColor.a >= 0.01f) 
		{
			curColor.a -= m_DeathSpeed * Time.deltaTime / 100f;
			matrial.color = curColor;
			yield return null;
		}

		Destroy (lineRender.gameObject);
	}


	#region event
	private void GsDisConnect(int index)
	{
		Color gsColor = _console.m_gsLines [index].material.color;

		int i = 0;
		foreach (GameObject Gob in _console.GetComponent<PS_ShowGSIcon>().m_GsLinePoints) 
		{
			Color pointColor = Gob.GetComponent<SpriteRenderer> ().color;

			if (Mathf.Abs(pointColor.r - gsColor.r) <= 0.01f && Mathf.Abs(pointColor.g - gsColor.g) <= 0.01f) 
			{
				break;
			}
			i++;
		}

		_console.GetComponent<PS_ShowGSIcon> ().m_GsLinePoints [i].SetActive (false);

		_console.m_gsLines.RemoveAt (index);
		_console.m_gsConsole.RemoveAt (index);
	}

	private void BdDisConnect(Bd_Console bdConsole)
	{
		bdConsole.m_CurConnected--;
	}


	#endregion

	private void SetInitReference()
	{
		_console = GetComponent<PS_Console> ();

		_mainCamera = _console.m_MainCamera;
	}


}
