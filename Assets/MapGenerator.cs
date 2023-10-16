using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
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

    void Plat0 (int largura, int altura, MapCell[,] mapa)
    {
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
        //---------------------------------------------//---------------------------------------------//---------------------------------------------//---------------------------------------------//
        //---------------------------------------------//---------------------------------------------//---------------------------------------------//---------------------------------------------//
        //---------------------------------------------//---------------------------------------------//---------------------------------------------//---------------------------------------------//
        //---------------------------------------------//---------------------------------------------//---------------------------------------------//---------------------------------------------//
        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                MapCell celula = mapa[x, y];
                Vector3Int posicaocelula = new Vector3Int(x, y, 0); // Ajuste a posição conforme necessário

                if (celula.terreno.Equals("Floor"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }
                }
                else if (celula.terreno.Equals("Water"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);

                }
            }
        }
    }

    void Plat0(int largura, int altura, MapCell[,] mapa, int startX, int startY)
    {
        for (int y = startY; y < startY + altura; y++)
        {
            for (int x = startX; x < startX + largura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                string terrenoene = "";
                int escolha = Random.Range(0, 2);
                int escolha2 = Random.Range(0, 2);

                if (terreno == "Water" || terreno == "Floor" || terreno == "BordaE" || terreno == "BordaR" || terreno == "LateralE" || terreno == "LateralR" || terreno == "InferiorE" || terreno == "InferiorR" || terreno == "Inferior" && y != altura - 1 || y != 0)
                {
                    if (x == startX || x == startX + largura - 1)
                    {
                        if (x == startX)
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
                    if (x == startX || x == startX + largura - 1)
                    {
                        if (x == startX)
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
                else if (y == 0)
                {
                    if (x == startX || x == startX + largura - 1)
                    {
                        if (x == startX)
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
        //---------------------------------------------//---------------------------------------------//---------------------------------------------//---------------------------------------------//
        //---------------------------------------------//---------------------------------------------//---------------------------------------------//---------------------------------------------//
        //---------------------------------------------//---------------------------------------------//---------------------------------------------//---------------------------------------------//
        //---------------------------------------------//---------------------------------------------//---------------------------------------------//---------------------------------------------//
        for (int y = startY; y < startY + altura; y++)
        {
            for (int x = startX; x < startX + largura; x++)
            {
                MapCell celula = mapa[x, y];
                Vector3Int posicaocelula = new Vector3Int(x, y, 0); // Ajuste a posição conforme necessário

                if (celula.terreno.Equals("Floor"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[0], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }
                }
                else if (celula.terreno.Equals("Water"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[1], posicaocelula, Quaternion.identity);

                    if (celula.terrenoene.Equals("Enemy"))
                    {
                        posicaocelula = new Vector3Int(x, y + 1, 0);
                        celula.objetoEnemy = Instantiate(prefabArray[15], posicaocelula, Quaternion.identity);
                    }

                }
                else if (celula.terreno.Equals("Ground"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[2], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Flower"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[3], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaE"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[4], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("BordaR"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[5], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralE"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[6], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("LateralR"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[7], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorE"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[8], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("InferiorR"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[10], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("Inferior"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[9], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaE"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[11], posicaocelula, Quaternion.identity);

                }
                else if (celula.terreno.Equals("MontanhaR"))
                {
                    celula.objetoInstanciado = Instantiate(prefabArray[12], posicaocelula, Quaternion.identity);
                }
            }
        }
    }

    void PlatP(int larguraa, int altura, MapCell[,] mapa, int startX, int startY, int escolhe)
    {
        int largura = larguraa /2;
        int alturaa = altura / 2;

        for (int y = startY; y < startY + alturaa; y++)
        {
            for (int x = startX; x < startX + largura; x++)
            {
                string terreno = ExpansorRegra("<terreno>");
                string terrenoene = "";
                int escolha = Random.Range(0, 2);
                int escolha2 = Random.Range(0, 2);
                terreno = "Empty";

                if (y == startY)
                {
                    if (x == startX || x == startX + largura -1)
                    {
                        if (x == startX)
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
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
                        }
                    }
                }
                else if (y == startY + 1)
                {
                    if (x == startX + 1 || x == startX + largura - 2)
                    {
                        if (x == startX + 1)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }
                    }
                    else if (x == startX || x == startX + largura - 1)
                    {
                        terreno = "Empty";
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
                        }
                    }
                    
                }
                else if (y == startY + 2)
                {
                    if (x == startX + 2 || x == startX + largura - 3)
                    {
                        if (x == startX + 2)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }
                    }
                    else if (x == startX + 1 || x == startX + largura - 2)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX || x == startX + largura - 1)
                    {
                        terreno = "Empty";
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
                        }
                    }
                }
                else if (y == startY + 3)
                {
                    if (x == startX + 3 || x == startX + largura - 4)
                    {
                        if (x == startX + 3)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }
                    }
                    else if (x == startX + 3 || x == startX + largura - 4)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 2 || x == startX + largura - 3)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 1 || x == startX + largura - 2)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX || x == startX + largura - 1)
                    {
                        terreno = "Empty";
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
                        }
                    }
                }
                else if (y == startY + 4)
                {
                    if (x == startX + 4 || x == startX + largura - 5)
                    {
                        if (x == startX + 4)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }
                    }
                    else if (x == startX + 4 || x == startX + largura - 5)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 3 || x == startX + largura - 4)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 2 || x == startX + largura - 3)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 1 || x == startX + largura - 2)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX || x == startX + largura - 1)
                    {
                        terreno = "Empty";
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
                        }
                    }
                }
                else if (y == startY + 5)
                {
                    if (x == startX + 5 || x == startX + largura - 6)
                    {
                        if (x == startX + 5)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }
                    }
                    else if (x == startX + 5 || x == startX + largura - 6)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 4 || x == startX + largura - 5)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 3 || x == startX + largura - 4)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 2 || x == startX + largura - 3)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 1 || x == startX + largura - 2)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX || x == startX + largura - 1)
                    {
                        terreno = "Empty";
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
                        }
                    }
                }
                else if (y == startY + 6)
                {
                    if (x == startX + 6 || x == startX + largura - 7)
                    {
                        if (x == startX + 6)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }
                    }
                    else if (x == startX + 6 || x == startX + largura - 7)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 5 || x == startX + largura - 6)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 4 || x == startX + largura - 5)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 3 || x == startX + largura - 4)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 2 || x == startX + largura - 3)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 1 || x == startX + largura - 2)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX || x == startX + largura - 1)
                    {
                        terreno = "Empty";
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
                        }
                    }
                }
                else if (y == startY + 7)
                {
                    if (x == startX + 7 || x == startX + largura - 8)
                    {
                        if (x == startX + 7)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }
                    }
                    else if (x == startX + 7 || x == startX + largura - 8)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 6 || x == startX + largura - 7)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 5 || x == startX + largura - 6)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 4 || x == startX + largura - 5)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 3 || x == startX + largura - 4)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 2 || x == startX + largura - 3)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 1 || x == startX + largura - 2)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX || x == startX + largura - 1)
                    {
                        terreno = "Empty";
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
                        }
                    }
                }
                else if (y == startY + 8)
                {
                    if (x == startX + 8 || x == startX + largura - 9)
                    {
                        if (x == startX + 8)
                        {
                            terreno = "MontanhaE";
                        }
                        else
                        {
                            terreno = "MontanhaR";
                        }
                    }
                    else if (x == startX + 8 || x == startX + largura - 9)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 7 || x == startX + largura - 8)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 6 || x == startX + largura - 7)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 5 || x == startX + largura - 6)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 4 || x == startX + largura - 5)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 3 || x == startX + largura - 4)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 2 || x == startX + largura - 3)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX + 1 || x == startX + largura - 2)
                    {
                        terreno = "Empty";
                    }
                    else if (x == startX || x == startX + largura - 1)
                    {
                        terreno = "Empty";
                    }
                    else
                    {
                        if (escolha == 0)
                        {
                            terreno = "Flower";
                        }
                        else
                        {
                            terreno = "Ground";
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

        for (int y = startY; y < startY + alturaa; y++)
        {
            for (int x = startX; x < startX + largura; x++)
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
    }

    void PlatP(int largura, int altura, MapCell[,] mapa, int startX, int startY)
    {
        int larguraa = largura/2;
        int alturaa = altura/2;

        for (int y = startY; y < startY + alturaa; y++)
        {
            string terreno = "Empty";
            int alturaAtual = y - startY;
            Debug.Log(alturaAtual.ToString() + "A");
            
            for (int x = startX; x < startX + larguraa; x++)
            {
                int larguraAtual = x - startX;
                Debug.Log(larguraAtual.ToString() + "L");

                int escolha = Random.Range(0, 2);

                if (y != -1)
                {
                    if (y == startY)
                    {
                        if (x == startX)
                        {
                            terreno = "InferiorE";
                        }
                        else if (x == startX + larguraa - 1)
                        {
                            terreno = "InferiorR";
                        }
                        else
                        {
                            terreno = "Inferior";
                        }
                    }
                    else if (y == (startY + 1))
                    {
                        if (x == startX)
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa - 1))
                        {
                            terreno = "MontanhaR";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                    else if (y == (startY + 2))
                    {
                        if (x == (startX + 1))
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa - 2))
                        {
                            terreno = "MontanhaR";
                        }
                        else if (x < (startX + 1) || x > (startX + larguraa - 2))
                        {
                            terreno = "Empty";


                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                    else if (y == (startY + 3))
                    {
                        if (x == (startX + 2))
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa - 3))
                        {
                            terreno = "MontanhaR";
                        }
                        else if (x < (startX + 2) || x > (startX + larguraa - 3))
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                    else if (y == (startY + 4))
                    {
                        if (x == (startX + 3))
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa - 4))
                        {
                            terreno = "MontanhaR";
                        }
                        else if (x < (startX + 3) || x > (startX + larguraa - 4))
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                    else if (y == (startY + 5))
                    {
                        if (x == (startX + 4))
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa - 5))
                        {
                            terreno = "MontanhaR";
                        }
                        else if (x < (startX + 4) || x > (startX + larguraa - 5))
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                    else if (y == (startY + 6))
                    {
                        if (x == (startX + 5))
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa - 6))
                        {
                            terreno = "MontanhaR";
                        }
                        else if (x < (startX + 5) || x > (startX + larguraa - 6))
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                }
                else
                {
                    terreno = "Empty";
                }

                mapa[x, y] = new MapCell
                {
                    terreno = terreno,
                    terrenoene = ""
                };
            }
        }

        for (int y = startY; y < startY + alturaa; y++)
        {
            for (int x = startX; x < startX + larguraa; x++)
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
    }

    void PlatPL(int largura, int altura, MapCell[,] mapa, int startX, int startY)
    {
        int larguraa = largura / 2;
        int alturaa = altura / 2;

        for (int y = startY; y < startY + alturaa; y++)
        {
            string terreno = "Empty";
            int alturaAtual = y - startY;
            Debug.Log(alturaAtual.ToString() + "A");

            for (int x = startX; x < startX + larguraa; x++)
            {
                int larguraAtual = x - startX;
                Debug.Log(larguraAtual.ToString() + "L");

                int escolha = Random.Range(0, 2);

                if (y != -1)
                {
                    if (y == startY)
                    {
                        if (x == startX)
                        {
                            terreno = "InferiorE";
                        }
                        else if (x == startX + larguraa - 1)
                        {
                            terreno = "InferiorR";
                        }
                        else
                        {
                            terreno = "Inferior";
                        }
                    }
                    else if (y == (startY + 1))
                    {
                        if (x == startX)
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa-1))
                        {
                            terreno = "LateralR";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                    else if (y == (startY + 2))
                    {
                        if (x == (startX + 3))
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa-1))
                        {
                            terreno = "LateralR";
                        }
                        else if (x < (startX + 3))
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                    else if (y == (startY + 3))
                    {
                        if (x == (startX + 7))
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa-1))
                        {
                            terreno = "LateralR";
                        }
                        else if (x < (startX + 7))
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                    else if (y == (startY + 4))
                    {
                        if (x == (startX + 10))
                        {
                            terreno = "MontanhaE";
                        }
                        else if (x == (startX + larguraa - 1))
                        {
                            terreno = "LateralR";
                        }
                        else if (x < (startX + 10))
                        {
                            terreno = "Empty";
                        }
                        else
                        {
                            if (escolha == 0)
                            {
                                terreno = "Flower";
                            }
                            else
                            {
                                terreno = "Ground";
                            }
                        }
                    }
                }
                else
                {
                    terreno = "Empty";
                }

                mapa[x, y] = new MapCell
                {
                    terreno = terreno,
                    terrenoene = ""
                };
            }
        }

        for (int y = startY; y < startY + alturaa; y++)
        {
            for (int x = startX; x < startX + larguraa; x++)
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

        int n3largura = Random.Range(8, 30);
        int n3altura = Random.Range(8, 26);

        int n4largura = Random.Range(20, 40);
        int n4altura = Random.Range(20, 46);

        mapa = new MapCell[100, 100];

        for (int y = 0; y < mapa.GetLength(0); y++)
        {
            for (int x = 0; x < mapa.GetLength(1); x++)
            {
                mapa[x, y] = new MapCell
                {
                    terreno = "Empty",
                    terrenoene = "Empty"
                };
            }
        }

        Plat0(largura, altura, mapa);
        Plat0(nlargura, naltura, mapa, 5, 0);
        Plat0(n2largura, n2altura, mapa, 12, 0);
        PlatP(n3largura, n3altura, mapa, 22, 0);
        PlatPL(n4largura, n4altura, mapa, 36, 0);
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

        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                if (mapa[x, y] != null)
                {
                    if (mapa[x, y].terreno == "Floor")
                    {
                    }
                    
                    if (mapa[x, y].terreno == "Water")
                    {
                    }

                    if (mapa[x, y].terreno == "Ground" || mapa[x, y].terreno == "Flower")
                    {
                        if (mapa[x + 1, y].terreno != "Empty")
                        {
                            if (mapa[x - 1, y].terreno != "Empty")
                            {
                                if (mapa[x, y - 1].terreno != "Empty")
                                {
                                    if (mapa[x, y + 1].terreno == "Empty")
                                    {

                                        GameObject objetoNaPosicao = mapa[x, y].objetoInstanciado;

                                        Destroy(objetoNaPosicao);


                                        mapa[x, y].objetoInstanciado = null;

                                        if (escolha == 0)
                                        {
                                            mapa[x, y].terreno = "Floor";
                                            mapa[x, y].objetoInstanciado = Instantiate(prefabArray[0], new Vector3(x, y, 0), Quaternion.identity);
                                        }
                                        else
                                        {
                                            mapa[x, y].terreno = "Water";
                                            mapa[x, y].objetoInstanciado = Instantiate(prefabArray[1], new Vector3(x, y, 0), Quaternion.identity);
                                        }

                                    }
                                }
                            }
                        }
                    }

                    if (mapa[x, y].terreno == "MontanhaE")
                    {
                        if (mapa[x + 1, y + 1].terreno == "MontanhaE")
                        {
                            if (mapa[x+1, y].terreno != "Empty" && mapa[x+1, y].terreno != "MontanhaR")
                            {
                                GameObject objetoNaPosicao = mapa[x + 1, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 1, y].terreno = "MontagemME";
                                //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 1, y].objetoInstanciado = Instantiate(prefabArray[13], new Vector3Int(x + 1, y, 0), Quaternion.identity);
                            }
                        }

                        if (mapa[x+1, y].terreno == "Empty")
                        {
                            if (mapa[x - 1, y].terreno == "Empty")
                            {
                                GameObject objetoNaPosicao = mapa[x, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x, y].terreno = "Empty";
                                mapa[x, y].objetoInstanciado = null;
                            }
                        }

                        if (mapa[x,y+1].terreno == "MontanhaR")
                        {
                            GameObject objetoNaPosicao = mapa[x, y+1].objetoInstanciado;

                            Destroy(objetoNaPosicao);

                            mapa[x, y+1].terreno = "Empty";
                            //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                            mapa[x , y+1].objetoInstanciado = null;
                        }
                        else if (mapa[x, y + 2].terreno == "MontanhaR")
                        {
                            GameObject objetoNaPosicao = mapa[x, y + 2].objetoInstanciado;

                            Destroy(objetoNaPosicao);

                            mapa[x, y + 2].terreno = "Empty";
                            //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                            mapa[x, y + 2].objetoInstanciado = null;
                        }
                        else if (mapa[x, y + 3].terreno == "MontanhaR")
                        {
                            GameObject objetoNaPosicao = mapa[x, y + 3].objetoInstanciado;

                            Destroy(objetoNaPosicao);

                            mapa[x, y + 3].terreno = "Empty";
                            //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                            mapa[x, y + 3].objetoInstanciado = null;
                        }
                        else if (mapa[x, y + 4].terreno == "MontanhaR")
                        {
                            GameObject objetoNaPosicao = mapa[x, y + 4].objetoInstanciado;

                            Destroy(objetoNaPosicao);

                            mapa[x, y + 4].terreno = "Empty";
                            //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                            mapa[x, y + 4].objetoInstanciado = null;
                        }
                        else if (mapa[x, y + 5].terreno == "MontanhaR")
                        {
                            GameObject objetoNaPosicao = mapa[x, y + 5].objetoInstanciado;

                            Destroy(objetoNaPosicao);

                            mapa[x, y + 5].terreno = "Empty";
                            //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                            mapa[x, y + 5].objetoInstanciado = null;
                        }


                    }
                    else if (mapa[x, y].terreno == "MontanhaR")
                    {
                        if (mapa[x - 1, y + 1].terreno == "MontanhaR")
                        {
                            if (mapa[x-1, y].terreno != "Empty" && mapa[x - 1, y].terreno != "MontanhaE")
                            {
                                GameObject objetoNaPosicao = mapa[x - 1, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 1, y].terreno = "MontagemMR";
                                //Instantiate(prefabArray[14], new Vector3Int(x-1, y, 0), Quaternion.identity);
                                mapa[x - 1, y].objetoInstanciado = Instantiate(prefabArray[14], new Vector3Int(x - 1, y, 0), Quaternion.identity);

                            }
                        }

                        if (mapa[x + 1, y].terreno == "Empty")
                        {
                            if (mapa[x - 1, y].terreno == "Empty")
                            {
                                GameObject objetoNaPosicao = mapa[x, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x, y].terreno = "Empty";
                                mapa[x, y].objetoInstanciado = null;
                            }
                        }

                        if (mapa[x, y + 1].terreno == "MontanhaE")
                        {
                            GameObject objetoNaPosicao = mapa[x, y + 1].objetoInstanciado;

                            Destroy(objetoNaPosicao);

                            mapa[x, y + 1].terreno = "Empty";
                            //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                            mapa[x, y + 1].objetoInstanciado = null;
                        }
                        else if (mapa[x, y + 3].terreno == "MontanhaE")
                        {
                            GameObject objetoNaPosicao = mapa[x, y + 3].objetoInstanciado;

                            Destroy(objetoNaPosicao);

                            mapa[x, y + 3].terreno = "Empty";
                            //Instantiate(prefabArray[13], new Vector3Int(x+1, y, 0), Quaternion.identity);
                            mapa[x, y + 3].objetoInstanciado = null;
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
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x - 1, y].terreno == "PlatE")
                            {

                                GameObject objetoNaPosicao = mapa[x - 1, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 1, y].terreno = "PlatE";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 1, y].objetoInstanciado = Instantiate(prefabArray[17], new Vector3Int(x - 1, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoE: " + mapa[x, y].terreno);
                            }
                            
                            if (mapa[x - 2, y].terreno == "PlatE")
                            {

                                GameObject objetoNaPosicao = mapa[x - 2, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 2, y].terreno = "PlatE";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 2, y].objetoInstanciado = Instantiate(prefabArray[17], new Vector3Int(x - 2, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoE: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x - 2, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x - 2, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 2, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 2, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x - 2, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }

                            if (mapa[x - 3, y].terreno == "PlatE")
                            {

                                GameObject objetoNaPosicao = mapa[x - 3, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 3, y].terreno = "PlatE";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 3, y].objetoInstanciado = Instantiate(prefabArray[17], new Vector3Int(x - 3, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoE: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x - 3, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x - 3, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x - 3, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x - 3, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x - 3, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
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
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x + 1, y].terreno == "PlatR")
                            {

                                GameObject objetoNaPosicao = mapa[x + 1, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 1, y].terreno = "PlatR";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 1, y].objetoInstanciado = Instantiate(prefabArray[18], new Vector3Int(x + 1, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoR: " + mapa[x, y].terreno);
                            }
                            
                            if (mapa[x + 2, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x + 2, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 2, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 2, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x + 2, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x + 2, y].terreno == "PlatR")
                            {

                                GameObject objetoNaPosicao = mapa[x + 2, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 2, y].terreno = "PlatR";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 2, y].objetoInstanciado = Instantiate(prefabArray[18], new Vector3Int(x + 2, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoR: " + mapa[x, y].terreno);
                            }

                            if (mapa[x + 3, y].terreno == "PlatM")
                            {

                                GameObject objetoNaPosicao = mapa[x + 3, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 3, y].terreno = "PlatM";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 3, y].objetoInstanciado = Instantiate(prefabArray[19], new Vector3Int(x + 3, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoM: " + mapa[x, y].terreno);
                            }
                            else if (mapa[x + 3, y].terreno == "PlatR")
                            {

                                GameObject objetoNaPosicao = mapa[x + 3, y].objetoInstanciado;

                                Destroy(objetoNaPosicao);

                                mapa[x + 3, y].terreno = "PlatR";
                                //Instantiate(prefabArray[17], new Vector3Int(x+1, y, 0), Quaternion.identity);
                                mapa[x + 3, y].objetoInstanciado = Instantiate(prefabArray[18], new Vector3Int(x + 3, y, 0), Quaternion.identity);
                                //Debug.Log("x: " + x + ", y: " + y + ", TerrenoR: " + mapa[x, y].terreno);
                            }
                        }
                    }
                }
                else
                {
                    mapa[x, y] = new MapCell
                    {
                        terreno = "Empty",
                        terrenoene = "Empty"
                    };
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



