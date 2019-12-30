using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Transform MyCameraTransform;
    public Transform MyTransform;
    // Start is called before the first frame update
    public PlayerAnimations player;
    public bool isBig;
    public BoxCollider myCollider;
    public SpriteRenderer sr;
    public ObjectiveDisplay objdis;
    public bool isA;
    public bool isS;
    public bool isM;
    void Start()
    {
        MyTransform = this.transform;
        MyCameraTransform = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 xandz = new Vector3(MyCameraTransform.position.x, MyTransform.position.y, MyCameraTransform.position.z);
        MyTransform.LookAt(xandz, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>())
            {   //player = other.GetComponent<PlayerAnimations>();
                if (isA)
                {
                    player.useA = true;
                    objdis.showUpdate("New Weapon Unlocked: Press 2 to use AR-308");
                }
                if (isS)
                {
                    player.useS = true;
                    objdis.showUpdate("New Weapon Unlocked: Press 3 to use the Trench Clearer");
                }
                if (isM)
                {
                    player.useM = true;
                    objdis.showUpdate("New Weapon Unlocked: Press 4 to use the MG");
                }
                //player.SelectWpn(1);
                DestroyBox();
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
    



