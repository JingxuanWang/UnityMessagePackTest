using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class MessagePackTest : MonoBehaviour
{
 
	void Start()
	{
		StartCoroutine(Parse());
	}
	IEnumerator Parse()
	{
        
		WWW www = new WWW("http://localhost/cgi-bin/msgpack.cgi");
		yield return www;
		
		MsgPack.BoxingPacker packer = new MsgPack.BoxingPacker();
		IList msg = (IList)packer.Unpack(www.bytes);
		for (int i = 0; i < msg.Count; i++)
		{
			Debug.Log(Encoding.UTF8.GetString((byte[])msg[i]));
		}
	}
}
