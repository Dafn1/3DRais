using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip select;

    private new AudioSource audio;

    private UIButton[] uIButton;
    private void Start()
    {
        audio = GetComponent<AudioSource>();

        uIButton = GetComponentsInChildren<UIButton>(true);

        for (int i = 0; i < uIButton.Length; i++)
        {
            uIButton[i].PointerClick += OnPointClicked;
            uIButton[i].PointerEnter += OnPointEnter;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < uIButton.Length; i++)
        {
            uIButton[i].PointerClick -= OnPointClicked;
            uIButton[i].PointerEnter -= OnPointEnter;
        }

    }

    private void OnPointEnter(UIButton arg0)
    {
        audio.PlayOneShot(select);
    }

    private void OnPointClicked(UIButton arg0)
    {
        audio.PlayOneShot(click);
    }
}
