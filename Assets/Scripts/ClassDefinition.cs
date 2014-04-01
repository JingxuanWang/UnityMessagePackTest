using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MsgPack;
using MsgPack.Serialization;
using GdiSystem.Display;

/// <summary>
/// Data class that implementing IPackable / IUnpackable interface.
/// </summary>
public class DataClass // : IPackable, IUnpackable
{
	public string data;
	public int count;

	public void UnpackFromMessage (Unpacker unpacker)
	{
		unpacker.ReadInt32(out count);
		unpacker.ReadString(out data);
	}

	public void PackToMessage (Packer packer, PackingOptions options)
	{
		packer.Pack(count);
		packer.PackString(data);
	}

	public void Dump()
	{
		Debug.Log(this.count);
		Debug.Log(this.data);

		DisplayViewManager.SetParameter<string>("data", this.data);
		DisplayViewManager.SetParameter<int>("count", this.count);
	}
}

/// <summary>
/// Base class.
/// </summary>
public class BaseClass
{
	public string data;
	public int count;

	public SubClassA a;

	public List<string> stringList = new List<string>();
	public List<SubClassB> bList = new List<SubClassB>();

	public void Init()
	{
		this.data = "hoge";
		this.count = 100;

		this.a = new SubClassA();
		this.a.Init();

		this.stringList.Add("This");
		this.stringList.Add("Is");
		this.stringList.Add("A");
		this.stringList.Add("Test");

		this.bList.Add(new SubClassB{ name = "Hello" });
		this.bList.Add(new SubClassB{ name = "World" });
	}

	public void Dump()
	{
		Debug.Log("data : " + this.data);
		Debug.Log("count : " + this.count);

		DisplayViewManager.SetParameter<string>("data", this.data);
		DisplayViewManager.SetParameter<int>("count", this.count);

		this.a.Dump();

		int i = 0;
		foreach (string str in this.stringList)
		{
			Debug.Log("stringList : " + str);
			DisplayViewManager.SetParameter<string>("str["+ i +"]", str);
			i++;
		}

		foreach (SubClassB b in this.bList)
		{
			b.Dump();
		}
	}
}

/// <summary>
/// Sub class a.
/// </summary>
public class SubClassA
{
	public float f;
	public double g;
	public long l;

	public void Init()
	{
		this.f = -0.123f;
		this.g = 1.2345;
		this.l = 1234567890; 
	}

	public void Dump()
	{
		Debug.Log("f : " + this.f);
		Debug.Log("g : " + this.g);
		Debug.Log("l : " + this.l);

		DisplayViewManager.SetParameter<float>("f", f);
		DisplayViewManager.SetParameter<double>("g", g);
		DisplayViewManager.SetParameter<long>("l", l);
	}
}

/// <summary>
/// Sub class b.
/// </summary>
public class SubClassB
{
	public string name;
	public void Dump()
	{
		Debug.Log("name : " + name);
		DisplayViewManager.SetParameter<string>("name", name);
	}
}