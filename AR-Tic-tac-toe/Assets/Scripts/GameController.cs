using UnityEngine;
using TMPro;
//https://docs.unity3d.com/ScriptReference/Transform-localPosition.html
//https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Object.FindObjectsByType.html
/// <summary>
/// Gère la logique complète du jeu Tic-Tac-Toe :
/// gestion des tours, placement des symboles,
/// détection de victoire et remise à zéro.
/// </summary>

public class GameController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI instructionsText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;

    public GameObject xPrefab;
    public GameObject oPrefab;

    private bool isXTurn = true;
    private int[] board = new int[9]; // 0 vide, 1 X, 2 O
    private int moveCount = 0;
    /// <summary>
    /// Initialise l'interface utilisateur et cache le panneau de fin de partie.
    /// </summary>
    void Start()
    {
        UpdateUI();
        // Vérifie que le panneau de fin de partie est bien assigné avant de l’utiliser.
        if (gameOverPanel != null)
        gameOverPanel.SetActive(false);
    }
    /// <summary>
    /// Tente de jouer un coup sur une cellule donnée.
    /// Instancie le symbole correspondant et vérifie l'état de la partie.
    /// </summary>
    /// <param name="cell">Cellule sélectionnée par le joueur.</param>
    public void Play(Cell cell)
    {
        if (cell.occupied) return;
        // Choix du prefab selon le joueur
        GameObject prefab = isXTurn ? xPrefab : oPrefab;
        GameObject symbol = Instantiate(prefab, cell.transform);
        // Placement/rotation/scale du symbole (spécifique à mes prefabs). Aussi, j'ai beaucoup bougé les préfab dans l'éditeur de Unity et j'ai juste récris ce qui avais a la position pour que sa coordonne avec mes cubes. 
        if (isXTurn)
        {
            symbol.transform.localPosition = new Vector3(-1.424f, -3.081f, -1.777f);
            symbol.transform.localRotation = Quaternion.identity;
            symbol.transform.localScale = Vector3.one * 1f;
        }
        //Le tour de O
        else
        {
            symbol.transform.localPosition = new Vector3(0f, 0f, 0.65f);
            symbol.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            symbol.transform.localScale = new Vector3(1f, 0.01f, 1f);
        }

        board[cell.index] = isXTurn ? 1 : 2;
        cell.occupied = true;
        moveCount++;

        // Vérifie si il y a une victoire.
        if (CheckWin())
        {
            // Vérifie que le texte du gagnant est assigné avant d'afficher le joueur victorieux.
            if (winnerText != null)
                winnerText.text = (isXTurn ? "X" : "O") + " a gagné!";
            // Vérifie que le panneau de fin de partie est valide puis l'affiche.
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            return;
        }

        // Vérifie si il y a un match nul
        if (moveCount >= 9)
        {   // Vérifie que le texte du gagnant est assigné avant d'afficher le message de match nul.
            if (winnerText != null)
                winnerText.text = "Match nul!";
            // Vérifie que le panneau de fin de partie existe puis l'affiche.
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            return;
        }

        isXTurn = !isXTurn;
        UpdateUI();
    }
    /// <summary>
    /// Met à jour l'affichage du joueur courant.
    /// </summary>
    void UpdateUI()
    {
        if (currentPlayerText != null)
            currentPlayerText.text = "Tour de " + (isXTurn ? "X" : "O");
    }
    /// <summary>
    /// Vérifie si une combinaison gagnante est présente sur le plateau.
    ///  J'ai demandé a chatgpt de me donner  la logique du jeux tic-tac-toe en c# pour le code et voici ce que j'ai compris pour checkwin et les commentaires dans checkwin cest pour m'aider a bien comprendre ce qu'il ma donné.
    /// </summary>
    /// <returns>True si un joueur a gagné, sinon false.</returns>
    bool CheckWin()
    {
        // Vérifie les 8 combinaisons gagnantes possibles
        int[,] wins =
        {
            {0,1,2},{3,4,5},{6,7,8}, // lignes
            {0,3,6},{1,4,7},{2,5,8},// colonnes
            {0,4,8},{2,4,6}  // diagonales
        };

        for (int i = 0; i < 8; i++)
        {
            int a = wins[i, 0], b = wins[i, 1], c = wins[i, 2];
            // Si la première case n’est pas vide et que les 3 sont égales. tu a gagné.
            if (board[a] != 0 &&
                board[a] == board[b] &&
                board[b] == board[c])
                return true;
        }

        return false;
    }
    /// <summary>
    /// Réinitialise complètement la partie :
    /// supprime les symboles et remet le plateau à zéro.
    /// </summary>
    public void NewGame()
    {
        Cell[] cells = FindObjectsByType<Cell>(FindObjectsSortMode.None);
        // Supprime tous les enfants (X et O instanciés)
        foreach (Cell cell in cells)
        {
            // Parcourt tous les objets enfants attachés à cette cellule.
            foreach (Transform child in cell.transform)
                Destroy(child.gameObject);

            cell.occupied = false;
        }

        board = new int[9];
        moveCount = 0;
        isXTurn = true;
        // Vérifie que le panneau de fin de partie existe avant de l'utiliser.
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateUI();
    }
}
