using UnityEngine;
using System.Collections;

public class abberationBend : MonoBehaviour {
    public static Vignetting Vignet;
    public static TwirlEffect Twirl;
    public static float TrippingTill;
    public static float TrippingPower = 0;
    public static bool Tripping = false;
    public bool callScript = false;
	// Use this for initialization
    public static void callTripping(float time)
    {
        Vignet.enabled = true;
        Twirl.enabled = true;
        TrippingTill = Time.time + time;
        Tripping = true;
    }
	void Start () {
        Vignet = gameObject.GetComponent<Vignetting>();
        Vignet.enabled = false;
        Twirl = gameObject.GetComponent<TwirlEffect>();
        Twirl.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (callScript)
        {
            callScript = false;
            callTripping(5);
        }
        if (Tripping)
        {
            if (TrippingTill > Time.time)
            {
                TrippingPower = Mathf.Min(1, TrippingPower + Time.deltaTime);
            }
            else
            {
                TrippingPower = Mathf.Max(0, TrippingPower - Time.deltaTime);
            }
            if(TrippingPower==0){
                Vignet.enabled = false;
                Twirl.enabled = false;
                Tripping=false;
            }
        }
        Vignet.chromaticAberration = Mathf.Sin(Time.time * 0.5f) * 200*TrippingPower;
        Twirl.angle = Mathf.Sin(Time.time * 0.8f) * 10 * TrippingPower;
	}
}
