using UnityEngine;
using System.Collections;

[System.Serializable] // Required so it shows up in the inspector 
public class GUI_ButtonTexture : MonoBehaviour
{
    public Texture normal = null;
    public Texture hover = null;
    public Texture armed = null;

    public Texture this[ButtonState state]
    {
        get
        {
            switch (state)
            {
                case ButtonState.normal:
                    return normal;
                case ButtonState.hover:
                    return hover;
                case ButtonState.armed:
                    return armed;
                default:
                    return null;
            }
        }
    }

    void OnMouseUp()
    {
        //Throw command to game manager to run Co-Routine.
        /*if (GameObject.FindGameObjectWithTag("MainMenu"))
            if (Statics.GUIMain.bTouch)
            {
                Debug.Log("Executed Popup");
                if (gameObject.name.Contains("PlaySlot"))
                {
                    Statics.SlotChosen = GameObject.FindGameObjectWithTag("Defaults").GetComponent<OnlinePlay>().StoredGames[gameObject.transform.parent.GetComponent<SlotScript>().orderinGameTableList];
                    Debug.Log(Statics.SlotChosen.GameId);
                }
                string s = gameObject.name.Remove(0, 4);          
                StartCoroutine(SelectedButton());
                if (s.Contains("BetAmount"))
                    Statics.GUIMain.AmountBet = this.GetComponent<BetsScript>();
                Statics.GUIMain.StartCoroutine("RunFunction", s);
            }
            else{ }
        else if (GameObject.FindGameObjectWithTag("MainGame"))
            if (Statics.GuiTouch.bTouch)
            {
                if (G_Notifications.Notifications != eNotificationType.ReRoll)
                {
                    StartCoroutine(SelectedButton());                   
                    Debug.Log("done with board");
                    string s = gameObject.name.Remove(0, 4);
                    Statics.GuiTouch.StartCoroutine("RunFunction", s);
                    
                }
                else
                {
                    Debug.Log("done with board");
                    string s = gameObject.name.Remove(0, 4);
                    Statics.GuiTouch.StartCoroutine("RunFunction", s);
                }
            }
            else
            { 
            
            }

        else
            Debug.Log("nothing was done.");*/
    }

    public void ToggleTexture()
    {
        if (GetComponent<GUITexture>().texture == normal)
        {
            if (armed!=null)
                GetComponent<GUITexture>().texture = armed;
        }
        else
        {
            if (normal!=null)
            GetComponent<GUITexture>().texture = normal;
        }
    }

    public void Disable()
    {
        GetComponent<GUITexture>().texture = armed;
    }

    public void SwitchUnselectable()
    {
        GetComponent<GUITexture>().texture = armed;
        normal = armed;
    }

    public IEnumerator SelectedButton()
    {
        if (hover)
        {
            GetComponent<GUITexture>().texture = hover;
            yield return new WaitForSeconds(.15f);
            GetComponent<GUITexture>().texture = normal;
        }
    }

    public void Reset()
    {
        GetComponent<GUITexture>().texture = normal;
    }
}



