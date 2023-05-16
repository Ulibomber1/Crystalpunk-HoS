using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    //Enemy enemy;
    //EnemySlime slime;
    //commented out code is for vfx if the bullet hits something
    //[SerializeField] Private Transform vfxHitGreen;
    //[SerializeField] private Transform vfxHitRed;
    private Rigidbody bulletRigidbody;

    private void Awake(){
        bulletRigidbody = GetComponent<Rigidbody>();
        //enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<Enemy>();
        //slime = GameObject.FindGameObjectWithTag("enemySlime").GetComponent<EnemySlime>();
    }

    private void Start(){
        float speed = 40f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other){
        
        //if(other.GetComponent<BulletTarget>() != null){
            //hit target
            //enemy.E_TakeDamage(2);
            //slime.E_TakeDamage(2);
            //Debug.Log("damaged enemy");
            //Destroy(this.gameObject);
            //Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            //} else {
            //hit something else
            //Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        //}
        //simple patch up to destroy bullet after hitting something in 1 second
        Destroy(this.gameObject, 1);
    }
}
