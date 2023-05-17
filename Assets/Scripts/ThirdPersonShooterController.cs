using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cmFreeLook;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    //[SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    private Vector3 aimDirection;
    private bool aiming = false;

/*    public void OnAim() //this works but is destructive of the fov value will fix later
    {
        if (!aiming)
        {
            aiming = true;
            cmFreeLook.m_Lens.FieldOfView = cmFreeLook.m_Lens.FieldOfView - 20;
        }
        else
        {
            aiming = false;
            cmFreeLook.m_Lens.FieldOfView = cmFreeLook.m_Lens.FieldOfView + 20;
        }
    }*/

    //variable for face towards aiming code
    Vector3 mouseWorldPosition = Vector3.zero;

    private void Update(){
        
        

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)){
            //debugTransform.position = raycastHit.point;
            //variable for face towards aiming code
            mouseWorldPosition = raycastHit.point;
        }

        // Convert Reticle position From screen space to world space through a raycast
        if(cmFreeLook.gameObject.activeSelf){
            //code to make the character face the way they are aiming
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            aimDirection = (worldAimTarget - transform.position).normalized;

            //change this to while onaim
            //transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        } 

    }

    public void Shoot(){
        //shoot where at the mouse cursor
        Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
        //shoot where the player is looking
        //Vector3 aimDir = transform.forward;
        //transform.forward = Vector3.Slerp(transform.forward, aimDirection, Time.deltaTime * 180f); //when you shoot the player faces the direction, may not be necessary 
        Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        
    }
}
