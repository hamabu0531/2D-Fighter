using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonDB : MonoBehaviour
{
    public Dictionary<string, AttackInfo> jsonData; // JSONÇÃì¸óÕÉfÅ[É^

    void Awake()
    {
        LoadJson();
    }

    private void LoadJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "AttackData.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            AttackData data = JsonUtility.FromJson<AttackData>(json);
            jsonData = new Dictionary<string, AttackInfo>();
            foreach (var attackWrapper in data.attacks)
            {
                jsonData[attackWrapper.key] = attackWrapper.attackInfo; // é´èëÇ…í«â¡
            }
        }
        else
        {
            Debug.LogError("Cannot load AttackData!");
        }
    }
}
