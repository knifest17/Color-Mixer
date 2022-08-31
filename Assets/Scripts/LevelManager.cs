using System;
using System.Linq;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] SelectingManager selectingManager;
        [SerializeField] Blender blender;
        [SerializeField] UIManager uiManager;
        [SerializeField] IngridientContainer ingredientContainer;
        [SerializeField] GameConfigSO gameConfig;

        int currentLevel;
        LevelSO levelConfig;

        void OnIngredientSelected(Ingredient ingredient)
        {
            ingredientContainer.RestoreIngridient(ingredient);
        }
        void OnIngredientAdded(Ingredient ingredient)
        {

        }

        void OnResultColor(Color color)
        {
            uiManager.ShowResult(true);
            uiManager.SetResultColor(color);
            float match = ColorTools.CompareColors(color, levelConfig.DesiredColor);
            print(match);
            var compliancePercent = Math.Ceiling(Mathf.Pow(match * 100, 2) / 100);
            uiManager.SetResultPercent((float)compliancePercent / 100.0f);
            print(compliancePercent);
            //if (compliancePercent >= 85) SetLevel(currentLevel + 1);
        }


        void SetLevel(int level)
        {
            ingredientContainer.Clear();
            blender.Clear();
            currentLevel = level;
            levelConfig = gameConfig.Levels[currentLevel - 1];
            ingredientContainer.SpawnIngridients(levelConfig.Ingredients);
            uiManager.SetDesiredColor(levelConfig.DesiredColor);
        }

        void Start()
        {
            SetLevel(3);
        }

        void OnEnable()
        {
            selectingManager.IngredientSelected += OnIngredientSelected;
            blender.IngrediendAdded += OnIngredientAdded;
            blender.Mixed += OnResultColor;
        }

        void OnDisable()
        {
            selectingManager.IngredientSelected -= OnIngredientSelected;
            blender.IngrediendAdded -= OnIngredientAdded;
            blender.Mixed -= OnResultColor;
        }
    }
}