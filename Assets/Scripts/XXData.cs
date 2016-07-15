using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace XXData{

[System.Serializable]

public class XXVector2 {
	public float	x = 0.0f;
	public float	y = 0.0f;
	public XXVector2() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(float);
			nSize += sizeof(float);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(x), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(y), 0, sizeof(float));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			x = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			y = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXVector2_Serializer {
	public static bool Store(MemoryStream _buf_, XXVector2 obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXVector2 obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXVector2 obj) { return obj.Size(); }
};
public enum XX_DB_TYPE {
	XX_DB_INVALID = 0,
	XX_DB_ACCOUNT = 001,
	XX_DB_INFO = 002,
	XX_DB_MAX,
}; // XX_DB_TYPE
public struct XX_DB_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_DB_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_DB_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_DB_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_DB_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_DB_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_DB_TYPE obj) { return sizeof(XX_DB_TYPE); }
};

[System.Serializable]

public enum XX_ACCOUNT_TYPE {
	XX_ACCOUNT_INVALID = 0,
	XX_ACCOUNT_GUEST = 1,
	XX_ACCOUNT_GOOGLE = 2,
	XX_ACCOUNT_FACEBOOK = 3,
}; // XX_ACCOUNT_TYPE
public struct XX_ACCOUNT_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_ACCOUNT_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_ACCOUNT_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_ACCOUNT_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_ACCOUNT_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_ACCOUNT_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_ACCOUNT_TYPE obj) { return sizeof(XX_ACCOUNT_TYPE); }
};

[System.Serializable]

public enum XX_MARKET_TYPE {
	XX_MARKET_INVALID = 0,
	XX_MARKET_IOS = 1,
	XX_MARKET_GOOGLE = 2,
	XX_MARKET_ONESTORE = 3,
	XX_MARKET_MAX,
}; // XX_MARKET_TYPE
public struct XX_MARKET_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_MARKET_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_MARKET_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_MARKET_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_MARKET_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_MARKET_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_MARKET_TYPE obj) { return sizeof(XX_MARKET_TYPE); }
};

[System.Serializable]

public enum XX_PLATFORM_TYPE {
	XX_PLATFORM_INVALID = 0,
	XX_PLATFORM_IOS = 1,
	XX_PLATFORM_ANDROID = 2,
	XX_PLATFORM_WIN32 = 3,
	XX_PLATFORM_MAX,
}; // XX_PLATFORM_TYPE
public struct XX_PLATFORM_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_PLATFORM_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_PLATFORM_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_PLATFORM_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_PLATFORM_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_PLATFORM_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_PLATFORM_TYPE obj) { return sizeof(XX_PLATFORM_TYPE); }
};

[System.Serializable]

public enum XX_FIELD_STATE_TYPE {
	XX_FIELD_STATE_INVALID,
	XX_FIELD_STATE_WAIT,
	XX_FIELD_STATE_READY,
	XX_FIELD_STATE_PLAY,
}; // XX_FIELD_STATE_TYPE
public struct XX_FIELD_STATE_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_FIELD_STATE_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_FIELD_STATE_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_FIELD_STATE_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_FIELD_STATE_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_FIELD_STATE_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_FIELD_STATE_TYPE obj) { return sizeof(XX_FIELD_STATE_TYPE); }
};

[System.Serializable]

public enum XX_PLAYER_STATE_TYPE {
	XX_PLAYER_STATE_INVALID,
	XX_PLAYER_STATE_DISCONNECT,
	XX_PLAYER_STATE_WAIT,
	XX_PLAYER_STATE_READY,
	XX_PLAYER_STATE_LOADING,
	XX_PLAYER_STATE_PLAY,
}; // XX_PLAYER_STATE_TYPE
public struct XX_PLAYER_STATE_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_PLAYER_STATE_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_PLAYER_STATE_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_PLAYER_STATE_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_PLAYER_STATE_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_PLAYER_STATE_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_PLAYER_STATE_TYPE obj) { return sizeof(XX_PLAYER_STATE_TYPE); }
};

