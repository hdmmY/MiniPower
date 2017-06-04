using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bd_Dead : MonoBehaviour {

	public bool m_isDead;

	public bool m_active;

	public Image m_DeadImage;

	public int m_factor;

	public float m_StartDead;

	public float m_DieTime;

	public Text text;

	public float _timer;
	private bool _startDead;

	private Bd_Console _console;

	private void Start()
	{
		_console = GetComponent<Bd_Console> ();

		_timer = 0f;

		m_isDead = false;
	
		// events
		_console.SelectEvent += ResetTimer;
	}

	private void Update()
	{
		if (!m_active)
			return;

		if (m_isDead) {
			m_active = false;
			StartCoroutine (Dead ());
		}

		m_factor = _console.m_Needed - _console.m_CurConnected;

		if (m_factor == 0) {
			_timer = 0f;
			m_DeadImage.fillAmount = 0f;
			_startDead = false;
			return;
		} 

		_timer += Time.deltaTime;

		if (_timer > m_StartDead) {
			float t = (_timer - m_StartDead) / (m_DieTime / m_factor);

			if (t >= 1) {
				m_isDead = true;
				return;
			}

			m_DeadImage.fillAmount = t;
		}
	}


	private void ResetTimer(Color color)
	{
		_timer = 0f;
		_startDead = false;
	}

	IEnumerator Dead()
	{
		text.gameObject.SetActive (true);

		yield return new WaitForSeconds (2.5f);

		text.text = "Two Second....";

		yield return new WaitForSeconds (1f);

		text.text = "One Second....";

		yield return new WaitForSeconds (1f);

		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
	}

}
