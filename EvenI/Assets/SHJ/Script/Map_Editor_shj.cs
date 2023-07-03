using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Map_Editor_shj : EditorWindow
{
    #region 변수설정
    private static Map_Editor_shj instance = null;

    Object[] obj;
    Stack<GameObject> created_obj; //뒤로가기 기능을 생성할 stack
    List<Vector3> created_pos;

    //GameObject panel; //판넬형 에디터 제작
    GameObject editor_guide; //방향키 에디터에서 제작되는 위치 표시
    GameObject map;

    int choice_num = 0;
    bool show_initset = false, show_grid = false;
    bool creating = false;
    Vector2Int panel_size;
    Vector2 offeset_pos = new Vector2(1, 1);

    string file_name = "new File";
    public float distance = 0.0f;

    Vector2[] pos;
    #endregion

    #region 에디터 창
    [MenuItem("Editor/Map_Editor")]
    static void Show_Window()
    {
        GetWindow(typeof(Map_Editor_shj)).Show(); //유니티 상단 탭에 표시
    }
    #endregion

    private void Awake() // 리소스 가져오기
    {
        instance = this;
        obj = Resources.LoadAll<Object>("Prefab"); //경로 입력해주세요 기본적으로 Resources는 안써도 됩니다.

        if (GameObject.Find("Editor_guide") == null) new GameObject("Editor_guide");
        if (GameObject.Find("Map") == null) new GameObject("Map");

        editor_guide = GameObject.Find("Editor_guide");
        editor_guide.transform.position = offeset_pos;
        if (editor_guide.GetComponent<Editor_Guide_shj>() == null) editor_guide.AddComponent<Editor_Guide_shj>();

        map = GameObject.Find("Map");
        //PrefabUtility.SaveAsPrefabAsset(map, "Assets/" + file_name + ".prefab"); //이방식을 이용하면 저장가능

        //panel = Resources.Load<GameObject>("panel");
    }

    public static Map_Editor_shj Getinstance { get { return instance; } }

    private void OnFocus()
    {
        if (instance == null) instance = this;
        if (created_obj == null)
        {
            created_obj = new Stack<GameObject>(); //다시 에디터로 돌아왔을때 뒤로가기 기능 재생성
            created_pos = new List<Vector3>();
            StackChk();
        }
    }

    private void OnGUI()
    {
        #region 그리드 설정
        show_grid = EditorGUILayout.BeginFoldoutHeaderGroup(show_grid, "장애물 및 아이템 선택");
        if (show_grid)
        {
            Texture[] obj_thumnail = new Texture[obj.Length];
            for (int i = 0; i < obj.Length; i++) obj_thumnail[i] = AssetPreview.GetAssetPreview(obj[i]);
            choice_num = GUILayout.SelectionGrid(choice_num, obj_thumnail, 4, GUILayout.MaxHeight(250), GUILayout.MaxWidth(300));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        GUILayout.Space(50);
        #endregion

        #region 버튼을 눌러서 생성하는 방식

        GUILayout.Label("방향키 버튼으로 제작");
        GUILayout.Label("+버튼 저장,0버튼 undo");
        string[] btn_text =
        {
            "↙", "▽", "↘",
            "◁", "생성", "▷",
            "↖", "△", "↗"
        }; //버튼에 보일 텍스트

        GUILayout.BeginVertical();
        for (int i = 2; i >= 0; i--)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 3; j++)
                if (GUILayout.Button(btn_text[i * 3 + j], GUILayout.Height(100), GUILayout.Width(100)))
                    Guide_Control((i * 3 + j + 1).ToString());
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.Label("*저장누를때 주의문구 없이 저장됩니다,파일명에 신경써주세요");

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("카메라 추적", GUILayout.Height(50), GUILayout.Width(100)))
        {
            Set_Scene_Camera();
        }

        GUILayout.EndHorizontal();



        GUILayout.Space(50);

        GUILayout.Label("저장할 파일명");
        file_name = EditorGUILayout.TextField(file_name, GUILayout.Width(100));

        GUILayout.Label("오브젝트 사이의 거리");
        distance = EditorGUILayout.FloatField(distance, GUILayout.Width(100));

        if (GUILayout.Button("오브젝트 정렬", GUILayout.Height(50), GUILayout.Width(100))) ObjectSort();

        #endregion
    }

    public void Guide_Control(string key)
    {
        Vector2[] pos ={
            new Vector2(-distance, -distance),new Vector2(0, -distance),new Vector2(distance, -distance),
            new Vector2(-distance, 0),new Vector2(0, 0),new Vector2(distance, 0),
            new Vector2(-distance, distance),new Vector2(0, distance),new Vector2(distance, distance)
        }; //배치될 포지션

        int key_num;

        switch (key)
        {
            case "+": //저장
                key_num = 10;
                break;

            case ".":
                key_num = 11;
                break;

            default:
                key_num = int.Parse(key);
                break;
        }

        //if (key == "+") key_num = 10; //저장
        //else key_num = int.Parse(key); //이동

        if (0 <= key_num && key_num <= 9)
        {
            if (key_num == 0 && created_obj.Count > 0)
            {
                GameObject c_obj = created_obj.Peek();
                created_obj.Pop();
                created_pos.Remove(c_obj.transform.position);

                editor_guide.transform.position = created_obj.Count != 0 ? created_obj.Peek().transform.position : c_obj.transform.position;
                Destroy(c_obj);
            }
            else if (key_num == 5)
            {
                creating = creating == true ? false : true;
                if (!created_pos.Contains(editor_guide.transform.position) && creating) Create(editor_guide.transform.position);
            }
            else
            {
                editor_guide.transform.position += (Vector3)pos[key_num - 1];
                if (creating) Create(editor_guide.transform.position);
            }
        }
        else
        {
            switch (key_num)
            {
                case 10:
                    //string path = "Assets/" + file_name + ".prefab";
                    //bool file_chk = File.Exists(path);
                    //if(file_chk)
                    //    Debug.Log("이미 있어요");

                    map.name = file_name;
                    PrefabUtility.SaveAsPrefabAsset(map, "Assets/Resources/Prefab/Map/" + file_name + ".prefab"); //이방식을 이용하면 저장가능 경로조정필요
                    break;
                case 11:
                    Set_Scene_Camera();
                    break;
            }
        }
    }

    void StackChk()
    {
        for (int i = 0; i < map.transform.childCount; i++)
        {
            created_obj.Push(map.transform.GetChild(i).gameObject);
            created_pos.Add(map.transform.GetChild(i).position);
        }
    }
    void Create(Vector3 guide_pos)
    {
        if (!created_pos.Contains(guide_pos))
        {
            GameObject c_obj = Instantiate((GameObject)obj[choice_num]);
            string parent_name = c_obj.GetComponent<SpriteRenderer>().sprite.name;

            if (GameObject.Find(parent_name) == null)
            {
                new GameObject(parent_name);
                GameObject.Find(parent_name).transform.SetParent(map.transform);
            }

            c_obj.transform.parent = GameObject.Find(parent_name).transform;
            c_obj.transform.position = editor_guide.transform.position;

            created_obj.Push(c_obj);
            created_pos.Add(c_obj.transform.position);
        }
    }

    void Set_Scene_Camera()
    {
        //씬 뷰 카메라 변경
        SceneView.lastActiveSceneView.pivot = editor_guide.transform.position;
    }

    void ObjectSort()
    {
        for (int i = 0; i < map.transform.childCount; i++)
        {
            GameObject child = map.transform.GetChild(i).gameObject;

            if (child.GetComponent<SpriteRenderer>() != null)
            {
                string sprite_name = child.GetComponent<SpriteRenderer>().sprite.name;

                if (GameObject.Find(sprite_name) == null)
                {
                    GameObject parent = new GameObject(sprite_name);
                    parent.transform.SetParent(map.transform);
                }

                child.transform.SetParent(GameObject.Find(sprite_name).transform);
                i--;
            }
        }
    }
}