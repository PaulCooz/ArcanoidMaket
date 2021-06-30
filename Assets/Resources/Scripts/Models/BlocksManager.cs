using System;
using Resources.Scripts.Libs;
using Resources.Scripts.Models;
using UnityEngine;
using UnityEngine.Serialization;

public class BlocksManager : MonoBehaviour
{
    public LevelManager levelManager;
    public BlocksGrid blocksGrid;

    private void Start()
    {
        NextLevel();
    }

    private void NextLevel()
    {
        var nextData = levelManager.GetNextLevel();
        blocksGrid.SetNewGrid(nextData);
    }
}
