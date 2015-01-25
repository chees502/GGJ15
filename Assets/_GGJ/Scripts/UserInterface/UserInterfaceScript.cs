using UnityEngine;
using System.Collections;

public class UserInterfaceScript : MonoBehaviour {

    private GameObject TimerObject;
    private GameObject Icon1, Icon2, Icon3, Icon4, Icon5;
    private GameObject ObjectivesBox, ObjectivesText;
    private bool[] Moving = { false, false, false, false, false };
    public float timeLeft = 180.0f;
    private bool iconMoving = false;
    public int curObjective;

    private Vector3 pos1 = new Vector3(0.025f, 0.915f, -1.0f);
    private Vector3 pos2 = new Vector3(0.125f, 0.9475f, -2.0f);
    private Vector3 pos3 = new Vector3(0.175f, 0.9475f, -3.0f);
    private Vector3 pos4 = new Vector3(0.175f, 0.88125f, -4.0f);
    private Vector3 pos5 = new Vector3(0.125f, 0.88125f, -5.0f);

    private Rect inset1 = new Rect(0.0f, -50.0f, 100.0f, 100.0f);
    private Rect inset2 = new Rect(0.0f, -25.0f, 50.0f, 50.0f);

    private Color Opaque = new Color(128.0f, 128.0f, 128.0f, 128.0f);
    private Color Transparent = new Color(128.0f, 128.0f, 128.0f, 0.0f);

    //Timer
    void FormatTime(float time) {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        TimerObject.guiText.text = minutes + ":" + seconds;
    }

    void Countdown() {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {
            //Game over
        }else {
            FormatTime(timeLeft);            
        }
    }
    
    //Icon Animations
    void FadeIn(GameObject Icon)
    {
        if (Icon.guiTexture.color.a >= 128.0f)
        {
            Icon.guiTexture.color = new Color(128.0f, 128.0f, 128.0f, 128.0f);
        }
        else
        {
            float alpha = Icon.guiTexture.color.a;
            alpha += 0.5f * Time.deltaTime;
            Icon.guiTexture.color = new Color(128.0f, 128.0f, 128.0f, alpha);
        }
    }

    void FadeOut(GameObject Icon)
    {
        if (Icon.guiTexture.color.a <= 0.0f)
        {
            Icon.guiTexture.color = new Color(128.0f, 128.0f, 128.0f, 0.0f);
        } 
        else
        {
            float alpha = Icon.guiTexture.color.a;
            alpha -= 0.5f * Time.deltaTime;
            Icon.guiTexture.color = new Color(128.0f, 128.0f, 128.0f, alpha);
        }
    }

    void SlideLeft(GameObject Icon, Vector3 EndPos, float Multiplier) {
        if(Multiplier == null) Multiplier = 1;
        float x = Icon.transform.position.x;
        float y = Icon.transform.position.y;
        if (x <= EndPos.x)
        {
            Icon.transform.position = new Vector3(EndPos.x, y, EndPos.z);
        }
        else {
            x -= 0.05f * Multiplier * Time.deltaTime;
            Icon.transform.position = new Vector3(x, y, EndPos.z);
        }
    }

    void SlideRight(GameObject Icon, Vector3 EndPos, float Multiplier) {
        if (Multiplier == null) Multiplier = 1;
        float x = Icon.transform.position.x;
        float y = Icon.transform.position.y;
        if (x >= EndPos.x)
        {
            Icon.transform.position = new Vector3(EndPos.x, y, EndPos.z);
        }
        else
        {
            x += 0.05f * Multiplier * Time.deltaTime;
            Icon.transform.position = new Vector3(x, y, EndPos.z);
        }
    }

    void SlideUp(GameObject Icon, Vector3 EndPos, float Multiplier) {
        if (Multiplier == null) Multiplier = 1;
        float x = Icon.transform.position.x;
        float y = Icon.transform.position.y;
        if (y >= EndPos.y)
        {
            Icon.transform.position = new Vector3(x, EndPos.y, EndPos.z);
        }
        else {
            y += 0.01f * Multiplier * Time.deltaTime;
            Icon.transform.position = new Vector3(x, y, EndPos.z);
        }
    }

    void SlideDown(GameObject Icon, Vector3 EndPos, float Multiplier) {
        if (Multiplier == null) Multiplier = 1;
        float x = Icon.transform.position.x;
        float y = Icon.transform.position.y;
        if (y <= EndPos.y)
        {
            Icon.transform.position = new Vector3(x, EndPos.y, EndPos.z);
        }
        else {
            y -= 0.01f * Multiplier * Time.deltaTime;
            Icon.transform.position = new Vector3(x, y, EndPos.z);
        }
    }

