using Board;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class InitialBoardLayoutEditorWindow : EditorWindow
    {
        [MenuItem("Game/Open InitialBoardLayoutEditorWindow")]
        static void Open()
        {
            var window = GetWindow<InitialBoardLayoutEditorWindow>("Initial Board Layout Editor");
            window.Init();
            window.Show();
        }

        Vector2 _scrollViewPosition;
        BoardCatalogue _boardCatalogue;
        BoardSettings _boardSettings;

        void Init()
        {
            _boardCatalogue = Resources.Load<BoardCatalogue>("BoardCatalogue");
            _boardSettings = Resources.Load<BoardSettings>("BoardSettings");

            SetInitialBoardTiles();
        }

        void OnGUI()
        {
            if (_boardSettings == null)
            {
                EditorGUILayout.HelpBox("BoardSettings is NULL", MessageType.Warning);
            }

            if (_boardCatalogue == null)
            {
                EditorGUILayout.HelpBox("BoardCatalogue is NULL", MessageType.Warning);
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Shuffle InitialBoardTiles"))
            {
                SetInitialBoardTiles();
            }

            EditorGUILayout.HelpBox(
                "By shuffling the InitialBoardTiles, all previous existing values will be lost",
                MessageType.Info);

            GUILayout.Space(10);
            DrawBoardCatalogue();

            EditorGUILayout.HelpBox(
                "Use the indexes above to define which asset should be in a (x,y) position upon board generation",
                MessageType.Info);

            GUILayout.Space(10);
            DrawInitialBoardTiles();

            EditorUtility.SetDirty(_boardSettings);
            AssetDatabase.SaveAssets();
        }

        void SetInitialBoardTiles()
        {
            if (_boardSettings == null)
            {
                return;
            }

            _boardSettings.InitialBoardLayout = new BoardLayout(
                _boardSettings.Width,
                _boardSettings.Height,
                _boardCatalogue.BoardTileSettingsArray.Length);
        }

        void DrawBoardCatalogue()
        {
            GUILayout.BeginVertical("box");
            {
                for (int i = 0; i < _boardCatalogue.BoardTileSettingsArray.Length; i++)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(
                        _boardCatalogue.BoardTileSettingsArray[i].AssetReference.AssetGUID);

                    GUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField($"Index [{i}]", EditorStyles.boldLabel);
                        EditorGUILayout.LabelField($"Asset [{assetPath}]");
                        EditorGUILayout.LabelField(
                            $"GrantedPoint [{_boardCatalogue.BoardTileSettingsArray[i].GrantedPoint}]");
                    }

                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.EndHorizontal();
        }

        void DrawInitialBoardTiles()
        {
            _scrollViewPosition = GUILayout.BeginScrollView(_scrollViewPosition, GUIStyle.none);

            GUILayout.BeginVertical("[Initial Board Tiles]", "box");
            {
                int width = _boardSettings.Width;
                int height = _boardSettings.Height;

                GUILayout.Space(25);

                for (int y = height - 1; y >= 0; y--)
                {
                    GUILayout.BeginHorizontal();
                    {
                        for (int x = 0; x < width; x++)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorGUILayout.LabelField($"[{x},{y}] ");
                                _boardSettings.InitialBoardLayout.Rows[x].Columns[y].TileValue =
                                    EditorGUILayout.IntSlider(
                                        _boardSettings.InitialBoardLayout.Rows[x].Columns[y].TileValue,
                                        0,
                                        _boardCatalogue.BoardTileSettingsArray.Length);
                            }

                            GUILayout.EndHorizontal();
                        }

                        GUILayout.EndHorizontal();
                    }
                }

                GUILayout.EndVertical();
            }

            GUILayout.EndScrollView();
        }
    }
}
