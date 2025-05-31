using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindowButton : CustomButton
{
    [SerializeField] private GameObject window;
    protected override void OnClick()
    {
        window.SetActive(false);
    }
}