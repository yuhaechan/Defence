using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    [SerializeField] ToggleGroup toggleGroup;
    [SerializeField] Toggle[] toggles;

    [SerializeField] SpawnPanel spawnPanel;
    [SerializeField] UpgradePanel upgradePanel;
        
    public void PanelControl_Init()
    {
        PanelControl_Btn_Init();
        spawnPanel.SpawnPanel_Init();
    }

    void PanelControl_Btn_Init()
    {

        toggles[0].onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                spawnPanel.SpawnPanel_Active();
            }
            else
            {
                spawnPanel.SpawnPanel_DisActive();
            }
        });

        toggles[1].onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                upgradePanel.UpgradePanel_Active();
            }
            else
            {
                upgradePanel.UpgradePanel_DisActive();
            }
        });

    }

  


}
