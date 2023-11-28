using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuSceneView : MonoBehaviour
    {
        [SerializeField]
        Button playButton;

        public Button PlayButton => playButton;
    }
}
