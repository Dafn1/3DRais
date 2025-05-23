using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoad : MonoBehaviour
{
    [SerializeField] private Setting[] allSettings;

    private void Awake()
    {
        for (int i = 0; i < allSettings.Length; i++)
        {
            allSettings[i].Apply();
            allSettings[i].Load();
        }
    }
}
