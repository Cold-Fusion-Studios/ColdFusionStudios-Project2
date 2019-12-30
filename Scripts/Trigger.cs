using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //Set up bools here to determine which functions will be used
    public bool changeObj;
    public string obj;
    public PlayerAnimations player;
    public ObjectiveDisplay objdis;
    public bool spawnenem;
    public WaveSpawner[] spawns;
    private bool firsttime;
    public bool destroy;
    public bool changemusic;
    public Music_Player mp;
    public int songnum;
    private void Start()
    {
        firsttime = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>() && firsttime)
            {
                firsttime = false;
                if (changeObj)
                {
                    objdis.showObjective(obj);
                    player.currentObj = obj;
                }
                if(spawnenem)
                {
                    for (int i = 0; i < spawns.Length; i++)
                    {
                        spawns[i].triggered = true;
                    }
                }
                if (destroy)
                { Destroy(this.gameObject); }

                if (changemusic)
                {
                    mp.changeSong(songnum);
                }

            }
        }
    }
}