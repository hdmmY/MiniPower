using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Press : MonoBehaviour {

	public HighLightEffect m_highlightScript;

	private PS_Console _console;

	private void Start()
	{
		_console = GetComponent<PS_Console> ();

		// event
		_console.SelectEvent += Seleted;
	}

	private void Seleted(Color color)
	{
		m_highlightScript.m_FadeOnce = true;
	}



}
