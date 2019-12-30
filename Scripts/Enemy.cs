using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
public class Enemy : MonoBehaviour
{
    public int dropchance;
    public Transform MyCameraTransform;
    public Transform MyTransform;
    public NavMeshAgent ai;
    float distance;
    public float health;
    Vector3 delta;
    RaycastHit hit;
    bool canSee;
    int time_elapsed;
    public Animator animator;
    public int lookDistance;
    public int stopDistance;
    public int retreatDistance;
    public int lastStandDistance;
    public SphereCollider body;
    public BoxCollider head;
    public SphereCollider body1;
    public BoxCollider head1;
    public Transform[] lootDrop;
    public int damage;
    public AudioSource MusicSource;
    public AudioClip Dying;
    public AudioClip Firing;
    public int timetoWait = 10;
    //  public Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        MusicSource.clip = Firing;
        MyTransform = this.transform;
        MyCameraTransform = Camera.main.transform;
        animator.SetBool("Walk", true);
        FloatingTextController.Initalize();
        CritFloatingTextController.Initalize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        //    position = new Vector2(MyTransform.position.x + Random.Range(-.5f, .5f), MyTransform.position.y + Random.Range(-.5f, .5f));
        MyTransform.LookAt(MyCameraTransform, Vector3.up);
        if (health > 0)
        {
            time_elapsed++;
            if (Physics.Raycast(MyTransform.transform.position, MyTransform.transform.forward, out hit, 200))
            {
                PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
                if (!(target == null))
                {
                    canSee = true;
                    InteractWithPlayer(target);

                }
                else
                {
                    //  Debug.Log("Can't see anyone!");
                    canSee = false;
                    InteractWithPlayer(target);
                }
            }

        }
    }

    public void takeDamage(float damage, bool crit)
    {
        if (crit)
        {
            damage = damage * 2;
            CritFloatingTextController.CreateFloatingText(damage.ToString(), MyTransform, distance, crit);
        }
        else
        {
            FloatingTextController.CreateFloatingText(damage.ToString(), MyTransform, distance, crit);
        }
        health = health - damage;
        if (health <= 0)
        {
            
            ai.SetDestination(MyTransform.position);
            animator.SetBool("Die", true);
            body.enabled = false;
            head.enabled = false;
           
            if (body1 != null)
            {
                body1.enabled = false;
            }
            if (head1 != null)
            {
                head1.enabled = false;
            }
            Dead();

        }
    }

    void InteractWithPlayer(PlayerHealth target)
    {
        if (canSee)
        {

            distance = Vector3.Distance(MyTransform.position, MyCameraTransform.position);
            if (distance <= 4)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Attack", false);
                Vector3 localPosition = -MyCameraTransform.transform.position + MyTransform.position;
                localPosition = localPosition.normalized; // The normalized direction in LOCAL space
                Vector3 destination = new Vector3(-localPosition.x * Time.deltaTime, -localPosition.y * Time.deltaTime, -localPosition.z * Time.deltaTime);
                ai.SetDestination(destination);
            }
            else
            {


                if (distance <= lastStandDistance)
                {
                    ai.SetDestination(MyTransform.position);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Attack", true);
                    ai.SetDestination(MyTransform.position);
                }




                else
                {
                    if (distance <= stopDistance && distance > retreatDistance)
                    {
                        StartCoroutine(WaitToShoot(timetoWait));
                        ai.SetDestination(MyTransform.position);
                        animator.SetBool("Walk", false);
                        animator.SetBool("Idle", false);
                        animator.SetBool("Attack", true);
                        ai.SetDestination(MyTransform.position);
                        //  if (time_elapsed % 25 == 0)
                        //{
                        //    time_elapsed = 0;
                        //     target.TakeDamage(4);
                        //   }
                    }

                    if (distance < lookDistance && distance > stopDistance)
                    {

                        animator.SetBool("Walk", true);
                        animator.SetBool("Idle", false);
                        animator.SetBool("Attack", false);
                        ai.SetDestination(MyCameraTransform.position);

                    }

                    // delta = MyTransform.position - MyCameraTransform.position;

                    if (distance < retreatDistance && distance > lastStandDistance)
                    {
                        animator.SetBool("Walk", true);
                        animator.SetBool("Idle", false);
                        animator.SetBool("Attack", false);
                        // Vector3 localPosition = -MyCameraTransform.transform.position + MyTransform.position;
                        //    localPosition = localPosition.normalized; // The normalized direction in LOCAL space
                        //  Vector3 destination = new Vector3(-localPosition.x * Time.deltaTime, -localPosition.y * Time.deltaTime, -localPosition.z * Time.deltaTime);
                        ai.SetDestination(MyCameraTransform.position);
                    }

                }
            }
        }



        else
        {
            ai.SetDestination(MyTransform.position);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
        }


    }
    public void Shoot()
    {
        MusicSource.Play();
        PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
        ai.SetDestination(MyTransform.position);
        time_elapsed = 0;
        if (target != null)
            target.TakeDamage(damage);
    }

    void Dead()
    {
        MusicSource.clip = Dying;
        MusicSource.Play();
        

        if (lootDrop != null)
            {
            int x = Random.Range(1, dropchance);

            if (x == 1)
            {
                Vector3 spawnpos = new Vector3(MyTransform.position.x, 0.22f, MyTransform.position.z);
                int i = Random.Range(0, lootDrop.Length);
                if (dropchance != 0)
                {
                    Instantiate(lootDrop[i], spawnpos, Quaternion.identity);
                }
            }
        }
        Destroy(this.gameObject,2.5f);
    }

    IEnumerator WaitToShoot(int time)
    {
        time = Random.Range(0, time*10);
        time = time / 100;
        yield return new WaitForSeconds(time);
    }

}
