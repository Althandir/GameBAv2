using UnityEngine;

public class LanguageTemplateCreator : MonoBehaviour
{
    [ContextMenu("CreateJson")]
    void CreateText()
    {
        Language.Container container = new Language.Container();
        string json = JsonUtility.ToJson(container);
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/language_template.json", json);
        Debug.Log("New language template crated.");
    }

    private void Awake()
    {
        Destroy(this);
    }
}