using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using MsgPack;
using MsgPack.Serialization;
using GdiSystem;
using GdiSystem.Display;


public class MessagePackTest : GameBehaviour
{
	private string prefix = "http://jingxuanwang.github.io/game/test";
	private string status = "Idle";

	private string simpleUrl = "/simple.msgpack";
	private string testUrl = "/test.msgpack";

	void Start()
	{
		DisplayViewManager.SetParameter<string>("Status", () => this.status, this);

//		TestLocalMessagePack();
//		TestLocalMessagePackSerialization();
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(100, 100, 200, 200), "Parse simple.msgpack"))
		{
			StartCoroutine(Parse(simpleUrl));
		}

		if (GUI.Button(new Rect(100, 350, 200, 200), "Parse test.msgpack"))
		{
			StartCoroutine(Parse(testUrl));
		}
	}

	IEnumerator Parse(string url)
	{
		WWW www = new WWW(prefix + url);
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			this.status = url;

			if (url == simpleUrl)
			{
				DataClass unpackData = new DataClass();
				using (MemoryStream mem = new MemoryStream(www.bytes))
				{
					Unpacker unpacker = Unpacker.Create(mem);
					unpackData.UnpackFromMessage(unpacker);
				}

				unpackData.Dump();

//				MessagePackSerializer<DataClass> mps = MessagePackSerializer.Create<DataClass>();
//				DataClass ret = mps.UnpackSingleObject(www.bytes);
//
//				ret.Dump();
			}

			if (url == testUrl)
			{
				MessagePackSerializer<BaseClass> mps = MessagePackSerializer.Create<BaseClass>();
				BaseClass ret = mps.UnpackSingleObject(www.bytes);

				ret.Dump();
			}
		}
		else
		{
			this.status = www.error;
		}
	}

	void TestLocalMessagePack()
	{
		byte[] bytes = null;

		// pack
		DataClass dc = new DataClass()
		{
			count = 120,
			data = "hoge",
		};
		using ( MemoryStream mem = new MemoryStream())
		{
			Packer packer = Packer.Create(mem);
			dc.PackToMessage(packer, null);
			bytes = mem.ToArray();
		}

		File.WriteAllBytes(Application.dataPath + "/simple_packer.msgpack", bytes);

		// unpack
		DataClass unpackData = new DataClass();
		using (MemoryStream mem = new MemoryStream(bytes))
		{
			Unpacker unpacker = Unpacker.Create(mem);
			unpackData.UnpackFromMessage(unpacker);
		}

		unpackData.Dump();

//		MessagePackSerializer<DataClass> mps = MessagePackSerializer.Create<DataClass>();
//		bytes = mps.PackSingleObject(dc);
//		File.WriteAllBytes(Application.dataPath + "/simple_serializer.msgpack", bytes);
//		DataClass ret = mps.UnpackSingleObject(bytes);
//		ret.Dump();
	}

	void TestLocalMessagePackSerialization()
	{
		BaseClass data = new BaseClass();
		data.Init();

		MessagePackSerializer<BaseClass> mps = MessagePackSerializer.Create<BaseClass>();
		byte[] bytes = mps.PackSingleObject(data);
		File.WriteAllBytes(Application.dataPath + "/test.msgpack", bytes);

		BaseClass ret = mps.UnpackSingleObject(bytes);

		ret.Dump();
	}
}