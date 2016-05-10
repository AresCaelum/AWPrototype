using UnityEngine;
using System.Collections;

public class WorldUV : MonoBehaviour {
    Renderer myRenderer = null;
    [SerializeField]
    Texture2D texture;
    // Use this for initialization
    void Start () {
        if (myRenderer == null)
            myRenderer = this.GetComponent<Renderer>();

        SetTexture(texture);
	}
	
	// Update is called once per frame
	void Update () {
        myRenderer.material.SetTextureOffset("_MainTex", new Vector2(-transform.position.x + transform.localScale.x * 0.5f, -transform.position.y + transform.localScale.y * 0.5f));
    }

    public void SetTexture(Texture _texture)
    {
        if(myRenderer == null)
            myRenderer = this.GetComponent<Renderer>();
        _texture.wrapMode = TextureWrapMode.Repeat;
        myRenderer.material = new Material(myRenderer.material);
        myRenderer.material.SetTexture("_MainTex", _texture);
        
        myRenderer.material.SetTextureScale("_MainTex",new Vector2(transform.localScale.x, transform.localScale.y));
    }
}
