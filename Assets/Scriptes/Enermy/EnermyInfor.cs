using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyInfor : MonoBehaviour
{
    float hp = 10f;
    float speed = 5.0f;
    Transform transform_Target;

    private void Start()
    {
        Move_Enermy(this.gameObject);
    }
    void Move_Enermy(GameObject obj)
    {
        GameManager.instance.Coroutine(Move_Enermy_Routine(obj));
    }

    IEnumerator Move_Enermy_Routine(GameObject obj) //enermy �ȿ� bool ������ ����ְų� �����̴� ���̶� bool ���� ���� �װɷ� �ؾ��ҵ�
    {
        while (true)
        {
            if (obj.transform.position == GameManager.instance.rect_TP_Poz[0].position)
            {
                transform_Target = GameManager.instance.rect_TP_Poz[1];
            }
            else if (obj.transform.position == GameManager.instance.rect_TP_Poz[1].position)
            {
                transform_Target = GameManager.instance.rect_TP_Poz[2];
            }
            else if (obj.transform.position == GameManager.instance.rect_TP_Poz[2].position)
            {
                transform_Target = GameManager.instance.rect_TP_Poz[3];
            }
            else if (obj.transform.position == GameManager.instance.rect_TP_Poz[3].position)
            {
                transform_Target = GameManager.instance.rect_TP_Poz[0];
            }

            obj.transform.position = Vector3.MoveTowards(obj.transform.position, transform_Target.position, speed * Time.deltaTime);
            yield return null;

        }

    }
}




