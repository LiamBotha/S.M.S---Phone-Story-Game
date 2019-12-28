using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {

    public GameObject smsPrefabPlayer;
    public GameObject smsPrefabHacked;
    public GameObject smsParent;

    private GameObject firstSms;
    [HideInInspector]
    public GameObject currentSms;

    public GameObject loadingScreen;
    //public GameObject bootScreen;

    public Color playerColor;
    public Color otherColor;

    [SerializeField] private int scrollAmount = 75;
    private Vector2 maxHeight;
    private Vector2 minHeight;

    public static bool isFocus = true;

    Scene intro;

    private void Start()
    {
        maxHeight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minHeight = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CallMessenger();
        }

        if(isFocus && ((AddMessage.count > 5 && SceneManager.GetActiveScene().name == "Intro") || AddMessage.count > 19))
        {
            DoScrolling();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void DoScrolling()
    {
        var bounds = GetScrollBounds();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (bounds.min.y <= minHeight.y)
            {
                Debug.Log("bottom");
            }

            if (bounds.max.y - 1000 > minHeight.y)
                smsParent.transform.Translate(new Vector2(0, -scrollAmount) * Screen.height / 1000 * 2f);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (bounds.max.y >= maxHeight.y)
            {
                Debug.Log("top");
            }

            if (bounds.min.y < maxHeight.y)
                smsParent.transform.Translate(new Vector2(0, scrollAmount) * Screen.height / 1000 * 2f);
        }
    }

    private void OnMouseDown()
    {
        CallMessenger();
    }

    public void CallMessenger()
    {
        intro = SceneManager.GetSceneByName("intro");


        if (isFocus && DialogSetter.dialogContainer != null && AddMessage.count < DialogSetter.dialogContainer.dialogs.Count)
        {
            if (DialogSetter.dialogContainer.dialogs[AddMessage.count].Character != 1)
            {
                currentSms = AddMessage.newMessage(smsPrefabPlayer, smsParent, currentSms, playerColor);
            }
            else
            {
                currentSms = AddMessage.newMessage(smsPrefabHacked, smsParent, currentSms, otherColor);
            }

            if (firstSms == null)
            {
                firstSms = currentSms;
            }

        }
    }

    public void ChangeScene(string scene)
    {
        if (scene != SceneManager.GetActiveScene().name /* && AddMessage.count == 14*/)
        {
            if (scene == "Main")
            {
                GameObject.Instantiate(loadingScreen, GameObject.FindObjectOfType<Canvas>().transform);
            }

            StartCoroutine(RebootPhone(scene));
        }
    }

    private Bounds GetScrollBounds()
    {
        if (firstSms != null)
        {
            var bounds = new Bounds(firstSms.transform.position, Vector3.zero);

            bounds.Encapsulate(currentSms.transform.position);

            return bounds;
        }
        else return default(Bounds);
    }

    IEnumerator RebootPhone(string scene)
    {
        isFocus = false;
        yield return new WaitForSeconds(0.30f);
        isFocus = true;
        SceneManager.LoadScene(scene);
    }

    public void ChangeFocus(bool setFocused)
    {
        isFocus = setFocused;
    }

    public void Ending()
    {
        GameObject panel = GameObject.Find("Panel");

        panel.SetActive(false);
    }
}
