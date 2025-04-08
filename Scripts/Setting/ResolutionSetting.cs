using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ResolutionSetting : Setting
{
    [SerializeField]
    private Vector2Int[] availibleResolution = new Vector2Int[]
    {
        new Vector2Int(800,600),
        new Vector2Int(1280,720),
        new Vector2Int(1600,900),
        new Vector2Int(1920,1080),
        new Vector2Int(2048,1155),
        new Vector2Int(4096,2304),
    };

    private int currentResolutionIndex = 0;
    public override bool isMinValue { get => currentResolutionIndex == 0; }
    public override bool isMaxValue { get => currentResolutionIndex == availibleResolution.Length - 1; }

    public override void SetNextValue()
    {
        if (isMaxValue == false)
        {
            currentResolutionIndex++;
        }
    }
    public override void SetPreviousValue()
    {
        if (isMaxValue == false)
        {
            currentResolutionIndex--;
        }
    }
    public override object GetValue()
    {
        return availibleResolution[currentResolutionIndex];
    }
    public override string GetStringValue()
    {
        return availibleResolution[currentResolutionIndex].x + "x" + availibleResolution[currentResolutionIndex].y;
    }
    public override void Apply()
    {
        Screen.SetResolution(availibleResolution[currentResolutionIndex].x, availibleResolution[currentResolutionIndex].y, true);
        Save();
    }

    public override void Load()
    {
        currentResolutionIndex = PlayerPrefs.GetInt(title, 0);
    }
    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolutionIndex);
    }
}
