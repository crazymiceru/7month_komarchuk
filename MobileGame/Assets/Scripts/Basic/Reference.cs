﻿using UnityEngine;

namespace MobileGame
{
    public static class Reference
    {
        internal static Camera MainCamera
        {
            get => _mainCamera != null ? _mainCamera : _mainCamera = Camera.main;
        }
        private static Camera _mainCamera;

        internal static Transform ActiveElements => _trash != null ? _trash : _trash = GameObject.FindObjectOfType<TagFolderActiveElements>().transform;
        private static Transform _trash;


        //internal static GameM GameM => _gameM != null ? _gameM : _gameM = new GameM();
        //private static GameM _gameM;

        internal static Transform Canvas => _canvas != null ? _canvas : _canvas = GameObject.FindObjectOfType<TagCanvas>().transform;
        private static Transform _canvas;
    }
}