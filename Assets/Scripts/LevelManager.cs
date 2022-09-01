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
        double lastResult;

        public void Next()
        {
            SetLevel(currentLevel == gameConfig.Levels.Length ? 1 : currentLevel + 1);
        }

        public void Restart()
        {
            SetLevel(currentLevel);
        }

        void OnIngredientSelected(Ingredient ingredient)
        {
            var restorePosition = ingredient.transform.localPosition;
            ingredientContainer.RestoreIngridient(ingredient, restorePosition);
            blender.AddIngredient(ingredient);
        }
        void OnIngredientAdded(Ingredient ingredient)
        {
            uiManager.SetMixBtn(true);
        }

        void OnMixStarted()
        {
            uiManager.SetMixBtn(false);
            selectingManager.gameObject.SetActive(false);
        }

        void OnResultColor(Color color)
        {
            uiManager.ShowResult(true);
            uiManager.SetResultColor(color);
            float match = ColorTools.CompareColors(color, levelConfig.DesiredColor);
            print(match);
            lastResult = Math.Ceiling(Mathf.Pow(match * 100, 2) / 100);
            uiManager.SetResultPercent((float)lastResult / 100.0f);
            print(lastResult);
        }

        void OnResultShown()
        {
            if (lastResult >= 85)
            {
                uiManager.SetNextBtn(true);
                uiManager.SetWinBackground(true);
            }
            else uiManager.SetRestartBtn(true);
        }

        void SetLevel(int level)
        {
            uiManager.SetWinBackground(false);
            uiManager.ShowResult(false);
            ingredientContainer.Clear();
            blender.Clear();
            currentLevel = level;
            levelConfig = gameConfig.Levels[currentLevel - 1];
            ingredientContainer.SpawnIngridients(levelConfig.Ingredients);
            uiManager.SetDesiredColor(levelConfig.DesiredColor);
            selectingManager.gameObject.SetActive(true);
            uiManager.SetMixBtn(false);
        }

        void Start()
        {
            SetLevel(1);
        }

        void OnEnable()
        {
            selectingManager.IngredientSelected += OnIngredientSelected;
            blender.IngrediendAdded += OnIngredientAdded;
            blender.MixStarted += OnMixStarted;
            blender.Mixed += OnResultColor;
            uiManager.ResultShown += OnResultShown;
        }

        void OnDisable()
        {
            selectingManager.IngredientSelected -= OnIngredientSelected;
            blender.IngrediendAdded -= OnIngredientAdded;
            blender.MixStarted -= OnMixStarted;
            blender.Mixed -= OnResultColor;
            uiManager.ResultShown -= OnResultShown;
        }
    }
}