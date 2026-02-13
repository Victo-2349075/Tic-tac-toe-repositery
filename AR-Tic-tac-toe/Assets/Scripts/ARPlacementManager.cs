using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
//https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/api/UnityEngine.XR.ARSubsystems.TrackableType.html
/// <summary>
/// Gère le placement du plateau Tic-Tac-Toe en réalité augmentée.
/// Effectue un raycast AR sur les plans détectés et instancie le board.
/// </summary>
public class ARPlacementManager : MonoBehaviour
{
    public GameObject boardPrefab;

    private ARRaycastManager raycastManager;
    private GameObject spawnedBoard;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    /// <summary>
    /// Récupère le composant ARRaycastManager au démarrage.
    /// </summary>
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }
    /// <summary>
    /// Vérifie si l'utilisateur touche l'écran et tente
    /// de placer le plateau sur un plan détecté.
    /// </summary>
    void Update()
    {
        // Si le plateau est déjà placé, on ne fait plus de raycast
        if (spawnedBoard != null)
            return;
        // Détecte un touch (mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Effectue un raycast AR depuis la position du touch et détecte un plan valide dans l’environnement.
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                spawnedBoard = Instantiate(boardPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
    /// <summary>
    /// Supprime le plateau actuellement placé
    /// et permet un nouveau placement.
    /// </summary>
    public void ResetPlacement()
    {
        if (spawnedBoard != null)
        {
            Destroy(spawnedBoard);
        }

        spawnedBoard = null;
    }
}
