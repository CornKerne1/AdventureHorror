using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Interact
{
    // Start is called before the first frame update
    private PlayerData PD;
    private Player player;
    private Vector3 rotation = new Vector3(0, 0, 0);
    private RemovableObjects rm;
    public P_Interact(PlayerData PD)
    {
        this.PD = PD;
        this.player = PD.player;
    }

    public void InteractCheck()
    {
        Ray ray = PD.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;// holds data on what is infront of the player
        Physics.Raycast(ray, out hit, PD.InteractRange);// finds what is infront of the player
        if (hit.collider != null)
            switch (hit.collider.tag) //determins if hit is interactable
            {
                case "Manipulable":
                    rm = hit.collider.gameObject.GetComponent<RemovableObjects>();
                    if (PD.inventory.hasTool(rm.NeededTool())) // checks to see if you have neccessary tool
                    {
                        Debug.Log("displaying tool " + rm.NeededTool());
                        PD.inventory.DisplayTool(rm.NeededTool()); // displays needed tool
                    }
                    break;
                default:
                    break;

            }
        else PD.inventory.HideTool();
    }

    public void Interact()
    {
        Ray ray = PD.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;// holds data on what is infront of the player
        Physics.Raycast(ray, out hit, PD.InteractRange);// finds what is infront of the player
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            switch (hit.collider.tag) //determins if hit is interactable
            {
                //case "Door":
                //    InteractWithDoor(hit.collider.gameObject); // opens or closes door (if you can)
                //    break;
                case "PickUpAble":
                    PickUpObject(hit.collider.gameObject);// picks up PickUp
                    break;
                case "HidingPlace":
                    //Debug.Log("so hidden wow");
                    HideInHidingPlace(hit.collider.gameObject);
                    break;
                case "Manipulable":
                    Manipulate(hit.collider.gameObject);
                    break;
                case "Removable":
                    Debug.Log("hit removable");
                    RemoveObject(hit.collider.gameObject);
                    break;
                case "Vent":
                    EnterVent(hit.collider.gameObject);
                    break;
                case "Tool":
                    Debug.Log(hit.collider.gameObject.tag);
                    AddTool(hit.collider.gameObject);
                    break;
                default:
                    break;
            }
        }
        else if (PD.inHand != null && PD.inHand.name.Contains("Flashlight"))
        {
            PD.inHand.GetComponent<Flashlight>().Use();
        }

    }
    //private void InteractWithDoor(GameObject door)
    //{
    //    door.GetComponent<Door>().interactWith();
    //}
    private void HideInHidingPlace(GameObject HP)
    {
        HP.GetComponent<HidingPlace>().interactWith(PD);

    }
    private void Manipulate(GameObject obj)
    {
        if (PD.inToolHand != null)
            PD.inToolHand.GetComponent<Tools>().Use(obj);
    }
    private void PickUpObject(GameObject obj)
    {

        //Attaches OBJ to player in a visable way
        //prevents you from picking up something else
        //Debug.Log("pickup");
        if (PD.inHand == null)
        {
            GameObject hand = GameObject.Find("Hand");
            if (obj.TryGetComponent(out FriendBehavior fB))
            {
                fB.PlayPickUpAnimation();
            }
            PD.inHand = obj;
            obj.GetComponent<Collider>().enabled = false;// turns off object collisions
            obj.GetComponent<Rigidbody>().useGravity = false; // turns off object so it can be in hand
            obj.transform.SetPositionAndRotation(player.hand.position, PD.cam.transform.rotation);
            obj.GetComponent<Rigidbody>().freezeRotation = true;
            obj.GetComponent<Rigidbody>().velocity = rotation;
            //obj.transform.position = player.hand.position; // fixes object to player hand position
            obj.transform.parent = hand.transform;// fixes object to players position/movment
        }
        //allows you to throw something
    }

    private void RemoveObject(GameObject obj)
    {
        obj.GetComponent<RemovableObjects>().Remove();
    }
    private void EnterVent(GameObject obj)
    {
        obj.GetComponent<VentPortal>().Teleport();
    }
    public void AddTool(GameObject obj)
    {

        Debug.Log("add tool to hand");
        PD.inventory.AddTool(obj);
        GameObject toolHand = GameObject.Find("ToolHand");
        obj.GetComponentInChildren<MeshRenderer>().enabled = false;// turns it invisible until needed
        obj.GetComponent<Collider>().enabled = false;// turns off object collisions
        obj.GetComponent<Rigidbody>().useGravity = false; // turns off object so it can be in hand
        obj.transform.SetPositionAndRotation(player.toolHand.position, PD.cam.transform.rotation);
        obj.GetComponent<Rigidbody>().freezeRotation = true;
        obj.GetComponent<Rigidbody>().velocity = rotation;
        //obj.transform.position = player.hand.position; // fixes object to player hand position
        obj.transform.parent = toolHand.transform;// fixes object to players position/movment
    }

    public void ThrowHandObj()
    {
        GameObject obj = PD.inHand;
        PD.inHand = null;
        obj.transform.parent = null;
        obj.GetComponent<Rigidbody>().useGravity = true;
        obj.GetComponent<Collider>().enabled = true;
        obj.GetComponent<Rigidbody>().freezeRotation = false;
        obj.GetComponent<Rigidbody>().velocity = (obj.transform.forward * PD.throwForce);


    }

}
