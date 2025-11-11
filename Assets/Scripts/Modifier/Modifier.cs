using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static EnemyCar;
using static ModifierData;

public class ModifierData
{
    public enum OperatorType
    {
        HighSpeed, SlowMode, SuddenDeath, FullHealth, Coins, Inverted
    }

    public OperatorType[] Operator;
    public string[] Text;
    public int CorrectIndex;

    public ModifierData(OperatorType[] op, string[] text, int correctIndex)
    {
        Operator = op;
        Text = text;
        CorrectIndex = correctIndex;
    }
}
public class Modifier : MonoBehaviour
{
    private ModifierData GenerateModifier()
    {
        List<int> ints = new List<int>();
        ints.Add(0);ints.Add(1);ints.Add(2);ints.Add(3);ints.Add(4);ints.Add(5);
        OperatorType[] op = new OperatorType[3];
        string[] point = new string[3];

        int correctIndex = Random.Range(0, point.Length);

        for (int i = 0; i < point.Length; i++)
        {
            int index = Random.Range(0, ints.Count);
            op[i] = (OperatorType)ints[index];
            ints.RemoveAt(index);
            switch (op[i])
            {
                case OperatorType.HighSpeed:
                    point[i] = "2x Speed\n2x Score";
                    break;
                case OperatorType.SlowMode:
                    point[i] = "Half Speed\nHalf Score";
                    break;
                case OperatorType.SuddenDeath:
                    point[i] = "1 Health\n2x Score";
                    break;
                case OperatorType.FullHealth:
                    point[i] = "Full Health";
                    break;
                case OperatorType.Coins:
                    point[i] = "10 Coins";
                    break;
                case OperatorType.Inverted:
                    point[i] = "Inverted\n2x Score";
                    break;
            }
        }
        return new ModifierData(op, point, correctIndex);
    }
    public GameObject AnswerGatePrefab;

    // Start is called before the first frame update
    void Start()
    {
        ModifierData modifier = GenerateModifier();
        float x = 2.9f;
        for (int i = 0; i < modifier.Text.Length; i++)
        {
            GameObject go = Instantiate(AnswerGatePrefab,this.transform);
            go.GetComponent<Answer>().Initialize(modifier.Text[i], modifier.CorrectIndex == i, (int)modifier.Operator[i]);
            go.transform.localPosition = new Vector3(2.35f + x, 0, 20.23712f);
            go.transform.localRotation = Quaternion.Euler(270,0,0);
            go.transform.localScale = new Vector3(1.15f, 3, 1);  
            x += 2.9f;
        }
    }
}
