using UnityEngine;

public class BillboardYAxis : MonoBehaviour
{
    public Transform target; // La cible � laquelle le billboard fait face (par d�faut, la cam�ra principale)

    private void Start()
    {
        // Si aucune cible n'est sp�cifi�e, utilise la cam�ra principale
        if (target == null)
        {
            target = Camera.main.transform;
        }

        // Assure que l'objet est align� verticalement au d�part
        AlignVertically();
    }

    private void Update()
    {
        if (target == null) return;

        // Calcule la direction vers la cible
        Vector3 direction = target.position - transform.position;

        // Neutralise les composantes X et Z pour que la rotation se fasse uniquement sur l'axe Y
        direction.y = 0;

        // V�rifie que la direction n'est pas nulle avant d'appliquer la rotation
        if (direction.sqrMagnitude > 0.001f)
        {
            // Applique une rotation pour que l'objet fasse face � la cible
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void AlignVertically()
    {
        // Conserve la position actuelle mais r�initialise les angles X et Z pour aligner l'objet verticalement
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, 0);
    }
}
