using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Overwatch : MonoBehaviour
{

    public PapaData ppD;
    public PlayerData pD;
    public Papa ppS;
    public PO_Overwatch oW;
    public GameObject papaPref;
    public GameObject timeOut;

    // Stop is called before the first frame update
    void Start()
    {
        Vector3 spawnPoint;
        NavMeshHit hit;
        //ppD = ScriptableObject.CreateInstance<PapaData>();              
        //Debug.Log(timeOut);
        if (NavMesh.SamplePosition(timeOut.transform.transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            spawnPoint = hit.position;
            Instantiate(papaPref, spawnPoint, timeOut.transform.transform.rotation);
            ppS = papaPref.GetComponent<Papa>();
            oW = new PO_Overwatch(ppD, pD, ppS, this);
            ppD.isActive = false;
        }
    }


    void Update()
    {
        Debug.Log(ppD.isActive);
        FearCheck();
    }
    private void FearCheck()
    {

        //Debug.Log(oW.papa.ppM);
        if (ppD.isActive)
        {
            if (ppD.despawning)
                oW.GetLocation();
        }
        else
        {
            //if(fear >= threshhold)
            //{
            RespawnPapa();

            //}
        }

    }

    public void RespawnPapa()
    {
        oW.GetLocation();
        if(ppD.spawnPoint == Vector3.zero)
        {
            oW.GetLocation();
        }
        else
        {
            ppS.agent.Warp(ppD.spawnPoint);
            Debug.Log("Tele");
            Debug.Log(ppD.spawnPoint);
            ppD.isActive = true;

        }

    }

}
