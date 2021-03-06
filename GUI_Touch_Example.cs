//
//
//  Generated by StarUML(tm) C# Add-In
//
//  @ Farkle
//  @ Name: GUI_Touch_Example.cs
//  @ 10/17/12 : 10/17/2012
//  @ Evan McCall : 
//
//

using UnityEngine;
using System.Linq;
using System.Collections;
using System;

public class GUI_Touch_Example : GUI_Touch_Base
{
    /// <summary>
    /// The score texture to display the final score
    /// </summary>
    public GUITexture Score = new GUITexture();

    public Transform DieMaster;
    /**********
    Start of the functions
     ***********/

    public override void Start()
    {
        base.Start();
        Reset();
        Debug.Log("Defaults Set for GUI_Touch");
    }

    //Runs the Menu CoRoutine for the Menu Button
    protected IEnumerator Menu()
    {
        //Application.LoadLevelAdditive(2);
        yield return 0;
    }

    protected IEnumerator ToMainMenu()
    {
        yield return 0;
        Statics.SoundManager.Reset();
        Statics.bGems = false;
        Statics.bBetChips = false;
        Statics.bOnline = false;
        Statics.Challenge = false;
        Statics.MainManager.ResetNewGame();
        /*if (!PhotonNetwork.insideLobby)
            PhotonNetwork.LeaveRoom();*/
        Application.LoadLevel(0);
    }

    //Runs the Collect Button Coroutine
    protected IEnumerator Collect()
    {
        //if (!Statics.bFreezeGame || !Statics.bOnline)
        if (G_Notifications.ActiveNotificationGame == eNotificationTypeGame.Null && !Statics.bFreezeGame)
        {
            Debug.Log("Collect executed in GUI_Touch");
            yield return 0;
            if (Statics.MainManager.Rolled && Statics.MainManager.iCommitedDice > 0)
            {
                if (Statics.MainManager.CheckLegal() && Statics.MainManager.iRollScoreTotal >= Statics.iMinimumCollect)
                {

                    //Statics.MainManager.CommitScore(Statics.MainManager.iTurn);
                    if (Statics.MainManager.iScore_Self >= 5000 * (Statics.iGemGain + 1))
                    {
                        Statics.SoundManager.PlayOneShot(Statics.SoundManager.SoundTracks[2]);
                    }
                    else
                        Statics.SoundManager.PlayOneShot(Statics.SoundManager.SoundTracks[3]);
                    //photonView.RPC("",
                    Statics.MainManager.CommitRound();
                }
                else
                {
                    Debug.Log("I am sorry you have to make a valid selection");
                    //Statics.MainManager.iScore += Statics.MainManager.iRollScore;
                    //Statics.MainManager.CommitScore(Statics.MainManager.iTurn);
                    //Statics.MainManager.ClearCommit();
                }
            }
            else
            {
                Debug.Log("You must roll the dice and choose one before you can commit them");
            }
        }
        Reset();
    }

    protected IEnumerator UnFarkle()
    {
        Reset();
        yield return 0;
    }

    protected IEnumerator Multiplier_x2()
    {
        //if (!Statics.bFreezeGame || !Statics.bOnline)
        if (!Statics.bFreezeGame)
        {
            Statics.SoundManager.PlayOneShot(Statics.SoundManager.PowerUp);
            if (G_Notifications.ActiveNotificationGame == eNotificationTypeGame.Null && Statics.MainManager.Rolled && Statics.MainManager.iRollScoreTotal >= 350 && Statics.iGemAmount >= Statics.iPowerupCost && Statics.MainManager.bMultiplier)
            {
                GameObject.Find("GUI_Multiplier_x2").GetComponent<GUI_ButtonTexture>().ToggleTexture();
                Statics.MainManager.Powerup_Use("PowerUpMultiplier_2X");
            }
        }
        Reset();
        yield return 0;
    }

