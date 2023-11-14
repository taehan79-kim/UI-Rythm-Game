using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Unjong
{
    public class Countdown : MonoBehaviour
    {
        public GameObject GamePanel;
        public GameObject NotePanel;
        public GameObject[] m_CountDown;
        public GameObject m_notePanel;
        private AudioSource m_audio;
        private GameManager3 m_gameManager;
        private RectTransform m_NotePointRect;


        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        public void Init()
        {
            GamePanel.SetActive(false);
            m_audio = this.GetComponent<AudioSource>();
            
            m_notePanel = Instantiate(NotePanel, GamePanel.transform.position, Quaternion.identity, GamePanel.transform);
            m_gameManager = FindObjectOfType<GameManager3>();
            
            //Debug.Log(GamePanel.transform.position);
            CountdownPlay(3);
            m_gameManager.AudioSet();


        }

        public void CountdownPlay(int i)
        {
            StartCoroutine(CountDownCo(i));
        }

        public IEnumerator CountDownCo(int i)
        {
            //m_CountDown[0].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            for (int j = 0; j < i; j++)
            {
                m_CountDown[j].SetActive(true);
                m_audio.Play();
                yield return new WaitForSeconds(1.0f);
                m_CountDown[j].SetActive(false);
            }
            GamePanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}