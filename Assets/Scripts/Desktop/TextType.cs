using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextType : FileType
{
    [SerializeField] TextAsset txtAsset;

    [SerializeField] private GameObject txtboxPanelPrefab;

    [SerializeField] private int fontSize = 60;
    [SerializeField] private float LineSpacing = 0.6f;

    private string[] txtArr; 

    public override void OpenFile()
    {
        if (ReadFile())
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject txtboxPanel = GameObject.Instantiate(txtboxPanelPrefab, canvas.transform);
            Text textbox = txtboxPanel.GetComponentInChildren<Text>();

            textbox.text = "";
            //foreach (string line in txtArr)
            //{
            //    textbox.text += line + System.Environment.NewLine;
            //}

            textbox.text = txtAsset.text;

            textbox.fontSize = fontSize;
            textbox.lineSpacing = LineSpacing;
        }
    }

    public bool ReadFile()
    {
        if (txtAsset != null)
        {
            txtArr = txtAsset.text.Split('}');
            return true;
        }
        return false;

    }
}
