using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesController : MonoBehaviour
{
    [SerializeField] private GameObject rulesPanel;

    void Start()
    {
        rulesPanel.SetActive(false);
    }

    public void ShowRules()
    {
        rulesPanel.SetActive(true);
    }

    public void HideRules()
    {
        rulesPanel.SetActive(false);
    }
}
