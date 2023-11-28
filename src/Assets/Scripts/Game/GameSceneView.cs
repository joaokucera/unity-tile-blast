using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameSceneView : MonoBehaviour
    {
        [SerializeField]
        Button returnButton;

        public Button ReturnButton => returnButton;
    }
}
