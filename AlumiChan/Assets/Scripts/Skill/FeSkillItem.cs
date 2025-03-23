using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeSkillItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //íœ.
        if (collision.gameObject.CompareTag("Player"))
        {
            //ƒAƒCƒeƒ€æ‚Á‚½ˆ—
            collision.gameObject.GetComponent<PlayerController>().SetSkill("Fe");
            Destroy(this.gameObject);
        }
    }
}
