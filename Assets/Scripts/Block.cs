using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{

    public int top { get; set; }
    public int bottom { get; set; }
    public int left { get; set; }
    public int right { get; set; }
    public int block { get; set; }
    public int leftTop { get; set; }
    public int rightTop { get; set; }
    public int leftBottom { get; set; }
    public int rightBotton { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public Block(int lx, int wy)
    {
        x = lx; y = wy;
        top = ConstNum.WALL;
        bottom = ConstNum.WALL;
        left = ConstNum.WALL;
        right = ConstNum.WALL;
        block = ConstNum.WALL;
        leftTop = ConstNum.WALL;
        rightTop = ConstNum.WALL;
        leftBottom = ConstNum.WALL;
        rightBotton = ConstNum.WALL;
    }
    public Block() { }

}
