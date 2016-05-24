using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHud : MonoBehaviour {
	static GameHud instance = null;
	[SerializeField] Image WeaponCoolDown;
	[SerializeField] Image DefenseBuffTimer;
	[SerializeField] Image DefenseBackground;
	// Use this for initialization
	void Start () {
		if (instance != null) {
			Destroy (this.gameObject);
			return;
		}
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static public void HandleUIUpdateWeapon(float ratio)
	{
		if (instance == null)
			return;
		Color weaponColor = instance.WeaponCoolDown.color;
		weaponColor.a = (ratio < 1.0f ? (0.2f + ratio * 0.5f) : 1.0f);
		instance.WeaponCoolDown.color = weaponColor;
	}

	static public void HandleUIUpdateArmor(float ratio)
	{
		if (instance == null)
			return;
		Color defenseColor = instance.DefenseBuffTimer.color;
		defenseColor.a = 1.0f - ratio;
		instance.DefenseBuffTimer.color = defenseColor;
	}

	static public void UpdateWeaponIcon(Sprite _icon)
	{
		if (instance == null)
			return;

		instance.WeaponCoolDown.sprite = _icon;
	}

	static public void UpdateDefenseIcon(Sprite _icon)
	{
		if (instance == null)
			return;

		instance.DefenseBuffTimer.sprite = _icon;
		instance.DefenseBuffTimer.color = new Color (1, 1, 1, 1);
	}

	static public void ActivateDefenseUI()
	{
		if (instance == null)
			return;
		Color defenseBGColor = instance.DefenseBackground.color;
		defenseBGColor.a = 1.0f;
		instance.DefenseBackground.color = defenseBGColor;
	}

	static public void DeactivateDefenseUI()
	{
		if (instance == null)
			return;
		Color defenseBGColor = instance.DefenseBackground.color;
		defenseBGColor.a = 0.0f;
		instance.DefenseBackground.color = defenseBGColor;
	}

	void OnDestroy()
	{
		if (this == instance) {
			instance = null;
		}
	}
}
