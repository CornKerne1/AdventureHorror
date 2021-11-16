using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : RemovableObjects
{
    public WireData WD;
    private Animator wireAnimator;
    public Color color;


    public void Awake()
    {
        WD.count = 0;
        WD.WireOrder.Clear();
        WD.Wires.Clear();
    }
    public void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        collider = gameObject.GetComponent<Collider>();
        WD.Wires.Add(this);
        //removable = Removable.Wire;
        //wireAnimator = gameObject.GetComponent<Animator>();
    }
    public override void Remove()
    {
        Debug.Log("removing wire");
        meshRenderer.enabled = false;
        collider.enabled = false;
        if (WD.WireOrder[WD.count]== this)
        {
            Debug.Log("you cut the right wire");
            WD.count++;
            Debug.Log(WD.count + " & " + WD.WireOrder.Count);
            if(WD.count == WD.WireOrder.Count)
            {
                Debug.Log("all the wires are cut, the door is open");
                WD.door.Unlock();
                WD.door.Open();
            }
        }
        else // ressets all the wires;
        {
            Debug.Log("papa has reset the wires try again");
            WD.count = 0;
            foreach(Wire w in WD.WireOrder)
            {
                w.meshRenderer.enabled = true;
                w.collider.enabled = true;
            }
        }
        
    }
    public void setColor(Color color)
    {
        this.color = color;
        meshRenderer.material.color = color;
    }
    
}
