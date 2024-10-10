#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection;
using JsonClass;
using System;

public class JsonLoader : EditorWindow
{
    static string jsonDatapath = "Assets/JsonFiles";
    static string scriptableDataPath = "Assets/Resources/Scriptable";
    static string scriptableFunctionPath = "Assets/Scripts/Scriptable/Function";
    static string scriptableScriptPath = "Assets/Scripts/Scriptable";
    static bool isButtonEnabled = true;

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
        EditorGUILayout.LabelField("확인 팝업이 안 뜬다면 한 번 더 클릭");
        GUI.enabled = isButtonEnabled;

        if (GUILayout.Button("Conversion"))
        {
            isButtonEnabled = false;
            OnClickJsonConversion();
        }

        EditorGUILayout.LabelField("!에러 주의 개발자 Test 용: Scriptable 파일을 Json으로 만들어주는 프로그램 ");

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

            foreach (FileInfo file in info.GetFiles("*.json"))
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                string path = Path.Combine(jsonDatapath, file.Name);
                TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);

                JToken jsonToken = JToken.Parse(textAsset.text);
                JObject jObject = jsonToken is JArray array ? (JObject)array[0] : (JObject)jsonToken;

                string classString = GenerateClassFromJson(jObject, fileName, jsonToken is JArray);
                File.WriteAllText(scriptableScriptPath + "/" + fileName + "Scriptable.cs", classString);
                AssetDatabase.Refresh();

                if (!File.Exists(scriptableFunctionPath + "/" + "Partial" + fileName + ".cs"))
                {
                    string partialFunction = GenerateClassFromJson(jObject, fileName, jsonToken is JArray , true, true);
                    File.WriteAllText(scriptableFunctionPath + "/" + "Partial" + fileName + ".cs", partialFunction);
                    AssetDatabase.Refresh();
                }
            }
            
            isCompiling = true;
            EditorApplication.update += CheckComp;
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("결과", "Json To Class 변환 실패 \n" + e.Message, "확인");
            isButtonEnabled = true;
        }
    }

    static bool isCompiling = false;

    static void CheckComp()
    {
        if (isCompiling)
        {
            // 컴파일이 완료되었는지 체크
            if (EditorApplication.isCompiling == false)
            {
                DirectoryInfo info = new DirectoryInfo(jsonDatapath);

                foreach (FileInfo file in info.GetFiles("*.json"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.Name);
                    string path = Path.Combine(jsonDatapath, file.Name);
                    TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    JToken jsonToken = JToken.Parse(textAsset.text);

                    CreateScriptableAsset(scriptableDataPath, fileName, jsonToken);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                isButtonEnabled = true;
                isCompiling = false;

                EditorApplication.update -= CheckComp;
                EditorUtility.DisplayDialog("결과", "Json To Class 변환 성공", "확인");
            }
        }
    }

    static void CreateScriptableAsset(string path, string name, JToken jToken)
    {
        if (!Directory.Exists(scriptableDataPath))
        {
            Directory.CreateDirectory(scriptableDataPath);
        }

        name = char.ToUpper(name[0]) + name.Substring(1);
        string dataPath = path + "/" + name + "Scriptable";
        Assembly assembly = Assembly.Load("Assembly-CSharp");
        System.Type scriptableType = assembly.GetType($"JsonClass.{name + "Scriptable"}");

        ScriptableObject scriptableObject = CreateInstance(scriptableType);

        if (jToken is JArray)
        {
            System.Type classType = assembly.GetType($"JsonClass.{name}");
            JArray jArray = (JArray)jToken;
            FieldInfo listField = scriptableType.GetField(char.ToLower(name[0]) + name.Substring(1), BindingFlags.Public | BindingFlags.Instance);
            IList listInstance = null;

            if (listField != null && typeof(IList).IsAssignableFrom(listField.FieldType))
            {
                listInstance = listField.GetValue(scriptableObject) as IList;
                if (listInstance == null)
                {
                    listInstance = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(classType));
                    listField.SetValue(scriptableObject, listInstance);
                }
            }

            foreach (JObject jObject in jArray)
            {
                object var = jObject.ToObject(classType);
                listInstance.Add(var);
            }
        }
        else
        {
            JObject jObject = (JObject)jToken;
            scriptableObject = (ScriptableObject)jObject.ToObject(scriptableType);
        }


        AssetDatabase.CreateAsset(scriptableObject, dataPath + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        isButtonEnabled = true;
    }

    string GenerateClassFromJson(JObject jsonObject, string className,bool isArray = true, bool isFirst = true,bool isFunction = false)
    {
        className = char.ToUpper(className[0]) + className.Substring(1);
        StringBuilder stringBuilder = new StringBuilder();
        List<JProperty> properties = new List<JProperty>();

        if (isFirst)
        {
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using UnityEngine;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("namespace JsonClass");
            stringBuilder.AppendLine("{");

            if (!isFunction)
            {
                stringBuilder.AppendLine("    public partial class " + className + "Scriptable" + " : ScriptableObject");
            }
            else
            {
                stringBuilder.AppendLine("    public partial class " + className + "Scriptable");
            }

            if (isArray)
            {
                stringBuilder.AppendLine("    {");

                if (!isFunction)
                {
                    string propertyName = char.ToLower(className[0]) + className.Substring(1);

                    stringBuilder.AppendLine($"        public List<{className}> {propertyName};");
                }

                stringBuilder.AppendLine("    }");
                stringBuilder.AppendLine();
            }
        }

        if (!isFunction && (isArray || (!isArray && !isFirst)))
        {
            stringBuilder.AppendLine("    [System.Serializable]");
        }

        if (isArray || (!isArray && !isFirst))
        {
            stringBuilder.AppendLine("    public partial class " + className);
        }

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

        stringBuilder.AppendLine("    }");
        stringBuilder.AppendLine();

        foreach (var property in properties)
        {
            string propertyName = char.ToLower(property.Name[0]) + property.Name.Substring(1);
            string propertyType;

            if (property.Value.Type == JTokenType.Object)
            {
                propertyType = char.ToLower(property.Name[0]) + property.Name.Substring(1);
                stringBuilder.Append(GenerateClassFromJson((JObject)property.Value, propertyName,isArray,false, isFunction));
            }
            else if (property.Value.Type == JTokenType.Array && ((JArray)property.Value)[0].Type == JTokenType.Object)
            {
                propertyType = char.ToUpper(property.Name[0]) + property.Name.Substring(1);
                JArray datasArray = (JArray)property.Value;

                stringBuilder.Append(GenerateClassFromJson((JObject)datasArray[0], propertyName, isArray,false, isFunction));
            }
        }

        if (isFirst)
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
        if (!Directory.Exists(jsonDatapath))
        {
            Directory.CreateDirectory(jsonDatapath);
        }

        List<Draw> datas = ScriptableManager.Instance.DrawScriptable.draw;
        string json = JsonConvert.SerializeObject(datas);
        string path = string.Format("{0}/{1}", jsonDatapath, "Draw.json");

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        List<Option> optionDatas = ScriptableManager.Instance.OptionScriptable.option;
        json = JsonConvert.SerializeObject(optionDatas);
        path = string.Format("{0}/{1}", jsonDatapath, "Option.json");

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        List<OptionProbability> optionProbability = ScriptableManager.Instance.OptionProbabilityScriptable.optionProbability;
        json = JsonConvert.SerializeObject(optionProbability);
        path = string.Format("{0}/{1}", jsonDatapath, "OptionProbability.json");

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        List<Localization> textCountries = ScriptableManager.Instance.LocalizationScriptable.localization;
        json = JsonConvert.SerializeObject(textCountries);
        path = string.Format("{0}/{1}", jsonDatapath, "Localization.json");

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        List<Item> itemTypeDatas = ScriptableManager.Instance.ItemScriptable.item;
        json = JsonConvert.SerializeObject(itemTypeDatas);
        path = string.Format("{0}/{1}", jsonDatapath, "Item.json");

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        List<Upgrade> upgrade = ScriptableManager.Instance.UpgradeScriptable.upgrade;
        json = JsonConvert.SerializeObject(upgrade);
        path = string.Format("{0}/{1}", jsonDatapath, "Upgrade.json");

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        StageScriptable stage = ScriptableManager.Instance.StageScriptable;
        json = JsonConvert.SerializeObject(stage);
        path = string.Format("{0}/{1}", jsonDatapath, "Stage.json");

        File.WriteAllText(path, json.ToString(), Encoding.UTF8);

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("결과", "Scriptable 에서 Json 변환", "확인");
    }
}
#endif