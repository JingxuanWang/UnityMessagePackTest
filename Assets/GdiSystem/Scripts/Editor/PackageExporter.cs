using UnityEngine;
using UnityEditor;
using System.Collections;

namespace GdiSystem.Editor
{
	public static class PackageExporter
	{
		[MenuItem("GDI/Export/ALL")]
		static void ExportAll()
		{
			ExportGdiSystemSceneLoad();
			ExportGdiSystemState();
			ExportGdiSystemDisplay();
			ExportGdiSystemTouch();
			ExportGdiSystemController();
			ExportGdiSystemAllInOne();
		}

		[MenuItem("GDI/Export/SceneLoad")]
		static void ExportGdiSystemSceneLoad()
		{
			AssetDatabase.ExportPackage(
				new string[]
				{
					"Assets/GdiSystem/Scripts",
					"Assets/GdiSystem/SceneLoad"
				},
				"SceneLoad.unitypackage",
				ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
			);
		}

		[MenuItem("GDI/Export/State")]
		static void ExportGdiSystemState()
		{
			AssetDatabase.ExportPackage(
				new string[]
				{
					"Assets/GdiSystem/Scripts",
					"Assets/GdiSystem/State"
				},
				"State.unitypackage",
				ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
			);
		}

		[MenuItem("GDI/Export/Display")]
		static void ExportGdiSystemDisplay()
		{
			AssetDatabase.ExportPackage(
				new string[]
				{
					"Assets/GdiSystem/Scripts",
					"Assets/GdiSystem/Display"
				},
				"Display.unitypackage",
				ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
			);
		}

		[MenuItem("GDI/Export/Touch")]
		static void ExportGdiSystemTouch()
		{
			AssetDatabase.ExportPackage(
				new string[]
				{
					"Assets/GdiSystem/Scripts",
					"Assets/GdiSystem/Touch"
				},
				"Touch.unitypackage",
				ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
			);
		}

		[MenuItem("GDI/Export/Controller")]
		static void ExportGdiSystemController()
		{
			AssetDatabase.ExportPackage(
				new string[]
				{
					"Assets/GdiSystem/Scripts",
					"Assets/GdiSystem/Touch",
					"Assets/GdiSystem/Controllers",
					"Assets/NGUI"
				},
				"Controllers.unitypackage",
				ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
			);
		}

		[MenuItem("GDI/Export/GdiSystemAllInOne")]
		static void ExportGdiSystemAllInOne()
		{
			AssetDatabase.ExportPackage(
				new string[]
				{
					"Assets/GdiSystem", 
					"Assets/NGUI"
				},
				"AllInOne.unitypackage",
				ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
			);
		}
	}
}