using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charatoer : MonoBehaviour
{


    //���ݴ�� �����Ÿ� ��
    [SerializeField] List<GameObject> list_Obj_Target = new List<GameObject>();
    [SerializeField] GameObject[] obj_findObject;
    [SerializeField] GameObject obj_Target;

    //���ݴ�� ã�� -> ������ �ٽ� ã��
    //���ݴ�� ã�� -> �ִٸ� ���� ã�� -> ����ã������ ���� ���� ã���� or ������ -> ������� ����Ʈ ���� ������ ���� Ÿ������

    //���� ����
    float killingrange = 3f;

    void Start()
    {
        Debug.Log("����");
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

    public void Find_Target() // ���ã��
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

    // gamemanager ���� �ʿ��ִ� �� ����Ʈ�� �����
    // ���⼭�� Ÿ�� ����Ʈ�� ����� �ɵ�
    // findobject �Ҷ� obj_findObject�� ����������
    // �� ����� ��ġ���� �޾ƾ��ϳ�?

}
