using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_ShowGSIcon : MonoBehaviour {

	public List<GameObject> m_GsLinePoints;

	private PS_Console _console;
	private Camera _camera;

	private List<SpriteRenderer> _spriteRenders;


	void Start()
	{
		SetInitReference ();

		// event
		_console.AddElectEvent += UpdateIcon;
	}


	void Update()
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit2D = Physics2D.Raycast (ray.origin, ray.direction, 100f);

			if (hit2D.collider != null && hit2D.transform.tag == _console.m_GsNodeTag) 
			{
				Color color = hit2D.transform.GetComponent<SpriteRenderer> ().color;

				if(hit2D.collider.transform.parent == transform)
					_console.CallSelect (color);
			}
		}
	}



	void SetInitReference()
	{
		_spriteRenders = new List<SpriteRenderer> ();
		foreach (GameObject gameOb in m_GsLinePoints) 
		{
			_spriteRenders.Add(gameOb.GetComponent<SpriteRenderer>());
		}

		_console = GetComponent<PS_Console> ();
		_camera = _console.m_MainCamera;
	}


	void UpdateIcon(GS_Console gsConsole, LineRenderer gsLineRender)
	{
		int index = 0;

		while (m_GsLinePoints [index].active)
			index++;

		m_GsLinePoints [index].SetActive (true);
		_spriteRenders [index].color = gsConsole.m_gsColor;

		_console.m_gsConsole.Add (gsConsole);
		_console.m_gsLines.Add (gsLineRender);
	}


	void RecieveInput()
	{
		
	}

}
