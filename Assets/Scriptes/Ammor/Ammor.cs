using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammor : MonoBehaviour
{
    bool attack = true;
    public GameObject target;
    float speed = 8f;
    public float damage;


    public void GotoTarget()
    {
        if (speed > 0 && target != null)
        {
            GameManager.instance.StartCoroutine(GoAndDistroy());
        }
    }

    IEnumerator GoAndDistroy()
    {
        while (attack)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enermy" && other.gameObject == target)
        {
            EnermyInfor enermyInfor = other.gameObject.GetComponent<EnermyInfor>();
            enermyInfor.list_ammors.Add(this.gameObject);
            attack = false;

            if (!enermyInfor.isTarget && !enermyInfor.CheckHP())
            {
                enermyInfor.list_ammors.RemoveAll(ammor => ammor.gameObject);
            }
            else
            {
                enermyInfor.list_ammors.RemoveAt(0);
            }
            Destroy(this.gameObject);
        }
    }
}
