using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Unjong
{
    public class ScoreManager3 : MonoBehaviour
    {
        public TMP_Text scoreText;
        //public TMP_Text PerfectText;
        public GameObject[] m_Bidan;
        public int m_Score = 0;
        public int m_GoalScore = 1000;
        public GameObject m_ScorePanel;
        public GameObject m_CompletePanel;
        public GameObject m_FailPanel;
        public GameObject m_ExitPanel;
        public GameObject[] PerfectPanels;


        private int m_perfectScore = 0;
        public TMP_Text PerfectScoreText;
        private int m_goodScore = 0;
        public TMP_Text GoodScoreText;
        private int m_missScore = 0;
        public TMP_Text MissScoreText;



        public void Init()
        {
            m_Score = 0;
            foreach (GameObject i in m_Bidan)
            {
                i.SetActive(false);
            }
        }

        public void IncreaseScore(int amountToIncrease)
        {
            if (amountToIncrease >= 10)
            {
                //PerfectText.text = "Perfect!";
                foreach (GameObject i in PerfectPanels)
                {
                    i.SetActive(false);
                }
                PerfectPanels[0].SetActive(true);
                m_perfectScore += 1;
            }
            else if (amountToIncrease >= 5)
            {
                //PerfectText.text = "Good!";
                foreach (GameObject i in PerfectPanels)
                {
                    i.SetActive(false);
                }
                PerfectPanels[1].SetActive(true);

                m_goodScore += 1;
            }
            else
            {
                //PerfectText.text = "Miss!";
                foreach (GameObject i in PerfectPanels)
                {
                    i.SetActive(false);
                }
                PerfectPanels[2].SetActive(true);
                m_missScore += 1;
            }

            m_Score += amountToIncrease;
            int tempScore = m_Score / 100;
            if (tempScore == 0)
            {
                m_Bidan[tempScore].SetActive(false);
            }
            else if (tempScore <= 9 && tempScore > 0)
            {
                m_Bidan[tempScore - 1].SetActive(true);
                m_Bidan[tempScore].SetActive(false);
            }
            else if (tempScore == 10)
            {
                m_Bidan[tempScore - 1].SetActive(true);
            }


            //scoreText.text = m_Score.ToString();
        }

        public void EndGame()
        {
            StartCoroutine(CompleteMission());
        }

        public IEnumerator CompleteMission()
        {
            yield return new WaitForSeconds(1.0f);
            PerfectScoreText.text = m_perfectScore.ToString();
            GoodScoreText.text = m_goodScore.ToString();
            MissScoreText.text = m_missScore.ToString();
            m_ScorePanel.SetActive(true);
            yield return new WaitForSeconds(5.0f);

            if (m_Score >= m_GoalScore)
            {
                m_ExitPanel.SetActive(false);
                m_CompletePanel.SetActive(true);
            }
            else
            {
                m_ExitPanel.SetActive(false);
                m_FailPanel.SetActive(true);
            }
        }
    }
}
