using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Drag : MonoBehaviour {

	private GS_Console _console;
	private Camera _mainCamera;

	private void Awake()
	{
		_console = GetComponent<GS_Console> ();

		_mainCamera = _console.m_MainCamera;

		_console.SelectEvent += Selected;
	}


	private void Selected()
	{
		if (!_console.m_CanDrag)
			return;

		_console.m_CanDrag = false;
			
		StartCoroutine (Drag ());


	}


	IEnumerator Drag()
	{
		while (Input.GetMouseButton (0)) 
		{
			Vector3 position = _mainCamera.ScreenToWorldPoint (Input.mousePosition);

			position.z = transform.position.z;

			transform.position = position;

			yield return null;
		}
	}
}