    void ScaleUp(GameObject Icon, Rect EndInset) {
        float y = Icon.guiTexture.pixelInset.y;
        float w = Icon.guiTexture.pixelInset.width;
        float h = Icon.guiTexture.pixelInset.height;
        if (y <= EndInset.y)
        {
            y = EndInset.y;
        }
        else {
            y -= 50 * Time.deltaTime;
        }
        if (w >= EndInset.width)
        {
            w = EndInset.width;
        }
        else {
            w += 50 * Time.deltaTime;
        }
        if (h >= EndInset.height)
        {
            h = EndInset.height;
        }
        else {
            h += 50 * Time.deltaTime;
        }
        Icon.guiTexture.pixelInset = new Rect(EndInset.x, y, w, h);
    }

    void ScaleDown(GameObject Icon, Rect EndInset) {
        float y = Icon.guiTexture.pixelInset.y;
        float w = Icon.guiTexture.pixelInset.width;
        float h = Icon.guiTexture.pixelInset.height;
        if (y >= EndInset.y)
        {
            y = EndInset.y;
        }
        else
        {
            y += 50 * 2 * Time.deltaTime;
        }
        if (w <= EndInset.width)
        {
            w = EndInset.width;
        }
        else
        {
            w -= 50 * Time.deltaTime;
        }
        if (h <= EndInset.height)
        {
            h = EndInset.height;
        }
        else
        {
            h -= 50 * Time.deltaTime;
        }
        Icon.guiTexture.pixelInset = new Rect(EndInset.x, y, w, h);
    }

    int GetIconPosition(GameObject Icon){
        return Icon.GetComponent<IconScript>().IconPosition;
    }

    void SetIconTexture(GameObject Icon, Texture IconTexture)
    {
        Icon.guiTexture.texture = IconTexture;
    }

    void MoveIcon(GameObject Icon, int num) {
        int position = GetIconPosition(Icon);
        if (position == 1)
        {
            FadeOut(Icon);
            SlideDown(Icon, pos5, 4);
            SlideRight(Icon, pos5, 2f);
            ScaleDown(Icon, inset2);
        }
        else if (position == 2)
        {
            SlideLeft(Icon, pos1, 2);
            SlideDown(Icon, pos1, 3);
            ScaleUp(Icon, inset1);
        }
        else if(position == 3)
        {
            SlideLeft(Icon, pos2, 1);        
        }
        else if (position == 4)
        {
            FadeIn(Icon);
            SlideUp(Icon, pos3, 5);        
        }
        else if(position == 5)
        {
            SlideRight(Icon, pos4, 1);
        }
        else
        {

        }
        Moving[num] = CheckMoving(Icon);        
    }
    
    bool CheckMoving(GameObject Icon)
    {
        //Check to make sure all x translations are complete
        if (Icon.transform.position.x == pos1.x || Icon.transform.position.x == pos2.x || Icon.transform.position.x == pos3.x || Icon.transform.position.x == pos4.x || Icon.transform.position.x == pos5.x)
        {
            //Check to make sure all y translations are complete
            if (Icon.transform.position.y == pos1.y || Icon.transform.position.y == pos2.y || Icon.transform.position.y == pos3.y || Icon.transform.position.y == pos4.y || Icon.transform.position.y == pos1.y)
            {
                //Check to make sure all y offsets are complete
                if (Icon.guiTexture.pixelInset.y == inset1.y || Icon.guiTexture.pixelInset.y == inset2.y) { 
                    //Check to make sure all width scaling is complete
                    if (Icon.guiTexture.pixelInset.width == inset1.width || Icon.guiTexture.pixelInset.width == inset2.width) { 
                        //Check to make sure all height scalling is complete
                        if (Icon.guiTexture.pixelInset.height == inset1.height || Icon.guiTexture.pixelInset.height == inset2.height)
                        {
                            return false;
                        }
                        else return true;
                    }
                    else return true;
                }
                else return true;
            }
            else return true;
        }
        else return true;
    }

    bool CheckFullRotation(){
        if(!Moving[0] && !Moving[1] && !Moving[2] && !Moving[3] && !Moving[4]){
            return false;
        } else{
            return true;
        }
    }

    void AnimateIcons() {
        if(iconMoving)
        {
            GameObject[] Icons = { Icon1, Icon2, Icon3, Icon4, Icon5 };
            for (int i = 0; i < 5; i++) { 
                MoveIcon(Icons[i], i);
            }
            iconMoving = CheckFullRotation();
            if (iconMoving == false) {
                for (int ii = 0; ii < Icons.Length; ii++) {
                    Icons[ii].GetComponent<IconScript>().IconPosition -= 1;
                    if (Icons[ii].GetComponent<IconScript>().IconPosition <= 0) Icons[ii].GetComponent<IconScript>().IconPosition = 5;
                }
            }
        }
    }

