using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityStandardAssets;

public class PlayerAnimations : MonoBehaviour
{ public Transform camera;
    public Animator gun_animator;
    public AmmoDisplay ammodisplay;
    public Ammo_Box ammobox;
    public Ammo_Box ammobox1;
    public WeaponPickup weaponpickup;
    public WeaponPickup weaponpickup1;
    public WeaponPickup weaponpickup2;

    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController other;
    public ObjectiveDisplay objectivedisplay;
    bool firing;
    int y = 4;
    int x = 0;
    public int pistolAmmo;
    public int MGAmmo;
    public int shotgunAmmo;
    public int ARAmmo;
    public bool lvl1;
    //public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController.MovementSettings fps;
    //public int damage;
    public int sgdamage_far;
    public int sgdamage_close;
    public int sgdamage_medium;
    public int ardamage;
    public int mgdamage;
    public int pistoldamage;
    public int maxpistol;
    public int maxmg;
    public int maxsg;
    public int maxar;
    //private GameObject fps;
    bool outofAmmo;
    bool switching = false;
    public AudioClip Pistol;
    public AudioClip Auto308;
    public AudioClip MG;
    public AudioClip SG;

    bool dontswitch;
    //  bool fullAuto;
    public AudioSource MusicSource;
    float time;
    public float mute_Delay;
    bool needtoMute;
    public bool headshot;
    int z = 0;
    public SpriteRenderer crosshairs;
    // bool isBig;
    public int wpnNum;
    private bool running;
    public bool useA;
    public bool useS;
    public bool useM;
    public string currentObj;
    // Start is called before the first frame update
    void Start()
    {
        MGAmmo = maxmg;
        pistolAmmo = maxpistol;
        shotgunAmmo = maxsg;
        ARAmmo = maxar;
        //gun_animator = this.anim
        firing = false;
        MusicSource.clip = Pistol;
        //   fullAuto = false;
        headshot = false;
        //   isBig = false;
        wpnNum = 0;
        AmmoCounter(0);
        outofAmmo = false;
        //fps = other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController.MovementSettings>();
        useA = false;
        useS = false;
        useM = false;
        if(lvl1)
        {   
            objectivedisplay.showObjective("Investigate The Enemy Compound", 5);
            currentObj = "Investigate The Enemy Compound";
            pistolAmmo = 0;
            shotgunAmmo = 5;
            MGAmmo = maxmg;
            ARAmmo = 30;
            other.canRun = false;
            
            AmmoCounter(0);
        }
    }

    void Objective()
    {
        objectivedisplay.showObjective(currentObj, 2);
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Objective();
        }
        running = other.Running;
        ammobox.player = this.GetComponent<PlayerAnimations>();
        ammobox1.player = this.GetComponent<PlayerAnimations>();
        weaponpickup.player = this.GetComponent<PlayerAnimations>();
        weaponpickup1.player = this.GetComponent<PlayerAnimations>();
        weaponpickup2.player = this.GetComponent<PlayerAnimations>();

        switching = false;
        
        if (!dontswitch)
        {
            WpnNumCheck();
        }


