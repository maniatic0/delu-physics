using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DeathHandler : MonoBehaviour {

	private Vector3 startPos;

	[SerializeField]
	private string[] deathTags;

	public UnityEvent onDeath;

	public UnityEvent onRevive;

	public bool Death {get; private set;}

	private void Awake() {
		startPos = transform.parent.position;
		Death = false;
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Other", other.transform);
		if (!Death && CheckTag(other.transform.tag)) {
			Debug.Log("Death", other.transform);
			Death = true;
			onDeath.Invoke();
			StartCoroutine(WaitToRevive());
		}
	}

	IEnumerator WaitToRevive() {
		yield return null;
		yield return new WaitUntil(() => Input.anyKeyDown);
		Debug.Log("Reviving", this);
		Death = false;
		transform.parent.position = startPos;
		onRevive.Invoke();
	}

	private bool CheckTag(string tag) {
		for (int i = 0; i < deathTags.Length; i++)
		{
			if (deathTags[i] == tag) {
				return true;
			}
		}
		return false;
	}
}
