using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    [Header("尺寸信息")]
    [Range(10, 100)]
    public int width;
    [Range(10, 100)]
    public int height;
    [Range(10, 100)]
    public int roomCount;

    [Header("生成方式")]
    public bool ifRoll = false;
    [Range(0, 999)]
    public int seed;

    Transform board;

    public GameMap gmap;

    public GameObject[] mapTiles;


    void Init()
    {
        gmap = new GameMap(height, width, roomCount);
        board = new GameObject("board").transform;
    }

    void BoardDraw()
    {
        int allx = width * 3/2;
        int ally = height * 3 / 2;
        for (int wx = 0; wx < width; wx++)
        {
            for (int ly = 0; ly < height; ly++)
            {
                int tempx = 3 * (wx+1);
                int tempy = 3 * (ly+1);
                setBoard(gmap.gmap[ly][wx], -allx+tempx, -ally+tempy);
            }
        }
    }

    void setBoard(Block block, int x, int y)
    {
        setObject(x - 1, y - 1, block.leftTop);
        setObject(x, y - 1, block.top);
        setObject(x + 1, y - 1, block.rightTop);

        setObject(x - 1, y, block.left);
        setObject(x, y, block.block);
        setObject(x + 1, y, block.right);

        setObject(x - 1, y + 1, block.leftBottom);
        setObject(x, y + 1, block.bottom);
        setObject(x + 1, y + 1, block.rightBotton);
    }

    void setObject(int x, int y, int m)
    {
        if (m == 0) return;
        else
        {
            GameObject gameObject = mapTiles[m - 1];
            GameObject instance = Instantiate(gameObject, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(board);
        }
    }

    public void BoardSetup()
    {
        Init();
        BoardDraw();
    }

}
