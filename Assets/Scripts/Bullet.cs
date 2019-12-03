using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        //3 sn içinde bomba biyere çarpmıyorsa destroy et.
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthBehaviour hb = collision.gameObject.GetComponent<HealthBehaviour>();
        if (hb != null)
        {
            hb.TakeDamage(15);
        }
        //mermi bir yere carptıysa 1 sn sonra yok et.
        //1 sn çünkü fiziksel özellikler uygulanabilsin.
        Destroy(gameObject, 1);
    }
}
