using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeSkillItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //削除.
        if (collision.gameObject.CompareTag("Player"))
        {
            //アイテム取った処理
            collision.gameObject.GetComponent<PlayerController>().SetSkill("Fe");
            Destroy(this.gameObject);
        }
    }
}
