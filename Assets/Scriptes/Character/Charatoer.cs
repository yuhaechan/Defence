using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Charatoer : MonoBehaviour
{
    //���ݴ�� �����Ÿ� ��
    [SerializeField] List<GameObject> list_Obj_Target = new List<GameObject>();
    [SerializeField] GameObject[] obj_findObject;
    [SerializeField] GameObject obj_Target;
    [SerializeField] GameObject arrmorPrefab;

    //���ݴ�� ã�� -> ������ �ٽ� ã��
    //���ݴ�� ã�� -> �ִٸ� ���� ã�� -> ����ã������ ���� ���� ã���� or ������ -> ������� ����Ʈ ���� ������ ���� Ÿ������

    //���� ����
    [SerializeField] bool attack = false;
    float killingrange = 3f;
    float attackDamage = 3.5f;
    float attackSpeed = 2f; 

    void Start()
    {
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
                }
                else if (Mathf.Abs(distance.x) < killingrange && Mathf.Abs(distance.y) < killingrange && !attack)
                {
                    GameManager.instance.StartCoroutine(Attack(obj_Target));
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

    void Find_Target() // ���ã��
    {
        list_Obj_Target.Clear();
        GameObject[] obj_findObjects = GameManager.instance.list_Obj_spawnEnermy.ToArray();
        try
        {
            foreach (GameObject obj_findObject in obj_findObjects)
            {
                Vector3 distance = transform.position - obj_findObject.transform.position;
                if (Mathf.Abs(distance.x) < killingrange && Mathf.Abs(distance.y) < killingrange)
                {
                    obj_Target = obj_findObject.gameObject;
                    break;
                }
            }
        }
        catch 
        {
            if (obj_Target == null)
            {
                Find_Target();
            }
        }
    }

    IEnumerator Attack(GameObject target)
    {
        attack = true;
        while (true)
        {
            try
            {
                EnermyInfor enermyinfor = target.GetComponent<EnermyInfor>();
                if (enermyinfor.isTarget)
                {
                    enermyinfor.CheckHP();
                    GameObject obj_Ammor = Instantiate(arrmorPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.gameObject.transform);
                    enermyinfor.SetHP(attackDamage);
                    obj_Ammor.transform.position = transform.position;
                    Ammor ammor = obj_Ammor.GetComponent<Ammor>();
                    ammor.target = target;
                    ammor.damage = attackDamage;
                    ammor.GotoTarget();
                }
                else
                {
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

            yield return new WaitForSeconds(attackSpeed);
        }
    }
    
    // gamemanager ���� �ʿ��ִ� �� ����Ʈ�� �����
    // ���⼭�� Ÿ�� ����Ʈ�� ����� �ɵ�
    // findobject �Ҷ� obj_findObject�� ����������
    // �� ����� ��ġ���� �޾ƾ��ϳ�?

}
