using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unjong
{
    public class GameManager3 : MonoBehaviour
    {

        public bool m_IsPaused;
        public GameObject m_PanelLoading;
        public GameObject m_NotePosition;

        private AudioSource m_audio;
        private ScoreManager3 m_scoreManager;
        private Countdown m_countdown;
        // Start is called before the first frame update
        void Start()
        {
            m_scoreManager = FindObjectOfType<ScoreManager3>();

        }

        public void AudioSet()
        {
            m_countdown = FindObjectOfType<Countdown>();
            Transform _temtrans = m_countdown.m_notePanel.transform;
            m_audio = _temtrans.GetChild(0).GetComponent<AudioSource>();
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Note_End")
            {
                m_scoreManager.EndGame();
            }
            else
            {
                Destroy(col.gameObject);
                m_scoreManager.IncreaseScore(-10);
                //print("miss");
                //GameObject particle = Instantiate(m_MissEffect, transform.position, Quaternion.identity);
                //Destroy(particle, 1.5f);
            }
        }

        public void ResetButtonClk()
        {
            m_IsPaused = false;
            Destroy(m_NotePosition.transform.GetChild(0).gameObject);
            m_PanelLoading.SetActive(true);
            m_countdown.Init();
            m_scoreManager.Init();
        }

        public void PauseButtonClk()
        {
            m_IsPaused = true;
            m_audio.Pause();
        }

        public void UnPauseButtonClk()
        {
            m_PanelLoading.SetActive(true);
            m_countdown.CountdownPlay(3);
            StartCoroutine(UnPauseCo());
        }

        public IEnumerator UnPauseCo()
        {
            yield return new WaitForSeconds(3.5f);
            m_IsPaused = false;
            m_audio.UnPause();
        }
    }
}
