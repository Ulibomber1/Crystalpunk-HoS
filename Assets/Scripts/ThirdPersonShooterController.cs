using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

   public void OnAim(){
        aimVirtualCamera.gameObject.SetActive(!aimVirtualCamera.gameObject.activeSelf);
   }
    //variable for face towards aiming code
    Vector3 mouseWorldPosition = Vector3.zero;

    private void Update(){
        
        

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)){
            debugTransform.position = raycastHit.point;
            //variable for face towards aiming code
            mouseWorldPosition = raycastHit.point;
        }

        if(aimVirtualCamera.gameObject.activeSelf){
            //code to make the character face the way they are aiming
            //Vector3 worldAimTarget = mouseWorldPosition;
            //worldAimTarget.y = transform.position.y;
            //Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            //transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        } 

    }

    public void OnFire(){
        //shoot where at the mouse cursor
        //Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
        //shoot where the player is looking
        Vector3 aimDir = transform.forward;
        Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        
    }
}
