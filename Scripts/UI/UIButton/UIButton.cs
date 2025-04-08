using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private bool Interectible;

    private bool focuse = false;
    public bool Focuse => focuse;

    public UnityEvent OnCliked;

    public event UnityAction<UIButton> PointerEnter;
    public event UnityAction<UIButton> PointerExit;
    public event UnityAction<UIButton> PointerClick;


    public virtual void SetFocuse()
    {
        if (Interectible == false) return;

        focuse = true;
    }

    public virtual void SetUnFocuse()
    {
        if (Interectible == false) return;
        focuse = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (Interectible == false) return;
        PointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (Interectible == false) return;
        PointerExit?.Invoke(this);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (Interectible == false) return;
        PointerClick?.Invoke(this);
        OnCliked?.Invoke();
    }
}
