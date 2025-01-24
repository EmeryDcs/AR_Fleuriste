using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; // Écran de chargement (optionnel)
    [SerializeField] private UnityEngine.UI.Slider progressBar; // Barre de progression (optionnel)
    [SerializeField] private GameObject choiceMenu; // Menu pour choisir "Continuer" ou "Recommencer"

    private string selectedScene; // Stocke la scène actuellement sélectionnée
    private string flowersSavePath; // Chemin vers le fichier de sauvegarde du bouquet
    private bool bouquetExists = false; // Indique si un bouquet existe déjà

    private void Start()
    {
        // Définir le chemin du fichier de sauvegarde du bouquet
        flowersSavePath = Application.persistentDataPath + "/flowers.json";

        // Vérifier si un bouquet a déjà été sauvegardé
        bouquetExists = File.Exists(flowersSavePath);
    }

    // Fonction appelée par différents boutons pour charger une scène
    public void LoadSceneAsync(string sceneName)
    {
        selectedScene = sceneName; // Enregistrer la scène sélectionnée

        if (bouquetExists)
        {
            // Afficher un menu de choix si un bouquet existe
            if (choiceMenu != null)
            {
                choiceMenu.SetActive(true);
                return;
            }
        }

        // Si aucun bouquet n'existe, charger directement la scène
        StartLoadingScene(selectedScene);
    }

    // Continuer avec le bouquet existant
    public void ContinueBouquet()
    {
        // Fermer le menu de choix (si affiché)
        if (choiceMenu != null)
        {
            choiceMenu.SetActive(false);
        }

        // Charger la scène précédemment sélectionnée
        StartLoadingScene(selectedScene);
    }

    // Recommencer avec un nouveau bouquet
    public void RestartBouquet()
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

        // Charger la scène précédemment sélectionnée
        StartLoadingScene(selectedScene);
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
