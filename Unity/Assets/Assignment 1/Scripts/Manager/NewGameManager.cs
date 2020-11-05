using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Complete
{
    public class NewGameManager : MonoBehaviour
    {
        [Header("GameProperties")]
        public GameObject m_DaddyManager;        //This is the manager that controlles all into scenes.
        private int WhichEVA;
        private int WhichIAAngel;
        private int WhichIAEVA;
        public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
        public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
        public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.

        [Header("Objects")]
        public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases.
        public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
        public Image m_ImageRound;
        public GameObject[] m_EVAPrefab;            // Reference to the prefabs the players will control.
        public EVAManager[] m_EVAS;                 // A collection of managers for enabling and disabling different aspects of the EVAS.
        public Image m_AngelWinsRound;
        public Image m_AsukaWinsRound;
        public Image m_ShinjiWinsRound;
        public Image m_ReiWinsRound;


        private int m_RoundNumber;                  // Which round the game is currently on.
        private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
        private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
        private EVAManager m_RoundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
        private EVAManager m_GameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.

        //UI ELEMENTS
        [Header("UI Elements")]
        public Image m_FillImagePlayer1;
        public Text m_TextPlayer1;
        public Image m_FillImagePlayer2;
        public Text m_TextPlayer2;
        public Image[] m_WinsPlayer1;
        public Image[] m_WinsPlayer2;

        //UI ELEMENTS
        [Header("Image WIN Round")]
        public Image m_RoundWinAngelImage;
        public Image m_RoundWinReiImage;
        public Image m_RoundWinShinjiImage;
        public Image m_RoundWinAsukaImage;

        [Header("Image WIN Round")]
        public Image m_GameWinAngelImage;
        public Image m_GameWinReiImage;
        public Image m_GameWinShinjiImage;
        public Image m_GameWinAsukaImage;
        private int m_WhoWinsRound;
        private int m_WhoWinsGame;

        private void Start()
        {
            FindSceneManager();
            //Init all Wins Image to disable to in future activate one to one depending which player wins the round
            WhichEVA = m_DaddyManager.GetComponent<ManagerScenes>().EVAChoose;
            WhichIAAngel = m_DaddyManager.GetComponent<ManagerScenes>().AngelChooseMovement;
            WhichIAEVA = m_DaddyManager.GetComponent<ManagerScenes>().EVAChooseMovement;
            // Create the delays so they only have to be made once.
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);

            SpawnAllEVAS();
            InitWinImageUI(m_EVAS[0], 0);
            InitWinImageUI(m_EVAS[WhichEVA], 0);
            SetCameraTargets();

            PutUIElmentsWell();
            // Once the tanks have been created and the camera is using them as targets, start the game.
            m_EVAS[0].m_EVAInfo.WhichIAMovement = WhichIAAngel;
            m_EVAS[WhichEVA].m_EVAInfo.WhichIAMovement = WhichIAEVA;
            m_EVAS[0].EVASetIAMovement();
            m_EVAS[WhichEVA].EVASetIAMovement();

            RoundWinnerImageFunction(m_WhoWinsRound, false);
            GameWinnerImageFunction(m_WhoWinsRound, false);

            StartCoroutine(GameLoop());
        }

        private void FindSceneManager()
        {
            m_DaddyManager = GameObject.Find("DaddyManager");
        }

        private void InitWinImageUI(EVAManager _eva, int _wins)
        {
            if (_eva == m_EVAS[0])
            {
                for (int i = 0; i < m_WinsPlayer1.Length; i++)
                {
                    m_WinsPlayer1[i].overrideSprite = m_EVAS[0].m_EVAInfo.WinImage;
                    if(_wins > i) m_WinsPlayer1[i].enabled = true;
                    else m_WinsPlayer1[i].enabled = false;
                }
            }
            if (_eva == m_EVAS[WhichEVA])
            {
                for (int i = 0; i < m_WinsPlayer2.Length; i++)
                {
                    m_WinsPlayer2[i].overrideSprite = m_EVAS[WhichEVA].m_EVAInfo.WinImage;
                    if (_wins > i) m_WinsPlayer2[i].enabled = true;
                    else m_WinsPlayer2[i].enabled = false;
                }
            }
        }

        private void PutUIElmentsWell()
        {
            m_TextPlayer1.text = m_EVAS[0].m_EVAInfo.EVAName;
            m_FillImagePlayer1.overrideSprite = m_EVAS[0].m_EVAInfo.EVAImage;
            m_TextPlayer2.text = m_EVAS[WhichEVA].m_EVAInfo.EVAName;
            m_FillImagePlayer2.overrideSprite = m_EVAS[WhichEVA].m_EVAInfo.EVAImage;
        }

        private void PlayInitAnimations()
        {
            m_EVAS[0].PlayDance(1);
            m_EVAS[1].PlayDance(1);
            m_EVAS[2].PlayDance(5);
            m_EVAS[3].PlayDance(5);
        }

        private void SpawnAllEVAS()
        {
            //Spawn Angel
            m_EVAS[0].m_Instance = Instantiate(m_EVAPrefab[0], m_EVAS[0].m_SpawnPoint.position, m_EVAS[0].m_SpawnPoint.rotation) as GameObject;
            m_EVAS[0].m_Instance.name = "Angel Player";
            m_EVAS[0].Setup();

            //Spawn EVA
            m_EVAS[WhichEVA].m_Instance = Instantiate(m_EVAPrefab[WhichEVA], m_EVAS[WhichEVA].m_SpawnPoint.position, m_EVAS[WhichEVA].m_SpawnPoint.rotation) as GameObject;
            m_EVAS[WhichEVA].m_Instance.name = "EVA Player";
            m_EVAS[WhichEVA].Setup();
        }


        private void SetCameraTargets()
        {
            // Create a collection of transforms the same size as the number of tanks.
            Transform[] targets = new Transform[2];

            targets[0] = m_EVAS[0].m_Instance.transform;
            targets[1] = m_EVAS[WhichEVA].m_Instance.transform;

            m_CameraControl.m_Targets = targets;
        }


        // This is called from start and will run each phase of the game one after another.
        private IEnumerator GameLoop()
        {
            // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
            //PlayInitEndDances(1);
            yield return StartCoroutine(RoundStarting());

            // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
            yield return StartCoroutine(RoundPlaying());

            // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
            yield return StartCoroutine(RoundEnding());

            // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
            if (m_GameWinner != null)
            {
                // If there is a game winner, restart the level.
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                // If there isn't a winner yet, restart this coroutine so the loop continues.
                // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
                StartCoroutine(GameLoop());
            }
        }

        public void PlayInitEndDances(int id)
        {
            for (int i = 0; i <= m_EVAS.Length; i++)
            {
                m_EVAS[i].PlayDance(id);
            }
        }

        private IEnumerator RoundStarting()
        {
            RoundWinnerImageFunction(m_WhoWinsRound, false);
            GameWinnerImageFunction(m_WhoWinsRound, false);

            // As soon as the round starts reset the tanks and make sure they can't move.
            ResetAllTanks();
            DisableEVAControl();

            // Snap the camera's zoom and position to something appropriate for the reset tanks.
            m_CameraControl.SetStartPositionAndSize();

            // Increment the round number and display text showing the players what round it is.
            m_RoundNumber++;
            m_MessageText.text = "ROUND: " + m_RoundNumber;
            m_ImageRound.enabled = true;

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_StartWait;
        }


        private IEnumerator RoundPlaying()
        {
            // As soon as the round begins playing let the players control the tanks.
            EnableEVAControl();

            // Clear the text from the screen.
            m_MessageText.text = string.Empty;
            m_ImageRound.enabled = false;

            // While there is not one tank left...
            while (!OneTankLeft())
            {
                // ... return on the next frame.
                yield return null;
            }
        }

        private IEnumerator RoundEnding()
        {
            // Stop tanks from moving.
            DisableEVAControl();

            // Clear the winner from the previous round.
            m_RoundWinner = null;

            // See if there is a winner now the round is over.
            m_RoundWinner = GetRoundWinner();

            // If there is a winner, increment their score.
            if (m_RoundWinner != null)
            {
                m_RoundWinner.m_Wins++;
                InitWinImageUI(m_RoundWinner, m_RoundWinner.m_Wins);
                m_RoundWinner.PlayDance(1);
            }

            // Now the winner's score has been incremented, see if someone has one the game.
            m_GameWinner = GetGameWinner();

            // Get a message based on the scores and whether or not there is a game winner and display it.
            //string message = EndMessage();
            //m_MessageText.text = message;

            if (m_GameWinner != null)
            {
                GameWinnerImageFunction(m_WhoWinsGame, true);
            }
            else
            {
                RoundWinnerImageFunction(m_WhoWinsRound, true);
            }

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_EndWait;
        }


        // This is used to check if there is one or fewer tanks remaining and thus the round should end.
        private bool OneTankLeft()
        {
            // Start the count of tanks left at zero.
            int numTanksLeft = 0;

            if (m_EVAS[0].m_Instance.activeSelf)
                numTanksLeft++;
            if (m_EVAS[WhichEVA].m_Instance.activeSelf)
                numTanksLeft++;

            // If there are one or fewer tanks remaining return true, otherwise return false.
            return numTanksLeft <= 1;
        }


        // This function is to find out if there is a winner of the round.
        // This function is called with the assumption that 1 or fewer tanks are currently active.
        private EVAManager GetRoundWinner()
        {
            if (m_EVAS[0].m_Instance.activeSelf)
            {
                m_WhoWinsRound = 0;
                return m_EVAS[0];
            }

            else if (m_EVAS[WhichEVA].m_Instance.activeSelf)
            {
                m_WhoWinsRound = WhichEVA;
                return m_EVAS[WhichEVA];
            }

            return null;
        }


        // This function is to find out if there is a winner of the game.
        private EVAManager GetGameWinner()
        {

            if (m_EVAS[0].m_Wins == m_NumRoundsToWin)
            {
                m_WhoWinsGame = 0;
                return m_EVAS[0];
            }
            else if (m_EVAS[WhichEVA].m_Wins == m_NumRoundsToWin)
            {
                m_WhoWinsGame = WhichEVA;
                return m_EVAS[WhichEVA];
            }

            return null;
        }

        // This function is used to turn all the tanks back on and reset their positions and properties.
        private void ResetAllTanks()
        {
            m_EVAS[0].Reset();
            m_EVAS[WhichEVA].Reset();
        }

        private void EnableEVAControl()
        {
            m_EVAS[0].EnableControl();
            m_EVAS[WhichEVA].EnableControl();
        }
        private void DisableEVAControl()
        {
            m_EVAS[0].DisableControl();
            m_EVAS[WhichEVA].DisableControl();
        }

        private void RoundWinnerImageFunction(int _whowins, bool activate)
        {
            m_RoundWinAngelImage.enabled = false;
            m_RoundWinReiImage.enabled = false;
            m_RoundWinShinjiImage.enabled = false;
            m_RoundWinAsukaImage.enabled = false;

            if (activate)
            {
                switch (_whowins)
                {
                    case (0):
                        m_RoundWinAngelImage.enabled = true;
                        break;
                    case (1):
                        m_RoundWinReiImage.enabled = true;
                        break;
                    case (2):
                        m_RoundWinShinjiImage.enabled = true;
                        break;
                    case (3):
                        m_RoundWinAsukaImage.enabled = true;
                        break;
                }
            }
        }

        private void GameWinnerImageFunction(int _whowins, bool activate)
        {
            m_GameWinAngelImage.enabled = false;
            m_GameWinReiImage.enabled = false;
            m_GameWinShinjiImage.enabled = false;
            m_GameWinAsukaImage.enabled = false;

            if (activate)
            {
                switch (_whowins)
                {
                    case (0):
                        m_GameWinAngelImage.enabled = true;
                        break;
                    case (1):
                        m_GameWinReiImage.enabled = true;
                        break;
                    case (2):
                        m_GameWinShinjiImage.enabled = true;
                        break;
                    case (3):
                        m_GameWinAsukaImage.enabled = true;
                        break;
                }
            }
        }
    }
}