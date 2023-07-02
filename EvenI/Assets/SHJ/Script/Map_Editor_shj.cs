using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Map_Editor_shj : EditorWindow
{
    #region ��������
    private static Map_Editor_shj instance = null;

    Object[] obj;
    Stack<GameObject> created_obj; //�ڷΰ��� ����� ������ stack
    List<Vector3> created_pos;

    //GameObject panel; //�ǳ��� ������ ����
    GameObject editor_guide; //����Ű �����Ϳ��� ���۵Ǵ� ��ġ ǥ��
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

    #region ������ â
    [MenuItem("Editor/Map_Editor")]
    static void Show_Window()
    {
        GetWindow(typeof(Map_Editor_shj)).Show(); //����Ƽ ��� �ǿ� ǥ��
    }
    #endregion

    private void Awake() // ���ҽ� ��������
    {
        instance = this;
        obj = Resources.LoadAll<Object>("Prefab"); //��� �Է����ּ��� �⺻������ Resources�� �Ƚᵵ �˴ϴ�.

        if (GameObject.Find("Editor_guide") == null) new GameObject("Editor_guide");
        if (GameObject.Find("Map") == null) new GameObject("Map");

        editor_guide = GameObject.Find("Editor_guide");
        editor_guide.transform.position = offeset_pos;
        if (editor_guide.GetComponent<Editor_Guide_shj>() == null) editor_guide.AddComponent<Editor_Guide_shj>();

        map = GameObject.Find("Map");
        //PrefabUtility.SaveAsPrefabAsset(map, "Assets/" + file_name + ".prefab"); //�̹���� �̿��ϸ� ���尡��

        //panel = Resources.Load<GameObject>("panel");
    }

    public static Map_Editor_shj Getinstance { get { return instance; } }

    private void OnFocus()
    {
        if (instance == null) instance = this;
        if (created_obj == null)
        {
            created_obj = new Stack<GameObject>(); //�ٽ� �����ͷ� ���ƿ����� �ڷΰ��� ��� �����
            created_pos = new List<Vector3>();
            StackChk();
        }
    }

    private void OnGUI()
    {
        #region �׸��� ����
        show_grid = EditorGUILayout.BeginFoldoutHeaderGroup(show_grid, "��ֹ� �� ������ ����");
        if (show_grid)
        {
            Texture[] obj_thumnail = new Texture[obj.Length];
            for (int i = 0; i < obj.Length; i++) obj_thumnail[i] = AssetPreview.GetAssetPreview(obj[i]);
            choice_num = GUILayout.SelectionGrid(choice_num, obj_thumnail, 4, GUILayout.MaxHeight(250), GUILayout.MaxWidth(300));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        GUILayout.Space(50);
        #endregion

        #region ��ư�� ������ �����ϴ� ���

        GUILayout.Label("����Ű ��ư���� ����");
        GUILayout.Label("+��ư ����,0��ư undo");
        string[] btn_text =
        {
            "��", "��", "��",
            "��", "����", "��",
            "��", "��", "��"
        }; //��ư�� ���� �ؽ�Ʈ

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
        GUILayout.Label("*���崩���� ���ǹ��� ���� ����˴ϴ�,���ϸ� �Ű���ּ���");

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("ī�޶� ����", GUILayout.Height(50), GUILayout.Width(100)))
        {
            Set_Scene_Camera();
        }

        GUILayout.EndHorizontal();



        GUILayout.Space(50);

        GUILayout.Label("������ ���ϸ�");
        file_name = EditorGUILayout.TextField(file_name, GUILayout.Width(100));

        GUILayout.Label("������Ʈ ������ �Ÿ�");
        distance = EditorGUILayout.FloatField(distance, GUILayout.Width(100));

        if (GUILayout.Button("������Ʈ ����", GUILayout.Height(50), GUILayout.Width(100))) ObjectSort();

        #endregion
    }

    public void Guide_Control(string key)
    {
        Vector2[] pos ={
            new Vector2(-distance, -distance),new Vector2(0, -distance),new Vector2(distance, -distance),
            new Vector2(-distance, 0),new Vector2(0, 0),new Vector2(distance, 0),
            new Vector2(-distance, distance),new Vector2(0, distance),new Vector2(distance, distance)
        }; //��ġ�� ������

        int key_num;

        switch (key)
        {
            case "+": //����
                key_num = 10;
                break;

            case ".":
                key_num = 11;
                break;

            default:
                key_num = int.Parse(key);
                break;
        }

        //if (key == "+") key_num = 10; //����
        //else key_num = int.Parse(key); //�̵�

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
                    //    Debug.Log("�̹� �־��");

                    map.name = file_name;
                    PrefabUtility.SaveAsPrefabAsset(map, "Assets/Resources/Prefab/Map/" + file_name + ".prefab"); //�̹���� �̿��ϸ� ���尡�� ��������ʿ�
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
        //�� �� ī�޶� ����
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