using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	public float m_totalTime;

	public int CurrentGroupIndex = 0;

	public float m_appearSpeed = 20f;

	[System.Serializable]
	public struct TimerAppear{
		public Transform Group;
		public float DelyTime;
	}

	public List<TimerAppear> Controller;

	void Update()
	{
		m_totalTime += Time.deltaTime;

		if (m_totalTime > Controller [CurrentGroupIndex].DelyTime) 
		{
			m_totalTime = 0f;

			Controller [CurrentGroupIndex].Group.gameObject.SetActive (true);

			foreach (Transform trans in Controller[CurrentGroupIndex].Group.GetComponentsInChildren<Transform>()) 
			{
				if (trans.parent == Controller [CurrentGroupIndex].Group) {
					
					StartCoroutine (Appear (trans));
				}
			}
			CurrentGroupIndex++;
		}
	}

	IEnumerator Appear(Transform trans)
	{
		SpriteRenderer sprite = trans.GetComponent<SpriteRenderer> ();

		Color curColor = sprite.color;
		curColor.a = 0f;

		sprite.color = curColor;

		trans.GetComponent<Bd_Dead> ().m_active = true;

		while (curColor.a <= 1f - 0.01f) {
			curColor.a += m_appearSpeed * Time.deltaTime / 100f;
			sprite.color = curColor;
			yield return null;
		}


	}

}
