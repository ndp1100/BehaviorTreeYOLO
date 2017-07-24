using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ItemBase
{
    public GameObject EntryOB;
    public GameObject ExitOB;

    public Animation animation;

    public void Open()
    {
        Debug.Log("Open Door");
        if (animation)
        {
            animation.Play("OpenDoor");
        }
    }

    public void Close()
    {
        Debug.Log("Close Door");
        if (animation)
        {
            animation.Play("CloseDoor");
        }
    }
}
