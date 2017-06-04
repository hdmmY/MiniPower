using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour {

	public List<GameObject> GenerateStations;

	private int _gsIndex;

	private void Start()
	{
		_gsIndex = 0;
	}

	public void ClickGenerateStaion()
	{
		if (_gsIndex <= GenerateStations.Count) 
		{
			GenerateStations [_gsIndex].GetComponent<GS_Console> ().m_CanDrag = true;
			GenerateStations [_gsIndex].SetActive (true);

			_gsIndex++;
		}

	}

	public void ClickIndustoryPS(GameObject IndustoryPowerStation)
	{
		Transform parent = GameObject.Find ("Power Stations").transform;

		GameObject PS_Industory = Instantiate (IndustoryPowerStation, transform);

		PS_Industory.name = "PS_Industory";
		PS_Industory.transform.SetParent (parent);
		PS_Industory.GetComponent<PS_Console> ().m_MainCamera = 
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}

	public void ClickNormalPS(GameObject NormalPowerStation)
	{
		Transform parent = GameObject.Find ("Power Stations").transform;

		GameObject PS_Normal = Instantiate (NormalPowerStation);

		PS_Normal.name = "PS_Normal";
		PS_Normal.transform.SetParent (parent);
		PS_Normal.GetComponent<PS_Console> ().m_MainCamera = 
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}


}
