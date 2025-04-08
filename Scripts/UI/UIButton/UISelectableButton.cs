using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image selecktImage;

    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;

    public override void SetFocuse()
    {
        base.SetFocuse();

        selecktImage.enabled = true;

        OnSelect?.Invoke();
    }

    public override void SetUnFocuse()
    {
        base.SetUnFocuse();

        selecktImage.enabled = false;

        OnUnSelect?.Invoke();

    }

    public void Quit()
    {
        Application.Quit();
    }
}
