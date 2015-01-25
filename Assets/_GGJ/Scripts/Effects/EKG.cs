using UnityEngine;
using System.Collections;

public class EKG : MonoBehaviour {
    public Texture2D surface;
    public Texture2D refrenceFG;
    public Texture2D flatLine;
    public Renderer fadeBG;
    public Renderer sweepMat;
    public Camera castCam;
    public float alpha = 0;
    Transform parent;
    public int index=0;
    public static bool flatLining = false;
    public bool hitMe = false;
	// Use this for initialization
	void Start () {
        surface = new Texture2D(600, 256);
        surface.wrapMode = TextureWrapMode.Clamp;
        renderer.material.SetTexture("_MainTex", surface);
        parent = transform.parent;
        for (int x = 0; x < surface.width; x++)
        {
            for (int y = 0; y < surface.height; y++)
            {
                surface.SetPixel(x, y, Color.black);
            }
        }
        surface.Apply();
	}

    // Update is called once per frame
    void Update()
    {

        for (int x = 0; x < 255; x++)
        {
            Color32 clr = refrenceFG.GetPixel(index%refrenceFG.width, x);
            surface.SetPixel(index%600, x,clr);
        }
        surface.SetPixel(index % 600, 256, new Color32(0, 0, 0, 0));
        sweepMat.material.SetTextureOffset("_MainTex", new Vector2(index*-.0016666f,0));
        surface.Apply();
        index++;
        if (hitMe)
        {
            flatLining = true;
        }
        if (flatLining)
        {
            parent.position = Vector3.Lerp(parent.position, Vector3.zero, Time.deltaTime*0.75f);
            parent.localScale = Vector3.Lerp(parent.localScale, new Vector3(4,4,4), Time.deltaTime * 0.25f);
            Debug.Log(Time.deltaTime);
            float dist = Vector3.Distance(parent.position, Vector3.zero);
            if (dist < 0.25f)
            {
                alpha = Mathf.Lerp(alpha, 1.0f, Time.deltaTime);
                fadeBG.material.SetColor("_Color",new Color(0,0,0,alpha));
            }
            if (dist < 0.1f && index % refrenceFG.width>240)
            {
                refrenceFG = flatLine;
            }
        }
	}
    public static void FlatLine()
    {
        flatLining = true;
    }
}
