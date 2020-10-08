using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EVAInfo : MonoBehaviour
{
    public string EVAName;
    [TextArea(2,7)]
    public string EVADescription;
    public Sprite EVAImage;
    public Color EVAColor;
    public Sprite WinImage;
}
