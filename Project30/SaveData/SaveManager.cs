using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Project30
{
    class SaveManager
    {
        private static readonly string GameStateFileName = "GameStateSaveData.xml";
        private static readonly string SettingsSaveFileName = "SettingsSaveData.xml";
        private static readonly string ScoreFileName = "ScoreData.xml";

        private static readonly int MaxScoreSaveFiles = 100;

        public static void Save(GameStateSaveData saveData)
        {
            using StreamWriter streamWriter = new StreamWriter(new FileStream(GameStateFileName, FileMode.Create));
            var Serializer = new XmlSerializer(typeof(GameStateSaveData));

            Serializer.Serialize(streamWriter, saveData);
        }

        public static void SaveSettingsData(SettingsSaveData settingsSaveData)
        {
            SettingsSaveDataZipped NextSettingsSaveDataZipped = new SettingsSaveDataZipped()
            {
                KeysCastingSaveDatas = new List<KeyCastingSaveData>(),
                ScreenWidth = settingsSaveData.ScreenWidth,
                ScreenHeight = settingsSaveData.ScreenHeight,
                IsEnableHardDrop = settingsSaveData.IsEnableHardDrop,
            };
            foreach (KeyValuePair<string, Keys> kvp in settingsSaveData.Keys)
            {
                KeyCastingSaveData NextKeyCastingSaveData = new KeyCastingSaveData()
                {
                    KeyName = kvp.Key,
                    Key = kvp.Value,
                };

                NextSettingsSaveDataZipped.KeysCastingSaveDatas.Add(NextKeyCastingSaveData);
            }

            using StreamWriter streamWriter = new StreamWriter(new FileStream(SettingsSaveFileName, FileMode.Create));
            var Serializer = new XmlSerializer(typeof(SettingsSaveDataZipped));  

            Serializer.Serialize(streamWriter, NextSettingsSaveDataZipped);
        }

        public static void SaveScoreData(ScoreData nextScoreData)
        {
            SaveScoreData SaveScoreData = LoadScoreData();
            int ScoresCount = SaveScoreData.ScoreDatas.Count;

            if (nextScoreData.Score == 0)
            {
                return;
            }

            if (ScoresCount == 0)
            {
                SaveScoreData.ScoreDatas.Add(nextScoreData);
            }
            else
            {
                int NextScore = nextScoreData.Score;
                for (int i = 0; i < ScoresCount; i++)
                {
                    int CurrentScore = SaveScoreData.ScoreDatas[i].Score;
                    if (NextScore >= CurrentScore)
                    {
                        SaveScoreData.ScoreDatas.Insert(i, nextScoreData);
                        break;
                    }
                    
                    if (i == ScoresCount - 1)
                    {
                        SaveScoreData.ScoreDatas.Add(nextScoreData);
                        break;
                    }
                }
            }

            while (SaveScoreData.ScoreDatas.Count > MaxScoreSaveFiles)
            {
                SaveScoreData.ScoreDatas.RemoveAt(SaveScoreData.ScoreDatas.Count - 1);
            }

            using StreamWriter streamWriter = new StreamWriter(new FileStream(ScoreFileName, FileMode.Create));
            var Serializer = new XmlSerializer(typeof(SaveScoreData));

            Serializer.Serialize(streamWriter, SaveScoreData);
        }

        public static void DeleteScoreData(int saveSlotIndex)
        {
            SaveScoreData SaveScoreData = LoadScoreData();

            SaveScoreData.ScoreDatas.RemoveAt(saveSlotIndex);

            using StreamWriter streamWriter = new StreamWriter(new FileStream(ScoreFileName, FileMode.Create));
            var Serializer = new XmlSerializer(typeof(SaveScoreData));

            Serializer.Serialize(streamWriter, SaveScoreData);
        }

        public static int LoadHighScore()
        {
            SaveScoreData SaveScoreData = LoadScoreData();

            if (SaveScoreData.ScoreDatas.Count == 0)
            {
                return 0;
            }

            return SaveScoreData.ScoreDatas[0].Score;
        }

        public static SaveScoreData LoadScoreData()
        {
            if (!File.Exists(ScoreFileName))
            {
                SaveScoreData NextSaveScoreData = new SaveScoreData()
                {
                    ScoreDatas = new List<ScoreData>()
                };

                return NextSaveScoreData;
            }

            using StreamReader streamWriter = new StreamReader(new FileStream(ScoreFileName, FileMode.Open));
            var Serializer = new XmlSerializer(typeof(SaveScoreData));

            var SaveScoreData = (SaveScoreData)Serializer.Deserialize(streamWriter);

            return SaveScoreData;
        }

        public static SettingsSaveData LoadSettingsSaveData()
        {
            if (!File.Exists(SettingsSaveFileName))
            {
                SettingsSaveData NewSettingsSaveData = GetDefaultSettingsSaveData();
                
                return NewSettingsSaveData;
            }

            using StreamReader streamWriter = new StreamReader(new FileStream(SettingsSaveFileName, FileMode.Open));
            var Serializer = new XmlSerializer(typeof(SettingsSaveDataZipped));

            var SettingsSaveDataZipped = (SettingsSaveDataZipped)Serializer.Deserialize(streamWriter);

            return new SettingsSaveData(SettingsSaveDataZipped);
        }

        public static SettingsSaveData GetDefaultSettingsSaveData()
        {
            SettingsSaveData NewSettingsSaveData = new SettingsSaveData()
            {
                ScreenWidth = 1600,
                ScreenHeight = 900,

                Keys = new Dictionary<string, Keys>()
                    {
                        { "UpPlayer1", Keys.Up },
                        { "DownPlayer1", Keys.Down },
                        { "LeftPlayer1", Keys.Left },
                        { "RightPlayer1", Keys.Right },
                        { "Button1Player1", Keys.RightShift },
                        { "Button2Player1", Keys.OemQuestion },

                        { "UpPlayer2", Keys.W },
                        { "DownPlayer2", Keys.S },
                        { "LeftPlayer2", Keys.A },
                        { "RightPlayer2", Keys.D },
                        { "Button1Player2", Keys.E },
                        { "Button2Player2", Keys.Q },
                    },

                IsEnableHardDrop = true,
            };

            return NewSettingsSaveData;
        }

        public static GameStateSaveData Load()
        {
            if (!File.Exists(GameStateFileName))
            {
                Random Random = new Random();

                GameStateSaveData NewSaveData = new GameStateSaveData()
                {
                    Lines = 0,
                    Score = 0,
                    BoardState = null,
                    PieceType = Random.Next(1, 8),
                    NextPieceType = Random.Next(1, 8),
                    TetrisLines = 0,
                };

                return NewSaveData;
            }

            using StreamReader streamWriter = new StreamReader(new FileStream(GameStateFileName, FileMode.Open));
            var Serializer = new XmlSerializer(typeof(GameStateSaveData));

            var SaveData = (GameStateSaveData)Serializer.Deserialize(streamWriter);

            return SaveData;
        }
    }
}
