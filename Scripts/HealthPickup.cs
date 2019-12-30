using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public Transform MyCameraTransform;
    public Transform MyTransform;
    // Start is called before the first frame update

    public bool isBig;
    public BoxCollider myCollider;
    public SpriteRenderer sr;
    // Start is called before the first frame update
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
            if (other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>() != null)
            {
                
                if (isBig)
                {
                    
                    PlayerHealth target = other.transform.GetComponent<PlayerHealth>();
                    
                    if (target.health < 100)
                    {
                        target.TakeDamage(-100);
                        DestroyBox();
                        
                    }

                }
                else
                {
                    PlayerHealth target = other.transform.GetComponent<PlayerHealth>();

                    if (target.health < 100)
                    {
                        target.TakeDamage(-25);
                        DestroyBox();
                    }
                }
            }
        }
    

    private void DestroyBox()
    {
        myCollider.enabled = false;
        sr.enabled = false;
        Destroy(this.gameObject);
    }
}
    
