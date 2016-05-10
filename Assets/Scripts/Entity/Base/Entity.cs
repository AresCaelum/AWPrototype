using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	[SerializeField]
	protected Animator myAnimator;

	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		UpdateAnimation ();
	}

	protected virtual void UpdateAnimation()
	{

	}
}
