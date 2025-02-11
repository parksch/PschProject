#if UNITY_EDITOR
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class SheetParsing : EditorWindow
{
    string url = "";
    string url2 = "";
    string gid;
    string jsonFileName;
    ServerSheetEnum serverSheetEnum;

    private const int keyIndex = 0;
    private const int typeIndex = 1;

    [MenuItem("Tools/ GoogleSheetParsing")]
    public static void ShowWindow()
    {

        EditorWindow window = GetWindow(typeof(SheetParsing));
        window.maxSize = window.minSize = new Vector2(800, 300);
    }

    private async void OnGUI()
    {
        var btnOptions = new[] { GUILayout.Width(128), GUILayout.Height(32) };
        gid = EditorGUILayout.TextField("Gid", gid);
        jsonFileName = EditorGUILayout.TextField("SaveFileName", jsonFileName);
        EditorGUILayout.LabelField("GoogleGid : 원하는 시트의 맨뒤 아이디 값");
        EditorGUILayout.LabelField("SaveFileName : 저장 할 Json 파일 이름");
        GUILayout.Space(20);

        if (jsonFileName == string.Empty) jsonFileName = "JsonFile";

        if (GUILayout.Button("Create", btnOptions))
        {
            Parsing();
        }

        var btnOptions2 = new[] { GUILayout.Width(128), GUILayout.Height(32) };

        if (GUILayout.Button("AllParsing", btnOptions2))
        {
            AllParsing();
        }

        GUILayout.Space(20);
        var serverDataBtn = new[] { GUILayout.Width(200), GUILayout.Height(32) };
        serverSheetEnum = (ServerSheetEnum)EditorGUILayout.EnumPopup("Server Sheet : ", serverSheetEnum);
        Req_ServerSheet reqServerSheet = new Req_ServerSheet();

        if (serverSheetEnum == ServerSheetEnum.All)
        {
            reqServerSheet.SheetsInfo.Clear(); // 리스트를 초기화합니다.
            foreach (ServerSheetEnum sheet in Enum.GetValues(typeof(ServerSheetEnum)))
            {
                if (sheet == ServerSheetEnum.All) continue; // 'All'은 제외합니다.

                reqServerSheet.SheetsInfo.Add(new SheetInfo
                {
                    gid = (int)sheet, // 수정됨: 현재 루프 변수 사용
                    sheet_name = sheet.ToString(),
                });
            }
        }
        else
        {
            reqServerSheet.SheetsInfo.Clear(); // 이전 선택을 초기화합니다.

            // 특정 시트만 선택한 경우 해당 시트 정보만 추가합니다.
            reqServerSheet.SheetsInfo.Add(new SheetInfo
            {
                gid = (int)serverSheetEnum,
                sheet_name = serverSheetEnum.ToString(),
            });
        }

        if (GUILayout.Button("Server Data Update", serverDataBtn))
        {
            await ServerDataResetMgr.Instance.ServerDataUpdate(reqServerSheet);
        }
    }

    void Parsing()
    {
        EditorCoroutineUtility.StartCoroutine(GoogleSheetParsing(), this);
    }

    IEnumerator GoogleSheetParsing()
    {
        string _url = string.Format("{0}/export?format=tsv&gid={1}", url, gid);
        string data = string.Empty;
        bool isFirstCall = true;
        string frontText = string.Empty;
        bool isListDictionary = (jsonFileName.Contains("$")) ? true : false;
        string fileName = jsonFileName.Replace("$", "");

        UnityWebRequest request = UnityWebRequest.Get(_url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            EditorUtility.DisplayDialog("Fail", "GoogleConnect Fail!", "OK");
            yield break;
        }

        data = request.downloadHandler.text;

        List<string> strs = data.Split("\r\n").ToList();
        List<string> keys = strs[keyIndex].Split('\t').ToList();
        List<string> types = strs[typeIndex].Split('\t').ToList();
        JArray jArray = new JArray();

        string assetType = typeof(ExternalDataAsset).ToString();
        StringBuilder csFile = new StringBuilder("using System.Collections.Generic;\n");
        csFile.Append("[System.Serializable]\n");
        csFile.Append("public class " + fileName + ((isListDictionary) ? " : " + assetType : "") + "\n" + "{\n");


        for (int row = 2; row < strs.Count; row++)
        {
            JObject keyValuePairs = new JObject();
            List<string> datas = strs[row].Split("\t").ToList();

            if (datas[0].Equals("DB_IGNORE") || datas[1].Equals("")) continue;

            for (int column = 1; column < keys.Count; column++)
            {
                if (types[column].Equals("DB_IGNORE") || keys[column].Equals(""))
                {
                    continue;
                }

                switch (types[column])
                {
                    case "string":
                        keyValuePairs.Add(keys[column], datas[column].Equals("") ? "" : datas[column]);
                        break;
                    case "int":
                        int @int = 0;

                        if (!datas[column].Equals(""))
                        {
                            int.TryParse(datas[column], out @int);
                        }

                        keyValuePairs.Add(keys[column], @int);
                        break;
                    case "float":
                        float @float = 0;

                        if (!datas[column].Equals(""))
                        {
                            float.TryParse(datas[column], out @float);
                        }

                        keyValuePairs.Add(keys[column], @float);
                        break;
                    case "arrayFloat":
                        JArray jArray1 = new JArray();
                        List<float> nums = new List<float>();
                        List<string> str_nums = datas[column].Split(",").ToList();

                        for (int k = 0; k < str_nums.Count; k++)
                        {
                            if (str_nums[k].Equals("")) continue;
                            float fValue = 0;
                            float.TryParse(str_nums[k], out fValue);
                            nums.Add(fValue);
                        }

                        jArray1.Add(nums);
                        keyValuePairs.Add(keys[column], jArray1);
                        break;
                    case "arrayString":
                        JArray jArray2 = new JArray();
                        List<string> strValues = datas[column].Split(",").ToList();
                        if (strValues.Count == 1 && strValues[0].Equals("")) { strValues.Clear(); }
                        jArray2.Add(strValues);
                        keyValuePairs.Add(keys[column], jArray2);
                        break;
                    case "arrayInt":
                        JArray jArray3 = new JArray();
                        List<int> nums2 = new List<int>();
                        List<string> str_nums2 = datas[column].Split(",").ToList();

                        for (int k = 0; k < str_nums2.Count; k++)
                        {
                            if (str_nums2[k].Equals("")) continue;
                            int iValue = 0;
                            int.TryParse(str_nums2[k], out iValue);
                            nums2.Add(iValue);
                        }

                        jArray3.Add(nums2);
                        keyValuePairs.Add(keys[column], jArray3);
                        break;
                    case "long":
                        long @long = 0;

                        if (!datas[column].Equals(""))
                        {
                            long.TryParse(datas[column], out @long);
                        }

                        keyValuePairs.Add(keys[column], @long);
                        break;
                    case "byte":
                        byte @byte = 0;

                        if (!datas[column].Equals(""))
                        {
                            byte.TryParse(datas[column], out @byte);
                        }

                        keyValuePairs.Add(keys[column], @byte);
                        break;
                    case "bool":
                        bool boolNum = false;

                        if (!datas[column].Equals(""))
                        {
                            if (datas[column].Equals("TRUE"))
                            {
                                boolNum = true;
                            }
                            else if (datas[column].Equals("FALSE"))
                            {
                                boolNum = false;
                            }
                        }

                        keyValuePairs.Add(keys[column], boolNum);
                        break;
                    default:
                        break;
                }
            }

            jArray.Add(keyValuePairs);
        }

        StringBuilder contructor = new StringBuilder(string.Empty);
        for (int j = 1; j < keys.Count; j++)
        {
            if (types[j].Equals("DB_IGNORE") || keys[j].Equals(""))
            {
                continue;
            }

            switch (types[j])
            {
                case "string":
                    FirstCallSetting($"{keys[j]}:string");
                    csFile.Append("\tpublic string " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                case "int":
                    FirstCallSetting($"{keys[j]}:int");
                    csFile.Append("\tpublic int " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                case "float":
                    FirstCallSetting($"{keys[j]}:float");
                    csFile.Append("\tpublic float " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                case "arrayFloat":
                    csFile.Append("\tpublic float[] " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                case "arrayString":
                    csFile.Append("\tpublic string[] " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                case "arrayInt":
                    csFile.Append("\tpublic int[] " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                case "byte":
                    FirstCallSetting($"{keys[j]}:byte");
                    csFile.Append("\tpublic byte " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                case "long":
                    FirstCallSetting($"{keys[j]}:long");
                    csFile.Append("\tpublic long " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                case "bool":
                    csFile.Append("\tpublic bool " + keys[j] + ";\n");
                    contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                    break;
                default:
                    break;
            }
        }


        string dictionaryValueType = ((isListDictionary) ? $"List<{assetType}>" : fileName);
        string dictionaryKeyType = frontText.Split(':')[1];
        csFile.Append($"\tpublic static Dictionary<{dictionaryKeyType},{dictionaryValueType}> table = new Dictionary<{dictionaryKeyType},{dictionaryValueType}>();\n");

        // Constructor and Converter
        if (isListDictionary)
        {
            csFile.Append($"\tpublic {fileName}() {{ }}\n");
            csFile.Append($"\tprivate {fileName}({assetType} asset)\n\t{{\n");
            csFile.Append($"\t\t{fileName} data = asset as {fileName};\n");
            csFile.Append(contructor.ToString() + "\t}\n");
            csFile.Append($"\tpublic static List<{fileName}> GetTableData({dictionaryKeyType} key)\n\t{{\n");
            csFile.Append($"\t\treturn table[key].ConvertAll<{fileName}>(\n\t\t\tnew System.Converter<{assetType}, {fileName}>((asset)=> {{ return new {fileName}(asset); }}));\n\t}}");
        }

        csFile.Append("\n}");

        string path = "";
        path = Path.Combine(Application.dataPath + "/Resources/JsonFiles/", fileName + ".json");
        string path2 = Path.Combine(Application.dataPath + "/Resources/DataClass/", fileName + ".cs");

        Debug.Log(jArray);

        File.WriteAllText(path, jArray.ToString());
        File.WriteAllText(path2, csFile.ToString());

        EditorUtility.DisplayDialog("Success", "Json successfully Save!", "OK");

        void FirstCallSetting(string frontData)
        {
            Debug.Log(frontData);
            if (isFirstCall)
            {
                frontText = frontData;
                csFile.Insert(0/*Front*/, "//" + frontData + ":" + ((isListDictionary) ? "true" : "false") + '\n');
                isFirstCall = false;
            }
        }
    }

    void AllParsing()
    {
        EditorCoroutineUtility.StartCoroutine(GoogleSheetAllParsing(), this);
    }

    IEnumerator GoogleSheetAllParsing()
    {
        string _url = string.Format("{0}", url2);
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(_url);
        yield return unityWebRequest.SendWebRequest();

        string data = unityWebRequest.downloadHandler.text;
        string[] values = data.Split("\t");

        for (int l = 0; l < values.Length; l++)
        {
            string name = values[l].Split(',')[0];
            string id = values[l].Split(',')[1];

            bool isFirstCall = true;
            string frontText = string.Empty;
            bool isListDictionary = (name.Contains("$")) ? true : false;
            if (isListDictionary) name = name.Replace("$", "");

            if (name.Contains('&') || name.Contains("건들X"))
            {
                continue;
            }
            url = "";
            _url = string.Format("{0}/export?format=tsv&gid={1}", url, id);

            unityWebRequest = UnityWebRequest.Get(_url);

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                EditorUtility.DisplayDialog("Fail", "GoogleConnect Fail!", "OK");
                yield break;
            }

            data = unityWebRequest.downloadHandler.text;

            List<string> strs = data.Split("\r\n").ToList();
            List<string> types = strs[typeIndex].Split('\t').ToList();
            List<string> keys = strs[keyIndex].Split('\t').ToList();
            JArray jArray = new JArray();

            string assetType = typeof(ExternalDataAsset).ToString();
            StringBuilder csFile = new StringBuilder("using System.Collections.Generic;\n");
            csFile.Append("[System.Serializable]\n");
            csFile.Append("public class " + name + ((isListDictionary) ? " : " + assetType : "") + "\n" + "{");

            for (int row = 2; row < strs.Count; row++)
            {
                JObject keyValuePairs = new JObject();
                List<string> datas = strs[row].Split("\t").ToList();

                if (datas[0].Equals("DB_IGNORE") || datas[1].Equals("")) continue;

                for (int column = 1; column < keys.Count; column++)
                {
                    if (types[column].Equals("DB_IGNORE") || keys[column].Equals(""))
                    {
                        continue;
                    }

                    switch (types[column])
                    {
                        case "string":
                            keyValuePairs.Add(keys[column], datas[column].Equals("") ? "" : datas[column]);
                            break;
                        case "int":
                            int @int = 0;

                            if (!datas[column].Equals(""))
                            {
                                int.TryParse(datas[column], out @int);
                            }

                            keyValuePairs.Add(keys[column], @int);
                            break;
                        case "float":
                            float @float = 0;

                            if (!datas[column].Equals(""))
                            {
                                float.TryParse(datas[column], out @float);
                            }

                            keyValuePairs.Add(keys[column], @float);
                            break;
                        case "arrayFloat":
                            JArray jArray1 = new JArray();
                            List<float> nums = new List<float>();
                            List<string> str_nums = datas[column].Split(",").ToList();

                            for (int k = 0; k < str_nums.Count; k++)
                            {
                                if (str_nums[k].Equals("")) continue;
                                float fValue = 0;
                                float.TryParse(str_nums[k], out fValue);
                                nums.Add(fValue);
                            }

                            jArray1.Add(nums);
                            keyValuePairs.Add(keys[column], jArray1);
                            break;
                        case "arrayString":
                            JArray jArray2 = new JArray();
                            List<string> strValues = datas[column].Split(",").ToList();
                            if (strValues.Count == 1 && strValues[0].Equals("")) { strValues.Clear(); }
                            jArray2.Add(strValues);
                            keyValuePairs.Add(keys[column], jArray2);
                            break;
                        case "arrayInt":
                            JArray jArray3 = new JArray();
                            List<int> nums2 = new List<int>();
                            List<string> str_nums2 = datas[column].Split(",").ToList();

                            for (int k = 0; k < str_nums2.Count; k++)
                            {
                                if (str_nums2[k].Equals("")) continue;
                                int iValue = 0;
                                int.TryParse(str_nums2[k], out iValue);
                                nums2.Add(iValue);
                            }

                            jArray3.Add(nums2);
                            keyValuePairs.Add(keys[column], jArray3);
                            break;
                        case "long":
                            long @long = 0;

                            if (!datas[column].Equals(""))
                            {
                                long.TryParse(datas[column], out @long);
                            }

                            keyValuePairs.Add(keys[column], @long);
                            break;
                        case "byte":
                            byte @byte = 0;

                            if (!datas[column].Equals(""))
                            {
                                byte.TryParse(datas[column], out @byte);
                            }

                            keyValuePairs.Add(keys[column], @byte);
                            break;
                        case "bool":
                            bool boolNum = false;

                            if (!datas[column].Equals(""))
                            {
                                if (datas[column].Equals("TRUE"))
                                {
                                    boolNum = true;
                                }
                                else if (datas[column].Equals("FALSE"))
                                {
                                    boolNum = false;
                                }
                            }

                            keyValuePairs.Add(keys[column], boolNum);
                            break;
                        default:
                            break;
                    }
                }

                jArray.Add(keyValuePairs);
            }

            StringBuilder contructor = new StringBuilder(string.Empty);
            for (int j = 1; j < keys.Count; j++)
            {
                if (types[j].Equals("DB_IGNORE") || keys[j].Equals(""))
                {
                    continue;
                }

                switch (types[j])
                {
                    case "string":
                        FirstCallSetting(ref csFile, ref frontText, ref isFirstCall, isListDictionary, $"{keys[j]}:string");
                        csFile.Append("\n\tpublic string " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    case "int":
                        FirstCallSetting(ref csFile, ref frontText, ref isFirstCall, isListDictionary, $"{keys[j]}:int");
                        csFile.Append("\n\tpublic int " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    case "float":
                        FirstCallSetting(ref csFile, ref frontText, ref isFirstCall, isListDictionary, $"{keys[j]}:float");
                        csFile.Append("\n\tpublic float " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    case "arrayFloat":
                        csFile.Append("\n\tpublic float[] " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    case "arrayString":
                        csFile.Append("\n\tpublic string[] " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    case "arrayInt":
                        csFile.Append("\n\tpublic int[] " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    case "byte":
                        FirstCallSetting(ref csFile, ref frontText, ref isFirstCall, isListDictionary, $"{keys[j]}:byte");
                        csFile.Append("\n\tpublic byte " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    case "long":
                        FirstCallSetting(ref csFile, ref frontText, ref isFirstCall, isListDictionary, $"{keys[j]}:long");
                        csFile.Append("\n\tpublic long " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    case "bool":
                        csFile.Append("\n\tpublic bool " + keys[j] + ";\n");
                        contructor.Append($"\t\t{keys[j]} = data.{keys[j]};\n");
                        break;
                    default:
                        break;
                }
            }

            string dictionaryValueType = ((isListDictionary) ? $"List<{assetType}>" : jsonFileName);
            string dictionaryKeyType = frontText.Split(':')[1];
            csFile.Append($"\tpublic static Dictionary<{dictionaryKeyType},{dictionaryValueType}> table = new Dictionary<{dictionaryKeyType},{dictionaryValueType}>();\n");

            // Constructor and Converter
            if (isListDictionary)
            {
                csFile.Append($"\tpublic {jsonFileName} () {{ }}\n");
                csFile.Append($"\tprivate {jsonFileName}({assetType} asset)\n\t{{\n");
                csFile.Append($"\t\t{jsonFileName} data = asset as {jsonFileName};\n");
                csFile.Append(contructor.ToString() + "\t}\n");
                csFile.Append($"\tpublic static List<{jsonFileName}> GetTableData({dictionaryKeyType} key)\n\t{{\n");
                csFile.Append($"\t\treturn table[key].ConvertAll<{jsonFileName}>(\n\t\t\tnew System.Converter<{assetType}, {jsonFileName}>((asset)=> {{ return new {jsonFileName}(asset); }}));\n\t}}");
            }

            csFile.Append("\n}");

            string path = "";
            path = Path.Combine(Application.dataPath + "/Resources/JsonFiles/", name + ".json");
            string path2 = Path.Combine(Application.dataPath + "/Resources/DataClass/", name + ".cs");

            Debug.Log(name + " Load Success");

            File.WriteAllText(path, jArray.ToString());
            File.WriteAllText(path2, csFile.ToString());
        }

        EditorUtility.DisplayDialog("Success", "Json successfully Save!", "OK");

        void FirstCallSetting(ref StringBuilder csFile, ref string frontText, ref bool isFirstCall, bool isListDictionary, string frontData)
        {
            if (isFirstCall)
            {
                frontText = frontData;
                csFile.Insert(0/*Front*/, "//" + frontData + ((isListDictionary) ? "true" : "false") + '\n');
            }

            isFirstCall = false;
        }
    }
}
#endif