    protected IEnumerator AdditionalRoll()
    {
        //if (!Statics.bFreezeGame || !Statics.bOnline)
        if (!Statics.bFreezeGame)
        {
            Statics.SoundManager.PlayOneShot(Statics.SoundManager.PowerUp);
            if (G_Notifications.ActiveNotificationGame == eNotificationTypeGame.Null && Statics.MainManager.Rolled && Statics.MainManager.CheckReRoll && Statics.iGemAmount >= Statics.iPowerupCost)
            {
                GameObject.Find("GUI_AdditionalRoll").GetComponent<GUI_ButtonTexture>().ToggleTexture();
                Statics.MainManager.Powerup_Use("PowerUpReRoll");
                Statics.SoundManager.PlayOneShot(Statics.SoundManager.SoundEffectClips[1]);
            }
        }
        Reset();
        yield return 0;
    }

    protected IEnumerator ReRoll_1()
    {
        //if (!Statics.bFreezeGame || !Statics.bOnline)
        if (!Statics.bFreezeGame)
        {
            Statics.SoundManager.PlayOneShot(Statics.SoundManager.PowerUp);
            if (Statics.MainManager.Rolled && Statics.iGemAmount >= Statics.iPowerupCost && Statics.MainManager.bReRoll_1Die)
                if (G_Notifications.ActiveNotificationGame == eNotificationTypeGame.Null && Statics.MainManager.Rolled)
                {
                    GameObject.Find("GUI_Close").GetComponent<GUITexture>().enabled = true;
                    if (Statics.MainManager.CheckReRoll_1Die)
                    {
                        GameObject.Find("GUI_ReRoll_1").GetComponent<GUI_ButtonTexture>().ToggleTexture();
                        Statics.MainManager.PowerupReRoll1();
                    }

                }
                else if (G_Notifications.ActiveNotificationGame == eNotificationTypeGame.ReRoll)
                {
                    GameObject.Find("GUI_Close").GetComponent<GUITexture>().enabled = false;
                    Statics.MainManager.UnSelectReRoll();
                }
        }
        Reset();
        yield return 0;
    }

    //Runs the Roll Button CoRoutine
    protected IEnumerator Roll()
    {
        Statics.FreezeGame(false);
        //if (!Statics.bFreezeGame || !Statics.bOnline)
        if (G_Notifications.ActiveNotificationGame == eNotificationTypeGame.Null)
        {
            yield return 0;
            foreach (Transform T in DieMaster)
            {
                if (!T.GetComponent<Die>().bStored)
                    T.GetComponent<Die>().Rolled = true;
            }
            if (Statics.MainManager.iCommitedDice > 0 && Statics.MainManager.CheckLegal() || !Statics.MainManager.Rolled || Statics.MainManager.bUnFarkle)
            {
                if (Statics.MainManager.bBonusRoll)
                {
                    Statics.SoundManager.PlayOneShot(Statics.SoundManager.SoundEffectClips[1]);
                    Statics.MainManager.StartCoroutine("StartBonusRollDice");
                    ToggleTouch(true);
                }
                else
                {
                    Debug.Log("Executed Roll");
                    if (Statics.MainManager.iRollNumber != 0)
                    {
                        if (Statics.MainManager.CheckLegal())
                        {
                            Statics.SoundManager.PlayOneShot(Statics.SoundManager.SoundEffectClips[1]);
                            Statics.MainManager.StartCoroutine("StartRollDice");
                        }
                        else
                            Debug.Log("Please choose atleast 1 Legal die to store this roll");
                    }
                    else
                    {
                        Statics.SoundManager.PlayOneShot(Statics.SoundManager.SoundEffectClips[1]);
                        Statics.MainManager.StartCoroutine("StartRollDice");
                    }
                    ToggleTouch(true);
                }
            }
            else
            {
                Debug.Log("Please choose atleast 1 Legal die to store this roll Rolled=" + Statics.MainManager.Rolled);

                ToggleTouch(true);
            }
        }
        Reset();
    }

    protected IEnumerator UndoFarkle_Yes()
    {
        yield return 0;
        if (Statics.iGemAmount >= Statics.iPowerupCost)
        {
            Statics.SoundManager.PlayOneShot(Statics.SoundManager.PowerUp);
            Statics.MainManager.PowerUp_UndoFarkle_Yes();
        }
        Reset();
    }

