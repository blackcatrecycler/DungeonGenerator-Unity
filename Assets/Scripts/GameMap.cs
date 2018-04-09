using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap
{
    int roomCount;
    System.Random rd;
    int width;//地图宽度 x轴
    int length;//地图长度 y轴
    public List<List<Block>> gmap;
    Rooms room;
    public List<Rooms> rooms;

    /// <summary>
    /// initializes a Gmap by using width,length and roomCount;
    /// </summary>
    /// <param name="wy">width</param>
    /// <param name="lx">length</param>
    /// <param name="rC">roomCount</param>
    public GameMap(int wy, int lx, int rC)
    {
        width = wy;
        length = lx;
        roomCount = rC;
        rd = new System.Random();
        room = new Rooms(0, 0, lx, wy);
        rooms = new List<Rooms>();
        gmap = new List<List<Block>>();
        Init();
    }

    void Init()
    {
        for (int i = 0; i < width; i++)
        {
            List<Block> tempDY = new List<Block>();
            for (int j = 0; j < length; j++)
            {
                tempDY.Add(new Block(j, i));
            }
            gmap.Add(tempDY);
        }//初始化地板
        RandomRooms();//初始化房间
        HuntAndKill();//生成完美迷宫
    }

    void SetRoom(Rooms s)
    {
        rooms.Add(s);
        for (int i = s.posy; i < (s.posy + s.height); i++)
            for (int j = s.posx; j < (s.posx + s.length); j++)
            {
                gmap[i][j].block = ConstNum.ROOM;
                if (i != s.posy) gmap[i][j].top = ConstNum.ROOM;
                if (i != (s.posy + s.height)) gmap[i][j].bottom = ConstNum.ROOM;
                if (j != s.posx) gmap[i][j].left = ConstNum.ROOM;
                if (j != (s.posx + s.length)) gmap[i][j].right = ConstNum.ROOM;
                if ((gmap[i][j].top == ConstNum.ROOM) && (gmap[i][j].left == ConstNum.ROOM)) gmap[i][j].leftTop = ConstNum.ROOM;
                if ((gmap[i][j].top == ConstNum.ROOM) && (gmap[i][j].left == ConstNum.ROOM)) gmap[i][j].leftTop = ConstNum.ROOM;
                if ((gmap[i][j].top == ConstNum.ROOM) && (gmap[i][j].left == ConstNum.ROOM)) gmap[i][j].leftTop = ConstNum.ROOM;
                if ((gmap[i][j].top == ConstNum.ROOM) && (gmap[i][j].left == ConstNum.ROOM)) gmap[i][j].leftTop = ConstNum.ROOM;
            }

    }

    /// <summary>
    /// Check this point is legal
    /// </summary>
    /// <param name="x">xPos</param>
    /// <param name="y">yPos</param>
    /// <returns></returns>
    bool CheckPos(int x, int y)
    {
        if (y < width && y >= 0 && x < length && x >= 0) return true;
        else return false;
    }

    /// <summary>
    /// Generate a perfect maze in map
    /// </summary>
    void HuntAndKill()
    {
        bool flag = true;
        List<Block> usefulBlocks = new List<Block>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (gmap[i][j].block == ConstNum.WALL)
                    usefulBlocks.Add(gmap[i][j]);
            }
        }

        while (usefulBlocks.Count > 0)
        {
            Block block = new Block();
            flag = true;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (!flag) break;
                    if (gmap[i][j].block == ConstNum.WALL)
                    {
                        if (i != width - 1) //下搜索
                            if (gmap[i + 1][j].block == ConstNum.ROAD)
                            {
                                block = gmap[i][j];
                                block.bottom = ConstNum.ROAD;
                                block.block = ConstNum.ROAD;
                                gmap[i + 1][j].top = ConstNum.ROAD;
                                flag = false;
                                break;
                            }
                        if (j != length - 1)//左搜索
                            if (gmap[i][j + 1].block == ConstNum.ROAD)
                            {
                                block = gmap[i][j];
                                block.right = ConstNum.ROAD;
                                block.block = ConstNum.ROAD;
                                gmap[i][j + 1].left = ConstNum.ROAD;
                                flag = false;
                                break;
                            }
                        if (j != 0)//右搜索
                            if (gmap[i][j - 1].block == ConstNum.ROAD)
                            {
                                block = gmap[i][j];
                                block.left = ConstNum.ROAD;
                                block.block = ConstNum.ROAD;
                                gmap[i][j - 1].right = ConstNum.ROAD;
                                flag = false;
                                break;
                            }
                        if (i != 0)//上搜索
                            if (gmap[i - 1][j].block == ConstNum.ROAD)
                            {
                                block = gmap[i][j];
                                block.top = ConstNum.ROAD;
                                block.block = ConstNum.ROAD;
                                gmap[i - 1][j].bottom = ConstNum.ROAD;
                                flag = false;
                                break;
                            }

                    }
                    if (!flag) break;
                }
            }
            if (flag)
            {
                block = RandomTool<Block>.RollItem(ref usefulBlocks, rd);
            }
            else usefulBlocks.Remove(block);
            //找一个格子进行搜索，如果找不到现有连接那么随机掷一个
            flag = false;
            while (!flag)
            {
                List<Block> bk = new List<Block>();
                if (block.y != 0)
                    if (gmap[block.y - 1][block.x].block == ConstNum.WALL) bk.Add(gmap[block.y - 1][block.x]);
                if (block.x != 0)
                    if (gmap[block.y][block.x - 1].block == ConstNum.WALL) bk.Add(gmap[block.y][block.x - 1]);
                if (block.y != width - 1)
                    if (gmap[block.y + 1][block.x].block == ConstNum.WALL) bk.Add(gmap[block.y + 1][block.x]);
                if (block.x != length - 1)
                    if (gmap[block.y][block.x + 1].block == ConstNum.WALL) bk.Add(gmap[block.y][block.x + 1]);
                if (bk.Count != 0)
                {
                    Block temp = RandomTool<Block>.RollItem(ref bk, rd);
                    usefulBlocks.Remove(temp);
                    gmap[temp.y][temp.x].block = ConstNum.ROAD;
                    if (temp.x == block.x)
                    {
                        if (temp.y > block.y)
                        {
                            gmap[temp.y][temp.x].top = ConstNum.ROAD;
                            gmap[block.y][block.x].bottom = ConstNum.ROAD;

                        }
                        else if (temp.y < block.y)
                        {
                            gmap[temp.y][temp.x].bottom = ConstNum.ROAD;
                            gmap[block.y][block.x].top = ConstNum.ROAD;
                        }
                    }
                    else if (temp.x > block.x)
                    {
                        gmap[temp.y][temp.x].left = ConstNum.ROAD;
                        gmap[block.y][block.x].right = ConstNum.ROAD;
                    }
                    else if (temp.x < block.x)
                    {
                        gmap[temp.y][temp.x].right = ConstNum.ROAD;
                        gmap[block.y][block.x].left = ConstNum.ROAD;

                    }
                    block = gmap[temp.y][temp.x];


                }
                else flag = true;

            }


        }
    }
    /// <summary>
    /// Check if this room could be placed  
    /// </summary>
    bool CheckRoom(Rooms s)
    {
        if (s.IfContent(room))
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].IfOverLap(s)) return false;
            }
            SetRoom(s); //设置房间
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Roll some rooms
    /// </summary>
    void RandomRooms()
    {
        for (int i = 0; i < roomCount; i++)
        {
            int temp1 = 0, temp2 = 0, temp3 = 0, temp4 = 0;
            RandomTool.Roll(ref temp1, ref temp2, width, 3, 6, rd);
            RandomTool.Roll(ref temp3, ref temp4, length, 3, 6, rd);
            CheckRoom(new Rooms(temp3, temp1, temp4, temp2));
        }
    }



}
