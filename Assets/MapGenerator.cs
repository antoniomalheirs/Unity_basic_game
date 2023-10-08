using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int espacoEntrePlataformasY = 3;
    public int espacoEntrePlataformasX = 8;

    public GameObject[] prefabArray;
    public GameObject playerPrefab;
    public GameObject prefabDecorativo;

    private MapCell[,] mapa;

    public class MapCell
    {
        public string terreno { get; set; }
        public string terrenoene { get; set; }
        public GameObject objetoInstanciado;
        public GameObject objetoEnemy;

        // Construtor sem parâmetros
        public MapCell()
        {
            terreno = "";
            terrenoene = "";
            objetoInstanciado = null;
        }
    }

    private Dictionary<string, string[]> regras = new Dictionary<string, string[]>();

    void Start()
    {
        regras["<terreno>"] = new string[] { "Floor", "Water", "Ground", "Flower", "BordaE", "BordaR", "LateralE", "LateralR", "InferiorE", "InferiorR", "Inferior", "MontanhaE", "MontanhaR", "Enemy", "PlatE", "PlatR", "PlatM" };
        regras["<mapa>"] = new string[] { "<terreno> " };

        Desenhamapa();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PrintMapa(mapa);
            PoliMapa(mapa);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Desenhamapa();
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

    void PlayerSpawnprimeiraplat(Vector3 position)
    {
        position.y += playerPrefab.transform.localScale.y / 2f;

        Instantiate(playerPrefab, position, Quaternion.identity);
    }

    void Desenhamapa()
    {
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
                string terrenoene = "";
                int escolha = Random.Range(0, 2);
                int escolha2 = Random.Range(0, 2);

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
                            PlayerSpawnprimeiraplat(EncontrarplatPos(x, y));
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

                            if (escolha2 == 0)
                            {
                                terrenoene = "Enemy";
                            }
                        }
                        else
                        {
                            terreno = "Water";

                            if (escolha2 == 0)
                            {
                                terrenoene = "Enemy";
                            }
                        }
                        
                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno,
                        terrenoene = terrenoene
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

                if (celula.terreno.Equals("Floor"))
                {
                    //Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }
                }
                else if (celula.terreno.Equals("Water"))
                {
                    //Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    //Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    //Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    //Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    //Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    //Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    //Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    //Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    //Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    //Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    //Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }
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
                string terrenoene = "";
                int escolha = Random.Range(0, 2);
                int escolha2 = Random.Range(0, 2);

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

                            if (escolha2 == 0)
                            {
                                terrenoene = "Enemy";
                            }
                        }
                        else
                        {
                            terreno = "Water";

                            if (escolha2 == 0)
                            {
                                terrenoene = "Enemy";
                            }
                        }

                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno,
                        terrenoene = terrenoene
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

                if (celula.terreno.Equals("Floor"))
                {
                    //Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }
                }
                else if (celula.terreno.Equals("Water"))
                {
                    //Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    //Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    //Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    //Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    //Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    //Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    //Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    //Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    //Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    //Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    //Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }
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
                string terrenoene = "";
                int escolha = Random.Range(0, 2);
                int escolha2 = Random.Range(0, 2);

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

                            if (escolha2 == 0)
                            {
                                terrenoene = "Enemy";
                            }
                        }
                        else
                        {
                            terreno = "Water";

                            if (escolha2 == 0)
                            {
                                terrenoene = "Enemy";
                            }
                        }

                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno,
                        terrenoene = terrenoene
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

                if (celula.terreno.Equals("Floor"))
                {
                    //Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }
                }
                else if (celula.terreno.Equals("Water"))
                {
                    //Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    //Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    //Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    //Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    //Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    //Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    //Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    //Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    //Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    //Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    //Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }
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
                string terrenoene = "";
                int escolha = Random.Range(0, 2);
                int escolha2 = Random.Range(0, 2);

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
                            terreno = "InferiorE";//-------------
                            mapa[x - 1, y] = new MapCell
                            {
                                terreno = "PlatM"
                            };

                            mapa[x - 2, y] = new MapCell
                            {
                                terreno = "PlatM"
                            };

                            mapa[x - 3, y] = new MapCell
                            {
                                terreno = "PlatE"
                            };

                        }
                        else
                        {
                            terreno = "InferiorR";//-------------


                            mapa[x + 1, y] = new MapCell
                            {
                                terreno = "PlatM"
                            };

                            mapa[x + 2, y] = new MapCell
                            {
                                terreno = "PlatM"
                            };

                            mapa[x + 3, y] = new MapCell
                            {
                                terreno = "PlatR"
                            };
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

                            if (escolha2 == 0)
                            {
                                terrenoene = "Enemy";
                            }
                        }
                        else
                        {
                            terreno = "Water";

                            if (escolha2 == 0)
                            {
                                terrenoene = "Enemy";
                            }
                        }

                    }

                    mapa[x, y] = new MapCell
                    {

                        terreno = terreno,
                        terrenoene = terrenoene
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

                if (celula.terreno.Equals("Floor"))
                {
                    //Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }
                }
                else if (celula.terreno.Equals("Water"))
                {
                    //Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[16], posicaocelula, Quaternion.identity);
                    }

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    //Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    //Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    //Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    //Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    //Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    //Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    //Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    //Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    //Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    //Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    //Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("PlatE"))
                {
                    //Instantiate(prefabArray[17], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[17], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("PlatR"))
                {
                    //Instantiate(prefabArray[18], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[18], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("PlatM"))
                {
                    //Instantiate(prefabArray[19], posicaocelula, Quaternion.identity);
                    celula.objetoInstanciado = Instantiate(prefabArray[19], posicaocelula, Quaternion.identity);

                }
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
                    line += mapa[x, y].terreno+" | "+mapa[x,y].terrenoene + " | ";
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

        int escolha = Random.Range(0, 2);

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

                       
                    }
                    
                    if (mapa[x, y].terreno == "Water")
                    {
                        countW++;
                        Debug.Log(countW.ToString() + "W");

                        
                    }
                    
                    if (mapa[x, y].terreno == "MontanhaE")
                    {
                        if (mapa[x + 1, y + 1].terreno == "MontanhaE")
                        {
                            

                            GameObject objetoNaPosicao = mapa[x + 1, y].objetoInstanciado;
                            
                            Destroy(objetoNaPosicao);
                            
                            mapa[x + 1, y].terreno = "MontagemME";
                            //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                            mapa[x + 1, y].objetoInstanciado = Instantiate(prefabArray[13], new Vector3Int(x + 1, y, 0), Quaternion.identity);

                        }
                        
                    }
                    else if (mapa[x, y].terreno == "MontanhaR")
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

                    if (mapa[x, y].terreno == "InferiorE")
                    {
                        if (mapa[x, y + 1].terreno == "MontanhaE")
                        {

                            if (mapa[x - 1, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x - 1, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 1, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 1, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x - 1, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x - 1, y].terreno == "PlatE")
                            {

                                GameObject objetoNaPosicao = mapa[x - 1, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 1, y].terreno = "PlatE";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 1, y].objetoInstanciado = Instantiate(prefabArray[17], new Vector3Int(x - 1, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoE: " + mapa[x, y].terreno);
                            }
                            
                            if (mapa[x - 2, y].terreno == "PlatE")
                            {

                                GameObject objetoNaPosicao = mapa[x - 2, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 2, y].terreno = "PlatE";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 2, y].objetoInstanciado = Instantiate(prefabArray[17], new Vector3Int(x - 2, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoE: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x - 2, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x - 2, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 2, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 2, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x - 2, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }

                            if (mapa[x - 3, y].terreno == "PlatE")
                            {

                                GameObject objetoNaPosicao = mapa[x - 3, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 3, y].terreno = "PlatE";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 3, y].objetoInstanciado = Instantiate(prefabArray[17], new Vector3Int(x - 3, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoE: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x - 3, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x - 3, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 3, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 3, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x - 3, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }

                        }

                    }
                    else if (mapa[x, y].terreno == "InferiorR")
                    {
                        if (mapa[x, y + 1].terreno == "MontanhaR")
                        {

                            if (mapa[x + 1, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x + 1, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 1, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 1, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x + 1, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x + 1, y].terreno == "PlatR")
                            {

                                GameObject objetoNaPosicao = mapa[x + 1, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 1, y].terreno = "PlatR";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 1, y].objetoInstanciado = Instantiate(prefabArray[18], new Vector3Int(x + 1, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoR: " + mapa[x, y].terreno);
                            }
                            
                            if (mapa[x + 2, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x + 2, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 2, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 2, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x + 2, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x + 2, y].terreno == "PlatR")
                            {

                                GameObject objetoNaPosicao = mapa[x + 2, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 2, y].terreno = "PlatR";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 2, y].objetoInstanciado = Instantiate(prefabArray[18], new Vector3Int(x + 2, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoR: " + mapa[x, y].terreno);
                            }

                            if (mapa[x + 3, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x + 3, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 3, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 3, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x + 3, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x + 3, y].terreno == "PlatR")
                            {

                                GameObject objetoNaPosicao = mapa[x + 3, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 3, y].terreno = "PlatR";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 3, y].objetoInstanciado = Instantiate(prefabArray[18], new Vector3Int(x + 3, y, 0), Quaternion.identity);
                                Debug.Log("x: " + x + ", y: " + y + ", TerrenoR: " + mapa[x, y].terreno);
                            }
                        }
                    }
                }
            }
        }
    }

    void LimparPrefabs()
    {
        GameObject[] objetosDestrutiveis = GameObject.FindGameObjectsWithTag("Floor");
        GameObject[] objetosDestrutiveis2 = GameObject.FindGameObjectsWithTag("Water");
        GameObject[] objetosDestrutiveis3 = GameObject.FindGameObjectsWithTag("EnemyDECEASED");
        GameObject[] objetosDestrutiveis4 = GameObject.FindGameObjectsWithTag("EnemyMUMMY");

        foreach (GameObject objeto in objetosDestrutiveis)
        {
            Destroy(objeto);
        }

        foreach (GameObject objeto in objetosDestrutiveis2)
        {
            Destroy(objeto);
        }

        foreach (GameObject objeto in objetosDestrutiveis3)
        {
            Destroy(objeto);
        }

        foreach (GameObject objeto in objetosDestrutiveis4)
        {
            Destroy(objeto);
        }
    }
}



