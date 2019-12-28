using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[ExecuteInEditMode]
public class FileExplorer : MonoBehaviour
{
    public List<FileType> files = new List<FileType>();

    public static Stack<FileExplorer> previousFolders = new Stack<FileExplorer>();

    private void Awake()
    {
        files = GetComponentsInChildren<FileType>().ToList();

        foreach (FileType file in files)
        {
            file.currentFolder = this;
        }
    }
    
    public void CloseFolder()
    {
        if (previousFolders.Count != 0)
        {
            FileExplorer rootFolder = previousFolders.Pop();

            rootFolder.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