[System.Serializable]

public enum XX_UNIT_DIRECTION_TYPE {
	XX_UNIT_DIRECTION_INVALID,
	XX_UNIT_DIRECTION_LEFT,
	XX_UNIT_DIRECTION_RIGHT,
}; // XX_UNIT_DIRECTION_TYPE
public struct XX_UNIT_DIRECTION_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_UNIT_DIRECTION_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_UNIT_DIRECTION_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_UNIT_DIRECTION_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_UNIT_DIRECTION_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_UNIT_DIRECTION_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_UNIT_DIRECTION_TYPE obj) { return sizeof(XX_UNIT_DIRECTION_TYPE); }
};

[System.Serializable]

public enum XX_UNIT_STATE_TYPE {
	XX_UNIT_STATE_INVALID,
	XX_UNIT_STATE_IDLE,
	XX_UNIT_STATE_MOVE,
	XX_UNIT_STATE_ROLL,
	XX_UNIT_STATE_JUMP,
	XX_UNIT_STATE_FALL,
	XX_UNIT_STATE_DAMAGE,
	XX_UNIT_STATE_ATTACK,
	XX_UNIT_STATE_LOOKUP,
	XX_UNIT_STATE_SIT,
	XX_UNIT_STATE_DIE,
	XX_UNIT_STATE_MAX,
}; // XX_UNIT_STATE_TYPE
public struct XX_UNIT_STATE_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_UNIT_STATE_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_UNIT_STATE_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_UNIT_STATE_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_UNIT_STATE_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_UNIT_STATE_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_UNIT_STATE_TYPE obj) { return sizeof(XX_UNIT_STATE_TYPE); }
};

[System.Serializable]

