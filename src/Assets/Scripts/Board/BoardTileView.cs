using System;
using UnityEngine;

namespace Board
{
    public class BoardTileView : MonoBehaviour
    {
        [SerializeField]
        GameObject item;

        [SerializeField]
        GameObject particle;

        public Action OnClickCallback;

        public void SetAsCollected()
        {
            item.SetActive(false);
            particle.SetActive(true);
        }

        void OnEnable()
        {
            SetDefaultVisibility();
        }

        void OnDisable()
        {
            SetDefaultVisibility();
        }

        void SetDefaultVisibility()
        {
            item.SetActive(true);
            particle.SetActive(false);
        }

        void OnMouseDown()
        {
            OnClickCallback?.Invoke();
        }
    }
}
