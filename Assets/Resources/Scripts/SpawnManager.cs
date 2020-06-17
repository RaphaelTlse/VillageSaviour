using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject tempGO;
    GameObject[] Npcs = new GameObject[15];

    public void Load_Npcs()
    {
        for (int i = 0; i < 15; i++)
            Npcs[i] = Resources.Load<GameObject>("Characters/Npc" + (i + 1));
    }

    public void Shuffle_Npcs()
    {
        for (int i = 0; i < Npcs.Length; i++)
        {
            int rnd = Random.Range(0, Npcs.Length);
            tempGO = Npcs[rnd];
            Npcs[rnd] = Npcs[i];
            Npcs[i] = tempGO;
        }
    }

    public void setRolesAndDialogTypes()
    {
        int _rnd = 0;

        Shuffle_Npcs();

        for (int i = 0; i < Npcs.Length; ++i)
        {
            _rnd = UnityEngine.Random.Range(1, 11);
            if (_rnd == 1)
                Npcs[i].GetComponent<NpcManager>()._isDialogLie = true;
            else if (_rnd == 2 || _rnd == 3)
                Npcs[i].GetComponent<NpcManager>()._isDialogHonest = true;
            else
                Npcs[i].GetComponent<NpcManager>()._isDialogIrrelevant = true;
        }

        Npcs[0].GetComponent<NpcManager>().IsMurderer = true;

        if (Npcs[0].GetComponent<NpcManager>()._isDialogHonest == true)
        {
            Npcs[0].GetComponent<NpcManager>()._isDialogHonest = false;
            _rnd = UnityEngine.Random.Range(1, 10);
            if (_rnd == 1)
                Npcs[0].GetComponent<NpcManager>()._isDialogLie = true;
            else
                Npcs[0].GetComponent<NpcManager>()._isDialogIrrelevant = true;
        }

        Debug.Log(Npcs[0].name + " is the murderer.");
    }

    public void Place_Npcs()
    {
        int children = transform.childCount;
        
        Shuffle_Npcs();

        for (int i = 0; i < children; ++i)
            GameObject.Instantiate(Npcs[i], transform.GetChild(i).transform.position, Quaternion.identity);
    }

    void Start()
    {
        Load_Npcs();
        if (Npcs != null)
        {
            setRolesAndDialogTypes();
            Place_Npcs();
        }
        else
        {
            Debug.LogError("Fatal error : the resources did not load properly.");
        }
    }
}
