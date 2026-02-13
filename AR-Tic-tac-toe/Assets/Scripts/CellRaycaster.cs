using UnityEngine;
using UnityEngine.InputSystem;
//https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Touch.html
//https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Mouse.html
//https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Camera.ScreenPointToRay.html
/// <summary>
/// Gère la détection des interactions utilisateur (tap et clic).
/// Aussi, cela effectue un raycast 3D depuis la caméra vers la scène.
/// Si une cellule est touchée cela appelle le GameController.
/// </summary>
public class CellRaycaster : MonoBehaviour
{
    public GameController game;
    private Camera cam;
    private ARInputActions inputActions;
    /// <summary>
    /// Initialise la caméra principale et les actions d'input.
    /// </summary>
    void Awake()
    {
        cam = Camera.main;
        inputActions = new ARInputActions();
    }
    /// <summary>
    /// Active les actions d'input et abonne l'événement Tap.
    /// </summary>
    void OnEnable()
    {
        inputActions.Enable();
        inputActions.AR.Tap.performed += OnTap;
    }
    /// <summary>
    /// Désactive les actions d'input et désabonne l'événement Tap.
    /// </summary>
    void OnDisable()
    {
        inputActions.AR.Tap.performed -= OnTap;
        inputActions.Disable();
    }
    /// <summary>
    /// Appelé lorsqu'un tap ou clic est détecté.
    /// Convertit la position écran en rayon 3D et vérifie
    /// si une cellule du plateau est touchée.
    /// </summary>
    /// <param name="ctx">Contexte de l'action Input System.</param>
    void OnTap(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos;
        // Vérifie qu’un écran tactile est disponible avant de lire un input tactile.
        if (Touchscreen.current != null)
            // Récupère la position actuelle du touch principal sur l’écran en coordonnées 2D.
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
      
        else
            // Lit la position écran du curseur via le Input System afin de l'utiliser pour un raycast.
            screenPos = Mouse.current.position.ReadValue();

            // Convertit la position écran 2D en un rayon 3D partant de la caméra vers la scène.
        Ray ray = cam.ScreenPointToRay(screenPos);

        // Vérifie si le rayon 3D entre en collision avec un objet possédant un Collider.
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell != null)
            {
                game.Play(cell);
            }
        }
    }
}
