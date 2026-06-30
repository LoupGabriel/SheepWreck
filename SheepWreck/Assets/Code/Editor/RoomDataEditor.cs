
using UnityEditor;
using UnityEngine;
using static RoomData;



[CustomEditor(typeof(RoomData))]
public class RoomDataEditor : Editor
{



    SerializedProperty m_buildingTypeProp;

    


    SerializedProperty m_productionRateProp;
    SerializedProperty m_energyConsumptionProp;
    SerializedProperty m_capacityProp;


    private void OnEnable()
    {
        m_buildingTypeProp = serializedObject.FindProperty("m_buildingType");
        m_productionRateProp = serializedObject.FindProperty("m_productionRate");
        m_energyConsumptionProp = serializedObject.FindProperty("m_energyConsumption");
        m_capacityProp = serializedObject.FindProperty("m_capacity");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(
       serializedObject,
        "m_Script",
        "m_productionRate",
        "m_energyConsumption",
        "m_capacity"
    );
        // Always show the Enum selection dropdown first
        EditorGUILayout.PropertyField(m_buildingTypeProp);
        EditorGUILayout.Space();

        EBuildingType currentType = (EBuildingType)m_buildingTypeProp.enumValueIndex;
        switch (currentType)
        {
            case EBuildingType.PRODUCE:
                EditorGUILayout.LabelField("Production Settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_productionRateProp);
                EditorGUILayout.PropertyField(m_energyConsumptionProp);
                EditorGUILayout.PropertyField(m_capacityProp);
                break;

            case EBuildingType.STOCK:
                EditorGUILayout.LabelField("Stockage Settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_capacityProp);
               
                break;

              
        }
        serializedObject.ApplyModifiedProperties();

    }


   
}
