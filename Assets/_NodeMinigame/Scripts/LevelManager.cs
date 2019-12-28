using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject[] levelList;
    public static int count = 0;

    GameObject currentLevel;
    GameObject restartLevel;

    [SerializeField]
    GameObject inputField;

    private void Start()
    {
        StartLevel();
        MatchManager.onVictory += EndLevel;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inputField.SetActive(true);
        }
    }

    public void StartLevel()
    {
        if (count >= levelList.Length)
            count = levelList.Length - 1;

        restartLevel = levelList[count];
        currentLevel = GameObject.Instantiate(levelList[count]);
        count++;
    }

    public void RestartLevel()
    {
        Destroy(currentLevel.gameObject);

        StartCoroutine(BuildLevel());
    }

    IEnumerator BuildLevel()
    {
        yield return new WaitForSeconds(0.3f);
        currentLevel = GameObject.Instantiate(restartLevel);
    }

    public void EndLevel(Object manager)
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(current);
        FolderType.root.SetActive(true);
    }

    public void EnterCode(string code)
    {
        inputField.SetActive(false);

        if(code == "AmBigBabyPlsHelp")
        {
            EndLevel(this);
        }
    }
}
