using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms{

    public int posx { get; set; }
    public int posy { get; set; }
    public int length { get; set; }
    public int height { get; set; }

    public Rooms() { }

    /// <summary>
    /// Initializes a Rooms by using posx,posy,length and height
    /// </summary>
    /// <param name="px">posx</param>
    /// <param name="py">posy</param>
    /// <param name="len">length</param>
    /// <param name="hei">height</param>
    public Rooms(int px, int py, int len, int hei)
    {
        posx = px;
        posy = py;
        length = len;
        height = hei;
    }

    /// <summary>
    /// Check if overlap each other;
    /// </summary>
    /// <param name="ro"></param>
    /// <returns></returns>
    public virtual bool IfOverLap(Rooms ro)
    {
        bool Top = this.posy >= (ro.posy + ro.height);
        bool Bottom = (this.posy + this.height) <= ro.posy;
        bool Left = (this.posx + this.length) <= ro.posx;
        bool Right = this.posx >= (ro.posx + ro.length);
        if (Top || Bottom || Left || Right)
        {
            return false;
        }
        else return true;
    }

    /// <summary>
    /// Check if in it
    /// </summary>
    /// <param name="ro"></param>
    /// <returns></returns>
    public virtual bool IfContent(Rooms ro)
    {
        bool Top = (this.posy + this.height) <= (ro.posy + ro.height);
        bool Bottom = this.posy >= ro.posy;
        bool Left = this.posx >= ro.posx;
        bool Right = (this.posx + this.length) <= (ro.posx + ro.length);
        if (Top && Bottom && Left && Right)
        {
            return true;
        }
        return false;
    }
}
