using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PapaData", menuName = "PapaData/Generic")]
public class PapaData : ScriptableObject
{

    [Header("Papa")]
    public GameObject Papa; //Papa object
    public Vector3 targetLastSeen;
    public GameObject player;
    
    
    [Header("Sight Variables")]
    
    [Range(0, 360)]
    public float angle;

    public float radius;
    public LayerMask targetLayer;
    public LayerMask occlusionLayers;
    public bool canSeeTarget;
    public bool isAgro = false;

    [Header("Movement")]
    public float papaBaseSpeed = 3.5f;
    public float chaseSpeedMultiplier = 2f;
    public LayerMask searchNodeLayer;



}
