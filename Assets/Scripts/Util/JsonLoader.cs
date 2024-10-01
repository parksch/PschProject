using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using static DrawScriptable;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;

#if UNITY_EDITOR
public class JsonLoader : EditorWindow
{
    string jsonDatapath = "Assets/JsonFiles";
    string scriptableDataPath = "Assets/Resources/Scriptable";
    string scriptableScriptPath = "Assets/Scripts/Scriptable";

    private (string handle, string type)[] typeRules =
    {
        ("n_", "int"),
        ("s_", "string"),
        ("f_", "float"),
        ("l_", "long"),
        ("b_", "bool"),
        ("e_", "enum")
    };

    [MenuItem("Tools/JsonLoader")]
    public static void ShowMyEditor()
    {
        EditorWindow window = GetWindow<JsonLoader>();
        window.titleContent = new GUIContent("JsonLoader");
        window.minSize = new Vector2(500, 500);
    }


    public void OnGUI()
    {
        EditorGUILayout.LabelField("Json 파일을 Scriptable 파일로 변경");

        if (GUILayout.Button("Conversion"))
        {
            OnClickJsonConversion();
        }

        EditorGUILayout.LabelField("Test : Scriptable 파일을 Json 파일로 변경");

        if (GUILayout.Button("Conversion"))
        {
            OnClickScriptableConversion();
        }
    }

    void OnClickJsonConversion()
    {
        if (!Directory.Exists(jsonDatapath))
        {
            Directory.CreateDirectory(jsonDatapath);
        }

        DirectoryInfo info = new DirectoryInfo(jsonDatapath);
        string[] assetPaths = Directory.GetFiles(jsonDatapath, "*.json");

        foreach (FileInfo file in info.GetFiles("*.json"))
        {
            string fileName = Path.GetFileNameWithoutExtension(file.Name);
            string path = Path.Combine(jsonDatapath, file.Name);
            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            JObject jObject = (JObject)(JArray.Parse(textAsset.text))[0];
            string classString = GenerateClassFromJson(jObject, fileName);
            Debug.Log(classString);
            File.WriteAllText(scriptableScriptPath + "/" + fileName + ".cs", classString);
        }

        EditorUtility.DisplayDialog("결과", "메세지","확인");
    }

    string GenerateClassFromJson(JObject jsonObject, string className,int tapCount = 0)
    {
        className = className.First().ToString().ToUpper() + className.Substring(1);
        StringBuilder stringBuilder = new StringBuilder();

        if (tapCount == 0)
        {
            stringBuilder.AppendLine("using System.Collections.Generic;");
        }

        stringBuilder.AppendLine(StringTap("[System.Serializable]",tapCount));
        stringBuilder.AppendLine(StringTap("public class " + className,tapCount));
        stringBuilder.AppendLine(StringTap("{",tapCount));

        foreach (var property in jsonObject.Properties())
        {
            string propertyName = property.Name.First().ToString().ToLower() + property.Name.Substring(1);
            string propertyType;

            if (property.Value.Type == JTokenType.Object)
            {
                propertyType = propertyName.First().ToString().ToUpper() + propertyName.Substring(1);
                

                stringBuilder.AppendLine(StringTap($"    public {propertyType} {propertyName};",tapCount));
                stringBuilder.AppendLine(GenerateClassFromJson((JObject)property.Value, propertyName,tapCount + 1));
            }
            else if (property.Value.Type == JTokenType.Array)
            {
                propertyType = propertyName.First().ToString().ToUpper() + propertyName.Substring(1);

                JArray datasArray = (JArray)property.Value;
                stringBuilder.AppendLine(StringTap($"    public List<{propertyType}> {propertyName};",tapCount));
                stringBuilder.AppendLine(GenerateClassFromJson((JObject)datasArray[0], propertyName,tapCount + 1));
            }
            else
            {
                propertyType = GetCSharpTypeFromJson(property.Value);
                stringBuilder.AppendLine(StringTap($"    public {propertyType} {propertyName};",tapCount));
            }

        }

        stringBuilder.AppendLine(StringTap("}",tapCount));

        return stringBuilder.ToString();
    }

    string StringTap(string str,int count)
    {
        string result = string.Empty;

        for (int i = 0; i < count * 4; i++)
        {
            result += " ";
        }

        result += str;
        
        return result;
    }

    private string GetCSharpTypeFromJson(JToken token)
    {
        switch (token.Type)
        {
            case JTokenType.Integer:
                return "int";
            case JTokenType.Float:
                return "float";
            case JTokenType.String:
                return "string";
            case JTokenType.Boolean:
                return "bool";
            case JTokenType.Array:
                return "List<" + GetCSharpTypeFromJson(((JArray)token)[0]) + ">";
            case JTokenType.Object:
                return "object"; 
            default:
                return "string";
        }
    }

    void OnClickScriptableConversion()
    {

        //if (!Directory.Exists(scriptableDataPath))
        //{
        //    Directory.CreateDirectory(scriptableDataPath);
        //}

        //string scriptablePath = scriptableDataPath;

        List<Datas.Pair<ClientEnum.Draw, Category>> datas = TableManager.Instance.DrawScriptable.Datas;
        string json = JsonConvert.SerializeObject(datas);
        string path = string.Format("{0}/{1}", jsonDatapath, "Draw.json");

        if (!File.Exists(path))
        {
            string emptyJson = "{}";
            string directoryPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(path, emptyJson);
        }

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        List<OptionScriptable.Data> optionDatas = TableManager.Instance.OptionScriptable.Datas;
        json = JsonConvert.SerializeObject(optionDatas);
        path = string.Format("{0}/{1}", jsonDatapath, "Option.json");

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        EditorUtility.DisplayDialog("결과", "메세지", "확인");
    }
}
#endif