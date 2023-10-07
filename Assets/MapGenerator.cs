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

    public GameObject[] prefabArray;
    public GameObject playerPrefab;
    public GameObject prefabDecorativo;

    private MapCell[,] mapa;

    public class MapCell
    {
        public string terreno { get; set; }
        public GameObject objetoInstanciado;

        // Construtor sem parâmetros
        public MapCell()
        {
            terreno = "";
            objetoInstanciado = null;
        }
    }

    private Dictionary<string, string[]> regras = new Dictionary<string, string[]>();

    void Start()
    {
        // Definindo as regras
        //tilemap.size = new Vector3Int(0, 0, 0);

        regras["<terreno>"] = new string[] { "Floor", "Water", "Ground", "Flower", "BordaE", "BordaR", "LateralE", "LateralR", "InferiorE", "InferiorR", "Inferior", "MontanhaE", "MontanhaR", "PlatE", "PlatR" };
        regras["<mapa>"] = new string[] { "<terreno> " };

        Desenhamapa();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            Desenhamapa();
            PoliMapa(mapa);
            
        }
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

    Vector3 EncontrarplatPos(int x, int y)
    {
        Vector3 plataformaSpawnPosition = new Vector3(x, y, 0f);
        return plataformaSpawnPosition;
    }

    void playerSpawnprimeiraplat(Vector3 position)
    {
        position.y += playerPrefab.transform.localScale.y / 2f;

        Instantiate(playerPrefab, position, Quaternion.identity);
    }

    void Desenhamapa()
    {
        LimparTilemap();
        LimparPrefabs();

        int largura = Random.Range(3, 5);
        int altura = Random.Range(2, 4);

        int nlargura = Random.Range(2, 6);
        int naltura = Random.Range(2, 3);

        int n2largura = Random.Range(2, 5);
        int n2altura = Random.Range(2, 3);

        int n3largura = Random.Range(6, 14);
        int n3altura = Random.Range(6, 12);

        int proximaPlataformaY = Random.Range(3, 6);
        int proximaPlataformaX = Random.Range(8, 12);

        mapa = new MapCell[80, 80];

        // Plataforma 0
        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                int escolha = Random.Range(0, 2);

                if (terreno == "Water" || terreno == "Floor" || terreno == "BordaE" || terreno == "BordaR" || terreno == "LateralE" || terreno == "LateralR" || terreno == "InferiorE" || terreno == "InferiorR" || terreno == "Inferior" && y != altura - 1 || y != 0)
                {
                    if (x == 0 || x == largura - 1)
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


                if (y == altura - 1)
                {
                    if (x == 0 || x == largura - 1)
                    {
                        if (x == 0)
                        {
                            terreno = "BordaE";
                            playerSpawnprimeiraplat(EncontrarplatPos(x, y));
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

                if (celula.terreno.Equals("Floor"))
                {
                    tile = floorTile;
                    //Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Water"))
                {
                    tile = waterTile;
                    Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    tile = groundTile;
                    //Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    tile = flowerTile;
                    //Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    tile = bordaeTile;
                    //Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    tile = bordarTile;
                    //Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    tile = lateraleTile;
                    //Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    tile = lateralrTile;
                    Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    tile = inferioreTile;
                    //Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    tile = inferiorrTile;
                    //Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    tile = inferiorTile;
                    //Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    tile = inferiorrTile;
                    //Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    tile = inferiorTile;
                    //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }

                // Ajuste a posição do Tilemap conforme necessário
                tilemap.SetTile(tilemap.WorldToCell(posicaocelula), tile);
            }
        }
        //------------------------------------------
        proximaPlataformaY = Random.Range(3, 6);
        proximaPlataformaX = Random.Range(8, 12);
        // Plataforma 1
        for (int y = proximaPlataformaY; y < proximaPlataformaY + naltura; y++)
        {
            for (int x = proximaPlataformaX; x < proximaPlataformaX + nlargura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                int escolha = Random.Range(0, 2);

                if (terreno == "Water" || terreno == "Floor" || terreno == "BordaE" || terreno == "BordaR" || terreno == "LateralE" || terreno == "LateralR" || terreno == "InferiorE" || terreno == "InferiorR" || terreno == "Inferior" && y != (proximaPlataformaY + naltura) - 1 || y != proximaPlataformaY)
                {
                    if (x == proximaPlataformaX || x == (proximaPlataformaX + nlargura) - 1)
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
                    //Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Water"))
                {
                    tile = waterTile;
                    Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    tile = groundTile;
                    //Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    tile = flowerTile;
                    //Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    tile = bordaeTile;
                    //Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    tile = bordarTile;
                    //Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    tile = lateraleTile;
                    //Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    tile = lateralrTile;
                    Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    tile = inferioreTile;
                    //Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    tile = inferiorrTile;
                    //Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    tile = inferiorTile;
                    //Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    tile = inferiorrTile;
                    //Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    tile = inferiorTile;
                    //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }

                // Ajuste a posição do Tilemap conforme necessário
                tilemap.SetTile(tilemap.WorldToCell(posicaocelula), tile);
            }
        }
        //------------------------------------------
        proximaPlataformaY = Random.Range(3, 6);
        proximaPlataformaX = Random.Range(8, 12);
        // Plataforma 2
        for (int y = (proximaPlataformaY * 2); y < (proximaPlataformaY * 2) + n2altura; y++)
        {
            for (int x = (proximaPlataformaX * 2); x < (proximaPlataformaX * 2) + n2largura; x++)
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
                else if (y == proximaPlataformaY * 2)
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
        for (int y = (proximaPlataformaY * 2); y < (proximaPlataformaY * 2) + n2altura; y++)
        {
            for (int x = proximaPlataformaX * 2; x < (proximaPlataformaX * 2) + n2largura; x++)
            {
                MapCell celula = mapa[x, y];
                Vector3Int posicaocelula = new Vector3Int(x, y, 0); // Ajuste a posição conforme necessário
                Tile tile = null;

                if (celula.terreno.Equals("Floor"))
                {
                    tile = floorTile;
                    //Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Water"))
                {
                    tile = waterTile;
                    Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    tile = groundTile;
                    //Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    tile = flowerTile;
                    //Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    tile = bordaeTile;
                    //Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    tile = bordarTile;
                    //Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    tile = lateraleTile;
                    //Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    tile = lateralrTile;
                    Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    tile = inferioreTile;
                    //Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    tile = inferiorrTile;
                    //Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    tile = inferiorTile;
                    //Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    tile = inferiorrTile;
                    //Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    tile = inferiorTile;
                    //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }

                // Ajuste a posição do Tilemap conforme necessário
                tilemap.SetTile(tilemap.WorldToCell(posicaocelula), tile);
            }
        }
        //------------------------------------------
        proximaPlataformaY = Random.Range(3, 4);
        proximaPlataformaX = Random.Range(8, 12);
        // Plataforma 3
        for (int y = (proximaPlataformaY * 3); y < (proximaPlataformaY * 3) + n3altura; y++)
        {
            for (int x = (proximaPlataformaX * 3); x < (proximaPlataformaX * 3) + n3largura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                int escolha = Random.Range(0, 2);

                if (terreno == "Water" || terreno == "Floor" || terreno == "BordaE" || terreno == "BordaR" || terreno == "LateralE" || terreno == "LateralR" || terreno == "InferiorE" || terreno == "InferiorR" || terreno == "Inferior" || terreno == "MontanhaE" || terreno == "MontanhaR" && y != (proximaPlataformaY * 3) + n3altura - 1 || y != (proximaPlataformaY * 3))
                {
                    if (x == (proximaPlataformaX * 3) || x == (proximaPlataformaX * 3) + n3largura - 1)
                    {
                        if (x == (proximaPlataformaX * 3))
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        }

                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        }
                    }

                    mapa[x, y] = new MapCell
                    {
                        terreno = terreno
                    };
                }


                if (y == (proximaPlataformaY * 3) + n3altura - 1)
                {
                    if (x == proximaPlataformaX * 3 || x == (proximaPlataformaX * 3) + n3largura - 1)
                    {
                        if (x == proximaPlataformaX * 3)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }

                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        }
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno
                    };
                }
                else if (y == proximaPlataformaY * 3)
                {
                    if (x == proximaPlataformaX * 3 || x == (proximaPlataformaX * 3) + n3largura - 1)
                    {
                        if (x == proximaPlataformaX * 3)
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
                else if (y == (proximaPlataformaY * 3) + 1)
                {
                    if (x == proximaPlataformaX * 3 || x == (proximaPlataformaX * 3) + n3largura - 1)
                    {
                        if (x == proximaPlataformaX * 3)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
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
                else if (y == (proximaPlataformaY * 3) + 2)
                {
                    if (x == proximaPlataformaX * 3 || x == (proximaPlataformaX * 3) + n3largura - 1)
                    {
                        if (x == proximaPlataformaX * 3)
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        }

                    }
                    else if (x == (proximaPlataformaX * 3) + 1 || x == (proximaPlataformaX * 3) + n3largura - 2)
                    {
                        if (x == (proximaPlataformaX * 3) + 1)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
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
                else if (y == (proximaPlataformaY * 3) + 3)
                {
                    if (x == proximaPlataformaX * 3 || x == (proximaPlataformaX * 3) + n3largura - 1)
                    {
                        if (x == proximaPlataformaX * 3)
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        }

                    }
                    else if (x == (proximaPlataformaX * 3) + 1 || x == (proximaPlataformaX * 3) + n3largura - 2)
                    {
                        if (x == (proximaPlataformaX * 3) + 1)
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        }
                    }
                    else if (x == (proximaPlataformaX * 3) + 2 || x == (proximaPlataformaX * 3) + n3largura - 3)
                    {
                        if (x == (proximaPlataformaX * 3) + 2)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        };
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
                else if (y == (proximaPlataformaY * 3) + 4)
                {
                    if (x == proximaPlataformaX * 3 || x == (proximaPlataformaX * 3) + n3largura - 1)
                    {
                        if (x == proximaPlataformaX * 3)
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        }

                    }
                    else if (x == (proximaPlataformaX * 3) + 1 || x == (proximaPlataformaX * 3) + n3largura - 2)
                    {
                        if (x == (proximaPlataformaX * 3) + 1)
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        }
                    }
                    else if (x == (proximaPlataformaX * 3) + 2 || x == (proximaPlataformaX * 3) + n3largura - 3)
                    {
                        if (x == (proximaPlataformaX * 3) + 2)
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            terreno = "Empty";
                        };
                    }
                    else if (x == (proximaPlataformaX * 3) + 3 || x == (proximaPlataformaX * 3) + n3largura - 4)
                    {
                        if (x == (proximaPlataformaX * 3) + 3)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        };
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

            }
        }
        //------------------------------------------
        // Plataforma 3
        for (int y = (proximaPlataformaY * 3); y < (proximaPlataformaY * 3) + n3altura; y++)
        {
            for (int x = proximaPlataformaX * 3; x < (proximaPlataformaX * 3) + n3largura; x++)
            {
                MapCell celula = mapa[x, y];
                Vector3Int posicaocelula = new Vector3Int(x, y, 0); // Ajuste a posição conforme necessário
                Tile tile = null;

                if (celula.terreno.Equals("Floor"))
                {
                    tile = floorTile;
                    //Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Water"))
                {
                    tile = waterTile;
                    Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    tile = groundTile;
                    //Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    tile = flowerTile;
                    //Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    tile = bordaeTile;
                    //Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    tile = bordarTile;
                    //Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    tile = lateraleTile;
                    //Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    tile = lateralrTile;
                    Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    tile = inferioreTile;
                    //Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    tile = inferiorrTile;
                    //Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    tile = inferiorTile;
                    //Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    tile = inferiorrTile;
                    //Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    tile = inferiorTile;
                    //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }

                // Ajuste a posição do Tilemap conforme necessário
                //tilemap.SetTile(tilemap.WorldToCell(posicaocelula), tile);
            }
        }
        //------------------------------------------
    }

    void PrintMapa(MapCell[,] mapa)
    {
        int altura = mapa.GetLength(0);
        int largura = mapa.GetLength(1);

        for (int y = 0; y < altura; y++)
        {
            string line = " ";
            for (int x = 0; x < largura; x++)
            {
                // Verifique se a célula não é nula antes de adicionar ao texto
                if (mapa[x, y] != null)
                {
                    line += mapa[x, y].terreno + " ";
                }
                else
                {
                    line += "* ";
                }
            }
            Debug.Log(line);
        }
    }

    void PoliMapa(MapCell[,] mapa)
    {
        int altura = mapa.GetLength(0);
        int largura = mapa.GetLength(1);

        int count = 0;
        int countW = 0;
        int countM = 0;

        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                if (mapa[x, y] != null)
                {
                    if (mapa[x, y].terreno == "Floor")
                    {
                        count++;
                        Debug.Log(count.ToString() + "F");
                        //Instantiate(prefabArray[1], new Vector3(x, y, 0f), Quaternion.identity);
                    }
                    if (mapa[x, y].terreno == "Water")
                    {
                        countW++;
                        Debug.Log(countW.ToString() + "W");
                        //Instantiate(prefabArray[1], new Vector3(x, y, 0f), Quaternion.identity);
                    }
                    if (mapa[x, y].terreno == "MontanhaE")
                    {
                        if (mapa[x + 1, y + 1].terreno == "MontanhaE")
                        {
                            if (mapa[x + 1, y + 1].terreno == "MontanhaE")
                            {
                                GameObject objetoNaPosicao = mapa[x + 1, y].objetoInstanciado;
                                Destroy(objetoNaPosicao);

                                mapa[x + 1, y].terreno = "MontagemME";
                                //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 1,y].objetoInstanciado = Instantiate(prefabArray[13], new Vector3Int(x + 1, y, 0), Quaternion.identity);
                            }
                        }
                    }
                    if (mapa[x, y].terreno == "MontanhaR")
                    {
                        if (mapa[x - 1, y + 1].terreno == "MontanhaR")
                        {
                            if (mapa[x - 1, y + 1].terreno == "MontanhaR")
                            {
                                GameObject objetoNaPosicao = mapa[x - 1, y].objetoInstanciado;
                                Destroy(objetoNaPosicao);

                                mapa[x - 1, y].terreno = "MontagemMR";
                                //Instantiate(prefabArray[14], new Vector3Int(x-1, y, 0), Quaternion.identity);
                                mapa[x - 1, y].objetoInstanciado = Instantiate(prefabArray[14], new Vector3Int(x - 1, y, 0), Quaternion.identity);
                            }

                        }

                    }
                }
                
            }
        }
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

    void LimparPrefabs()
    {
        GameObject[] objetosDestrutiveis = GameObject.FindGameObjectsWithTag("Floor");
        GameObject[] objetosDestrutiveis2 = GameObject.FindGameObjectsWithTag("Water");


        foreach (GameObject objeto in objetosDestrutiveis)
        {
            Destroy(objeto);
        }
        foreach (GameObject objeto in objetosDestrutiveis2)
        {
            Destroy(objeto);
        }

    }


}



