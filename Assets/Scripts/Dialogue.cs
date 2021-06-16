using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class Dialogue
{
    public Node currentNode;
    private int id;
    private Node[] playerNodes, patientNodes;
    private string[] playerDialogue, patientDialogue;

    public Dialogue(int id)
    {
        this.id = id;
    }

    private Node ParsePatientNode(int index)
    {
        string line = patientDialogue[index];
        string[] parsedLine = line.Split(';');
        string word = parsedLine[0];
        Node patientNode = new Node(Node.Owner.PATIENT, word, 0);
        if (parsedLine.Length != 1)
        {
            string[] parsedData = parsedLine[1].Split('#');
            for (int i = 0; i < parsedData.Length; i++)
            {
                int answerIndex = int.Parse(parsedData[i]);
                Node answer = playerNodes[answerIndex];
                if (answer == null)
                {
                    answer = ParsePlayerNode(answerIndex);
                }
                patientNode.AddAnswer(answer);
            }
        }
        patientNodes[index] = patientNode;
        return patientNode;
    }

    private Node ParsePlayerNode(int index)
    {
        string[] parsedPlayerNodeData = playerDialogue[index].Split(';');
        string playerNodeWord = parsedPlayerNodeData[0];
        Node playerNode = new Node(Node.Owner.PLAYER, playerNodeWord, int.Parse(parsedPlayerNodeData[1]));
        int answerIndex = int.Parse(parsedPlayerNodeData[2]);
        Node answer = patientNodes[answerIndex];
        if (answer == null)
            answer = ParsePatientNode(answerIndex);
        playerNode.AddAnswer(answer);
        playerNodes[index] = playerNode;
        return playerNode;
    }

    public IEnumerator ReadDialogue()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            string patientPath = "jar:file://" + Application.dataPath + "!assets" + "/Dialogues/Dialogue_" + id + "/Patient.txt";
            string playerPath = "jar:file://" + Application.dataPath + "!assets" + "/Dialogues/Dialogue_" + id + "/Player.txt";

            UnityWebRequest patientRequest = UnityWebRequest.Get(patientPath);
            yield return patientRequest.SendWebRequest();
            string patientText = patientRequest.downloadHandler.text;
            UnityWebRequest playerRequest = UnityWebRequest.Get(playerPath);
            yield return playerRequest.SendWebRequest();
            string playerText = playerRequest.downloadHandler.text;

            patientDialogue = patientText.Split('\n');
            playerDialogue = playerText.Split('\n');
            
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string patientPath = Application.dataPath + "/Raw/Dialogues/Dialogue_" + id + "/Patient.txt";
            string playerPath = Application.dataPath + "/Raw/Dialogues/Dialogue_" + id + "/Player.txt";
            patientDialogue = File.ReadAllLines(patientPath);
            playerDialogue = File.ReadAllLines(playerPath);
        }
        else
        {
            string patientPath = Application.streamingAssetsPath + "/Dialogues/Dialogue_" + id + "/Patient.txt";
            string playerPath = Application.streamingAssetsPath + "/Dialogues/Dialogue_" + id + "/Player.txt";
            patientDialogue = File.ReadAllLines(patientPath);
            playerDialogue = File.ReadAllLines(playerPath);

        }


        playerNodes = new Node[playerDialogue.Length];
        patientNodes = new Node[patientDialogue.Length];
        ParsePatientNode(0);
        SetCurrentNode(patientNodes[0]);
    }
    public void SetCurrentNode(Node node)
    {

        currentNode = node;
        if (currentNode != null)
        {
            Singleton.SPEECH.SetText(node.GetWord());
            Singleton.SELECTION.SetAnswers(node.GetAnswers());
        }

        else
            Singleton.SPEECH.SetText("");

    }
    public class Node
    {
        public Owner owner;
        private string word;
        private Node[] answers;
        private int numberOfAnswers = 0;
        private int moralePoint;

        public Node(Owner owner, string word, int moralePoint)
        {
            this.owner = owner;
            this.word = word;
            this.answers = new Node[2];
            this.moralePoint = moralePoint;
        }

        public void AddAnswer(Node answer)
        {
            if (numberOfAnswers < answers.Length)
            {
                answers[numberOfAnswers] = answer;
                numberOfAnswers++;
            }
        }

        public Node GetAnswer() { return answers[0]; }
        public Node[] GetAnswers()
        {
            Node[] result = new Node[answers.Length];
            answers.CopyTo(result, 0);
            return result;
        }
        public int GetMoralePoint()
        {
            return moralePoint;
        }
        public string GetWord()
        {
            return (string)word.Clone();
        }
        public enum Owner { PATIENT, PLAYER }

    }
}
