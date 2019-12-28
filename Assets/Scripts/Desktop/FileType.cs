using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FileType : MonoBehaviour
{
    public FileExplorer currentFolder;

    public abstract void OpenFile();
}
