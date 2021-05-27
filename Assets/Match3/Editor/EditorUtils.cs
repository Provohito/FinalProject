using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

[InitializeOnLoad]
public class EditorUtils
{
    static string keyRestoreScenePath; // путь к сцене, которая была открыта
    static string keyNeedRestore; // флаг, который сигнализирует, что нужно запустить изначальную сцену
    static EditorUtils()
    {
        EditorApplication.update += Update; // подписание на Update

        keyRestoreScenePath = Application.dataPath + "restoreScenePath";
        keyNeedRestore = Application.dataPath + "needRestore";
    }

    static void Update() // Отслеживание состояния редактора
    {
        if(!EditorApplication.isPlaying && 
           !EditorApplication.isPlayingOrWillChangePlaymode && 
           PlayerPrefs.GetInt(keyNeedRestore) == 1)
        {
            PlayerPrefs.SetInt(keyNeedRestore, 0);
            EditorSceneManager.OpenScene(PlayerPrefs.GetString(keyRestoreScenePath));
        }
    }
    [MenuItem("Template/Run Game")]
    public static void RunGame()
    {
        if (!EditorApplication.isPlaying)
        {
            PlayerPrefs.SetString(keyRestoreScenePath, EditorSceneManager.GetActiveScene().path);
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
            PlayerPrefs.SetInt(keyNeedRestore, 1);
            EditorApplication.isPlaying = true;
        }
    }
}