    //Objectives!
    void UpdateObjective(int objective) {
        
    }
   
    void Awake()
    {
        //Create Timer Object
        TimerObject = new GameObject("Timer");
        TimerObject.transform.position = new Vector3(0.5f, 0.98f, 0.5f);
        TimerObject.AddComponent("GUIText");
        TimerObject.guiText.anchor = TextAnchor.UpperCenter;
        TimerObject.guiText.alignment = TextAlignment.Center;
        TimerObject.guiText.fontSize = 32;
        FormatTime(timeLeft);
        //Create Icons
        //Icon 01
        Icon1 = new GameObject("Icon01");
        Icon1.transform.position = new Vector3(0.025f, 0.915f, 0.0f);
        Icon1.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon1.AddComponent("GUITexture");
        Icon1.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder1") as Texture;
        Icon1.guiTexture.pixelInset = new Rect(0.0f, -50.0f, 100.0f, 100.0f);
        Icon1.AddComponent<IconScript>();
        Icon1.GetComponent<IconScript>().IconPosition = 1;
        //Icon 02
        Icon2 = new GameObject("Icon02");
        Icon2.transform.position = new Vector3(0.125f, 0.9475f, 0.0f);
        Icon2.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon2.AddComponent("GUITexture");
        Icon2.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder2") as Texture;
        Icon2.guiTexture.pixelInset = new Rect(0.0f, -25.0f, 50.0f, 50.0f);
        Icon2.AddComponent<IconScript>();
        Icon2.GetComponent<IconScript>().IconPosition = 2;
        //Icon 03
        Icon3 = new GameObject("Icon03");
        Icon3.transform.position = new Vector3(0.175f, 0.9475f, 0.0f);
        Icon3.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon3.AddComponent("GUITexture");
        Icon3.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder3") as Texture;
        Icon3.guiTexture.pixelInset = new Rect(0.0f, -25.0f, 50.0f, 50.0f);
        Icon3.AddComponent<IconScript>();
        Icon3.GetComponent<IconScript>().IconPosition = 3;
        //Icon 04
        Icon4 = new GameObject("Icon04");
        Icon4.transform.position = new Vector3(0.175f, 0.88125f, 0.0f);
        Icon4.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon4.AddComponent("GUITexture");
        Icon4.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder4") as Texture;
        Icon4.guiTexture.pixelInset = new Rect(0.0f, -25.0f, 50.0f, 50.0f);
        Icon4.guiTexture.color = Transparent;
        Icon4.AddComponent<IconScript>();
        Icon4.GetComponent<IconScript>().IconPosition = 4;
        //Icon 05
        Icon5 = new GameObject("Icon05");
        Icon5.transform.position = new Vector3(0.125f, 0.88125f, 0.0f);
        Icon5.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        Icon5.AddComponent("GUITexture");
        Icon5.guiTexture.texture = Resources.Load("GUITextures/PickupPlaceHolder5") as Texture;
        Icon5.guiTexture.pixelInset = new Rect(0.0f, -25.0f, 50.0f, 50.0f);
        Icon5.guiTexture.color = Transparent;
        Icon5.AddComponent<IconScript>();
        Icon5.GetComponent<IconScript>().IconPosition = 5;
        //Objective Text
        ObjectivesBox = new GameObject("ObjectivesBox");
        ObjectivesBox.transform.position = new Vector3(0.8f, 0.98f, -1.0f);
        ObjectivesBox.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        ObjectivesBox.AddComponent("GUITexture");
        ObjectivesBox.guiTexture.texture = Resources.Load("GUITextures/BlankTexture") as Texture;
        ObjectivesBox.guiTexture.pixelInset = new Rect(-175.0f, -28.0f, 350.0f, 33.0f);
        ObjectivesBox.guiTexture.color = new Color(0.0f, 0.0f, 0.0f, 0.25f);
        ObjectivesText = new GameObject("ObjectivesText");
        ObjectivesText.transform.position = new Vector3(0.8f, 0.98f, 0.0f);
        ObjectivesText.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        ObjectivesText.AddComponent<ObjectiveScript>();
        ObjectivesText.AddComponent("GUIText");
        ObjectivesText.guiText.text = ObjectivesText.GetComponent<ObjectiveScript>().ObjectiveDesc[curObjective];
        ObjectivesText.guiText.fontSize = 20;
        ObjectivesText.guiText.anchor = TextAnchor.UpperCenter;
        ObjectivesText.guiText.alignment = TextAlignment.Center;
    }

    //On Frame
    void Update() {
        Countdown();
        AnimateIcons();
        UpdateObjective(curObjective);
    }
}
