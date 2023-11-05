using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enermy : MonoBehaviour
{
    [SerializeField] Transform[] rect_TP_Poz; //TP - Turning Point


    [SerializeField] Transform spawn_ParentTransform; 
    [SerializeField] GameObject[] prefab_SpawnEnermy;
    Transform transform_Target;

    //------
    float speed = 5.0f;

    public void Enermy_Init()
    {

    }
    public void Clear_Enermy()
    {
        GameManager.instance.StopAllCoroutines();
        for(int i=0; i <GameManager.instance.list_Obj_spawnEnermy.Count; i++)
        {
            Destroy(GameManager.instance.list_Obj_spawnEnermy[i].gameObject);
            GameManager.instance.list_Obj_spawnEnermy.Remove(GameManager.instance.list_Obj_spawnEnermy[i]);
        }
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

        spawnedObject.transform.position = new Vector3(rect_TP_Poz[0].position.x, rect_TP_Poz[0].position.y, rect_TP_Poz[0].position.z);

        GameManager.instance.list_Obj_spawnEnermy.Add(spawnedObject);
        GameManager.instance.SetEnermyCount();
        Move_Enermy(spawnedObject);
    }

    void Move_Enermy(GameObject obj)
    {
        GameManager.instance.Coroutine(Move_Enermy_Routine(obj));
    }

    IEnumerator Move_Enermy_Routine(GameObject obj) //enermy 안에 bool 값으로 살아있거나 움직이는 중이란 bool 값을 만들어서 그걸로 해야할듯
    {
        while (true)
        {

            if(obj.transform.position == rect_TP_Poz[0].position)
            {
                transform_Target = rect_TP_Poz[1];
            }
            else if(obj.transform.position == rect_TP_Poz[1].position)
            {
                transform_Target = rect_TP_Poz[2];
            }
            else if (obj.transform.position == rect_TP_Poz[2].position)
            {
                transform_Target = rect_TP_Poz[3];
            }
            else if (obj.transform.position == rect_TP_Poz[3].position)
            {
                transform_Target = rect_TP_Poz[0];
            }

            obj.transform.position = Vector3.MoveTowards(obj.transform.position, transform_Target.position, speed * Time.deltaTime);
            yield return null;

        }

    }
}