    protected IEnumerator UndoFarkle_No()
    {
        yield return 0;
        Statics.SoundManager.PlayOneShot(Statics.SoundManager.PowerUp);
        Statics.MainManager.UndoFarkle_No();
        Reset();
    }

    protected IEnumerator EndGame_Yes()
    {
        //Statics.MainManager.ResetNewGame();
        yield return 0;
        Statics.SoundManager.Reset();
        Statics.MainManager.ResetNewGame();
        /* if (!PhotonNetwork.insideLobby)
             PhotonNetwork.LeaveRoom();*/
        Application.LoadLevel(0);
    }

    protected IEnumerator EndGame_No()
    {
        yield return 0;
        if (Statics.bBetChips)
            if (Statics.iGemAmount >= 3 && Statics.iChipAmount > Statics.iChipsBet)
            {
                Statics.MainManager.ResetNewGame();
                Statics.SoundManager.Reset();
            }
            else
            {
                Statics.MainManager.ResetNewGame();
                Statics.SoundManager.Reset();
                Application.LoadLevel(0);
            }
        else
        {
            Statics.MainManager.ResetNewGame();
            Statics.SoundManager.Reset();
        }
    }

    protected IEnumerator EnterName()
    {
        yield return 0;
        /*if (Statics.HighScore.name == "Anonymous")
        {
            ToggleNotification(eNotificationType.EndGame, false);
            ToggleNotification(eNotificationType.EnterName, true);
        }
        else
        {
            Statics.EndGameCache.ToggleSubmitName(false);
            Statics.MainManager.SubmitScore(name);
        }*/
        Reset();
    }

    protected IEnumerator Options()
    {
        yield return 0;
        Application.LoadLevel(0);
    }

    protected IEnumerator SubmitGameTable()
    {
        yield return 0;
        Statics.MainManager.SubmitGameTable();
        Reset();
    }

    public override IEnumerator RunFunction(string Function)
    {
        Debug.Log("Run Function " + Function + " has been executed");
        bool bExecute = false;
        foreach (string S in EnumeratorFunctions)
        {
            if (S.Contains(Function))// || Function.Contains(S))
            {
                ToggleTouch(false);
                StartCoroutine(S);
                bExecute = true;
                break;
            }
        }
        if (!bExecute)
        {
            Debug.Log("The function " + Function + " is not a valid function to string ");
            Reset();
        }
        return null;
    }

