using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sprite_Reskin : MonoBehaviour {
	public string subFolder;
	public string SpriteSheet;
	SpriteRenderer myRenderer;

	void Start()
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer> ();
		if (!myRenderer)
			Destroy (this);
	}

	// Update is called once per frame
	void LateUpdate () {

		Sprite[] sprites = Resources.LoadAll<Sprite> ("Sprites/" + subFolder + "/" + SpriteSheet);
		string SpriteName = myRenderer.sprite.name;
		Sprite newSprite = null;

		for (int i = 0; i < sprites.Length; i++) {
			Sprite temporarySprite = sprites.GetValue (i) as Sprite;
			if (temporarySprite.name.Equals (SpriteName)) {
				newSprite = temporarySprite;
				break;
			}
		}
		if (newSprite)
			myRenderer.sprite = newSprite;

	}
}
