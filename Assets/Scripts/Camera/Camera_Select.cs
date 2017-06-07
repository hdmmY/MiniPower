using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class Camera_Select : MonoBehaviour {

	public string IndustoryPowerStationTag;
	public string NormalPowerStationTag;
	public string IndustoryBuildingTag;
	public string NormalBuildingTag;
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
					hit2D.transform.GetComponent<PS_Console> ().CallSelect (Color.white);
					return;
				}

				if (tag == GenerateStationTag) 
				{
					hit2D.transform.GetComponent<GS_Console> ().m_Selected = true;

					return;
				}

				if (tag == IndustoryBuildingTag || tag == NormalBuildingTag) {
					hit2D.transform.GetComponent<Bd_Console> ().CallSelectEvent (Color.white);
				}
			}
		}





	}

}
