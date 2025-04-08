using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : UISelectableButton
{
    [SerializeField] private Setting setting;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Image previousImage;
    [SerializeField] private Image nextImage;

    private void Start()
    {
        ApplyPropperties(setting);

    }

    public void SetNextValueSetting()
    {
        setting?.SetNextValue();
        setting.Apply();
        UpdateInfo();
    }
    public void SetPreviousValueSetting()
    {
        setting?.SetPreviousValue();
        setting?.Apply();
        UpdateInfo();

    }

    private void UpdateInfo()
    {
        titleText.text = setting.Title;
        valueText.text = setting.GetStringValue();

        previousImage.enabled = !setting.isMinValue;
        nextImage.enabled = !setting.isMaxValue;
    }
    public void ApplyPropperties(Setting property)
    {
        if (property == null) return;

        setting = property;
        UpdateInfo();

    }
}
