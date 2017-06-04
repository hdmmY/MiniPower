using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class Camera_Select : MonoBehaviour {

	public string IndustoryPowerStationTag;
	public string NormalPowerStationTag;
	public string BuildingTag;
	public string GenerateStationTag;

	private Camera _camera;

	private void Start()
	{
		_camera = GetComponent<Camera> ();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			
			Ray ray = _camera.ScreenPointToRay (Input.mousePosition);

			RaycastHit2D hit2D = Physics2D.Raycast (ray.origin, ray.direction, 100f);

			if (hit2D.collider != null) 
			{
				string tag = hit2D.transform.tag;

				if (tag == IndustoryPowerStationTag || tag == NormalPowerStationTag) 
				{
					hit2D.transform.GetComponent<PS_Console> ().m_Selected = true;

					return;
				}

				if (tag == GenerateStationTag) 
				{
					hit2D.transform.GetComponent<GS_Console> ().m_Selected = true;

					return;
				}
			}
		}





	}

}
