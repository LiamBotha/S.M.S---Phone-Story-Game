using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AddMessage
{
    //static GameObject currentSms;
    static GameObject camera;
    public static int count = 0;
    const int DIST = 115;

    public static GameObject newMessage(GameObject newSms, GameObject parent, GameObject currentSms, Color color)
    {
        if (currentSms != null)
        {
            if (DialogSetter.dialogContainer != null && count < DialogSetter.dialogContainer.dialogs.Count && DialogSetter.dialogContainer.dialogs[count].Description == ";")
            {
                DialogSetter.CallEvent(count);

                count++; // after else it won't find it when checking for event
                return currentSms;
            }
            else
            {
                //Creates the messages then calls static method to assign text
                GameObject message = GameObject.Instantiate(newSms, parent.transform);

                message.GetComponent<Image>().color = color;

                message.transform.position = new Vector2(message.transform.position.x, currentSms.transform.position.y);

                message.transform.Translate(new Vector2(0, -DIST) * Screen.height / 1000 * 2);

                if (DialogSetter.WriteMessage(message.GetComponentInChildren<Text>(), count) == false)
                {
                    MonoBehaviour.Destroy(message);
                    MessageManager.isFocus = false;
                }
                else
                {
                    count++;

                    //scrolls the messages up once they fill the screen
                    if ((count >= 5 && count <= 15) || (count >= 19))
                    {
                        //parent.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y + DIST);
                        parent.transform.Translate(new Vector2(0, DIST) * Screen.height / 1000 * 2f);
                    }
                }
                return message;
            }
        }
        else
        {
            //Creates a base message if no others exist
            GameObject message = GameObject.Instantiate(newSms, parent.transform);

            message.GetComponent<Image>().color = color;

            DialogSetter.WriteMessage(message.GetComponentInChildren<Text>(), count);
            count++;
            return message;
        }
    }
}
