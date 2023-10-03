using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarCenario : MonoBehaviour
{
    [Header("Cenario")]
    [SerializeField] private int _width, _height;
    [Range(0, 25)] [SerializeField] private int _SpawnFloor;
    [Range(0, 25)] [SerializeField] private int _SpawnEnemys;
    [Range(0, 25)] [SerializeField] private int _SpawnStairs;
    [Range(0, 25)] [SerializeField] private int _SpawnWaterfloor;

    [SerializeField] private int _polishCount;
    [SerializeField] private string _seed;
    [SerializeField] private bool _useRandomSeed;

    private int[,] map;
    // Start is called before the first frame update
    void Start()
    {
        GeraMapa();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GeraMapa();
        }
    }

    private void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (map[x, y] == 1) Gizmos.color = Color.black;
                    if (map[x, y] == 2) Gizmos.color = Color.red;
                    if (map[x, y] == 3) Gizmos.color = Color.yellow;
                    if (map[x, y] == 4) Gizmos.color = Color.blue;
                    Vector3 pos = new Vector3(x - _width / 2, y - _height / 2, 0);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }

    void PreencheMapa()
    {
        if (_useRandomSeed)
        {
            _seed = Time.time.ToString();
        }
        System.Random rand = new System.Random(_seed.GetHashCode());

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int floor = rand.Next(0, 25 + _SpawnFloor);
                int enemy = rand.Next(0, 25 + _SpawnEnemys); ;
                int stair = rand.Next(0, 25 + _SpawnStairs); ;
                int waterfloor = rand.Next(0, 25 + _SpawnWaterfloor); ;

                int maior = floor;
                int id = 1;

                if (enemy > maior)
                {
                    id = 2;
                }

                if (stair > maior)
                {
                    id = 3;
                }

                if (waterfloor > maior)
                {
                    id = 4;
                }

                map[x, y] = id;
            }
        }
    }

    void PolimentoMapa()
    {
        int[,] newMap = (int[,])map.Clone();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int floorCount = EncontraVizinho(x, y, 1);
                int stairCount = EncontraVizinho(x, y, 3);
                int waterfloorCount = EncontraVizinho(x, y, 4);

                if (floorCount > 5)
                {
                    newMap[x, y] = 1;
                }

                if (newMap[x, y] == 4)
                {
                    PintaVizinho(x, y, 1, newMap);
                }
            }
        }

        map = newMap;
    }

    int EncontraVizinho(int x, int y, int vizinho)
    {
        int count = 0;

        for (int vizinhoX = x - 1; vizinhoX <= x + 1; vizinhoX++)
        {
            for (int vizinhoY = y - 1; vizinhoY <= y + 1; vizinhoY++)
            {
                if (vizinhoX == x && vizinhoY == y) continue;

                if (vizinhoX < 0 || vizinhoX >= _width || vizinhoY < 0 || vizinhoY >= _height)
                {
                    continue;
                }
                else if (map[vizinhoX, vizinhoY] == vizinho)
                {
                    count++;
                }
            }
        }
        return count;
    }

    void PintaVizinho(int x, int y, int id, int[,] tempmap)
    {
        for (int vizinhoX = x - 1; vizinhoX <= x + 1; vizinhoX++)
        {
            for (int vizinhoY = y - 1; vizinhoY <= y + 1; vizinhoY++)
            {
                if (vizinhoX == x && vizinhoY == y) continue;

                if (vizinhoX < 0 || vizinhoX >= _width || vizinhoY < 0 || vizinhoY >= _height)
                {
                    continue;
                }
                tempmap[vizinhoX, vizinhoY] = id;
            }
        }
    }

    void GeraMapa()
    {
        map = new int[_width, _height];
        PreencheMapa();

        for (int i = 0; i < _polishCount; i++)
        {
            PolimentoMapa();
        }
    }
}