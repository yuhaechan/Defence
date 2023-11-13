using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enermy : MonoBehaviour
{


    [SerializeField] Transform spawn_ParentTransform;
    [SerializeField] GameObject[] prefab_SpawnEnermy;

    //------

    public void Enermy_Init()
    {

    }
    public void Clear_Enermy()
    {
        GameManager.instance.StopAllCoroutines();
        for (int i = 0; i < GameManager.instance.list_Obj_spawnEnermy.Count; i++)
        {
            Destroy(GameManager.instance.list_Obj_spawnEnermy[i].gameObject);
        }
        GameManager.instance.list_Obj_spawnEnermy.RemoveAll(enermy => enermy.gameObject);
    }
    /*
    public void Spawn_Enermy()
    {
        GameObject spawnedObject = Instantiate(prefab_SpawnEnermy, new Vector3(0, 0, 0), Quaternion.identity, spawn_ParentTransform);

        spawnedObject.transform.position = new Vector3(rect_TP_Poz[0].position.x, rect_TP_Poz[0].position.y, rect_TP_Poz[0].position.z);

        GameManager.instance.list_Obj_spawnEnermy.Add(spawnedObject);
        GameManager.instance.SetEnermyCount();
        Move_Enermy(spawnedObject);
    }
    */
    public void Spawn_Enermy(int prefabNum)
    {
        GameObject spawnedObject = Instantiate(prefab_SpawnEnermy[prefabNum], new Vector3(0, 0, 0), Quaternion.identity, spawn_ParentTransform);

        spawnedObject.transform.position = new Vector3(GameManager.instance.rect_TP_Poz[0].position.x, GameManager.instance.rect_TP_Poz[0].position.y, GameManager.instance.rect_TP_Poz[0].position.z);

        GameManager.instance.list_Obj_spawnEnermy.Add(spawnedObject);
        GameManager.instance.SetEnermyCount();
    }
}


