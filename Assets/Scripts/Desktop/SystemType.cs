using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SystemType : FileType
{
    public static GameObject root;

    [SerializeField] private GameObject menuPrefab;
    private GameObject confirmMenu;

    public override void OpenFile()
    {
        GameObject canvas = GameObject.Find("Canvas");
        confirmMenu = GameObject.Instantiate(menuPrefab,canvas.transform);
        confirmMenu.transform.Find("btnYes").GetComponent<Button>().onClick.AddListener(CallEvent);
        confirmMenu.transform.Find("btnNo").GetComponent<Button>().onClick.AddListener(delegate { ClosePopup(confirmMenu); });
    }

    //public void DeleteFile()
    //{
    //    Destroy(transform.parent.parent.gameObject); // revisit later MAKE SURE ITS DELETING THE MISSION FOLDER -> TUTORIAL01 etc...
    //}

    private void CallEvent()
    {
        GetComponent<UnityEngine.UI.Button>().interactable = false;
        ClosePopup(confirmMenu);
        FileExplorer.previousFolders.Clear();
        MessageManager.isFocus = true;

        StartCoroutine(OpenScene());
    }

    IEnumerator OpenScene()
    {
        root = GameObject.Find("Root");
        var loading = SceneManager.LoadSceneAsync("Event02", LoadSceneMode.Additive);
        yield return loading;

        transform.parent.parent.gameObject.SetActive(false);
        root.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Event02"));
    }

    public void ClosePopup(GameObject menu)
    {
        Destroy(menu);
    }
}
