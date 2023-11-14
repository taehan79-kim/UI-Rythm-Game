using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unjong
{
    public class Note : MonoBehaviour
    {
        private GameManager3 m_gameManager;
        Rigidbody2D rb;
        public float speed;

        private GameObject m_CanvasObject;
        private RectTransform m_canvasRect;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            m_gameManager = FindObjectOfType<GameManager3>();
            m_CanvasObject = GameObject.FindWithTag("UnjongM3Canvas");
            m_canvasRect = m_CanvasObject.GetComponent<RectTransform>();
            speed = m_canvasRect.localScale.y * 1000;
            rb.velocity = new Vector2(0, -speed);
        }

        // Update is called once per frame
        void Update()
        {
            if (m_gameManager.m_IsPaused)
            {
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, -speed);
            }
        }
    }
}