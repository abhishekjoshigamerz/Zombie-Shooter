using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleflash;
    [SerializeField] GameObject hitEffect;
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    private void Shoot(){
       
        PlayMuzzleFlash();
        ProcessRaycast();
        
    }

    private void PlayMuzzleFlash()
    {
        muzzleflash.Play();
    }

    private void ProcessRaycast(){
        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position,FPCamera.transform.forward,out hit,range)){
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target == null)return;
            target.TakeDamage(damage);
            Debug.Log("I hit this thing" + hit.transform.name);
        }else{
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject bulletImpact = Instantiate(hitEffect,hit.point,Quaternion.LookRotation(hit.normal));
        Destroy(bulletImpact,0.1f);
    }
}
