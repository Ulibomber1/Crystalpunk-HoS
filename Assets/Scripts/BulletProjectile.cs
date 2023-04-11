using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    //commented out code is for vfx if the bullet hits something
    //[SerializeField] Private Transform vfxHitGreen;
    //[SerializeField] private Transform vfxHitRed;
    private Rigidbody bulletRigidbody;

    private void Awake(){
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start(){
        float speed = 40f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other){
        /*
        if(other.GetComponent<BulletTarget>() != null){
            //hit target
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        } else {
            //hit something else
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        */
        Destroy(gameObject);
    }
}
