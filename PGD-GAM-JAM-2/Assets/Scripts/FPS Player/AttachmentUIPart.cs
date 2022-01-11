using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentUIPart : MonoBehaviour
{
    [SerializeField]private Sprite myImage;

    public Sprite getImage()
    {
        return myImage;
    }
}