public class XXAccountInfo {
	public string	AccountID = "";
	public string	UserID = "";
	public uint	UserSEQ = 0;
	public XX_ACCOUNT_TYPE	AccountType = new XX_ACCOUNT_TYPE();
	public XX_MARKET_TYPE	MarketType = new XX_MARKET_TYPE();
	public XX_PLATFORM_TYPE	PlatformType = new XX_PLATFORM_TYPE();
	public XXAccountInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != AccountID) { nSize += Encoding.UTF8.GetByteCount(AccountID); }
			nSize += sizeof(int); 
			if(null != UserID) { nSize += Encoding.UTF8.GetByteCount(UserID); }
			nSize += sizeof(uint);
			nSize += XX_ACCOUNT_TYPE_Serializer.Size(AccountType);
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
			if(null != UserID) {
				int UserID_length = Encoding.UTF8.GetByteCount(UserID);
				_buf_.Write(BitConverter.GetBytes(UserID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(UserID), 0, UserID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(UserSEQ), 0, sizeof(uint));
			if(false == XX_ACCOUNT_TYPE_Serializer.Store(_buf_, AccountType)) { return false; }
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
			int UserID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(UserID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] UserID_buf = new byte[UserID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, UserID_buf, 0, UserID_length);
			UserID = System.Text.Encoding.UTF8.GetString(UserID_buf);
			_buf_.Position += UserID_length;
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			UserSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(false == XX_ACCOUNT_TYPE_Serializer.Load(ref AccountType, _buf_)) { return false; }
			if(false == XX_MARKET_TYPE_Serializer.Load(ref MarketType, _buf_)) { return false; }
			if(false == XX_PLATFORM_TYPE_Serializer.Load(ref PlatformType, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXAccountInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXAccountInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXAccountInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXAccountInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXRewardInfo {
	public uint	Gold = 0;
	public uint	Exp = 0;
	public XXRewardInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(uint);
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(Gold), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(Exp), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			Gold = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			Exp = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXRewardInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXRewardInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXRewardInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXRewardInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXMonsterSpawnInfo {
	public string	MonsterID = "";
	public XXVector2	Position = new XXVector2();
	public float	interval = 0.0f;
	public float	count = 0.0f;
	public XXMonsterSpawnInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != MonsterID) { nSize += Encoding.UTF8.GetByteCount(MonsterID); }
			nSize += XXVector2_Serializer.Size(Position);
			nSize += sizeof(float);
			nSize += sizeof(float);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != MonsterID) {
				int MonsterID_length = Encoding.UTF8.GetByteCount(MonsterID);
				_buf_.Write(BitConverter.GetBytes(MonsterID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(MonsterID), 0, MonsterID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(false == XXVector2_Serializer.Store(_buf_, Position)) { return false; }
			_buf_.Write(BitConverter.GetBytes(interval), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(count), 0, sizeof(float));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MonsterID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(MonsterID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] MonsterID_buf = new byte[MonsterID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, MonsterID_buf, 0, MonsterID_length);
			MonsterID = System.Text.Encoding.UTF8.GetString(MonsterID_buf);
			_buf_.Position += MonsterID_length;
			if(false == XXVector2_Serializer.Load(ref Position, _buf_)) { return false; }
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			interval = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			count = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMonsterSpawnInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXMonsterSpawnInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMonsterSpawnInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMonsterSpawnInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXRoomGimmickInfo {
	public string	GimmickID = "";
	public bool	Active = false;
	public XXRoomGimmickInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != GimmickID) { nSize += Encoding.UTF8.GetByteCount(GimmickID); }
			nSize += sizeof(bool);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != GimmickID) {
				int GimmickID_length = Encoding.UTF8.GetByteCount(GimmickID);
				_buf_.Write(BitConverter.GetBytes(GimmickID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(GimmickID), 0, GimmickID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(Active), 0, sizeof(bool));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int GimmickID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(GimmickID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] GimmickID_buf = new byte[GimmickID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, GimmickID_buf, 0, GimmickID_length);
			GimmickID = System.Text.Encoding.UTF8.GetString(GimmickID_buf);
			_buf_.Position += GimmickID_length;
			if(sizeof(bool) > _buf_.Length - _buf_.Position) { return false; }
			Active = BitConverter.ToBoolean(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(bool);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXRoomGimmickInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXRoomGimmickInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXRoomGimmickInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXRoomGimmickInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXRoomInfo {
	public string	RoomID = "";
	public string	RoomName = "";
	public string	GroupID = "";
	public string	NextGroupID = "";
	public XXRewardInfo	RewardInfo = new XXRewardInfo();
	public List<XXRoomGimmickInfo >	GimmickInfos = new List<XXRoomGimmickInfo >();
	public XXRoomInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != RoomID) { nSize += Encoding.UTF8.GetByteCount(RoomID); }
			nSize += sizeof(int); 
			if(null != RoomName) { nSize += Encoding.UTF8.GetByteCount(RoomName); }
			nSize += sizeof(int); 
			if(null != GroupID) { nSize += Encoding.UTF8.GetByteCount(GroupID); }
			nSize += sizeof(int); 
			if(null != NextGroupID) { nSize += Encoding.UTF8.GetByteCount(NextGroupID); }
			nSize += XXRewardInfo_Serializer.Size(RewardInfo);
			nSize += sizeof(int);
			foreach(var GimmickInfos_itr in GimmickInfos) { 
				XXRoomGimmickInfo GimmickInfos_elmt = GimmickInfos_itr;
				nSize += XXRoomGimmickInfo_Serializer.Size(GimmickInfos_elmt);
			}
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != RoomID) {
				int RoomID_length = Encoding.UTF8.GetByteCount(RoomID);
				_buf_.Write(BitConverter.GetBytes(RoomID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(RoomID), 0, RoomID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != RoomName) {
				int RoomName_length = Encoding.UTF8.GetByteCount(RoomName);
				_buf_.Write(BitConverter.GetBytes(RoomName_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(RoomName), 0, RoomName_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != GroupID) {
				int GroupID_length = Encoding.UTF8.GetByteCount(GroupID);
				_buf_.Write(BitConverter.GetBytes(GroupID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(GroupID), 0, GroupID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != NextGroupID) {
				int NextGroupID_length = Encoding.UTF8.GetByteCount(NextGroupID);
				_buf_.Write(BitConverter.GetBytes(NextGroupID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(NextGroupID), 0, NextGroupID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(false == XXRewardInfo_Serializer.Store(_buf_, RewardInfo)) { return false; }
			_buf_.Write(BitConverter.GetBytes(GimmickInfos.Count), 0, sizeof(int));
			foreach(var GimmickInfos_itr in GimmickInfos) { 
				XXRoomGimmickInfo GimmickInfos_elmt = GimmickInfos_itr;
				if(false == XXRoomGimmickInfo_Serializer.Store(_buf_, GimmickInfos_elmt)) { return false; }
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int RoomID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(RoomID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] RoomID_buf = new byte[RoomID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, RoomID_buf, 0, RoomID_length);
			RoomID = System.Text.Encoding.UTF8.GetString(RoomID_buf);
			_buf_.Position += RoomID_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int RoomName_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(RoomName_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] RoomName_buf = new byte[RoomName_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, RoomName_buf, 0, RoomName_length);
			RoomName = System.Text.Encoding.UTF8.GetString(RoomName_buf);
			_buf_.Position += RoomName_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int GroupID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(GroupID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] GroupID_buf = new byte[GroupID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, GroupID_buf, 0, GroupID_length);
			GroupID = System.Text.Encoding.UTF8.GetString(GroupID_buf);
			_buf_.Position += GroupID_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int NextGroupID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(NextGroupID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] NextGroupID_buf = new byte[NextGroupID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, NextGroupID_buf, 0, NextGroupID_length);
			NextGroupID = System.Text.Encoding.UTF8.GetString(NextGroupID_buf);
			_buf_.Position += NextGroupID_length;
			if(false == XXRewardInfo_Serializer.Load(ref RewardInfo, _buf_)) { return false; }
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int GimmickInfos_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int GimmickInfos_itr=0; GimmickInfos_itr<GimmickInfos_length; GimmickInfos_itr++) {
				XXRoomGimmickInfo GimmickInfos_val = new XXRoomGimmickInfo();
				if(false == XXRoomGimmickInfo_Serializer.Load(ref GimmickInfos_val, _buf_)) { return false; }
				GimmickInfos.Add(GimmickInfos_val);
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXRoomInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXRoomInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXRoomInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXRoomInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXRoomGroupInfo {
	public string	GroupID = "";
	public string	GroupName = "";
	public List<string >	NextGroupIDs = new List<string >();
	public uint	NeedStamina = 0;
	public XXRoomGroupInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != GroupID) { nSize += Encoding.UTF8.GetByteCount(GroupID); }
			nSize += sizeof(int); 
			if(null != GroupName) { nSize += Encoding.UTF8.GetByteCount(GroupName); }
			nSize += sizeof(int);
			foreach(var NextGroupIDs_itr in NextGroupIDs) { 
				string NextGroupIDs_elmt = NextGroupIDs_itr;
				nSize += sizeof(int); 
				if(null != NextGroupIDs_elmt) { nSize += Encoding.UTF8.GetByteCount(NextGroupIDs_elmt); }
			}
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != GroupID) {
				int GroupID_length = Encoding.UTF8.GetByteCount(GroupID);
				_buf_.Write(BitConverter.GetBytes(GroupID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(GroupID), 0, GroupID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != GroupName) {
				int GroupName_length = Encoding.UTF8.GetByteCount(GroupName);
				_buf_.Write(BitConverter.GetBytes(GroupName_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(GroupName), 0, GroupName_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(NextGroupIDs.Count), 0, sizeof(int));
			foreach(var NextGroupIDs_itr in NextGroupIDs) { 
				string NextGroupIDs_elmt = NextGroupIDs_itr;
				if(null != NextGroupIDs_elmt) {
					int NextGroupIDs_elmt_length = Encoding.UTF8.GetByteCount(NextGroupIDs_elmt);
					_buf_.Write(BitConverter.GetBytes(NextGroupIDs_elmt_length), 0, sizeof(int));
					_buf_.Write(Encoding.UTF8.GetBytes(NextGroupIDs_elmt), 0, NextGroupIDs_elmt_length);
				}
				else {
					_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
				}
			}
			_buf_.Write(BitConverter.GetBytes(NeedStamina), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int GroupID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(GroupID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] GroupID_buf = new byte[GroupID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, GroupID_buf, 0, GroupID_length);
			GroupID = System.Text.Encoding.UTF8.GetString(GroupID_buf);
			_buf_.Position += GroupID_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int GroupName_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(GroupName_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] GroupName_buf = new byte[GroupName_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, GroupName_buf, 0, GroupName_length);
			GroupName = System.Text.Encoding.UTF8.GetString(GroupName_buf);
			_buf_.Position += GroupName_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int NextGroupIDs_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int NextGroupIDs_itr=0; NextGroupIDs_itr<NextGroupIDs_length; NextGroupIDs_itr++) {
				string NextGroupIDs_val = "";
				if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
				int NextGroupIDs_val_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
				_buf_.Position += sizeof(int);
				if(NextGroupIDs_val_length > _buf_.Length - _buf_.Position) { return false; }
				byte[] NextGroupIDs_val_buf = new byte[NextGroupIDs_val_length];
				Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, NextGroupIDs_val_buf, 0, NextGroupIDs_val_length);
				NextGroupIDs_val = System.Text.Encoding.UTF8.GetString(NextGroupIDs_val_buf);
				_buf_.Position += NextGroupIDs_val_length;
				NextGroupIDs.Add(NextGroupIDs_val);
			}
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			NeedStamina = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXRoomGroupInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXRoomGroupInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXRoomGroupInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXRoomGroupInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXLocalMapInfo {
	public string	MapID = "";
	public string	MapName = "";
	public Dictionary<string, XXRoomGroupInfo >	RoomGroupInfos = new Dictionary<string, XXRoomGroupInfo >();
	public XXLocalMapInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != MapID) { nSize += Encoding.UTF8.GetByteCount(MapID); }
			nSize += sizeof(int); 
			if(null != MapName) { nSize += Encoding.UTF8.GetByteCount(MapName); }
			nSize += sizeof(int);
			foreach(var RoomGroupInfos_itr in RoomGroupInfos) {
				string RoomGroupInfos_key = RoomGroupInfos_itr.Key;
				XXRoomGroupInfo RoomGroupInfos_elmt = RoomGroupInfos_itr.Value;
				nSize += sizeof(int); 
				if(null != RoomGroupInfos_key) { nSize += Encoding.UTF8.GetByteCount(RoomGroupInfos_key); }
				nSize += XXRoomGroupInfo_Serializer.Size(RoomGroupInfos_elmt);
			}
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
			if(null != MapName) {
				int MapName_length = Encoding.UTF8.GetByteCount(MapName);
				_buf_.Write(BitConverter.GetBytes(MapName_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(MapName), 0, MapName_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(RoomGroupInfos.Count), 0, sizeof(int));
			foreach(var RoomGroupInfos_itr in RoomGroupInfos) {
				string RoomGroupInfos_key = RoomGroupInfos_itr.Key;
				XXRoomGroupInfo RoomGroupInfos_elmt = RoomGroupInfos_itr.Value;
				if(null != RoomGroupInfos_key) {
					int RoomGroupInfos_key_length = Encoding.UTF8.GetByteCount(RoomGroupInfos_key);
					_buf_.Write(BitConverter.GetBytes(RoomGroupInfos_key_length), 0, sizeof(int));
					_buf_.Write(Encoding.UTF8.GetBytes(RoomGroupInfos_key), 0, RoomGroupInfos_key_length);
				}
				else {
					_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
				}
				if(false == XXRoomGroupInfo_Serializer.Store(_buf_, RoomGroupInfos_elmt)) { return false; }
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
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MapName_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(MapName_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] MapName_buf = new byte[MapName_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, MapName_buf, 0, MapName_length);
			MapName = System.Text.Encoding.UTF8.GetString(MapName_buf);
			_buf_.Position += MapName_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int RoomGroupInfos_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int RoomGroupInfos_itr=0; RoomGroupInfos_itr<RoomGroupInfos_length; RoomGroupInfos_itr++) {
				string RoomGroupInfos_key = "";
				XXRoomGroupInfo RoomGroupInfos_elmt = new XXRoomGroupInfo();
				if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
				int RoomGroupInfos_key_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
				_buf_.Position += sizeof(int);
				if(RoomGroupInfos_key_length > _buf_.Length - _buf_.Position) { return false; }
				byte[] RoomGroupInfos_key_buf = new byte[RoomGroupInfos_key_length];
				Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, RoomGroupInfos_key_buf, 0, RoomGroupInfos_key_length);
				RoomGroupInfos_key = System.Text.Encoding.UTF8.GetString(RoomGroupInfos_key_buf);
				_buf_.Position += RoomGroupInfos_key_length;
				if(false == XXRoomGroupInfo_Serializer.Load(ref RoomGroupInfos_elmt, _buf_)) { return false; }
				RoomGroupInfos[RoomGroupInfos_key] = RoomGroupInfos_elmt;
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXLocalMapInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXLocalMapInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXLocalMapInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXLocalMapInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXLocalMapData {
	public string	MapID = "";
	public XXLocalMapData() {
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
public struct XXLocalMapData_Serializer {
	public static bool Store(MemoryStream _buf_, XXLocalMapData obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXLocalMapData obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXLocalMapData obj) { return obj.Size(); }
};

[System.Serializable]

public class XXWorldMapInfo {
	public string	MapName = "";
	public Dictionary<string, XXLocalMapInfo >	LocalMapInfos = new Dictionary<string, XXLocalMapInfo >();
	public XXWorldMapInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != MapName) { nSize += Encoding.UTF8.GetByteCount(MapName); }
			nSize += sizeof(int);
			foreach(var LocalMapInfos_itr in LocalMapInfos) {
				string LocalMapInfos_key = LocalMapInfos_itr.Key;
				XXLocalMapInfo LocalMapInfos_elmt = LocalMapInfos_itr.Value;
				nSize += sizeof(int); 
				if(null != LocalMapInfos_key) { nSize += Encoding.UTF8.GetByteCount(LocalMapInfos_key); }
				nSize += XXLocalMapInfo_Serializer.Size(LocalMapInfos_elmt);
			}
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != MapName) {
				int MapName_length = Encoding.UTF8.GetByteCount(MapName);
				_buf_.Write(BitConverter.GetBytes(MapName_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(MapName), 0, MapName_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(LocalMapInfos.Count), 0, sizeof(int));
			foreach(var LocalMapInfos_itr in LocalMapInfos) {
				string LocalMapInfos_key = LocalMapInfos_itr.Key;
				XXLocalMapInfo LocalMapInfos_elmt = LocalMapInfos_itr.Value;
				if(null != LocalMapInfos_key) {
					int LocalMapInfos_key_length = Encoding.UTF8.GetByteCount(LocalMapInfos_key);
					_buf_.Write(BitConverter.GetBytes(LocalMapInfos_key_length), 0, sizeof(int));
					_buf_.Write(Encoding.UTF8.GetBytes(LocalMapInfos_key), 0, LocalMapInfos_key_length);
				}
				else {
					_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
				}
				if(false == XXLocalMapInfo_Serializer.Store(_buf_, LocalMapInfos_elmt)) { return false; }
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MapName_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(MapName_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] MapName_buf = new byte[MapName_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, MapName_buf, 0, MapName_length);
			MapName = System.Text.Encoding.UTF8.GetString(MapName_buf);
			_buf_.Position += MapName_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int LocalMapInfos_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int LocalMapInfos_itr=0; LocalMapInfos_itr<LocalMapInfos_length; LocalMapInfos_itr++) {
				string LocalMapInfos_key = "";
				XXLocalMapInfo LocalMapInfos_elmt = new XXLocalMapInfo();
				if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
				int LocalMapInfos_key_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
				_buf_.Position += sizeof(int);
				if(LocalMapInfos_key_length > _buf_.Length - _buf_.Position) { return false; }
				byte[] LocalMapInfos_key_buf = new byte[LocalMapInfos_key_length];
				Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, LocalMapInfos_key_buf, 0, LocalMapInfos_key_length);
				LocalMapInfos_key = System.Text.Encoding.UTF8.GetString(LocalMapInfos_key_buf);
				_buf_.Position += LocalMapInfos_key_length;
				if(false == XXLocalMapInfo_Serializer.Load(ref LocalMapInfos_elmt, _buf_)) { return false; }
				LocalMapInfos[LocalMapInfos_key] = LocalMapInfos_elmt;
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXWorldMapInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXWorldMapInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXWorldMapInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXWorldMapInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXUnitData {
	public XX_UNIT_STATE_TYPE	State = new XX_UNIT_STATE_TYPE();
	public float	HealthPoint = 0.0f;
	public XXVector2	Position = new XXVector2();
	public XXVector2	Velocity = new XXVector2();
	public XXUnitData() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XX_UNIT_STATE_TYPE_Serializer.Size(State);
			nSize += sizeof(float);
			nSize += XXVector2_Serializer.Size(Position);
			nSize += XXVector2_Serializer.Size(Velocity);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XX_UNIT_STATE_TYPE_Serializer.Store(_buf_, State)) { return false; }
			_buf_.Write(BitConverter.GetBytes(HealthPoint), 0, sizeof(float));
			if(false == XXVector2_Serializer.Store(_buf_, Position)) { return false; }
			if(false == XXVector2_Serializer.Store(_buf_, Velocity)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XX_UNIT_STATE_TYPE_Serializer.Load(ref State, _buf_)) { return false; }
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			HealthPoint = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(false == XXVector2_Serializer.Load(ref Position, _buf_)) { return false; }
			if(false == XXVector2_Serializer.Load(ref Velocity, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXUnitData_Serializer {
	public static bool Store(MemoryStream _buf_, XXUnitData obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXUnitData obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXUnitData obj) { return obj.Size(); }
};

[System.Serializable]

public class XXMonsterData : XXUnitData {
	public uint	MonsterSEQ = 0;
	public string	MonsterID = "";
	public XXMonsterData() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize = base.Size();
			nSize += sizeof(uint);
			nSize += sizeof(int); 
			if(null != MonsterID) { nSize += Encoding.UTF8.GetByteCount(MonsterID); }
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			base.Store(_buf_);
			_buf_.Write(BitConverter.GetBytes(MonsterSEQ), 0, sizeof(uint));
			if(null != MonsterID) {
				int MonsterID_length = Encoding.UTF8.GetByteCount(MonsterID);
				_buf_.Write(BitConverter.GetBytes(MonsterID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(MonsterID), 0, MonsterID_length);
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
			if(false == base.Load(_buf_)) return false;
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			MonsterSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MonsterID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(MonsterID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] MonsterID_buf = new byte[MonsterID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, MonsterID_buf, 0, MonsterID_length);
			MonsterID = System.Text.Encoding.UTF8.GetString(MonsterID_buf);
			_buf_.Position += MonsterID_length;
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMonsterData_Serializer {
	public static bool Store(MemoryStream _buf_, XXMonsterData obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMonsterData obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMonsterData obj) { return obj.Size(); }
};
}
