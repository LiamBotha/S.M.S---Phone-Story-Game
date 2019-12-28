using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FolderType : FileType
{
    public static GameObject root;

    [SerializeField]
    private GameObject confirmPrefab;
    private GameObject confirmMenu;

    //Containers
    [SerializeField]
    private FileExplorer nextFolder;

    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite lockedSprite;

    [SerializeField]
    private bool isLocked;

    [SerializeField]
    private string eventName = "event02";

    private void Start()
    {
        root = GameObject.Find("Root");

        if (isLocked)
        {
            GetComponent<Image>().sprite = lockedSprite;
        }
    }

    public override void OpenFile()
    {
        if(isLocked)
        {
            GameObject canvas = FindObjectOfType<Canvas>().gameObject;
            confirmMenu = GameObject.Instantiate(confirmPrefab, canvas.transform);

            confirmMenu.GetComponentInChildren<Button>().onClick.AddListener(CallEvent); // sets the method to call on click
            confirmMenu.transform.Find("btnQuit").GetComponent<Button>().onClick.AddListener(delegate { ClosePopup(confirmMenu); });
        }
        else if(nextFolder != null)
        {
            FileExplorer.previousFolders.Push(currentFolder);

            nextFolder.gameObject.SetActive(true);
            currentFolder.gameObject.SetActive(false);
        }
    }

    private void ClosePopup(GameObject confirmMenu)
    {
        Destroy(confirmMenu);
        Debug.Log("Destroying " + confirmMenu);
    }

    private void CallEvent()
    {
        Destroy(confirmMenu.gameObject);
        isLocked = false;

        StartCoroutine(OpenScene());
    }

    IEnumerator OpenScene()
    {
        var loading = SceneManager.LoadSceneAsync(eventName, LoadSceneMode.Additive);
        yield return loading;

        root.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(eventName));
    }
}
