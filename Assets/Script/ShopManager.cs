using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{

    public int imoney = 100;
    public Text Moneytext;
    public Text invotery;
    public Text summary;
    public InputField inputMoney;
    public GameObject Success;
    public GameObject Error;
    public GameObject Stock;
    public InputField inputStock;
    public Dictionary<string, int> itemList = new Dictionary<string, int>();
    public Dictionary<string, int> countList = new Dictionary<string, int>();
    public List<Text> buttonText = new List<Text>();
    private string stock = "";

    void Start()
    {
        Moneytext.text = "Money: " + imoney.ToString();
        itemList.Add("Apple", 100);
        itemList.Add("Armor", 1000);
        itemList.Add("Axe", 300);
        itemList.Add("Archer", 1000);
        itemList.Add("Bag", 300);
        itemList.Add("Belt", 100);
        itemList.Add("Book", 300);
        itemList.Add("boot", 300);
        itemList.Add("bracelet", 300);
        itemList.Add("cloth", 300);
        foreach (KeyValuePair<string, int> i in itemList) {
            countList[i.Key] = 5;
        }
    }

    void Update()
    {
        if (stock == "") return;
        int price = 0;
        int num = int.Parse(inputStock.text);
        price = num * itemList[stock];
        summary.text = "ราคา : " + price.ToString();
    }

    void UpdateInventery()
    {
        string text = "";
        foreach (KeyValuePair<string, int> i in countList) {
            if (i.Value < 5) {
                text += i.Key + " มีอยู่ " + (5 - i.Value).ToString() + " อัน\n";
            }
        }
        invotery.text = text;
    }

    public void openStock(string item)
    {
        Stock.SetActive(true);
        stock = item;
    }

    public void iBuyStock()
    {
        int num = int.Parse(inputStock.text);
        int price = 0;
        int index = 0;
        foreach (KeyValuePair<string, int> i in countList) {
            if (i.Key == stock) {
                price += itemList[i.Key] * num;
                int count = countList[i.Key];
                if (price > imoney || count < num) {
                    error();
                    return;
                }
                imoney -= price;
                countList[i.Key] -= num;
                buttonText[index].text = stock + " " + countList[i.Key] + " อัน " + " " + itemList[i.Key] + " บาท";
                success();
                Moneytext.text = "Money: " + imoney.ToString();
                UpdateInventery();
                Stock.SetActive(false);
            }
            index++;
        }
    }


    public void addmoney()
    {
        imoney += int.Parse(inputMoney.text);
        Moneytext.text = "Money: " + imoney.ToString();
    }

    public void additem(string item)
    {
        int buy = itemList[item];
        if (buy > imoney) {
            error();
            return;
        }
        int index = 0;
        foreach (KeyValuePair<string, int> i in itemList) {
            if (i.Key == item) {
                int count = countList[i.Key];
                if (count <= 0) {
                    error();
                    return;
                }
                countList[i.Key]--;
                imoney -= buy;
                buttonText[index].text = i.Key + " " + countList[i.Key] + " อัน " + " " + i.Value + " บาท";
                success();
            }
            index++;
        }
        Moneytext.text = "Money: " + imoney.ToString();
        UpdateInventery();
    }

    public void success()
    {
        Success.SetActive(true);
    }

    public void error()
    {
        Error.SetActive(true);
    }
}
