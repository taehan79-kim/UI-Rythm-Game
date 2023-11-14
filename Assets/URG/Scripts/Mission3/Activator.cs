using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unjong
{
    public class Activator : MonoBehaviour
    , IPointerDownHandler
    {
        public KeyCode key;
        public Color m_color;
        public Sprite m_NoteSprite;

        bool active = false;
        public List<GameObject> m_Notes;

        public bool m_CreateMode;
        public GameObject m_NoteCreate;
        public GameObject m_NotePosition;
        public GameObject m_DestroyEffect;
        public GameObject m_PressEffect;
        public GameObject m_EffectPoint;

        private ScoreManager3 m_scoreManager;
        private int m_NoteCnt = 0;

        // Start is called before the first frame update
        void Start()
        {
            //Image myImage = GetComponent<Image>();
            //myImage.color = m_color;
            m_scoreManager = FindObjectOfType<ScoreManager3>();
        }

        // Update is called once per frame
        void Update()
        {
            #region KEYPAD
            // 에디터 키보드 부분(삭제 및  생략 가능)
            if (m_CreateMode && Input.GetKeyDown(key))
            {
                if (Input.GetKeyDown(key))
                {
                    GameObject n = Instantiate(m_NoteCreate, transform.position, Quaternion.identity, m_NotePosition.transform);
                    Image myImage1 = n.GetComponent<Image>();
                    myImage1.sprite = m_NoteSprite;
                    //myImage1.color = new Color (0.6f, 0.6f, 0.6f,1f);
                }
            }
            else
            {
                if (Input.GetKeyDown(key))
                {
                    StartCoroutine(Pressed());
                }
                if (Input.GetKeyDown(key) && active)
                {
                    if (Mathf.Abs(m_Notes[0].gameObject.transform.position.y - this.transform.position.y) > 100f)
                    {
                        //print("good");
                        m_scoreManager.IncreaseScore(5);
                    }
                    else
                    {
                        //print("perfect");
                        m_scoreManager.IncreaseScore(10);
                    }
                    Destroy(m_Notes[0].gameObject);
                    GameObject particle = Instantiate(m_DestroyEffect, transform.position, Quaternion.identity, m_EffectPoint.transform);
                    ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                    var main = ps.main;
                    main.startColor = m_color;
                    Destroy(particle, 1.5f);
                }
            }
            #endregion
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            ActivatorClick();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("Exit");
        }

        public void ActivatorClick()
        {
            if (m_CreateMode)
            {
                GameObject n = Instantiate(m_NoteCreate, transform.position, Quaternion.identity, m_NotePosition.transform);
                Image myImage1 = n.GetComponent<Image>();
                myImage1.sprite = m_NoteSprite;
                //myImage1.color = new Color (0.6f, 0.6f, 0.6f,1f);
            }
            else
            {
                StartCoroutine(Pressed());
                if (active)
                {
                    if (Mathf.Abs(m_Notes[0].gameObject.transform.position.y - this.transform.position.y) > 100f)
                    {
                        //print("good");
                        m_scoreManager.IncreaseScore(5);
                    }
                    else
                    {
                        //print("perfect");
                        m_scoreManager.IncreaseScore(10);
                    }
                    Destroy(m_Notes[0].gameObject);
                    GameObject particle = Instantiate(m_DestroyEffect, transform.position, Quaternion.identity, m_EffectPoint.transform);
                    ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                    var main = ps.main;
                    main.startColor = m_color;
                    Destroy(particle, 1.5f);
                }
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            active = true;
            if (col.gameObject.tag == "Note")
            {
                m_Notes.Add(col.gameObject);
                m_NoteCnt++;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (!m_CreateMode)
            {
                m_NoteCnt--;
                m_Notes.RemoveAt(0);
                if (m_NoteCnt <= 0)
                {
                    active = false;
                }
            }
        }

        IEnumerator Pressed()
        {
            GameObject particle = Instantiate(m_PressEffect, transform.position, Quaternion.identity, m_EffectPoint.transform);
            Destroy(particle, 0.2f);
            yield return null;
        }

    }
}
