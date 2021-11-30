using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class P_Inventory
{
    private PlayerData PD;
    private Player player;
    private Vector3 rotation = new Vector3(0, 0, 0);
    private MeshRenderer mr;
    private bool toolHidden = true;

    public P_Inventory(PlayerData PD)
    {
        PD.tools = new List<GameObject>();
        this.PD = PD;
        this.player = PD.player;
        PD.inventory = this;
    }

    public void ThrowObj()
    {
        GameObject obj = PD.inHand;

    }

    public void AddTool(GameObject tool)
    {
        Debug.Log("you added " + tool + " to your toolbelt");
        PD.tools.Add(tool);
    }
    public void DisplayTool(string toolName)
    {
        foreach (GameObject tool in PD.tools)// loops through toolbelt
        {
            Tool t = tool.GetComponent<Tool>(); //assignment that increases efficency
            if (t.IsTool().Equals(toolName))// locates needed tool
            {
                Debug.Log("Displaying the tool");
                mr = tool.GetComponentInChildren<MeshRenderer>();
                if(PD.inToolHand != null)
                {
                    HideTool();
                }
                PD.inToolHand = tool;
                mr.enabled = true;
                toolHidden = false;
                Debug.Log("Is tool hidden?:" + mr.enabled);
            }
        }
    }
    public void HideTool()
    {
        if (!toolHidden)
        {
            mr.enabled = false;
            PD.inToolHand = null;
            toolHidden = true;
        }
    }

    public bool hasTool(string toolName)
    {
        foreach (GameObject tool in PD.tools)
        {
            Tool t = tool.GetComponent<Tool>();
            if (t.IsTool().Equals(toolName))
            {
                return true;

            }
        }
        return false;
    }
}
