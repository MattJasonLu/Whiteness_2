using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public bool isPaused = false;
    public List<RoleUnitDAO> roleUnits = new List<RoleUnitDAO>();

    private void Start()
    {
        _instance = this;
        Init();
    }

    void Init()
    {
        //LoadGame();
        if (roleUnits.Count == 0)
        {
            RoleUnitDAO role1 = new RoleUnitDAO();
            role1.unitId = "P001";
            role1.EXP = 60;
            roleUnits.Add(role1);
        }
    }

    public void OnPause()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void OnResume()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void ReloadScene()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveGame()
    {
        //定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //创建存档文件夹
        if (!IOHelper.IsDirectoryExists(dirpath))
        { 
            IOHelper.CreateDirectory(dirpath);
        }
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";
        //保存数据
        IOHelper.SetData(filename, roleUnits);
        
    }

    public void LoadGame()
    {
        //定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        if (IOHelper.IsDirectoryExists(dirpath))
        {
            //定义存档文件路径
            string filename = dirpath + "/GameData.sav";
            if (IOHelper.IsFileExists(filename))
            {
                //读取数据
                roleUnits = (List<RoleUnitDAO>)IOHelper.GetData(filename, typeof(List<RoleUnitDAO>));
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
