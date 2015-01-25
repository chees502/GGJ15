using UnityEngine;
using System.Collections;

public class EKG : MonoBehaviour {
    public Texture2D surface;
    public Texture2D refrenceFG;
    public Renderer sweepMat;
    public int index=0;
	// Use this for initialization
	void Start () {
        surface = new Texture2D(600, 256);
        surface.wrapMode = TextureWrapMode.Clamp;
        renderer.material.SetTexture("_MainTex", surface);
	}
	
	// Update is called once per frame
	void Update () {

        for (int x = 0; x < 255; x++)
        {
            Color32 clr = refrenceFG.GetPixel(index%refrenceFG.width, x);
            surface.SetPixel(index%600, x,clr);
        }
        surface.SetPixel(index % 600, 256, new Color32(0, 0, 0, 0));
        sweepMat.material.SetTextureOffset("_MainTex", new Vector2(index*-.0016666f,0));
        surface.Apply();
        index++;
	}
}
