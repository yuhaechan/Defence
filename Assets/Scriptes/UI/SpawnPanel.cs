using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPanel : MonoBehaviour
{
    [SerializeField] GameObject Popup;

    [SerializeField] List<GameObject> list_Prefab_SpawnCharaters = new List<GameObject>(); // 소환할수있는 캐릭터
    [SerializeField] Transform spawn_ParentTransform;
    [SerializeField] Transform[] transform_SpawnPoz;


    [SerializeField] Button[] btn_Spawns;

    public void SpawnPanel_Init()
    {
        SpawnPanel_Btn_Init();
    }

    void SpawnPanel_Btn_Init()
    {
        for(int i =0; i<btn_Spawns.Length; i++)
        {
            btn_Spawns[i].onClick.RemoveAllListeners();
            btn_Spawns[i].onClick.AddListener(() =>
            {
                if (GameManager.instance.timer)
                {
                    if (GameManager.instance.list_Obj_SpawnCharaters.Count < 25)
                    {
                        Spawn_Charater();
                    }
                }
            });
        }
    }

    void Spawn_Charater()
    {
        GameObject spawnedObject = Instantiate(list_Prefab_SpawnCharaters[0], new Vector3(0, 0, 0), Quaternion.identity, spawn_ParentTransform);
        spawnedObject.transform.position = new Vector3(transform_SpawnPoz[GameManager.instance.list_Obj_SpawnCharaters.Count].position.x, transform_SpawnPoz[GameManager.instance.list_Obj_SpawnCharaters.Count].position.y, transform_SpawnPoz[GameManager.instance.list_Obj_SpawnCharaters.Count].position.z);
        GameManager.instance.list_Obj_SpawnCharaters.Add(spawnedObject);
    }


    public void SpawnPanel_Active()
    {
        Popup.SetActive(true);
    }

    public void SpawnPanel_DisActive()
    {
        Popup.SetActive(false);
    }
}
