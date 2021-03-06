using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Panel : RemovableObjects
{
    private List<Screw> screws = new List<Screw>();
    private Animator panelAnimator;
    public VentPortal VentPortal;
    public string neededTool = "none";
    private bool removed = false;

    public void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        collider = gameObject.GetComponent<Collider>();
        //panelAnimator = gameObject.GetComponent<Animator>();
        //removable = Removable.Panel;
    }
    public override void Remove()
    {
        if (screws.Count == 0)
        {
            if (!removed)
            {
                removed = true;
                FindObjectOfType<AudioManager>().Play("vent_grate_3");
                StartCoroutine(DropPanel());
            }

        }
    }
    public void AddScrewToList(Screw screw)
    {
        screws.Add(screw);
    }
    public void RemoveScrewFromList(Screw screw)
    {
        screws.Remove(screw);

    }

    public override string NeededTool()
    {
        return neededTool;
    }
    IEnumerator DropPanel()
    {
        yield return new WaitForSeconds(.5f);
        //meshRenderer.enabled = false;
        //collider.enabled = false;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * 800);
        if (VentPortal != null)
            VentPortal.canExit = true;
    }
}
