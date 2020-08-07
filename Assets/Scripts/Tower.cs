using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject deathFX;

    void Death()
    {
        GameObject fx = Instantiate(deathFX, transform.position, deathFX.transform.rotation);
        anim.SetTrigger("Death");
        Destroy(fx, 7f);
        Destroy(gameObject, 8f);
    }
}
