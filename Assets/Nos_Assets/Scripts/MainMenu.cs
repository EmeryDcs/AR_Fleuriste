using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO; // Pour manipuler les fichiers locaux

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; // Écran de chargement (optionnel)
    [SerializeField] private UnityEngine.UI.Slider progressBar; // Barre de progression (optionnel)
    [SerializeField] private GameObject choiceMenu; // Menu pour choisir "Continuer" ou "Recommencer"

    private string flowersSavePath;
    private bool bouquetExists = false;

    private void Start()
    {
        // Définir le chemin du fichier de sauvegarde du bouquet
        flowersSavePath = Application.persistentDataPath + "/flowers.json";

        // Vérifier si un bouquet a déjà été sauvegardé
        bouquetExists = File.Exists(flowersSavePath);
    }


    public void LoadSceneAsync(string sceneName)
    {
        if (bouquetExists)
        {
            // Afficher un menu de choix si un bouquet existe
            if (choiceMenu != null)
            {
                choiceMenu.SetActive(true);
                return;
            }
        }

        // Si aucun bouquet n'existe ou si l'utilisateur décide de continuer sans choix
        StartLoadingScene(sceneName);
    }


    public void ContinueBouquet(string sceneName)
    {
        // Fermer le menu de choix (si affiché)
        if (choiceMenu != null)
        {
            choiceMenu.SetActive(false);
        }

        // Charger la scène avec les données actuelles
        StartLoadingScene(sceneName);
    }


    public void RestartBouquet(string sceneName)
    {
        // Supprimer le fichier de sauvegarde existant
        if (File.Exists(flowersSavePath))
        {
            File.Delete(flowersSavePath);
            Debug.Log("Fichier de bouquet supprimé, nouveau départ.");
        }

        // Fermer le menu de choix (si affiché)
        if (choiceMenu != null)
        {
            choiceMenu.SetActive(false);
        }

        // Charger la scène avec un bouquet vierge
        StartLoadingScene(sceneName);
    }

    private void StartLoadingScene(string sceneName)
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        StartCoroutine(LoadSceneCoroutine(sceneName));
    }


    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (progressBar != null)
            {
                progressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            }

            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
