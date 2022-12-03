using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float energy;
    public int food;
    public int achivements;

    #region Serialization
    /*регион дл€ сохранений*/
    public void SaveData() //метод, который реализует класс, в котором записываютс€ все переменные в этом скрипте, сохран€ютс€ в json и записываютс€ в файл
    {
        Wrapper wrapper = new Wrapper(energy,food,achivements);
        string json = JsonUtility.ToJson(wrapper);
        string saveFile = Application.persistentDataPath + "/saveFile.json";
        File.WriteAllText(saveFile, json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        StreamReader reader = new StreamReader(path);
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(reader.ReadToEnd());
        energy = wrapper.energy;
        food = wrapper.food;
        achivements = wrapper.achivements;
    }
    public class Wrapper
    {
        public Wrapper(
        float energy,
        int food,
        int achivements
        )
        {
            this.energy = energy;
            this.food = food;
            this.achivements = achivements;
        }
        public float energy;
        public int food;
        public int achivements;
    }
    #endregion
}
