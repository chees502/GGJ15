using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System;

public class ScoreUtility {
    public class ScoreInfo {
        public string userName;
        public int score;

        public ScoreInfo(string name, int score) {
            this.userName = name;
            this.score = score;
        }
    }

    public const string filepath = "HighScoreXml.xml";

    public const string tagUserName = "UserName";
    public const string tagScore = "Score";

    static public List<ScoreInfo> LoadScores() {
        List<ScoreInfo> scores = new List<ScoreInfo>();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filepath);
        foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes[0]) {
            ScoreInfo info = new ScoreInfo(
                node.Attributes[tagUserName].Value,
                Convert.ToInt32(node.Attributes[tagScore].Value));
            scores.Add(info);
        }

        return scores;
    }

    static public void SaveScores(List<ScoreInfo> scores) {
        Debug.Log(scores.Count);

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("HighScores");
        xmlDoc.AppendChild(rootNode);

        foreach (ScoreInfo info in scores) {
            XmlNode scoreNode = xmlDoc.CreateElement("ScoreInfo");

            XmlAttribute name = xmlDoc.CreateAttribute(tagUserName);
            name.Value = info.userName;
            scoreNode.Attributes.Append(name);

            XmlAttribute score = xmlDoc.CreateAttribute(tagScore);
            score.Value = info.score.ToString();
            scoreNode.Attributes.Append(score);

            rootNode.AppendChild(scoreNode);
        }

        xmlDoc.Save(filepath);
    }
}
