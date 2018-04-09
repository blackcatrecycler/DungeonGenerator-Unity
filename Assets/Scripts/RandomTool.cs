using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class RandomTool
{
    /// <summary>
    /// Roll a pair of nums , x+y&lt;=All;you can size y
    /// </summary>
    /// <param name="x">x</param>
    /// <param name="y">y</param>
    /// <param name="All">All</param>
    /// <param name="miny">the minimam of y</param>
    /// <param name="maxy">the maxmam of y</param>
    /// <param name="rd">Random seed</param>
    public static void Roll(ref int x, ref int y, int All, int miny, int maxy, System.Random rd)
    {
        y = rd.Next(miny, maxy);
        x = rd.Next(0, All - y);
    }

    public static int Roll(int x, int y, System.Random rd)
    {
        return rd.Next(x, y);
    }
}

static class RandomTool<T>
{
    /// <summary>
    /// Roll an Item in list,and delete it from the list
    /// </summary>
    /// <param name="a">List</param>
    /// <param name="rd">Random seed</param>
    /// <returns></returns>
    public static T RollItem(ref List<T> a, System.Random rd)
    {

        T m = default(T);
        int se = rd.Next(0, a.Count - 1);
        m = a[se];
        a.Remove(m);
        return m;
    }
}
