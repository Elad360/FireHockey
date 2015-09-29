using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] Players;
        public Text ScoreText;
        public int MaxPlayerScore = 2;

        private int[] _playersScore = new int[2];
        private List<Vector3> _playersPosition;
        private List<Quaternion> _playersRotation;
        private int _currentPlayerId;

        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        _instance = new GameManager();
                    }
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        private static GameManager CreateGameManager()
        {
            var obj = new GameObject("GameManager");
            var manager = obj.AddComponent<GameManager>();
            return manager;
        }

        protected void Awake()
        {
            _currentPlayerId = Random.Range(0, 2);
            _playersPosition = new List<Vector3>();
            _playersRotation = new List<Quaternion>();
            foreach (var player in Players)
            {
                _playersPosition.Add(player.transform.localPosition);
                _playersRotation.Add(player.transform.localRotation);
            }
        }

        protected void Start()
        {
            UpdateInteractivePlayers();
        }

        protected void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                ResetSession();
            }
        }

        public void UpdateScore(int playerId)
        {
            var opponentId = GetOpponent(playerId);
            _playersScore[opponentId]++;
            ScoreText.text = _playersScore[0] + " : " + _playersScore[1];
            if (_playersScore[opponentId] >= MaxPlayerScore)
            {
                ResetScore();
            }
           
            ResetSession();
            SwitchTurn(playerId);
        }

        public void ResetScore()
        {
            _playersScore[0] = _playersScore[1] = 0;
            ScoreText.text = _playersScore[0] + " : " + _playersScore[1];
        }

        public void SwitchTurn()
        {
            _currentPlayerId = GetOpponent();
            UpdateInteractivePlayers();
        }

        public void SwitchTurn(int playerId)
        {
            _currentPlayerId = playerId;
            UpdateInteractivePlayers();
        }

        private void UpdateInteractivePlayers()
        {
            for (int i = 0; i < Players.Length; i++)
            {
                var playerController = Players[i].GetComponent<OnDragPlayer>();
                if (Players[i].tag == "Player" + _currentPlayerId)
                {
                    playerController.IsInteractive = true;
                }
                else if (Players[i].tag == "Player" + GetOpponent())
                {
                    playerController.IsInteractive = false;
                }
            }
        }

        private int GetOpponent()
        {
            return _currentPlayerId == 0 ? 1 : 0;
        }

        private int GetOpponent(int playerId)
        {
            return playerId == 0 ? 1 : 0;
        }

        private void ResetSession()
        {
            for (int i = 0; i < Players.Length; i++)
            {
                Players[i].transform.localPosition = _playersPosition[i];
                Players[i].transform.localRotation = _playersRotation[i];
                Players[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Players[i].GetComponent<Rigidbody2D>().angularVelocity = 0f;
            }
        }
    }
}