    public override void OnMouseUp()
    {
        Debug.Log("Executed Mouse up in GUI_Touch.cs");
        Debug.Log(Statics.bFreezeGame);
        if (bManageTouch)
        {
            Statics.SoundManager.PlayOneShot(Statics.SoundManager.SoundEffectClips[0]);
            foreach (GUITexture G in TouchObjects)
            {
                if (G.GetComponent<GUITexture>().enabled)
                {
                    if (G.HitTest(Input.mousePosition))
                    {
                        Debug.Log("The button hit was " + G.name);
                        if (G.gameObject.name.Contains("GUI_"))
                        {
                            StartCoroutine(G.GetComponent<GUI_ButtonTexture>().SelectedButton());
                            Statics.SoundManager.PlayOneShot(Statics.SoundManager.SoundEffectClips[0]);
                            string s = G.gameObject.name.Remove(0, 4);
                            StartCoroutine(RunFunction(s));
                            break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator SubmitScore()
    {
        yield return 0;
        if (!Statics.MainManager.bSubmittedName)
        {
            G_Notifications.ActiveNotificationGame = eNotificationTypeGame.SubmitScore;
            G_Notifications.bNotification = true;
        }
        else
        {
            Statics.SoundManager.PlayOneShot(Statics.SoundManager.WrongButton);
        }
        
        Reset();
    }


    IEnumerator SubmitName()
    {
        yield return 0;
        //Move Logic Test to 
        TextAsset textAsset = (TextAsset)Resources.Load("badwords", typeof(TextAsset));
        String[] BadWords;
        BadWords = textAsset.text.Split("\n"[0]).ToArray<String>();


        foreach (String S in BadWords)
        {
            if (Statics.gt_SubmitNameTextBox.text.Contains(S))
            {
                Statics.gt_SubmitNameTextBox.text = "Choose Another Name";
                Reset();
                StopCoroutine("SubmitName");
            }
        }
        Statics.EndGameCache.ToggleSubmitName(false);
        Statics.MainManager.SubmitScore(name);
        ToggleNotification(eNotificationTypeGame.EndGame, true);
        ToggleNotification(eNotificationTypeGame.EnterName, false);
        Reset();
    }

    IEnumerator Close()
    {
        yield return 0;
        if (G_Notifications.ActiveNotificationGame == eNotificationTypeGame.ReRoll)
        {
            Statics.MainManager.UnSelectReRoll();

        }
        GameObject.Find("GUI_Close").GetComponent<GUITexture>().enabled = false;
        Reset();
    }
    

    protected IEnumerator HighScore()
    {
        ToggleNotification(eNotificationTypeGame.HighScore, true);
        ToggleNotification(eNotificationTypeGame.EndGame, false);
        Statics.HSManager.ToggleTable(true);
        G_Notifications.ActiveNotificationGame = eNotificationTypeGame.HighScore;
        Reset();
        yield return 0;
    }

    protected void ToggleNotification(eNotificationTypeGame Notification, bool OnOff)
    {
        //Statics.MainManager.toggleNotification(Statics.MainManager.ReturnNotification(Notification.ToString()), OnOff);
    }

#if (UNITY_IPHONE || UNITY_ANDROID)
    public override void Update()
    {
        if (!Statics.MainManager.GameStarted)
        {
            if (bManageTouch)
            {
                int count = Input.touchCount;
                if (count < 2 && count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Touch touch = Input.GetTouch(i);
                        foreach (GUITexture G in TouchObjects)
                        {
                            if (G.GetComponent<GUITexture>().enabled && G.gameObject.activeSelf)
                            {
                                if (G.HitTest(touch.position))
                                {
                                    if (touch.phase == TouchPhase.Ended)
                                    {
                                        Debug.Log("Phase Ended");
                                        if (G.gameObject.name.Contains("GUI_"))
                                        {
                                            string s = G.gameObject.name.Remove(0, 4);
                                            Debug.Log("Running " + s);
                                            StartCoroutine("RunFunction", s);
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                        foreach (GUIText G in TouchText)
                        {
                            if (G.GetComponent<GUIText>().enabled)
                            {
                                if (G.HitTest(touch.position))
                                {

                                    Debug.Log("The button hit was " + G.name);
                                    if (touch.phase == TouchPhase.Ended && touch.phase != TouchPhase.Stationary)
                                    {
                                        if (G.gameObject.name.Contains("GUI_"))
                                        {
                                            string s = G.gameObject.name.Remove(0, 4);
                                            StartCoroutine("RunFunction", s);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("GameFrozen");
            int count = Input.touchCount;
            if (count < 2 && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    foreach (GUITexture G in TouchObjects)
                    {
                        if (G.GetComponent<GUITexture>().enabled)
                        {
                            if (G.HitTest(touch.position))
                            {

                                Debug.Log("The button hit was " + G.name);
                                if (touch.phase == TouchPhase.Ended)
                                {
                                    if (G.gameObject.name.Contains("GUI_Close"))
                                    {
                                        G.GetComponent<GUITexture>().enabled = false;
                                        string s = G.gameObject.name.Remove(0, 4);
                                        StartCoroutine("RunFunction", s);
                                        break;
                                    }
                                }

                            }
                        }
                    }
                    foreach (GUIText G in TouchText)
                    {
                        if (G.GetComponent<GUIText>().enabled)
                        {
                            if (G.HitTest(touch.position))
                            {

                                Debug.Log("The button hit was " + G.name);
                                if (touch.phase == TouchPhase.Ended && touch.phase != TouchPhase.Stationary)
                                {
                                    if (G.gameObject.name.Contains("GUI_"))
                                    {
                                        string s = G.gameObject.name.Remove(0, 4);
                                        StartCoroutine("RunFunction", s);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

#endif
}




