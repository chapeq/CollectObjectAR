using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ReadCSV : MonoBehaviour
{
    [System.Serializable]
    public class PositionFile
    {
        public int id;
        public Vector3 pos;
    }

    public List<PositionFile> positions = new List<PositionFile>();

    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/StreamingAssets/CustomPositions.csv";
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine("jar:file://" + Application.dataPath + "!/assets/CustomPositions.csv");
#endif
        StartCoroutine(ParseCSV(path));
    }

    IEnumerator ParseCSV(string path)
    {
        string csvtext = "";
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            csvtext = www.downloadHandler.text;
        }

        var lines = csvtext.Split("\n");
        for (int i = 1; i < lines.Length; i++)
        {
            var column = lines[i].Split(new string[] { ";", ",", "\n" }, System.StringSplitOptions.None);
            PositionFile pos = new PositionFile();
            pos.id = int.Parse(column[0]);
            pos.pos = ParseVector3(column[1], column[2], column[3]);
            positions.Add(pos);
        }
    }

    public Vector3 ParseVector3(string x, string y, string z)
    {
        Vector3 position = new Vector3();
        float.TryParse(x, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out position.x);
        float.TryParse(y, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out position.y);
        float.TryParse(z, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out position.z);
        return position;
    }
}
