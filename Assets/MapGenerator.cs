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
    public Tile bordaeTile;
    public Tile bordarTile;
    public Tile lateraleTile;
    public Tile lateralrTile;
    public Tile inferioreTile;
    public Tile inferiorrTile;
    public Tile inferiorTile;

    public int espacoEntrePlataformasY = 3;
    public int espacoEntrePlataformasX = 8;

    private MapCell[,] mapa;

    private Dictionary<string, string[]> regras = new Dictionary<string, string[]>();

    void Start()
    {
        // Definindo as regras
        //tilemap.size = new Vector3Int(0, 0, 0);

        regras["<terreno>"] = new string[] { "Floor", "Water", "Ground", "Flower", "BordaE", "BordaR", "LateralE", "LateralR", "InferiorE", "InferiorR", "Inferior" };
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
        LimparTilemap();

        int largura = Random.Range(3, 5)/*5*/; 
        int altura = Random.Range(2, 4)/*4*/; 

        int nlargura = Random.Range(2, 6)/*6*/; 
        int naltura = Random.Range(2, 3)/*3*/;

        int n2largura = Random.Range(2, 5)/*3*/;
        int n2altura = Random.Range(2, 3)/*2*/;

        int proximaPlataformaY = Random.Range(3, 6);//espacoEntrePlataformasY;
        int proximaPlataformaX = Random.Range(8, 12);//espacoEntrePlataformasX;

        mapa = new MapCell[40, 40]; // Inicializa o mapa com a largura e altura desejadas
 
        // Plataforma 0
        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                int escolha = Random.Range(0, 2);

                if (terreno == "Water" || terreno == "Floor" || terreno == "BordaE" || terreno ==  "BordaR" || terreno == "LateralE" || terreno == "LateralR" || terreno == "InferiorE" || terreno == "InferiorR" || terreno == "Inferior" && y != altura-1 || y != 0)
                {
                    if (x == 0 || x == largura-1)
                    {
                        if (x == 0)
                        {
                            terreno = "LateralE";
                        }
                        else
                        {
                            terreno = "LateralR";
                        }

                    }
                    else 
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
                }

                
                if (y == altura-1)
                {
                    if (x == 0 || x == largura-1)
                    {
                        if (x == 0)
                        {
                            terreno = "BordaE";
                        } 
                        else 
                        {
                            terreno = "BordaR";
                        }
               
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Floor";
                        }
                        else
                        {
                            terreno = "Water";
                        }
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno
                    };
                }
                else if (y == 0)
                {
                    if (x == 0 || x == largura - 1)
                    {
                        if (x == 0)
                        {
                            terreno = "InferiorE";
                        }
                        else
                        {
                            terreno = "InferiorR";
                        }

                    }
                    else
                    {
                        terreno = "Inferior";
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno
                    };
                }
            }
        }
        //------------------------------------------
        // Plataforma 0
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
                else if (celula.terreno.Equals("BordaE") && y == altura - 1)
                {
                    tile = bordaeTile;

                }
                else if (celula.terreno.Equals("BordaR") && y == altura - 1)
                {
                    tile = bordarTile;

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    tile = lateraleTile;

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    tile = lateralrTile;

                }
                else if (celula.terreno.Equals("InferiorE") )
                {
                    tile = inferioreTile;

                }
                else if (celula.terreno.Equals("InferiorR") )
                {
                    tile = inferiorrTile;

                }
                else if (celula.terreno.Equals("Inferior") )
                {
                    tile = inferiorTile;

                }

                // Ajuste a posição do Tilemap conforme necessário
                tilemap.SetTile(tilemap.WorldToCell(posicaocelula), tile);
            }
        }
        //------------------------------------------
        proximaPlataformaY = Random.Range(3, 6);//espacoEntrePlataformasY;
        proximaPlataformaX = Random.Range(8, 12);//espacoEntrePlataformasX;
        // Plataforma 1
        for (int y = proximaPlataformaY; y < proximaPlataformaY + naltura; y++)
        {
            for (int x = proximaPlataformaX; x < proximaPlataformaX + nlargura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                int escolha = Random.Range(0, 2);

                if (terreno == "Water" || terreno == "Floor" || terreno == "BordaE" || terreno ==  "BordaR" || terreno == "LateralE" || terreno == "LateralR" || terreno == "InferiorE" || terreno == "InferiorR" || terreno == "Inferior" && y != (proximaPlataformaY + naltura) - 1 || y != proximaPlataformaY)
                {
                    if (x == proximaPlataformaX || x == (proximaPlataformaX + nlargura) -1)
                    {
                        if (x == proximaPlataformaX)
                        {
                            terreno = "LateralE";
                        }
                        else
                        {
                            terreno = "LateralR";
                        }

                    }
                    else 
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
                }

                
                if (y == (proximaPlataformaY + naltura) - 1)
                {
                    if (x == proximaPlataformaX || x == (proximaPlataformaX + nlargura) - 1)
                    {
                        if (x == proximaPlataformaX)
                        {
                            terreno = "BordaE";
                        } 
                        else 
                        {
                            terreno = "BordaR";
                        }
               
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Floor";
                        }
                        else
                        {
                            terreno = "Water";
                        }
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno
                    };
                }
                else if (y == proximaPlataformaY)
                {
                    if (x == proximaPlataformaX || x == (proximaPlataformaX + nlargura) - 1)
                    {
                        if (x == proximaPlataformaX)
                        {
                            terreno = "InferiorE";
                        }
                        else
                        {
                            terreno = "InferiorR";
                        }

                    }
                    else
                    {
                        terreno = "Inferior";
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno
                    };
                }

            }
        }
        //------------------------------------------
        // Plataforma 1
        for (int y = proximaPlataformaY; y < proximaPlataformaY + naltura; y++)
        {
            for (int x = proximaPlataformaX; x < (proximaPlataformaX + nlargura); x++)
            {
                MapCell celula = mapa[x, y];
                Vector3Int posicaocelula = new Vector3Int(x, y, 0); // Ajuste a posição conforme necessário
                Tile tile = null;

                if (celula.terreno.Equals("Floor"))
                {
                    tile = floorTile;

                }
                else if (celula.terreno.Equals("Water"))
                {
                    tile = waterTile;

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    tile = groundTile;

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    tile = flowerTile;

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    tile = bordaeTile;

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    tile = bordarTile;

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    tile = lateraleTile;

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    tile = lateralrTile;

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    tile = inferioreTile;

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    tile = inferiorrTile;

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    tile = inferiorTile;

                }

                // Ajuste a posição do Tilemap conforme necessário
                tilemap.SetTile(tilemap.WorldToCell(posicaocelula), tile);
            }
        }
        //------------------------------------------
        proximaPlataformaY = Random.Range(3, 6);//espacoEntrePlataformasY;
        proximaPlataformaX = Random.Range(8, 12);//espacoEntrePlataformasX;
        // Plataforma 2
        for (int y = (proximaPlataformaY*2); y < (proximaPlataformaY*2)+ n2altura; y++)
        {
            for (int x = (proximaPlataformaX*2); x < (proximaPlataformaX*2) + n2largura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                int escolha = Random.Range(0, 2);

                if (terreno == "Water" || terreno == "Floor" || terreno == "BordaE" || terreno == "BordaR" || terreno == "LateralE" || terreno == "LateralR" || terreno == "InferiorE" || terreno == "InferiorR" || terreno == "Inferior" && y != (proximaPlataformaY * 2) + n2altura - 1 || y != (proximaPlataformaY * 2))
                {
                    if (x == (proximaPlataformaX * 2) || x == (proximaPlataformaX * 2) + n2largura - 1)
                    {
                        if (x == (proximaPlataformaX * 2))
                        {
                            terreno = "LateralE";
                        }
                        else
                        {
                            terreno = "LateralR";
                        }

                    }
                    else
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
                }


                if (y == (proximaPlataformaY * 2) + n2altura - 1)
                {
                    if (x == proximaPlataformaX * 2 || x == (proximaPlataformaX * 2) + n2largura - 1)
                    {
                        if (x == proximaPlataformaX * 2)
                        {
                            terreno = "BordaE";
                        }
                        else
                        {
                            terreno = "BordaR";
                        }

                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Floor";
                        }
                        else
                        {
                            terreno = "Water";
                        }
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno
                    };
                }
                else if (y == proximaPlataformaY *  2)
                {
                    if (x == proximaPlataformaX * 2 || x == (proximaPlataformaX * 2) + n2largura - 1)
                    {
                        if (x == proximaPlataformaX * 2)
                        {
                            terreno = "InferiorE";
                        }
                        else
                        {
                            terreno = "InferiorR";
                        }

                    }
                    else
                    {
                        terreno = "Inferior";
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno
                    };
                }

            }
        }
        //------------------------------------------
        // Plataforma 2
        for (int y = (proximaPlataformaY*2); y < (proximaPlataformaY*2) + n2altura; y++)
        {
            for (int x = proximaPlataformaX * 2; x < (proximaPlataformaX * 2) + n2largura; x++)
            {
                MapCell celula = mapa[x, y];
                Vector3Int posicaocelula = new Vector3Int(x, y, 0); // Ajuste a posição conforme necessário
                Tile tile = null;

                if (celula.terreno.Equals("Floor"))
                {
                    tile = floorTile;

                }
                else if (celula.terreno.Equals("Water"))
                {
                    tile = waterTile;

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    tile = groundTile;

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    tile = flowerTile;

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    tile = bordaeTile;

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    tile = bordarTile;

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    tile = lateraleTile;

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    tile = lateralrTile;

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    tile = inferioreTile;

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    tile = inferiorrTile;

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    tile = inferiorTile;

                }

                // Ajuste a posição do Tilemap conforme necessário
                tilemap.SetTile(tilemap.WorldToCell(posicaocelula), tile);
            }
        }
        //------------------------------------------
    }

    void LimparTilemap()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                tilemap.SetTile(pos, null);
            }
        }
    }

}
