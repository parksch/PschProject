using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

#if UNITY_EDITOR
public class JsonLoader : EditorWindow
{
    string jsonDatapath = "Assets/JsonFiles";
    string scriptableDataPath = "Assets/Resources/Scriptable";
    string scriptableFunctionPath = "Assets/Scripts/Scriptable/Function";
    string scriptableScriptPath = "Assets/Scripts/Scriptable";

    bool isRunning = false;

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
            GUI.enabled = false;
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
        if (!Directory.Exists(scriptableScriptPath))
        {
            Directory.CreateDirectory(scriptableScriptPath);
        }

        if (!Directory.Exists(scriptableFunctionPath))
        {
            Directory.CreateDirectory(scriptableFunctionPath);
        }

        try
        {
            DirectoryInfo info = new DirectoryInfo(jsonDatapath);
            string[] assetPaths = Directory.GetFiles(jsonDatapath, "*.json");

            foreach (FileInfo file in info.GetFiles("*.json"))
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                string path = Path.Combine(jsonDatapath, file.Name);
                TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                JObject jObject = (JObject)(JArray.Parse(textAsset.text))[0];

                string classString = GenerateClassFromJson(jObject, fileName);
                File.WriteAllText(scriptableScriptPath + "/" + fileName + ".cs", classString);

                string partialFunction = GenerateClassFromJson(jObject, fileName, 0, true);
                File.WriteAllText(scriptableFunctionPath + "/" + "Partial" + fileName + ".cs", partialFunction);
            }

            EditorUtility.DisplayDialog("결과", "Json To Class 변환 성공", "확인");
            GUI.enabled = true;
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("결과", "Json To Class 변환 실패 \n" + e.Message, "확인");
            GUI.enabled = true;
        }
    }

    string GenerateClassFromJson(JObject jsonObject, string className,int tapCount = 0,bool isFunction = false)
    {
        className = char.ToUpper(className[0]) + className.Substring(1);
        StringBuilder stringBuilder = new StringBuilder();
        List<JProperty> properties = new List<JProperty>();

        if (tapCount == 0)
        {
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("//using symbol");
            stringBuilder.AppendLine("namespace JsonClass");
            stringBuilder.AppendLine("{");
        }

        if (!isFunction)
        {
            stringBuilder.AppendLine("    [System.Serializable]");
        }

        stringBuilder.AppendLine("    public partial class " + className);
        stringBuilder.AppendLine("    {");

        foreach (var property in jsonObject.Properties())
        {
            string propertyName = char.ToLower(property.Name[0]) + property.Name.Substring(1);
            string propertyType;

            if (property.Value.Type == JTokenType.Object)
            {
                propertyType = char.ToUpper(property.Name[0]) + property.Name.Substring(1);

                if (!isFunction)
                {
                    stringBuilder.AppendLine($"        public {propertyType} {propertyName};");
                }
                properties.Add(property);
            }
            else if (property.Value.Type == JTokenType.Array && ((JArray)property.Value)[0].Type == JTokenType.Object)
            {
                propertyType = char.ToUpper(property.Name[0]) + property.Name.Substring(1);

                JArray datasArray = (JArray)property.Value;
                if (!isFunction)
                {
                    stringBuilder.AppendLine($"        public List<{propertyType}> {propertyName};");
                }
                properties.Add(property);
            }
            else
            {
                propertyType = GetCSharpTypeFromJson(property.Value);
                if (!isFunction)
                {
                    stringBuilder.AppendLine($"        public {propertyType} {propertyName};");
                }
            }

        }

        if (isFunction)
        {
            stringBuilder.AppendLine("//class symbol");
        }

        stringBuilder.AppendLine("    }");
        stringBuilder.AppendLine();

        foreach (var property in properties)
        {
            string propertyName = char.ToLower(property.Name[0]) + property.Name.Substring(1);
            string propertyType;

            if (property.Value.Type == JTokenType.Object)
            {
                propertyType = char.ToLower(property.Name[0]) + property.Name.Substring(1);
                stringBuilder.Append(GenerateClassFromJson((JObject)property.Value, propertyName,tapCount + 1, isFunction));
            }
            else if (property.Value.Type == JTokenType.Array && ((JArray)property.Value)[0].Type == JTokenType.Object)
            {
                propertyType = char.ToUpper(property.Name[0]) + property.Name.Substring(1);
                JArray datasArray = (JArray)property.Value;

                stringBuilder.Append(GenerateClassFromJson((JObject)datasArray[0], propertyName,tapCount + 1, isFunction));
            }
        }

        if (tapCount == 0)
        {
            stringBuilder.AppendLine("}");
        }

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

        List<Datas.Pair<ClientEnum.Draw, DrawScriptable.Category>> datas = TableManager.Instance.DrawScriptable.Datas;
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