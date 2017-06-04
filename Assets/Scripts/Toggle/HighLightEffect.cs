using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightEffect : MonoBehaviour {

	public bool m_FadeOnce;

	public float m_StartScale;
	public float m_EndScale;

	public float m_FadeSpeed;

	public GameObject m_HighLightCanvas;

	private bool _isFading;

	private Transform _canvasTrans;
	private SpriteRenderer _spriteRender;

	void Start()
	{
		SetInitReference ();

		_canvasTrans.localScale = m_StartScale * Vector3.one;

		_isFading = false;
	}

	void Update()
	{

		if (m_FadeOnce) 
		{			
			if (_isFading) {
				StopCoroutine (StartFade ());
				_canvasTrans.localScale = m_StartScale * Vector3.one;
				StartCoroutine (StartFade ());
			} 
			else {
				_isFading = true;
				StartCoroutine (StartFade ());
			}

			m_FadeOnce = false;
		}


	}


	IEnumerator StartFade()
	{
		float deltFade = m_EndScale - m_StartScale;

		Vector3 localScale = _canvasTrans.localScale;

		while (localScale.x <= m_EndScale) 
		{
			localScale += deltFade * m_FadeSpeed * Time.deltaTime * Vector3.one;

			_canvasTrans.localScale = localScale ;

			yield return null;
		}

		while (localScale.y >= m_StartScale) 
		{
			localScale -= deltFade * m_FadeSpeed * Time.deltaTime  * Vector3.one;

			_canvasTrans.localScale = localScale;

			yield return null;
		}

		_isFading = false;
	}





	void SetInitReference()
	{
		_canvasTrans = m_HighLightCanvas.transform;
		_spriteRender = _canvasTrans.GetComponent<SpriteRenderer> ();
	}
}
