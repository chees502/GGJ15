using UnityEngine;
using System.Collections;

public class EKG : MonoBehaviour {
    public Texture2D surface;
    public Texture2D refrenceFG;
    public Texture2D flatLine;
    public Renderer sweepMat;
    public Camera castCam;
    Transform parent;
    public int index=0;
    public static bool flatLining = false;
    public float resetTimer=float.MaxValue;
	// Use this for initialization
	void Start () {
        surface = new Texture2D(600, 256);
        surface.wrapMode = TextureWrapMode.Clamp;
        renderer.material.SetTexture("_MainTex", surface);
        parent = transform.parent;
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
        if (flatLining)
        {
            parent.position = Vector3.Lerp(parent.position, Vector3.zero, Time.deltaTime*0.75f);
            parent.localScale = Vector3.Lerp(parent.localScale, new Vector3(4,4,4), Time.deltaTime * 0.25f);
            if (Vector3.Distance(parent.position, Vector3.zero) < 0.1f)
            {
                refrenceFG = flatLine;
                resetTimer = Time.time + 10;
            }
            if (resetTimer < Time.time)
            {
                Application.LoadLevel("Menu");
            }
        }
	}
    public static void FlatLine()
    {
        flatLining = true;
    }
}
