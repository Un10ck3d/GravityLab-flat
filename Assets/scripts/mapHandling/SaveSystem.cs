using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveMap(int[,] Iblocks){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Map1.map";
        FileStream stream = new FileStream(path, FileMode.Create);

        Map data = new Map(Iblocks);

        formatter.Serialize(stream, data);

        stream.Close();

    }

    public static int[,] LoadMap(){

        string path = Application.persistentDataPath + "/Map1.map";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int[,] map = formatter.Deserialize(stream) as int[,];
            stream.Close();
            return map;
        }

        else{
            return new int[0,0];
        }

    }
    public static void WriteString(string name, string str)
    {
        string path = "Assets/MapsData/" + name + ".txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(str);
        writer.Close();
        //Re-import the file to update the reference in the editor
    }

    public static bool removeFile(string name){
        string path = "Assets/MapsData/" + name + ".txt";
        if(!File.Exists(path)) return false;

        File.Delete(path);

        return !File.Exists(path);


    }
    public static string ReadString(string name)
    {

        string path = "Assets/MapsData/" + name + ".txt";
        if(!File.Exists(path)) return "0 0 0 0";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        string data = reader.ReadToEnd();
        reader.Close();
        return data;
    }

    public static string[] getAllSavedMapNames(){
        string[] paths = Directory.GetFiles("Assets/MapsData/", "*.txt");
        string[] names = new string[paths.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            names[i] = Path.GetFileName(paths[i].Replace(".txt", ""));
            // Debug.Log(names[i]); 
        }
        return names;
        // return paths;
    }

    public static string getDeafultMapName(){
        return getAllSavedMapNames()[0];
    }

}
