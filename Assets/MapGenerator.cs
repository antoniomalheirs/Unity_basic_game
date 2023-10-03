using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile floorTile;
    public Tile waterTile;
    public Tile groundTile;
    public Tile flowerTile;

    private MapCell[,] mapa;

    private Dictionary<string, string[]> regras = new Dictionary<string, string[]>();

    void Start()
    {
        // Definindo as regras
        //tilemap.size = new Vector3Int(0, 0, 0);

        regras["<terreno>"] = new string[] { "Floor", "Water", "Ground", "Flower" };
        regras["<mapa>"] = new string[] {"<terreno> "};

        Desenhamapa();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Desenhamapa();
        }
    }

    public class MapCell
    {
        public string terreno; // Tipo de terreno ("Floor", "Water", "Ground", etc.)
        public int altura; // Altura da plataforma
    }

    private string ExpansorRegra(string regra)
    {
        if (regras.ContainsKey(regra))
        {
            string[] opcoes = regras[regra];
            string opcaoselecionada = opcoes[Random.Range(0, opcoes.Length)];
            return opcaoselecionada;
        }
        else
        {
            return regra;
        }
    }

    private void Desenhamapa()
    {
        int largura = 8; // Largura da plataforma
        int altura = 3; // Altura da plataforma

        mapa = new MapCell[largura, altura]; // Inicializa o mapa com a largura e altura desejadas

        int index = 0;
        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                int escolha = Random.Range(0, 2);

                if (terreno == "Water" || terreno == "Floor" && y != 2)
                {
                    if (escolha == 0)
                    {
                        terreno = "Ground";
                    }
                    else
                    {
                        terreno = "Flower";
                    }
                }

                mapa[x, y] = new MapCell
                {
                    terreno = terreno
                };

                if (y == altura-1)
                {
                    if (escolha == 0)
                    {
                        terreno = "Floor";
                    }
                    else
                    {
                        terreno = "Water";
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno
                    };
                }

                index++;
            }
        }

        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                MapCell celula = mapa[x, y];
                Vector3Int posicaocelula = new Vector3Int(x, y, 0); // Ajuste a posição conforme necessário
                Tile tile = null;

                if (celula.terreno.Equals("Floor") && y == altura - 1)
                {
                    tile = floorTile;
                    
                }
                else if (celula.terreno.Equals("Water") && y == altura - 1)
                {
                    tile = waterTile;
                    
                }
                else if (celula.terreno.Equals("Ground") && y != altura - 1)
                {
                    tile = groundTile;
                    
                }
                else if (celula.terreno.Equals("Flower") && y != altura - 1)
                {
                    tile = flowerTile;

                }

                // Ajuste a posição do Tilemap conforme necessário
                tilemap.SetTile(tilemap.WorldToCell(posicaocelula), tile);
            }
        }
    }
}
