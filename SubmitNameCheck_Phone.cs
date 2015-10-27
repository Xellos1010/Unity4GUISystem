using UnityEngine;
using System.Collections;

public class SubmitNameCheck_Phone : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        if (gameObject.GetComponent<GUIText>())
        {
            Statics.gt_SubmitNameTextBox = gameObject.GetComponent<GUIText>();
        }
        else if (gameObject.GetComponent<GUITexture>())
        {
            Statics.gt_SubmitNameBox = gameObject.GetComponent<GUITexture>();
        }
	}

    /*protected void NamePressed()
    {
        string s = gameObject.name.Remove(0, 4);
        Statics.GuiTouch.StartCoroutine("RunFunction", s);
    }*/

    TouchScreenKeyboard Keyboard = null;
    string name = "";
    bool Open = false;

    protected IEnumerator NameText()
    {
        Debug.Log("We have executed NameText");
        yield return 0;
        Keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Enter Text");
        Open = true;
        //Statics.GuiTouch.ToggleTouch(false);
        //Reset();
    }

    void Update()
    {
        if (Open)
        {
            Debug.Log("Keyboard is now Open");
            if (Keyboard.active)
            {
                if (Keyboard.text.Length > 15)
                    Keyboard.text = Keyboard.text.Substring(0, 15);
                else
                {
                    name = Keyboard.text;
                    Statics.gt_SubmitNameTextBox.text = Keyboard.text;
                }
            }
            if (Keyboard.wasCanceled || Keyboard.done)
            {
                name = Keyboard.text;
                Statics.gt_SubmitNameTextBox.text = Keyboard.text;
                Open = false;
                //Statics.GuiTouch.ToggleTouch(true);
            }
        }
        else
        {
            if (G_Notifications.ActiveNotificationGame == eNotificationTypeGame.EnterName)
            {
                int count = Input.touchCount; //How many touch's are on the screen
                if (count < 2) //Checks to make sure there is only 1 touch at a time
                    for (int i = 0; i < count; i++) //Makes Cycles through all the touches in this case the first
                    {
                        Touch touch = Input.GetTouch(i); //Gets the touch information
                        //Debug.Log("Touch for Dice Aquired");

                        if (gameObject.GetComponent<GUITexture>())
                        {
                            if (gameObject.GetComponent<GUITexture>().HitTest(touch.position)) //Does a hit test for the specifc texture associates with this class instance
                            {
                                Debug.Log("Target Defined " + gameObject.name);
                                if (touch.phase == TouchPhase.Ended) // Makes sure the guiTexture in enabled and the finger has left the Device
                                {
                                    StartCoroutine(NameText());
                                }
                            }
                        }
                        else if (gameObject.GetComponent<GUIText>())
                        {
                            if (gameObject.GetComponent<GUIText>().HitTest(touch.position)) //Does a hit test for the specifc texture associates with this class instance
                            {
                                Debug.Log("Target Defined " + gameObject.name);
                                if (touch.phase == TouchPhase.Ended) // Makes sure the guiTexture in enabled and the finger has left the Device
                                {
                                    StartCoroutine(NameText());
                                }
                            }
                        }
                    }
            }
        }
    }

}
