using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace XXMessage{

using XXData;
using XXError;


[System.Serializable]

public enum XX_LOGIN_TYPE {
	XX_LOGIN_INVALID,
	XX_LOGIN_ACCESS_TOKEN,
	XX_LOGIN_RECONNECT,
}; // XX_LOGIN_TYPE
public struct XX_LOGIN_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_LOGIN_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_LOGIN_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_LOGIN_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_LOGIN_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_LOGIN_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_LOGIN_TYPE obj) { return sizeof(XX_LOGIN_TYPE); }
};
public class XXMsgCliSvr_User_CreateAccount_Req {
	public const int MSG_ID = 1001;
	public string	AccountID = "";
	public string	AccessToken = "";
	public string	UserID = "";
	public XX_ACCOUNT_TYPE	AccountType = new XX_ACCOUNT_TYPE();
	public uint	WorldID = 0;
	public XXMsgCliSvr_User_CreateAccount_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != AccountID) { nSize += Encoding.UTF8.GetByteCount(AccountID); }
			nSize += sizeof(int); 
			if(null != AccessToken) { nSize += Encoding.UTF8.GetByteCount(AccessToken); }
			nSize += sizeof(int); 
			if(null != UserID) { nSize += Encoding.UTF8.GetByteCount(UserID); }
			nSize += XX_ACCOUNT_TYPE_Serializer.Size(AccountType);
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != AccountID) {
				int AccountID_length = Encoding.UTF8.GetByteCount(AccountID);
				_buf_.Write(BitConverter.GetBytes(AccountID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(AccountID), 0, AccountID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != AccessToken) {
				int AccessToken_length = Encoding.UTF8.GetByteCount(AccessToken);
				_buf_.Write(BitConverter.GetBytes(AccessToken_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(AccessToken), 0, AccessToken_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != UserID) {
				int UserID_length = Encoding.UTF8.GetByteCount(UserID);
				_buf_.Write(BitConverter.GetBytes(UserID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(UserID), 0, UserID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(false == XX_ACCOUNT_TYPE_Serializer.Store(_buf_, AccountType)) { return false; }
			_buf_.Write(BitConverter.GetBytes(WorldID), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int AccountID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(AccountID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] AccountID_buf = new byte[AccountID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, AccountID_buf, 0, AccountID_length);
			AccountID = System.Text.Encoding.UTF8.GetString(AccountID_buf);
			_buf_.Position += AccountID_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int AccessToken_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(AccessToken_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] AccessToken_buf = new byte[AccessToken_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, AccessToken_buf, 0, AccessToken_length);
			AccessToken = System.Text.Encoding.UTF8.GetString(AccessToken_buf);
			_buf_.Position += AccessToken_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int UserID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(UserID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] UserID_buf = new byte[UserID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, UserID_buf, 0, UserID_length);
			UserID = System.Text.Encoding.UTF8.GetString(UserID_buf);
			_buf_.Position += UserID_length;
			if(false == XX_ACCOUNT_TYPE_Serializer.Load(ref AccountType, _buf_)) { return false; }
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			WorldID = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgCliSvr_User_CreateAccount_Req_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgCliSvr_User_CreateAccount_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgCliSvr_User_CreateAccount_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgCliSvr_User_CreateAccount_Req obj) { return obj.Size(); }
};
public class XXMsgSvrCli_User_CreateAccount_Ans {
	public const int MSG_ID = 1001;
	public XXError	Error = new XXError();
	public XXMsgSvrCli_User_CreateAccount_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXError_Serializer.Size(Error);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Store(_buf_, Error)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Load(ref Error, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_User_CreateAccount_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_User_CreateAccount_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_User_CreateAccount_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_User_CreateAccount_Ans obj) { return obj.Size(); }
};
public class XXMsgCliSvr_User_CreateCharacter_Req {
	public const int MSG_ID = 1002;
	public XXMsgCliSvr_User_CreateCharacter_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgCliSvr_User_CreateCharacter_Req_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgCliSvr_User_CreateCharacter_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgCliSvr_User_CreateCharacter_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgCliSvr_User_CreateCharacter_Req obj) { return obj.Size(); }
};
public class XXMsgSvrCli_User_CreateCharacter_Ans {
	public const int MSG_ID = 1002;
	public XXMsgSvrCli_User_CreateCharacter_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_User_CreateCharacter_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_User_CreateCharacter_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_User_CreateCharacter_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_User_CreateCharacter_Ans obj) { return obj.Size(); }
};
public class XXMsgCliSvr_User_Login_Req {
	public const int MSG_ID = 1003;
	public string	AccountID = "";
	public string	AccessToken = "";
	public XX_ACCOUNT_TYPE	AccountType = new XX_ACCOUNT_TYPE();
	public XX_LOGIN_TYPE	LoginType = new XX_LOGIN_TYPE();
	public XX_MARKET_TYPE	MarketType = new XX_MARKET_TYPE();
	public XX_PLATFORM_TYPE	PlatformType = new XX_PLATFORM_TYPE();
	public XXMsgCliSvr_User_Login_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != AccountID) { nSize += Encoding.UTF8.GetByteCount(AccountID); }
			nSize += sizeof(int); 
			if(null != AccessToken) { nSize += Encoding.UTF8.GetByteCount(AccessToken); }
			nSize += XX_ACCOUNT_TYPE_Serializer.Size(AccountType);
			nSize += XX_LOGIN_TYPE_Serializer.Size(LoginType);
			nSize += XX_MARKET_TYPE_Serializer.Size(MarketType);
			nSize += XX_PLATFORM_TYPE_Serializer.Size(PlatformType);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != AccountID) {
				int AccountID_length = Encoding.UTF8.GetByteCount(AccountID);
				_buf_.Write(BitConverter.GetBytes(AccountID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(AccountID), 0, AccountID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != AccessToken) {
				int AccessToken_length = Encoding.UTF8.GetByteCount(AccessToken);
				_buf_.Write(BitConverter.GetBytes(AccessToken_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(AccessToken), 0, AccessToken_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(false == XX_ACCOUNT_TYPE_Serializer.Store(_buf_, AccountType)) { return false; }
			if(false == XX_LOGIN_TYPE_Serializer.Store(_buf_, LoginType)) { return false; }
			if(false == XX_MARKET_TYPE_Serializer.Store(_buf_, MarketType)) { return false; }
			if(false == XX_PLATFORM_TYPE_Serializer.Store(_buf_, PlatformType)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int AccountID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(AccountID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] AccountID_buf = new byte[AccountID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, AccountID_buf, 0, AccountID_length);
			AccountID = System.Text.Encoding.UTF8.GetString(AccountID_buf);
			_buf_.Position += AccountID_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int AccessToken_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(AccessToken_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] AccessToken_buf = new byte[AccessToken_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, AccessToken_buf, 0, AccessToken_length);
			AccessToken = System.Text.Encoding.UTF8.GetString(AccessToken_buf);
			_buf_.Position += AccessToken_length;
			if(false == XX_ACCOUNT_TYPE_Serializer.Load(ref AccountType, _buf_)) { return false; }
			if(false == XX_LOGIN_TYPE_Serializer.Load(ref LoginType, _buf_)) { return false; }
			if(false == XX_MARKET_TYPE_Serializer.Load(ref MarketType, _buf_)) { return false; }
			if(false == XX_PLATFORM_TYPE_Serializer.Load(ref PlatformType, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgCliSvr_User_Login_Req_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgCliSvr_User_Login_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgCliSvr_User_Login_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgCliSvr_User_Login_Req obj) { return obj.Size(); }
};
public class XXMsgSvrCli_User_Login_Ans {
	public const int MSG_ID = 1003;
	public XXError	Error = new XXError();
	public XXAccountInfo	AccountInfo = new XXAccountInfo();
	public string	AccessToken = "";
	public ulong	CurrentTime = 0;
	public ulong	PenaltyStartTime = 0;
	public ulong	PenaltyEndTime = 0;
	public uint	PatchVersion = 0;
	public XXMsgSvrCli_User_Login_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXError_Serializer.Size(Error);
			nSize += XXAccountInfo_Serializer.Size(AccountInfo);
			nSize += sizeof(int); 
			if(null != AccessToken) { nSize += Encoding.UTF8.GetByteCount(AccessToken); }
			nSize += sizeof(ulong);
			nSize += sizeof(ulong);
			nSize += sizeof(ulong);
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Store(_buf_, Error)) { return false; }
			if(false == XXAccountInfo_Serializer.Store(_buf_, AccountInfo)) { return false; }
			if(null != AccessToken) {
				int AccessToken_length = Encoding.UTF8.GetByteCount(AccessToken);
				_buf_.Write(BitConverter.GetBytes(AccessToken_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(AccessToken), 0, AccessToken_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(CurrentTime), 0, sizeof(ulong));
			_buf_.Write(BitConverter.GetBytes(PenaltyStartTime), 0, sizeof(ulong));
			_buf_.Write(BitConverter.GetBytes(PenaltyEndTime), 0, sizeof(ulong));
			_buf_.Write(BitConverter.GetBytes(PatchVersion), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Load(ref Error, _buf_)) { return false; }
			if(false == XXAccountInfo_Serializer.Load(ref AccountInfo, _buf_)) { return false; }
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int AccessToken_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(AccessToken_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] AccessToken_buf = new byte[AccessToken_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, AccessToken_buf, 0, AccessToken_length);
			AccessToken = System.Text.Encoding.UTF8.GetString(AccessToken_buf);
			_buf_.Position += AccessToken_length;
			if(sizeof(ulong) > _buf_.Length - _buf_.Position) { return false; }
			CurrentTime = BitConverter.ToUInt64(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(ulong);
			if(sizeof(ulong) > _buf_.Length - _buf_.Position) { return false; }
			PenaltyStartTime = BitConverter.ToUInt64(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(ulong);
			if(sizeof(ulong) > _buf_.Length - _buf_.Position) { return false; }
			PenaltyEndTime = BitConverter.ToUInt64(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(ulong);
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			PatchVersion = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_User_Login_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_User_Login_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_User_Login_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_User_Login_Ans obj) { return obj.Size(); }
};
public class XXMsgSvrSvr_User_Kickout_Req {
	public const int MSG_ID = 1004;
	public uint	UserSEQ = 0;
	public XXMsgSvrSvr_User_Kickout_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(UserSEQ), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			UserSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrSvr_User_Kickout_Req_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrSvr_User_Kickout_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrSvr_User_Kickout_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrSvr_User_Kickout_Req obj) { return obj.Size(); }
};
public class XXMsgSvrSvr_User_Kickout_Ans {
	public const int MSG_ID = 1005;
	public XXError	Error = new XXError();
	public XXMsgSvrSvr_User_Kickout_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXError_Serializer.Size(Error);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Store(_buf_, Error)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Load(ref Error, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrSvr_User_Kickout_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrSvr_User_Kickout_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrSvr_User_Kickout_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrSvr_User_Kickout_Ans obj) { return obj.Size(); }
};
public class XXMsgSvrCli_User_Kickout_Ntf {
	public const int MSG_ID = 1001006;
	public XXError	Error = new XXError();
	public XXMsgSvrCli_User_Kickout_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXError_Serializer.Size(Error);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Store(_buf_, Error)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Load(ref Error, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_User_Kickout_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_User_Kickout_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_User_Kickout_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_User_Kickout_Ntf obj) { return obj.Size(); }
};
public class XXMsgCliSvr_Map_GetLocalMap_Req {
	public const int MSG_ID = 2001;
	public string	MapID = "";
	public XXMsgCliSvr_Map_GetLocalMap_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != MapID) { nSize += Encoding.UTF8.GetByteCount(MapID); }
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != MapID) {
				int MapID_length = Encoding.UTF8.GetByteCount(MapID);
				_buf_.Write(BitConverter.GetBytes(MapID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(MapID), 0, MapID_length);
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
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MapID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(MapID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] MapID_buf = new byte[MapID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, MapID_buf, 0, MapID_length);
			MapID = System.Text.Encoding.UTF8.GetString(MapID_buf);
			_buf_.Position += MapID_length;
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgCliSvr_Map_GetLocalMap_Req_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgCliSvr_Map_GetLocalMap_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgCliSvr_Map_GetLocalMap_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgCliSvr_Map_GetLocalMap_Req obj) { return obj.Size(); }
};
public class XXMsgSvrCli_Map_GetLocalMap_Ans {
	public const int MSG_ID = 2001;
	public XXError	Error = new XXError();
	public XXLocalMapInfo	LocalMapInfo = new XXLocalMapInfo();
	public XXMsgSvrCli_Map_GetLocalMap_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXError_Serializer.Size(Error);
			nSize += XXLocalMapInfo_Serializer.Size(LocalMapInfo);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Store(_buf_, Error)) { return false; }
			if(false == XXLocalMapInfo_Serializer.Store(_buf_, LocalMapInfo)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Load(ref Error, _buf_)) { return false; }
			if(false == XXLocalMapInfo_Serializer.Load(ref LocalMapInfo, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_Map_GetLocalMap_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_Map_GetLocalMap_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_Map_GetLocalMap_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_Map_GetLocalMap_Ans obj) { return obj.Size(); }
};
public class XXMsgCliSvr_Map_CreateField_Req {
	public const int MSG_ID = 2003;
	public List<string >	RoomGroupIDs = new List<string >();
	public XXMsgCliSvr_Map_CreateField_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int);
			foreach(var RoomGroupIDs_itr in RoomGroupIDs) { 
				string RoomGroupIDs_elmt = RoomGroupIDs_itr;
				nSize += sizeof(int); 
				if(null != RoomGroupIDs_elmt) { nSize += Encoding.UTF8.GetByteCount(RoomGroupIDs_elmt); }
			}
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(RoomGroupIDs.Count), 0, sizeof(int));
			foreach(var RoomGroupIDs_itr in RoomGroupIDs) { 
				string RoomGroupIDs_elmt = RoomGroupIDs_itr;
				if(null != RoomGroupIDs_elmt) {
					int RoomGroupIDs_elmt_length = Encoding.UTF8.GetByteCount(RoomGroupIDs_elmt);
					_buf_.Write(BitConverter.GetBytes(RoomGroupIDs_elmt_length), 0, sizeof(int));
					_buf_.Write(Encoding.UTF8.GetBytes(RoomGroupIDs_elmt), 0, RoomGroupIDs_elmt_length);
				}
				else {
					_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
				}
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int RoomGroupIDs_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int RoomGroupIDs_itr=0; RoomGroupIDs_itr<RoomGroupIDs_length; RoomGroupIDs_itr++) {
				string RoomGroupIDs_val = "";
				if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
				int RoomGroupIDs_val_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
				_buf_.Position += sizeof(int);
				if(RoomGroupIDs_val_length > _buf_.Length - _buf_.Position) { return false; }
				byte[] RoomGroupIDs_val_buf = new byte[RoomGroupIDs_val_length];
				Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, RoomGroupIDs_val_buf, 0, RoomGroupIDs_val_length);
				RoomGroupIDs_val = System.Text.Encoding.UTF8.GetString(RoomGroupIDs_val_buf);
				_buf_.Position += RoomGroupIDs_val_length;
				RoomGroupIDs.Add(RoomGroupIDs_val);
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgCliSvr_Map_CreateField_Req_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgCliSvr_Map_CreateField_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgCliSvr_Map_CreateField_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgCliSvr_Map_CreateField_Req obj) { return obj.Size(); }
};
public class XXMsgSvrCli_Map_CreateField_Ans {
	public const int MSG_ID = 2003;
	public XXError	Error = new XXError();
	public uint	FieldSEQ = 0;
	public XX_FIELD_STATE_TYPE	State = new XX_FIELD_STATE_TYPE();
	public List<XXRoomInfo >	RoomInfos = new List<XXRoomInfo >();
	public List<string >	MonsterIDs = new List<string >();
	public XXMsgSvrCli_Map_CreateField_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXError_Serializer.Size(Error);
			nSize += sizeof(uint);
			nSize += XX_FIELD_STATE_TYPE_Serializer.Size(State);
			nSize += sizeof(int);
			foreach(var RoomInfos_itr in RoomInfos) { 
				XXRoomInfo RoomInfos_elmt = RoomInfos_itr;
				nSize += XXRoomInfo_Serializer.Size(RoomInfos_elmt);
			}
			nSize += sizeof(int);
			foreach(var MonsterIDs_itr in MonsterIDs) { 
				string MonsterIDs_elmt = MonsterIDs_itr;
				nSize += sizeof(int); 
				if(null != MonsterIDs_elmt) { nSize += Encoding.UTF8.GetByteCount(MonsterIDs_elmt); }
			}
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Store(_buf_, Error)) { return false; }
			_buf_.Write(BitConverter.GetBytes(FieldSEQ), 0, sizeof(uint));
			if(false == XX_FIELD_STATE_TYPE_Serializer.Store(_buf_, State)) { return false; }
			_buf_.Write(BitConverter.GetBytes(RoomInfos.Count), 0, sizeof(int));
			foreach(var RoomInfos_itr in RoomInfos) { 
				XXRoomInfo RoomInfos_elmt = RoomInfos_itr;
				if(false == XXRoomInfo_Serializer.Store(_buf_, RoomInfos_elmt)) { return false; }
			}
			_buf_.Write(BitConverter.GetBytes(MonsterIDs.Count), 0, sizeof(int));
			foreach(var MonsterIDs_itr in MonsterIDs) { 
				string MonsterIDs_elmt = MonsterIDs_itr;
				if(null != MonsterIDs_elmt) {
					int MonsterIDs_elmt_length = Encoding.UTF8.GetByteCount(MonsterIDs_elmt);
					_buf_.Write(BitConverter.GetBytes(MonsterIDs_elmt_length), 0, sizeof(int));
					_buf_.Write(Encoding.UTF8.GetBytes(MonsterIDs_elmt), 0, MonsterIDs_elmt_length);
				}
				else {
					_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
				}
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Load(ref Error, _buf_)) { return false; }
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			FieldSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(false == XX_FIELD_STATE_TYPE_Serializer.Load(ref State, _buf_)) { return false; }
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int RoomInfos_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int RoomInfos_itr=0; RoomInfos_itr<RoomInfos_length; RoomInfos_itr++) {
				XXRoomInfo RoomInfos_val = new XXRoomInfo();
				if(false == XXRoomInfo_Serializer.Load(ref RoomInfos_val, _buf_)) { return false; }
				RoomInfos.Add(RoomInfos_val);
			}
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MonsterIDs_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int MonsterIDs_itr=0; MonsterIDs_itr<MonsterIDs_length; MonsterIDs_itr++) {
				string MonsterIDs_val = "";
				if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
				int MonsterIDs_val_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
				_buf_.Position += sizeof(int);
				if(MonsterIDs_val_length > _buf_.Length - _buf_.Position) { return false; }
				byte[] MonsterIDs_val_buf = new byte[MonsterIDs_val_length];
				Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, MonsterIDs_val_buf, 0, MonsterIDs_val_length);
				MonsterIDs_val = System.Text.Encoding.UTF8.GetString(MonsterIDs_val_buf);
				_buf_.Position += MonsterIDs_val_length;
				MonsterIDs.Add(MonsterIDs_val);
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_Map_CreateField_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_Map_CreateField_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_Map_CreateField_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_Map_CreateField_Ans obj) { return obj.Size(); }
};
public class XXMsgCliSvr_Map_JoinField_Req {
	public const int MSG_ID = 2004;
	public uint	FieldSEQ = 0;
	public XXMsgCliSvr_Map_JoinField_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(FieldSEQ), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			FieldSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgCliSvr_Map_JoinField_Req_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgCliSvr_Map_JoinField_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgCliSvr_Map_JoinField_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgCliSvr_Map_JoinField_Req obj) { return obj.Size(); }
};
public class XXMsgSvrCli_Map_JoinField_Ans : XXMsgSvrCli_Map_CreateField_Ans {
	public const int MSG_ID = 2004;
	public XXMsgSvrCli_Map_JoinField_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize = base.Size();
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			base.Store(_buf_);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == base.Load(_buf_)) return false;
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_Map_JoinField_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_Map_JoinField_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_Map_JoinField_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_Map_JoinField_Ans obj) { return obj.Size(); }
};
public class XXMsgSvrCli_Map_JoinField_Ntf {
	public const int MSG_ID = 1002004;
	public XXMsgSvrCli_Map_JoinField_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_Map_JoinField_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_Map_JoinField_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_Map_JoinField_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_Map_JoinField_Ntf obj) { return obj.Size(); }
};
public class XXMsgCliSvr_Map_ReadyGame_Ntf {
	public const int MSG_ID = 1002005;
	public XXMsgCliSvr_Map_ReadyGame_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgCliSvr_Map_ReadyGame_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgCliSvr_Map_ReadyGame_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgCliSvr_Map_ReadyGame_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgCliSvr_Map_ReadyGame_Ntf obj) { return obj.Size(); }
};
public class XXMsgSvrCli_Map_ReadyGame_Ntf {
	public const int MSG_ID = 1002006;
	public XXMsgSvrCli_Map_ReadyGame_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_Map_ReadyGame_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_Map_ReadyGame_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_Map_ReadyGame_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_Map_ReadyGame_Ntf obj) { return obj.Size(); }
};
public class XXMsgSvrCli_Map_StartGame_Ntf {
	public const int MSG_ID = 1002007;
	public XXMsgSvrCli_Map_StartGame_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_Map_StartGame_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_Map_StartGame_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_Map_StartGame_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_Map_StartGame_Ntf obj) { return obj.Size(); }
};
public class XXMsgSvrCli_Map_RoomData_Ntf {
	public const int MSG_ID = 1002008;
	public XXMsgSvrCli_Map_RoomData_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMsgSvrCli_Map_RoomData_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, XXMsgSvrCli_Map_RoomData_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMsgSvrCli_Map_RoomData_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMsgSvrCli_Map_RoomData_Ntf obj) { return obj.Size(); }
};
}
