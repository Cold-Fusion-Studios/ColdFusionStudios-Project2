using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Box : MonoBehaviour
{
    public Transform MyCameraTransform;
    public Transform MyTransform;
    // Start is called before the first frame update
    public PlayerAnimations player;
    public bool isBig;
    public BoxCollider myCollider;
    public SpriteRenderer sr;
    void Start()
    {
        MyTransform = this.transform;
        MyCameraTransform = Camera.main.transform;
           
    }

    // Update is called once per frame
    void Update()
    { Vector3 xandz = new Vector3(MyCameraTransform.position.x, MyTransform.position.y, MyCameraTransform.position.z);
      MyTransform.LookAt(xandz, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>())
            {   //player = other.GetComponent<PlayerAnimations>();
                if (isBig)
                {
                    if (player.ARAmmo < player.maxar)
                    {
                        player.ARAmmo = player.maxar;
                        DestroyBox();

                    }
                    if (player.MGAmmo < player.maxmg)
                    {
                        player.MGAmmo = player.maxmg;
                        DestroyBox();
                    }
                    if (player.shotgunAmmo < player.maxsg)
                    {
                        player.shotgunAmmo = player.maxsg;
                        DestroyBox();
                    }
                    if (player.pistolAmmo < player.maxpistol)
                    {
                        player.pistolAmmo = player.maxpistol;
                        DestroyBox();
                    }
                }
                else
                {
                    if (player.wpnNum == 0)
                    {
                        if (player.pistolAmmo < player.maxpistol)
                        {
                            player.AmmoCounter(-player.maxpistol + player.pistolAmmo);
                            DestroyBox();
                        }
                    }
                    if (player.wpnNum == 1)
                    {
                        if (player.ARAmmo < player.maxar)
                        {
                            player.AmmoCounter(-player.maxar + player.ARAmmo);
                            DestroyBox();
                        }
                    }
                    if (player.wpnNum == 3)
                    {
                        if (player.shotgunAmmo < player.maxsg)
                        {
                            player.AmmoCounter(-player.maxsg + player.shotgunAmmo);
                            DestroyBox();
                        }
                    }
                    if (player.wpnNum == 2)
                    {
                        if (player.MGAmmo < player.maxmg)
                        {
                            player.AmmoCounter(-player.maxmg + player.MGAmmo);
                            DestroyBox();
                        }
                    }
                }
            }
        }
    }
    private void DestroyBox()
    {
        player.AmmoCounter(0);
        myCollider.enabled = false;
        sr.enabled = false;
        Destroy(this.gameObject);
    }
}
    

