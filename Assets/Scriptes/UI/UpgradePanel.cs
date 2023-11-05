using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] GameObject Popup;

    public void UpgradePanel_Active()
    {
        Popup.SetActive(true);
    }

    public void UpgradePanel_DisActive()
    {
        Popup.SetActive(false);
    }
}
