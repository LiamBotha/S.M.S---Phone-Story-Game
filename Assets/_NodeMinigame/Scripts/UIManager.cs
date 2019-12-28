using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text actionsText;
    private MatchManager manager;

    private void Start()
    {
        manager = GameObject.FindObjectOfType<MatchManager>();
    }

    private void Update()
    {
        actionsText.text = manager.remainingActions.ToString();
    }
}
