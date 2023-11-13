using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnermyInfor : MonoBehaviour
{
    float hp = 10f;
    float speed = 5.0f;
    Transform transform_Target;
    public bool isTarget = true;
    int listnum;
    public List<GameObject> list_ammors = new List<GameObject>();

    private void Start()
    {
        for(int i=0; i< GameManager.instance.list_Obj_spawnEnermy.Count; i++)
        {
            if (this.gameObject == GameManager.instance.list_Obj_spawnEnermy[i])
            {
                listnum = i;
                break;
            }
        }
        Move_Enermy(this.gameObject);
    }
    void Move_Enermy(GameObject obj)
    {
        GameManager.instance.Coroutine(Move_Enermy_Routine(obj));
    }

    IEnumerator Move_Enermy_Routine(GameObject obj) //enermy 안에 bool 값으로 살아있거나 움직이는 중이란 bool 값을 만들어서 그걸로 해야할듯
    {
        while (isTarget)
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
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Armmor")// && other.gameObject == target)
        {

        }
    }
    */
    public void SetHP(float damage, GameObject obj)
    {
        list_ammors.Add(obj);
        
        if((hp = hp - damage) < 0)
        {
            hp = hp - damage;
            //타겟 해제
            isTarget = false;
            GameManager.instance.list_Obj_spawnEnermy.Remove(GameManager.instance.list_Obj_spawnEnermy[listnum]);
            try
            {
                Debug.Log(list_ammors.Count);
                //Destroy(this.gameObject);
                GameManager.instance.list_Obj_spawnEnermy.Remove(this.gameObject);
            }
            catch
            {

            }
        }
        
    }
}




