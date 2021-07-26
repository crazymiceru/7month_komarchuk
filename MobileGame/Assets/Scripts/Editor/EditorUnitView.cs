using UnityEditor;
using UnityEngine;

namespace MobileGame
{
    [CustomEditor(typeof(UnitView))]
    public class EditorUnitView : Editor
    {
        private IUnitView _unitView;
        private void OnEnable()
        {
            _unitView = target as IUnitView;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (_unitView != null)
            {
                float win = Screen.width;

                var typeItem = _unitView.GetTypeItem();
                var build = UtilsUnit.ParseType(typeItem.type).SetNumCfg(typeItem.cfg);
                EditorGUI.BeginDisabledGroup(true);
                foreach (var item in build.GetDataName())
                {
                    var value = LoadResources.GetValue<ScriptableObject>(item);
                    if (value != null) EditorGUILayout.ObjectField("Script", value, typeof(ScriptableObject), false);
                    else EditorGUILayout.LabelField("Not Load:", item);
                }
                EditorGUI.BeginDisabledGroup(false);
            }
        }
    }
}