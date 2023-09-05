using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    public GameObject ourText;


    public void Hide()
    {
        ourText.SetActive(false);
    }

    public void Show()
    {
        ourText.SetActive(true);
    }
}
