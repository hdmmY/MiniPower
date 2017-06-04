using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Press : MonoBehaviour {

	public HighLightEffect m_highlightScript;

	private GS_Console _console;

	private void Start()
	{
		_console = GetComponent<GS_Console> ();

		_console.SelectEvent += Selected;
	}

	private void Selected()
	{
		m_highlightScript.m_FadeOnce = true;
	}


}
