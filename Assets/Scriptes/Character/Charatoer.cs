using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Charatoer : MonoBehaviour
{
    //공격대상 사정거리 안
    [SerializeField] List<GameObject> list_Obj_Target = new List<GameObject>();
    [SerializeField] GameObject[] obj_findObject;
    [SerializeField] GameObject obj_Target;
    [SerializeField] GameObject arrmorPrefab;

    //공격대상 찾기 -> 없으면 다시 찾기
    //공격대상 찾기 -> 있다면 전부 찾기 -> 전부찾은다음 제일 먼저 찾은것 or 가까운것 -> 사라지면 리스트 제일 마지막 것을 타깃으로

    //공격 범위
    bool attack = false;
    float killingrange = 3f;
    float attackDamage = 1.5f;
    float attackSpeed = 1f; 

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

    void Find_Target() // 계속찾기
    {
        list_Obj_Target.Clear();
        GameObject[] obj_findObjects = GameManager.instance.list_Obj_spawnEnermy.ToArray();

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

    IEnumerator Attack(GameObject target)
    {
        attack = true;
        while (true)
        {
            try
            {
                EnermyInfor enermyinfor = obj_Target.GetComponent<EnermyInfor>();
                if (enermyinfor.isTarget)
                {
                    GameObject obj_Ammor = Instantiate(arrmorPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.gameObject.transform);
                    enermyinfor.SetHP(attackDamage, obj_Ammor);
                    enermyinfor.list_ammors.Add(obj_Ammor);
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
    
    // gamemanager 에서 맵에있는 적 리스트를 만들고
    // 여기서는 타겟 리스트만 만들면 될듯
    // findobject 할때 obj_findObject로 넣은다음에
    // 그 대상의 위치값을 받아야하나?

}
