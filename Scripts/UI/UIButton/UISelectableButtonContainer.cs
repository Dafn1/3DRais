using System;
using UnityEngine;

public class UISelectableButtonContainer : MonoBehaviour
{
    [SerializeField] private Transform buttonContainer;

    public bool Interectible = true;

    public void SetInterectible(bool interectible) => Interectible = interectible;

    private UISelectableButton[] buttons;

    private int selectionButtonIndex = 0;

    private void Start()
    {
        buttons = buttonContainer.GetComponentsInChildren<UISelectableButton>();

        if (buttons == null) Debug.LogError("Button list is enpty!");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter += OnPointerEnter;
        }
        if (Interectible == false) return;

        buttons[selectionButtonIndex].SetFocuse();
    }
    private void OnDestroy()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
        }
    }

    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    private void SelectButton(UIButton button)
    {
        if (Interectible == false) return;
        buttons[selectionButtonIndex].SetUnFocuse();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (button == buttons[i])
            {
                selectionButtonIndex = i;
                button.SetFocuse();
                break;
            }
        }
    }

    public void SelectNext()
    {

    }

    public void SelectPrevious()
    {

    }


}
