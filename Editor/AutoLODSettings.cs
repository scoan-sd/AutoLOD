using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.AutoLOD.Utilities;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Unity.AutoLOD
{
	[FilePath("ProjectSettings/AutoLODSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public class AutoLODSettings : ScriptableSingleton<AutoLODSettings>
    {
        public int m_maxExecutionTime = 16; // ms
        public string m_meshSimplifierType;
        public string m_batcherType;
        public int m_maxLOD = 2;
        public bool m_generateOnImport = false;
        public bool m_saveAssets = true;
        public int m_initialLODMaxPolyCount = 200000;
        public bool m_sceneLODEnabled = false;
        public bool m_showVolumeBounds = false;
        
        public int maxExecutionTime
        {
            set
            {
                m_maxExecutionTime = value;
				Save(true);
            }
            get { return m_maxExecutionTime; }
        }
        
        public Type meshSimplifierType
        {
            set
            {
                if (typeof(IMeshSimplifier).IsAssignableFrom(value))
                    m_meshSimplifierType = value.AssemblyQualifiedName;
                else if (value == null)
                    m_meshSimplifierType = null;
				Save(true);
            }
            get
            {
				Type type = null;
				if (m_meshSimplifierType != null)
				{
					type = Type.GetType(m_meshSimplifierType);
                	if (type == null && AutoLOD.meshSimplifiers.Count > 0)
                    	type = Type.GetType(AutoLOD.meshSimplifiers[0].AssemblyQualifiedName);
				}
                return type;
            }
        }

        public Type batcherType
        {
            set
            {
                if (typeof(IBatcher).IsAssignableFrom(value))
                    m_batcherType = value.AssemblyQualifiedName;
                else if (value == null)
                    m_batcherType = null;
				Save(true);
            }
            get
            {
				Type type = null;
				if (m_batcherType != null)
				{
					type = Type.GetType(m_batcherType);
                	if (type == null && AutoLOD.batchers.Count > 0)
                    	type = Type.GetType(AutoLOD.batchers[0].AssemblyQualifiedName);
				}
                return type;
            }
        }

        public int maxLOD
        {
            set
            {
                m_maxLOD = value;
				Save(true);
            }
            get { return m_maxLOD; }
        }

        public bool generateOnImport
        {
            set
            {
                m_generateOnImport = value;
				Save(true);
            }
            get { return m_generateOnImport; }
        }

        public bool saveAssets
        {
            set
            {
                m_saveAssets = value;
				Save(true);
            }
            get { return m_saveAssets; }
        }

        public int initialLODMaxPolyCount
        {
            set
            {
                m_initialLODMaxPolyCount = value;
				Save(true);
            }
            get { return m_initialLODMaxPolyCount; }
        }

        public bool sceneLODEnabled
        {
            set
            {
                m_sceneLODEnabled = value;
				Save(true);
            }
            get { return m_sceneLODEnabled; }
        }

        public bool showVolumeBounds
        {
            set
            {
                m_showVolumeBounds = value;
				Save(true);
            }
            get { return m_showVolumeBounds; }
        }
    }
}