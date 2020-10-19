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


        private void Start()
        {
            FindSceneManager();
            //Init all Wins Image to disable to in future activate one to one depending which player wins the round
            WhichEVA = m_DaddyManager.GetComponent<ManagerScenes>().EVAChoose;
            // Create the delays so they only have to be made once.
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);

            SpawnAllEVAS();
            InitWinImageUI();
            SetCameraTargets();

            PutUIElmentsWell();
            // Once the tanks have been created and the camera is using them as targets, start the game.

            StartCoroutine(GameLoop());
        }

        private void FindSceneManager()
        {
            m_DaddyManager = GameObject.Find("DaddyManager");
        }

        private void InitWinImageUI()
        {
            for (int i = 0; i < m_WinsPlayer1.Length; i++)
            {
                m_WinsPlayer1[i].overrideSprite = m_EVAS[0].m_EVAInfo.WinImage;
                m_WinsPlayer1[i].enabled = false;
            }
            for (int i = 0; i < m_WinsPlayer2.Length; i++)
            {
                m_WinsPlayer2[i].overrideSprite = m_EVAS[WhichEVA].m_EVAInfo.WinImage;
                m_WinsPlayer2[i].enabled = false;
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

            // For each of these transforms...
            //for (int i = 0; i < targets.Length; i++)
            //{
            //    // ... set it to the appropriate tank transform.
            //    targets[i] = m_EVAS[i].m_Instance.transform;
            //}

            // These are the targets the camera should follow.
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

        private void PlusUIWinnerImage(int _playernumber)
        {
            if (_playernumber == 1)
            {
                for (int i = 0; i < m_WinsPlayer1.Length; i++)
                {
                    if (m_WinsPlayer1[i].IsActive()) continue;
                    else
                    {
                        m_WinsPlayer1[i].enabled = true;
                        return;
                    }
                }
            }
            else if (_playernumber == 2)
            {
                for (int i = 0; i < m_WinsPlayer2.Length; i++)
                {
                    if (m_WinsPlayer2[i].IsActive()) continue;
                    else
                    {
                        m_WinsPlayer2[i].enabled = true;
                        return;
                    }
                }
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
                m_RoundWinner.PlayDance(1);
            }

            // Now the winner's score has been incremented, see if someone has one the game.
            m_GameWinner = GetGameWinner();

            // Get a message based on the scores and whether or not there is a game winner and display it.
            string message = EndMessage();
            m_MessageText.text = message;

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
                  return m_EVAS[0];

            else if (m_EVAS[WhichEVA].m_Instance.activeSelf)
                return m_EVAS[WhichEVA];

            // If none of the tanks are active it is a draw so return null.
            return null;
        }


        // This function is to find out if there is a winner of the game.
        private EVAManager GetGameWinner()
        {

            if (m_EVAS[0].m_Wins == m_NumRoundsToWin)
                return m_EVAS[0];
            else if (m_EVAS[WhichEVA].m_Wins == m_NumRoundsToWin)
                return m_EVAS[WhichEVA];

            // If no tanks have enough rounds to win, return null.
            return null;
        }


        // Returns a string message to display at the end of each round.
        private string EndMessage()
        {
            // By default when a round ends there are no winners so the default end message is a draw.
            string message = "DRAW!";

            // If there is a winner then change the message to reflect that.
            if (m_RoundWinner != null)
                message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

            // Add some line breaks after the initial message.
            message += "\n\n\n\n";

            // Go through all the tanks and add each of their scores to the message.
            message += m_EVAS[0].m_ColoredPlayerText + ": " + m_EVAS[0].m_Wins + " WINS\n";
            message += m_EVAS[WhichEVA].m_ColoredPlayerText + ": " + m_EVAS[WhichEVA].m_Wins + " WINS\n";


            // If there is a game winner, change the entire message to reflect that.
            if (m_GameWinner != null)
                message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

            return message;
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
    }
}