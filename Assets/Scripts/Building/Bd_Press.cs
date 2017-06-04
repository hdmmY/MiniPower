using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bd_Press : MonoBehaviour {

	public HighLightEffect m_highlightScript;

	public Color m_pressColor;

	private Bd_Console _console;

	private void Start()
	{
		_console = GetComponent<Bd_Console> ();

		// event
		_console.SelectEvent += Seleted;
	}

	private void Seleted(Color color)
	{
		m_highlightScript.m_FadeOnce = true;
	}
}
