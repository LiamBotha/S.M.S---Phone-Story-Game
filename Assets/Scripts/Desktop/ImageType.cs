using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageType : FileType
{
    [SerializeField] private Sprite img;
    [SerializeField] private GameObject imgPanelPrefab;

    public override void OpenFile()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject panel = GameObject.Instantiate(imgPanelPrefab, canvas.transform);

        panel.GetComponentInChildren<UnityEngine.UI.Image>().sprite = img;
    }
}
