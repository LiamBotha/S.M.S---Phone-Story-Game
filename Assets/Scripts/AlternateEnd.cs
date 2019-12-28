using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlternateEnd : MonoBehaviour
{
    [SerializeField] GameObject parent;

    [SerializeField] GameObject[] messages;

    [SerializeField] GameObject restartMenu;

    [SerializeField] GameObject panel;

    const int DIST = 115;
    int count = 0;

    private void OnMouseDown()
    {
        if(count < messages.Length)
        {
            DisplayEnd();
            count++;
        }
        else
        {
            panel.SetActive(false);
            restartMenu.SetActive(true);
        }
    }

    void DisplayEnd()
    {
        messages[count].SetActive(true);
        if (count > 2)
        {
            parent.transform.Translate(new Vector2(0, DIST) * Screen.height / 1000 * 2f);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        AddMessage.count = 0;
    }
}
