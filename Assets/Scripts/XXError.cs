using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace XXError{

[System.Serializable]

public enum XX_ERROR_CODE {
	XX_ERROR_SUCCESS,
	XX_ERROR_ERR_ALREADY_LOGINED,
	XX_ERROR_WRN_ALREADY_EXIST,
	XX_ERROR_ERR_INVALID_USER,
	XX_ERROR_ERR_NOTFOUND_USER,
	XX_ERROR_ERR_AUTH_FAIL,
	XX_ERROR_ERR_AUTH_LEVEL,
	XX_ERROR_ERR_INVALID_LOGIN_TYPE,
	XX_ERROR_ERR_INVALID_USER_ID,
	XX_ERROR_ERR_INVALID_ACCOUNT_ID,
	XX_ERROR_ERR_INVALID_ACCOUNT_TYPE,
	XX_ERROR_ERR_INVALID_MARKET_TYPE,
	XX_ERROR_ERR_INVALID_LOCALMAP_ID,
	XX_ERROR_ERR_INVALID_ROOMGROUP_ID,
	XX_ERROR_ERR_INVALID_ROOM_ID,
	XX_ERROR_ERR_DUPLICATE_USER,
	XX_ERROR_ERR_PANALTY_USER,
	XX_ERROR_ERR_ROOM_IS_NOT_WAIT,
	XX_ERROR_ERR_ROOM_IS_NOT_READY,
	XX_ERROR_ERR_ALREADY_IN_ROOM,
	XX_ERROR_ERR_NOT_HOST_USER,
	XX_ERROR_ERR_NOT_ALL_USER_READY,
	XX_ERROR_ERR_USER_IS_NOT_PLAY,
	XX_ERROR_ERR_MSGFORMAT,
	XX_ERROR_ERR_UNDEFINED,
}; // XX_ERROR_CODE
public struct XX_ERROR_CODE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_ERROR_CODE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_ERROR_CODE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_ERROR_CODE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_ERROR_CODE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_ERROR_CODE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_ERROR_CODE obj) { return sizeof(XX_ERROR_CODE); }
};
public class XXError {
	public XX_ERROR_CODE	Code = new XX_ERROR_CODE();
	public string	Message = "";
	public XXError() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XX_ERROR_CODE_Serializer.Size(Code);
			nSize += sizeof(int); 
			if(null != Message) { nSize += Encoding.UTF8.GetByteCount(Message); }
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XX_ERROR_CODE_Serializer.Store(_buf_, Code)) { return false; }
			if(null != Message) {
				int Message_length = Encoding.UTF8.GetByteCount(Message);
				_buf_.Write(BitConverter.GetBytes(Message_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(Message), 0, Message_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XX_ERROR_CODE_Serializer.Load(ref Code, _buf_)) { return false; }
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int Message_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(Message_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] Message_buf = new byte[Message_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, Message_buf, 0, Message_length);
			Message = System.Text.Encoding.UTF8.GetString(Message_buf);
			_buf_.Position += Message_length;
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXError_Serializer {
	public static bool Store(MemoryStream _buf_, XXError obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXError obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXError obj) { return obj.Size(); }
};
}
