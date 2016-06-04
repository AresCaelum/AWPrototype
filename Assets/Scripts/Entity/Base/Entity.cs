using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	[SerializeField]
	protected Animator myAnimator;
	[SerializeField]
	protected int nMaxHealth;
	protected int nCurrentHealth;
	// Use this for initialization
	protected virtual void Start () {
		nCurrentHealth = nMaxHealth;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		UpdateAnimation ();
	}

	protected virtual void UpdateAnimation()
	{

	}

	protected float getHealthRatio()
	{
		return (float)nCurrentHealth / (float)nMaxHealth;
	}

	protected virtual void TakeDamage(int _amount)
	{
		if (!isDead ()) {
			nCurrentHealth = Mathf.Max (nCurrentHealth - _amount, 0);

			if (isDead ()) {
				StartCoroutine (HandleDeathAnimation());
			}
		}
	}

	protected virtual void RecoverHealth(int _amount)
	{
		if (!isDead ()) {
			nCurrentHealth = Mathf.Min (nCurrentHealth + _amount, nMaxHealth);
		}
	}

	protected bool isDead()
	{
		return nCurrentHealth < 1;
	}

	protected virtual IEnumerator HandleDeathAnimation()
	{
		yield return new WaitForSeconds(0.0f);
	}
}
