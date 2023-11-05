using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charatoer : MonoBehaviour
{


    //공격대상 사정거리 안
    [SerializeField] List<GameObject> list_Obj_Target = new List<GameObject>();
    [SerializeField] GameObject[] obj_findObject;
    [SerializeField] GameObject obj_Target;

    //공격대상 찾기 -> 없으면 다시 찾기
    //공격대상 찾기 -> 있다면 전부 찾기 -> 전부찾은다음 제일 먼저 찾은것 or 가까운것 -> 사라지면 리스트 제일 마지막 것을 타깃으로

    //공격 범위
    float killingrange = 3f;

    void Start()
    {
        Debug.Log("생성");
        RutineStart(); 
    }

    void RutineStart()
    {
        Find_Target();
        GameManager.instance.StartCoroutine(Charater_Action());
    }

    IEnumerator Charater_Action()
    {
        while (true)
        {
            try
            {
                Vector3 distance = this.gameObject.transform.position - obj_Target.transform.position;
                if(!(Mathf.Abs(distance.x) < killingrange && Mathf.Abs(distance.y) < killingrange))
                {
                    obj_Target = null;
                    Find_Target();
                }
            }
            catch
            {
                if(obj_Target == null)
                {
                    Find_Target();
                }

            }
            yield return null;
        }
    }

    public void Find_Target() // 계속찾기
    {
            try
            {
                list_Obj_Target.Clear();
                obj_findObject = FindObjectsOfType<GameObject>();
                for (int i=0; i<obj_findObject.Length; i++)
                {
                    for(int a = 0; a < GameManager.instance.list_Obj_spawnEnermy.Count; a++)
                    {
                        if (obj_findObject[i].gameObject == GameManager.instance.list_Obj_spawnEnermy[a])
                        {
                            list_Obj_Target.Add(obj_findObject[i]);
                            Vector3 distance = this.gameObject.transform.position - obj_findObject[i].transform.position;
                            if (Mathf.Abs(distance.x) < killingrange && Mathf.Abs(distance.y) < killingrange)
                            {
                                Debug.Log(obj_findObject[i].gameObject.tag);
                                obj_Target = obj_findObject[i].gameObject;
                                break;
                            }

                        }
                    }
                }
            }
            catch
            {
                if (obj_findObject == null)
                {
                    Debug.Log("here");
                }
            }
    }

    // gamemanager 에서 맵에있는 적 리스트를 만들고
    // 여기서는 타겟 리스트만 만들면 될듯
    // findobject 할때 obj_findObject로 넣은다음에
    // 그 대상의 위치값을 받아야하나?

}
