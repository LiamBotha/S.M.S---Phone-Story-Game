using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class DialogSetter
{
    public static DialogCollection dialogContainer = DialogCollection.DialogLoad("Assets/S.M.S_Script.xml");

    public static void CallEvent(int val)
    {
        if (val < dialogContainer.dialogs.Count && IsEvent(val))
        {
            string trigger = dialogContainer.dialogs[val].Trigger;
            Debug.Log(trigger);

            if (GameObject.Find("Canvas").transform.Find(trigger) != null)
            {
                GameObject mission = (GameObject.Find("Canvas").transform.Find(trigger).gameObject);
                mission.SetActive(true);
                MessageManager.isFocus = false;
            }
            else if(trigger == "ChangeScene")
            {
                MessageManager mm = GameObject.FindObjectOfType<MessageManager>();

                mm.ChangeScene("Main");
            }
        }
    }

    public static void RunMinigame(int val)
    {
        SceneManager.LoadScene("Event0" + val);
    }

    public static bool IsEvent(int val)
    {
        if (dialogContainer == null || dialogContainer.dialogs[val] == null)
        {
            return true;
        }
        else
        {
            if (dialogContainer.dialogs[val].Trigger == null)
            {
                return false;
            }
        }
        return true;
    }

    public static bool WriteMessage(Text sms, int val)
    {
        if (dialogContainer != null && val < dialogContainer.dialogs.Count && dialogContainer.dialogs[val].Description != ";")
        {
            sms.text = dialogContainer.dialogs[val].Description;
            return true;
        }
        else return false;
    }
}