        //ammodisplay.wpnNumtoDisplay = wpnNum;
        ButtonCheck();
        
            }

    void WpnNumCheck()
    {
        int prevwpn = wpnNum;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (wpnNum >= 4)
            {
                wpnNum = 0;
            }

            wpnNum++;
            switching = true;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (wpnNum <= 0)
            {
                wpnNum = 4;
            }
            switching = true;
            wpnNum--;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switching = true;
            wpnNum = 0;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {   //AR 308

            switching = true;
            wpnNum = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switching = true;
            wpnNum = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switching = true;
            wpnNum = 2;
        }
        if (prevwpn != wpnNum && !lvl1)
        {
            SelectWpn(wpnNum);
        }

        else if (prevwpn != wpnNum && lvl1)
        {
            Lvl1SelectWpn(wpnNum, prevwpn);
        }
    }

    void ButtonCheck()
    {
        if (outofAmmo)
        {
            z = 0;
            //  Debug.Log("Should shrink");
            // crosshairs.transform.localScale = new Vector3(.2f, .2f, 1);
            //  Shrink();
            //  y = 0;
            x = 0;
            y = 0;
            gun_animator.SetBool("KeepFiring", false);
            gun_animator.SetBool("Idle", true);
            gun_animator.SetBool("Fire", false);
            dontswitch = false;
            //   if (fullAuto)
            {//
                StartCoroutine(Stop());
                //StartCoroutine(Shrink());
                //       Stop();
                //   }
                firing = false;
                // fullAuto = false;
            }
        }

        else
        {
            if (Input.GetButton("Fire1") && firing && !switching && (wpnNum == 0 || wpnNum == 3) && !outofAmmo && !running)
            {
                gun_animator.SetBool("Fire", false);
                gun_animator.SetBool("Idle", true);
                dontswitch = false;

            }

            else if (Input.GetButton("Fire1") && firing && !switching && (wpnNum != 0 && wpnNum != 3) && !outofAmmo && !running)
            {
                //  crosshairs.transform.localScale = new Vector3(.3f, .3f, 1);
                MusicSource.volume = 1;
                gun_animator.SetBool("KeepFiring", true);
                gun_animator.SetBool("Fire", false);
                gun_animator.SetBool("Idle", false);
                dontswitch = true;
                //   fullAuto = true;
                /*      if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 50))
                      {
                          //   Debug.Log(hit.transform.name);

                          Enemy target = hit.transform.GetComponent<Enemy>();

                          y++;
                          ///////////////NOT WORKING

                          if (target != null)
                          {

                              if (y % 20 == 0)
                              {
                                  //   Debug.Log("Can damage");
                                  Wait();
                                      target.takeDamage((int) (damage * Random.Range(0.5f, 1.5f)));
                                  //  Debug.Log(target.health);
                              }

                          }

                      }*/
            }

            else if (Input.GetButtonDown("Fire1") && !firing && !switching && !outofAmmo && !running)
            {

                //  crosshairs.transform.localScale = new Vector3(.3f, .3f, 1);

                MusicSource.volume = 1;
                gun_animator.SetBool("Fire", true);
                gun_animator.SetBool("KeepFiring", false);
                gun_animator.SetBool("Idle", false);
                firing = true;
                dontswitch = true;

                //  Debug.Log("fIRING");

                /*     if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 50))
              {
                     //   Debug.Log(hit.transform.name);

                          Enemy target = hit.transform.GetComponent<Enemy>();

                     //        y++;
                     ///////////////NOT WORKING

                         if (target != null)
                  {

                         //         if (y % 2 == 0)
                      {
                             //   Debug.Log("Can damage");
                             Wait();
                             target.takeDamage((int) (damage * Random.Range(0.5f, 1.5f)));
                             Debug.Log(target.health);
                     }

                     }

                 } */
            }


            else if (Input.GetButtonUp("Fire1") && !switching && (wpnNum != 0 && wpnNum != 3))// && firing)
            {
                z = 0;
                //  Debug.Log("Should shrink");
                // crosshairs.transform.localScale = new Vector3(.2f, .2f, 1);
                //  Shrink();
                //  y = 0;
                x = 0;
                y = 0;
                gun_animator.SetBool("KeepFiring", false);
                gun_animator.SetBool("Idle", true);
                gun_animator.SetBool("Fire", false);
                dontswitch = false;
                //   if (fullAuto)
                {//
                    StartCoroutine(Stop());
                    //StartCoroutine(Shrink());
                    //       Stop();
                    //   }
                    firing = false;
                    // fullAuto = false;
                }
            }
            else if (Input.GetButtonUp("Fire1") && !switching && (wpnNum == 0 || wpnNum == 3))// && firing)
            {
                z = 0;
                //  Shrink();

                x = 0;
                gun_animator.SetBool("KeepFiring", false);
                gun_animator.SetBool("Idle", true);
                gun_animator.SetBool("Fire", false);
                dontswitch = false;
                //   if (fullAuto)
                {//

                    //StartCoroutine(Shrink());
                    //       Stop();
                    //   }
                    firing = false;
                    // fullAuto = false;
                }
            }

        }
    }
    public void SelectWpn(int num)
    {
        gun_animator.SetBool("switcha", false);
        gun_animator.SetBool("switchs", false);
        gun_animator.SetBool("switchmg", false);
        gun_animator.SetBool("switchp", false);
        if (num == 0)
        {
            //damage = 50;
            MusicSource.clip = Pistol;
            gun_animator.SetBool("switchp", true);
            AmmoCounter(0);
        }
        if (num == 1)
        {
            MusicSource.clip = Auto308;
            //damage = 30;
            gun_animator.SetBool("switcha", true);
            AmmoCounter(0);
        }
        if (num == 3)
        {
            MusicSource.clip = SG;
            //damage = 69;
            gun_animator.SetBool("switchs", true);
            AmmoCounter(0);
        }
        if (num == 2)
        {
            MusicSource.clip = MG;
            //damage = 60;
            gun_animator.SetBool("switchmg", true);
            AmmoCounter(0);
        }



    }
    public void Lvl1SelectWpn(int num, int prev)
    {

        gun_animator.SetBool("switcha", false);
        gun_animator.SetBool("switchs", false);
        gun_animator.SetBool("switchmg", false);
        gun_animator.SetBool("switchp", false);
        if (num == 0)
        {
            //damage = 50;
            MusicSource.clip = Pistol;
            gun_animator.SetBool("switchp", true);
            AmmoCounter(0);
        }
        else if (num == 1 && useA)
        {
            MusicSource.clip = Auto308;
            //damage = 30;
            gun_animator.SetBool("switcha", true);
            AmmoCounter(0);
        }
        else if (num == 3 && useS)
        {
            MusicSource.clip = SG;
            //damage = 69;
            gun_animator.SetBool("switchs", true);
            AmmoCounter(0);
        }
        else if (num == 2 && useM)
        {
            MusicSource.clip = MG;
            //damage = 60;
            gun_animator.SetBool("switchmg", true);
            AmmoCounter(0);
        }

        else
        {
            wpnNum = prev;
        }

    }
    public void ShotgunFire()
    {   StartCoroutine(Grow());
        MusicSource.Play();
        AmmoCounter(1);
        RaycastHit hit;
        RaycastHit hit1;
        RaycastHit hit2;


        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 10))
        {
            //   Debug.Log(hit.transform.name);
            if (hit.collider.tag.Equals("Head"))
            {
                //    Debug.Log("Headshot");
                headshot = true;

            }
            if (hit.collider.tag.Equals("Body"))
            {
                //   Debug.Log("Bodyshot");
                headshot = false;
            }
            Enemy target = hit.transform.GetComponent<Enemy>();

            y++;
            ///////////////NOT WORKING

            if (target != null)
            {
                // Debug.Log("Hit");
                //  Debug.Log(headshot);

                x = x + (int)Random.Range(1f, 5f);

                if (x % 2 == 0)
                {
                    if (headshot)
                    {
                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }

                }

                else
                {
                    if (headshot)
                    {

                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));

                    }
                }
                // if (y % 5 == 0)
                {
                    y = 0;

                    target.takeDamage((int)(sgdamage_close * Random.Range(0.5f, 1.5f)), headshot);
                }

            }

        }


        else if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit1, 15))
        {
            //   Debug.Log(hit.transform.name);
            if (hit1.collider.tag.Equals("Head") && hit1.collider.tag != null)
            {
                //    Debug.Log("Headshot");
                headshot = true;

            }
            if (hit1.collider.tag.Equals("Body") && hit1.collider.tag != null)
            {
                //   Debug.Log("Bodyshot");
                headshot = false;
            }
            Enemy target = hit1.transform.GetComponent<Enemy>();

            y++;
            ///////////////NOT WORKING

            if (target != null)
            {
                // Debug.Log("Hit");
                //  Debug.Log(headshot);

                x = x + (int)Random.Range(1f, 5f);

                if (x % 2 == 0)
                {
                    if (headshot)
                    {
                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }

                }

                else
                {
                    if (headshot)
                    {

                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));

                    }
                }
             //   if (y % 5 == 0)
                {
                    y = 0;
                    
                    target.takeDamage((int)(sgdamage_medium * Random.Range(0.5f, 1.5f)), headshot);
                }

            }

        }


        else if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit2, 50))
        {
            //   Debug.Log(hit.transform.name);
            if (hit2.collider.tag.Equals("Head") && hit2.collider.tag != null)
            {
                //    Debug.Log("Headshot");
                headshot = true;

            }
            if (hit2.collider.tag.Equals("Body") && hit2.collider.tag != null)
            {
                //   Debug.Log("Bodyshot");
                headshot = false;
            }
            Enemy target = hit2.transform.GetComponent<Enemy>();

            y++;
            ///////////////NOT WORKING

            if (target != null)
            {
                // Debug.Log("Hit");
                //  Debug.Log(headshot);

                x = x + (int)Random.Range(1f, 5f);

                if (x % 2 == 0)
                {
                    if (headshot)
                    {
                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }

                }

                else
                {
                    if (headshot)
                    {

                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));

                    }
                }
              //  if (y % 5 == 0)
                {
                    y = 0;
                    
                    target.takeDamage((int)(sgdamage_far * Random.Range(0.9f, 1.1f)), headshot);
                }

            }

        }





    }
    public void SingleShotFire()
        {
        StartCoroutine(Grow());
        AmmoCounter(1);
        MusicSource.Play();
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 35))
        {
            //   Debug.Log(hit.transform.name);
            if (hit.collider.tag.Equals("Head"))
            {
                //    Debug.Log("Headshot");
                headshot = true;

            }

            else
            {
                //   Debug.Log("Bodyshot");
                headshot = false;
            }
            Enemy target = hit.transform.GetComponent<Enemy>();

            
            ///////////////NOT WORKING

            if (target != null)
            {
                // Debug.Log("Hit");
                //  Debug.Log(headshot);

                x = x + (int)Random.Range(1f, 5f);

                if (x % 2 == 0)
                {
                    if (headshot)
                    {
                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }

                }

                else
                {
                    if (headshot)
                    {

                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));

                    }
                }
                // (y % 5 == 0)
                {
                  //  y = 0;
                    
                    target.takeDamage((int)(pistoldamage * Random.Range(0.9f, 1.1f)), headshot);
                   // y++;
                }

            }

        }


    }

    public void CrossShrink()
    {
        StartCoroutine(Shrink());
    }

    public void Fire()
    {
        

        StartCoroutine(Grow());
       
        headshot = false;
        RaycastHit hit;
        
       // if (z % 2 == 0)
        {
            AmmoCounter(1);
            MusicSource.Play();
        }

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 35))
        {
            //   Debug.Log(hit.transform.name);
            if (hit.collider.tag.Equals("Head"))
            {
                //    Debug.Log("Headshot");
                headshot = true;

            }
            if (hit.collider.tag.Equals("Body"))
            {
                //   Debug.Log("Bodyshot");
                headshot = false;
            }
            Enemy target = hit.transform.GetComponent<Enemy>();
            
            ///////////////NOT WORKING

            if (target != null)
            {
                // Debug.Log("Hit");
                //  Debug.Log(headshot);

                x = x + (int)Random.Range(1f, 5f);

                if (x % 2 == 0)
                {
                    if (headshot)
                    {
                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 + Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }

                }

                else
                {
                    if (headshot)
                    {

                        CritFloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));
                    }
                    else
                    {
                        FloatingTextController.screenPosition = new Vector2(Screen.width / 2 - Random.Range(10f, 20f), Screen.height / 2 + Random.Range(10f, 20f));

                    }
                }
                // if (y % 5 == 0)
                if (wpnNum == 1)
                {

                    target.takeDamage((int)(ardamage * Random.Range(0.9f, 1.1f)), headshot);
                }
                else
                {
                    target.takeDamage((int)(mgdamage * Random.Range(0.9f, 1.1f)), headshot);
                }
                
                y++;

            }

        }
        z++;
    }

    public void AmmoCounter(int ammoUsed)
    {
        if (wpnNum == 0)
        {
            pistolAmmo = pistolAmmo - ammoUsed;
            ammodisplay.changeAmmo(pistolAmmo);
            if (pistolAmmo <= 0)
            {
                outofAmmo = true;
                pistolAmmo = 0;
            }
            else
            {
                outofAmmo = false;
            }
        }

        if (wpnNum == 1)
        {
            ARAmmo-= ammoUsed;
            
            if (ARAmmo <= 0)
            {
                outofAmmo = true;
                ARAmmo = 0;
            }
            else
            {
                outofAmmo = false;
            }
            ammodisplay.changeAmmo(ARAmmo);
        }

        if (wpnNum == 3)
        {
            shotgunAmmo-= ammoUsed;
            ammodisplay.changeAmmo(shotgunAmmo);
            if (shotgunAmmo <= 0)
            {
                outofAmmo = true;
                shotgunAmmo = 0;
            }
            else
            {
                outofAmmo = false;
            }
        }

        if (wpnNum == 2)
        {
            MGAmmo-= ammoUsed;
            ammodisplay.changeAmmo(MGAmmo);
            if (MGAmmo <= 0)
            {
                outofAmmo = true;
                MGAmmo = 0;
            }
            else
            {
                outofAmmo = false;
            }
        }

    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(.14f);
        MusicSource.volume = 0;
    }
    // IEnumerator Wait()
    //   {
    //     yield return new WaitForSeconds(.5f);
    //
    // }
    IEnumerator Shrink()
    {
        yield return new WaitForSeconds(0f);
        crosshairs.transform.localScale = new Vector3(.04f, .04f, 1);
    }
    IEnumerator Grow()
    {
        yield return new WaitForSeconds(0f);
        crosshairs.transform.localScale = new Vector3(.06f, .06f, 1);
        
    }

    
} 
