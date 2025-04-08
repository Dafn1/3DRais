using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GraphicsQualitySetting : Setting
{
    private int currerntLevelIndex = 0;

    public override bool isMinValue { get => currerntLevelIndex == 0; }
    public override bool isMaxValue { get => currerntLevelIndex == QualitySettings.names.Length - 1; }


    public override void SetNextValue()
    {
        if (isMaxValue == false) currerntLevelIndex++;
    }

    public override void SetPreviousValue()
    {
        if (isMinValue == false) currerntLevelIndex--;
    }

    public override object GetValue()
    {
        return QualitySettings.names[currerntLevelIndex];
    }
    public override string GetStringValue()
    {
        return QualitySettings.names[currerntLevelIndex];
    }

    public override void Apply()
    {
        QualitySettings.SetQualityLevel(currerntLevelIndex);
        Save();
    }

    public override void Load()
    {
        currerntLevelIndex = PlayerPrefs.GetInt(title, 0);
    }
    private void Save()
    {
        PlayerPrefs.SetInt(title, currerntLevelIndex);
    }
}
