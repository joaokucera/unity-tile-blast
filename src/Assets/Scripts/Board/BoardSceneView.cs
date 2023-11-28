using TMPro;
using UnityEngine;

namespace Board
{
    public class BoardSceneView : MonoBehaviour
    {
        [SerializeField]
        Camera boardCamera;

        [SerializeField]
        Transform boardParent;

        [SerializeField]
        TextMeshProUGUI scoreText;

        public Camera BoardCamera => boardCamera;
        public Transform BoardParent => boardParent;
        public TextMeshProUGUI ScoreText => scoreText;
    }
}
