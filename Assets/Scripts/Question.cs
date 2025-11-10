using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static EnemyCar;
using static QuestionData;

public class QuestionData
{
    public enum OperatorType
    {
        HighSpeed, SlowMode, SuddenDeath, FullHealth, Coins, Inverted
    }

    public OperatorType[] Operator;
    public string[] Answers;
    public int CorrectAnswerIndex;

    public QuestionData(OperatorType[] op, string[] answers, int correctIndex)
    {
        Operator = op;
        Answers = answers;
        CorrectAnswerIndex = correctIndex;
    }
}
public class Question : MonoBehaviour
{
    private QuestionData GenerateQuestion()
    {
        List<int> ints = new List<int>();
        ints.Add(0);ints.Add(1);ints.Add(2);ints.Add(3);ints.Add(4);ints.Add(5);
        OperatorType[] op = new OperatorType[3];
        string[] answers = new string[3];

        int correctAnswerIndex = Random.Range(0, answers.Length);

        for (int i = 0; i < answers.Length; i++)
        {
            int index = Random.Range(0, ints.Count);
            op[i] = (OperatorType)ints[index];
            ints.RemoveAt(index);
            switch (op[i])
            {
                case OperatorType.HighSpeed:
                    answers[i] = "2x Speed\n2x Score";
                    break;
                case OperatorType.SlowMode:
                    answers[i] = "Half Speed\nHalf Score";
                    break;
                case OperatorType.SuddenDeath:
                    answers[i] = "1 Health\n2x Score";
                    break;
                case OperatorType.FullHealth:
                    answers[i] = "Full Health";
                    break;
                case OperatorType.Coins:
                    answers[i] = "10 Coins";
                    break;
                case OperatorType.Inverted:
                    answers[i] = "Inverted\n2x Score";
                    break;
            }
        }
        return new QuestionData(op, answers, correctAnswerIndex);
    }
    public GameObject AnswerGatePrefab;

    // Start is called before the first frame update
    void Start()
    {
        QuestionData question = GenerateQuestion();
        float x = 2.9f;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            GameObject go = Instantiate(AnswerGatePrefab,this.transform);
            go.GetComponent<Answer>().Initialize(question.Answers[i], question.CorrectAnswerIndex == i, (int)question.Operator[i]);
            go.transform.localPosition = new Vector3(2.35f + x, 0, 20.23712f);
            go.transform.localRotation = Quaternion.Euler(270,0,0);
            go.transform.localScale = new Vector3(1.15f, 3, 1);  
            x += 2.9f;
        }
    }
}
