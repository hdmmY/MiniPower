using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_ShowNeedIcon : MonoBehaviour {

	public GameObject m_OneIcon;
	public GameObject m_TwoIcon;
	public GameObject m_ThreeIcon;

	public Color32 m_IconColor;

	private GS_Console _console;

	private SpriteRenderer _oneRender;
	private SpriteRenderer _twoRender;
	private SpriteRenderer _threeRender;

	private void Start()
	{
		SetInitReference ();

		ShowCorrectIcon ();
	}


	void Update()
	{
		ShowCorrectIcon ();
	}



	void SetInitReference()
	{
		_console = GetComponent<GS_Console> ();

		_oneRender = m_OneIcon.GetComponent<SpriteRenderer> ();
		_twoRender = m_TwoIcon.GetComponent<SpriteRenderer> ();
		_threeRender = m_ThreeIcon.GetComponent<SpriteRenderer> ();
	}



	void ShowCorrectIcon()
	{
		switch (_console.m_TotalElect - _console.m_UsedElect) 
		{
		case 0:
			m_OneIcon.SetActive (false);
			m_TwoIcon.SetActive (false);
			m_ThreeIcon.SetActive (false);

			break;

		case 1:
			m_OneIcon.SetActive (true);
			m_TwoIcon.SetActive (false);
			m_ThreeIcon.SetActive (false);

			_oneRender.color = m_IconColor;

			break;

		case 2:
			m_OneIcon.SetActive (true);
			m_TwoIcon.SetActive (true);
			m_ThreeIcon.SetActive (false);

			_oneRender.color = m_IconColor;
			_twoRender.color = m_IconColor;

			break;

		case 3:
			m_OneIcon.SetActive (true);
			m_TwoIcon.SetActive (true);
			m_ThreeIcon.SetActive (true);

			_oneRender.color = m_IconColor;
			_twoRender.color = m_IconColor;
			_threeRender.color = m_IconColor;

			break;

		default:
			m_OneIcon.SetActive (false);
			m_TwoIcon.SetActive (false);
			m_ThreeIcon.SetActive (false);

			break;
		}
	}
}
