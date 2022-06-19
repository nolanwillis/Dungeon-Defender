using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataFileHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionSecret = "snorlax";

    // Constructor
    public DataFileHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        // Use Path.Combine, other OS's use different path syntax
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // Load the serialized data from the file
                string dataToLoad = "";
                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }
                // Encrypt data if enabled
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // Deserialize data (JSON -> C#)
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured when trying to load data to file: "
                    + fullPath + "\n" + ex);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        // Use Path.Combine, other OS's use different path syntax
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // Create directory path file will be written to if no data file exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            // Serialize game data (C# -> JSON)
            string dataToStore = JsonUtility.ToJson(data, true);
            // Encrypt data if enabled
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            // Write the serialized data to the file
            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error occured when trying to save data to file: "
                + fullPath + "\n" + ex);
        }
    }

    // XOR encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionSecret
                [i % encryptionSecret.Length]);
        }
        return modifiedData;
    }
}
