using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace XXField{

using XXData;
using XXError;


[System.Serializable]

public enum XX_UNIT_SUBSTATE_TYPE {
	XX_UNIT_SUBSTATE_INVALID,
	XX_UNIT_SUBSTATE_IDLE,
	XX_UNIT_SUBSTATE_WALK,
	XX_UNIT_SUBSTATE_RUN,
	XX_UNIT_SUBSTATE_FLY,
	XX_UNIT_SUBSTATE_ROLL,
	XX_UNIT_SUBSTATE_JUMPREADY,
	XX_UNIT_SUBSTATE_JUMP,
	XX_UNIT_SUBSTATE_DOWNJUMP,
	XX_UNIT_SUBSTATE_FALL,
	XX_UNIT_SUBSTATE_LAND,
	XX_UNIT_SUBSTATE_LOOKUP,
	XX_UNIT_SUBSTATE_SIT,
	XX_UNIT_SUBSTATE_DAMAGE,
	XX_UNIT_SUBSTATE_RAISE,
	XX_UNIT_SUBSTATE_KNOCKDOWN,
	XX_UNIT_SUBSTATE_DOWN_FALL,
	XX_UNIT_SUBSTATE_DOWN_BOUND,
	XX_UNIT_SUBSTATE_DOWN_ING,
	XX_UNIT_SUBSTATE_STANDUP,
	XX_UNIT_SUBSTATE_DIE,
	XX_UNIT_SUBSTATE_ATTACK_1,
	XX_UNIT_SUBSTATE_ATTACK_2,
	XX_UNIT_SUBSTATE_ATTACK_3,
	XX_UNIT_SUBSTATE_ATTACK_4,
	XX_UNIT_SUBSTATE_AIRATTACK_1,
	XX_UNIT_SUBSTATE_AIRATTACK_2,
	XX_UNIT_SUBSTATE_AIRATTACK_3,
	XX_UNIT_SUBSTATE_SPATTACK_1,
	XX_UNIT_SUBSTATE_SPATTACK_2,
	XX_UNIT_SUBSTATE_SPATTACK_3,
	XX_UNIT_SUBSTATE_SPATTACK_4,
	XX_UNIT_SUBSTATE_SPATTACK_5,
	XX_UNIT_SUBSTATE_MAX,
}; // XX_UNIT_SUBSTATE_TYPE
public struct XX_UNIT_SUBSTATE_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_UNIT_SUBSTATE_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_UNIT_SUBSTATE_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_UNIT_SUBSTATE_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_UNIT_SUBSTATE_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_UNIT_SUBSTATE_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_UNIT_SUBSTATE_TYPE obj) { return sizeof(XX_UNIT_SUBSTATE_TYPE); }
};

[System.Serializable]

public class XXHitInfo {
	public float	Damage = 0.0f;
	public float	HitStopTime = 0.0f;
	public float	StiffTime = 0.0f;
	public float	HitDurationTime = 0.0f;
	public XXVector2	HitBackDistance = new XXVector2();
	public float	HitBackTime = 0.0f;
	public string	HitEffect = "";
	public float	HitEffectAngle = 0.0f;
	public float	KnockDownScore = 0.0f;
	public float	KnockDownTime = 0.0f;
	public bool	Critical = false;
	public XXHitInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += XXVector2_Serializer.Size(HitBackDistance);
			nSize += sizeof(float);
			nSize += sizeof(int); 
			if(null != HitEffect) { nSize += Encoding.UTF8.GetByteCount(HitEffect); }
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(bool);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(Damage), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(HitStopTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(StiffTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(HitDurationTime), 0, sizeof(float));
			if(false == XXVector2_Serializer.Store(_buf_, HitBackDistance)) { return false; }
			_buf_.Write(BitConverter.GetBytes(HitBackTime), 0, sizeof(float));
			if(null != HitEffect) {
				int HitEffect_length = Encoding.UTF8.GetByteCount(HitEffect);
				_buf_.Write(BitConverter.GetBytes(HitEffect_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(HitEffect), 0, HitEffect_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(HitEffectAngle), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(KnockDownScore), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(KnockDownTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(Critical), 0, sizeof(bool));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			Damage = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			HitStopTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			StiffTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			HitDurationTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(false == XXVector2_Serializer.Load(ref HitBackDistance, _buf_)) { return false; }
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			HitBackTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int HitEffect_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(HitEffect_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] HitEffect_buf = new byte[HitEffect_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, HitEffect_buf, 0, HitEffect_length);
			HitEffect = System.Text.Encoding.UTF8.GetString(HitEffect_buf);
			_buf_.Position += HitEffect_length;
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			HitEffectAngle = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			KnockDownScore = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			KnockDownTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(bool) > _buf_.Length - _buf_.Position) { return false; }
			Critical = BitConverter.ToBoolean(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(bool);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXHitInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXHitInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXHitInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXHitInfo obj) { return obj.Size(); }
};
public enum XX_EFFECT_MOVE_TYPE {
	XX_EFFECT_MOVE_NO,
	XX_EFFECT_MOVE_GROUND_WAVE,
	XX_EFFECT_MOVE_AIR_STRIGHT,
	XX_EFFECT_MOVE_AIR_CURVE,
}; // XX_EFFECT_MOVE_TYPE
public struct XX_EFFECT_MOVE_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_EFFECT_MOVE_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_EFFECT_MOVE_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_EFFECT_MOVE_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_EFFECT_MOVE_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_EFFECT_MOVE_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_EFFECT_MOVE_TYPE obj) { return sizeof(XX_EFFECT_MOVE_TYPE); }
};

[System.Serializable]

public class XXAttackEffectInfo {
	public string	AnimationName = "";
	public string	ResourcePath = "";
	public XX_EFFECT_MOVE_TYPE	MoveType = new XX_EFFECT_MOVE_TYPE();
	public XXVector2	MoveDistance = new XXVector2();
	public float	MoveTime = 0.0f;
	public float	StartTime = 0.0f;
	public XXVector2	StartPosition = new XXVector2();
	public List<XXHitInfo >	HitInfos = new List<XXHitInfo >();
	public int	MaxHitCount = 0;
	public XXAttackEffectInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != AnimationName) { nSize += Encoding.UTF8.GetByteCount(AnimationName); }
			nSize += sizeof(int); 
			if(null != ResourcePath) { nSize += Encoding.UTF8.GetByteCount(ResourcePath); }
			nSize += XX_EFFECT_MOVE_TYPE_Serializer.Size(MoveType);
			nSize += XXVector2_Serializer.Size(MoveDistance);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += XXVector2_Serializer.Size(StartPosition);
			nSize += sizeof(int);
			foreach(var HitInfos_itr in HitInfos) { 
				XXHitInfo HitInfos_elmt = HitInfos_itr;
				nSize += XXHitInfo_Serializer.Size(HitInfos_elmt);
			}
			nSize += sizeof(int);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != AnimationName) {
				int AnimationName_length = Encoding.UTF8.GetByteCount(AnimationName);
				_buf_.Write(BitConverter.GetBytes(AnimationName_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(AnimationName), 0, AnimationName_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != ResourcePath) {
				int ResourcePath_length = Encoding.UTF8.GetByteCount(ResourcePath);
				_buf_.Write(BitConverter.GetBytes(ResourcePath_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(ResourcePath), 0, ResourcePath_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(false == XX_EFFECT_MOVE_TYPE_Serializer.Store(_buf_, MoveType)) { return false; }
			if(false == XXVector2_Serializer.Store(_buf_, MoveDistance)) { return false; }
			_buf_.Write(BitConverter.GetBytes(MoveTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(StartTime), 0, sizeof(float));
			if(false == XXVector2_Serializer.Store(_buf_, StartPosition)) { return false; }
			_buf_.Write(BitConverter.GetBytes(HitInfos.Count), 0, sizeof(int));
			foreach(var HitInfos_itr in HitInfos) { 
				XXHitInfo HitInfos_elmt = HitInfos_itr;
				if(false == XXHitInfo_Serializer.Store(_buf_, HitInfos_elmt)) { return false; }
			}
			_buf_.Write(BitConverter.GetBytes(MaxHitCount), 0, sizeof(int));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int AnimationName_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(AnimationName_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] AnimationName_buf = new byte[AnimationName_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, AnimationName_buf, 0, AnimationName_length);
			AnimationName = System.Text.Encoding.UTF8.GetString(AnimationName_buf);
			_buf_.Position += AnimationName_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int ResourcePath_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(ResourcePath_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] ResourcePath_buf = new byte[ResourcePath_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, ResourcePath_buf, 0, ResourcePath_length);
			ResourcePath = System.Text.Encoding.UTF8.GetString(ResourcePath_buf);
			_buf_.Position += ResourcePath_length;
			if(false == XX_EFFECT_MOVE_TYPE_Serializer.Load(ref MoveType, _buf_)) { return false; }
			if(false == XXVector2_Serializer.Load(ref MoveDistance, _buf_)) { return false; }
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			MoveTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			StartTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(false == XXVector2_Serializer.Load(ref StartPosition, _buf_)) { return false; }
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int HitInfos_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int HitInfos_itr=0; HitInfos_itr<HitInfos_length; HitInfos_itr++) {
				XXHitInfo HitInfos_val = new XXHitInfo();
				if(false == XXHitInfo_Serializer.Load(ref HitInfos_val, _buf_)) { return false; }
				HitInfos.Add(HitInfos_val);
			}
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			MaxHitCount = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXAttackEffectInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXAttackEffectInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXAttackEffectInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXAttackEffectInfo obj) { return obj.Size(); }
};

[System.Serializable]

public enum XX_ATTACK_TYPE {
	XX_ATTACK_INVALID = 0x0000,
	XX_ATTACK_GROUND = 0x0001,
	XX_ATTACK_AIR = 0x0010,
	XX_ATTACK_BOTH = 0x0011,
}; // XX_ATTACK_TYPE
public struct XX_ATTACK_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_ATTACK_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_ATTACK_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_ATTACK_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_ATTACK_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_ATTACK_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_ATTACK_TYPE obj) { return sizeof(XX_ATTACK_TYPE); }
};

[System.Serializable]

public class XXAttackInfo {
	public string	Name = "";
	public string	AttackID = "";
	public string	AnimationName = "";
	public float	CoolTime = 0.0f;
	public float	MoveDistance = 0.0f;
	public float	MoveTime = 0.0f;
	public float	MoveStartTime = 0.0f;
	public float	NextInputStartTime = 0.0f;
	public float	NextInputEndTime = 0.0f;
	public XX_ATTACK_TYPE	AttackType = new XX_ATTACK_TYPE();
	public string	Parent = "";
	public List<XXAttackEffectInfo >	EffectInfos = new List<XXAttackEffectInfo >();
	public XXAttackInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != Name) { nSize += Encoding.UTF8.GetByteCount(Name); }
			nSize += sizeof(int); 
			if(null != AttackID) { nSize += Encoding.UTF8.GetByteCount(AttackID); }
			nSize += sizeof(int); 
			if(null != AnimationName) { nSize += Encoding.UTF8.GetByteCount(AnimationName); }
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += XX_ATTACK_TYPE_Serializer.Size(AttackType);
			nSize += sizeof(int); 
			if(null != Parent) { nSize += Encoding.UTF8.GetByteCount(Parent); }
			nSize += sizeof(int);
			foreach(var EffectInfos_itr in EffectInfos) { 
				XXAttackEffectInfo EffectInfos_elmt = EffectInfos_itr;
				nSize += XXAttackEffectInfo_Serializer.Size(EffectInfos_elmt);
			}
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != Name) {
				int Name_length = Encoding.UTF8.GetByteCount(Name);
				_buf_.Write(BitConverter.GetBytes(Name_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(Name), 0, Name_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != AttackID) {
				int AttackID_length = Encoding.UTF8.GetByteCount(AttackID);
				_buf_.Write(BitConverter.GetBytes(AttackID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(AttackID), 0, AttackID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != AnimationName) {
				int AnimationName_length = Encoding.UTF8.GetByteCount(AnimationName);
				_buf_.Write(BitConverter.GetBytes(AnimationName_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(AnimationName), 0, AnimationName_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(CoolTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(MoveDistance), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(MoveTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(MoveStartTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(NextInputStartTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(NextInputEndTime), 0, sizeof(float));
			if(false == XX_ATTACK_TYPE_Serializer.Store(_buf_, AttackType)) { return false; }
			if(null != Parent) {
				int Parent_length = Encoding.UTF8.GetByteCount(Parent);
				_buf_.Write(BitConverter.GetBytes(Parent_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(Parent), 0, Parent_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(EffectInfos.Count), 0, sizeof(int));
			foreach(var EffectInfos_itr in EffectInfos) { 
				XXAttackEffectInfo EffectInfos_elmt = EffectInfos_itr;
				if(false == XXAttackEffectInfo_Serializer.Store(_buf_, EffectInfos_elmt)) { return false; }
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int Name_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(Name_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] Name_buf = new byte[Name_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, Name_buf, 0, Name_length);
			Name = System.Text.Encoding.UTF8.GetString(Name_buf);
			_buf_.Position += Name_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int AttackID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(AttackID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] AttackID_buf = new byte[AttackID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, AttackID_buf, 0, AttackID_length);
			AttackID = System.Text.Encoding.UTF8.GetString(AttackID_buf);
			_buf_.Position += AttackID_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int AnimationName_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(AnimationName_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] AnimationName_buf = new byte[AnimationName_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, AnimationName_buf, 0, AnimationName_length);
			AnimationName = System.Text.Encoding.UTF8.GetString(AnimationName_buf);
			_buf_.Position += AnimationName_length;
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			CoolTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			MoveDistance = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			MoveTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			MoveStartTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			NextInputStartTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			NextInputEndTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(false == XX_ATTACK_TYPE_Serializer.Load(ref AttackType, _buf_)) { return false; }
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int Parent_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(Parent_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] Parent_buf = new byte[Parent_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, Parent_buf, 0, Parent_length);
			Parent = System.Text.Encoding.UTF8.GetString(Parent_buf);
			_buf_.Position += Parent_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int EffectInfos_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int EffectInfos_itr=0; EffectInfos_itr<EffectInfos_length; EffectInfos_itr++) {
				XXAttackEffectInfo EffectInfos_val = new XXAttackEffectInfo();
				if(false == XXAttackEffectInfo_Serializer.Load(ref EffectInfos_val, _buf_)) { return false; }
				EffectInfos.Add(EffectInfos_val);
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXAttackInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXAttackInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXAttackInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXAttackInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXUnitStatInfo {
	public uint	MaxHealthPoint = 0;
	public float	AttackPoint = 0.0f;
	public float	DefensePoint = 0.0f;
	public XXUnitStatInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(uint);
			nSize += sizeof(float);
			nSize += sizeof(float);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(MaxHealthPoint), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(AttackPoint), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(DefensePoint), 0, sizeof(float));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			MaxHealthPoint = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			AttackPoint = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			DefensePoint = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXUnitStatInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXUnitStatInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXUnitStatInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXUnitStatInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXMonsterStatInfo : XXUnitStatInfo {
	public string	MonsterID = "";
	public string	Name = "";
	public uint	Level = 0;
	public XXMonsterStatInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize = base.Size();
			nSize += sizeof(int); 
			if(null != MonsterID) { nSize += Encoding.UTF8.GetByteCount(MonsterID); }
			nSize += sizeof(int); 
			if(null != Name) { nSize += Encoding.UTF8.GetByteCount(Name); }
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			base.Store(_buf_);
			if(null != MonsterID) {
				int MonsterID_length = Encoding.UTF8.GetByteCount(MonsterID);
				_buf_.Write(BitConverter.GetBytes(MonsterID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(MonsterID), 0, MonsterID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			if(null != Name) {
				int Name_length = Encoding.UTF8.GetByteCount(Name);
				_buf_.Write(BitConverter.GetBytes(Name_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(Name), 0, Name_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(Level), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == base.Load(_buf_)) return false;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MonsterID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(MonsterID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] MonsterID_buf = new byte[MonsterID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, MonsterID_buf, 0, MonsterID_length);
			MonsterID = System.Text.Encoding.UTF8.GetString(MonsterID_buf);
			_buf_.Position += MonsterID_length;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int Name_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(Name_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] Name_buf = new byte[Name_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, Name_buf, 0, Name_length);
			Name = System.Text.Encoding.UTF8.GetString(Name_buf);
			_buf_.Position += Name_length;
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			Level = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMonsterStatInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXMonsterStatInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMonsterStatInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMonsterStatInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXPlayerStatInfo : XXUnitStatInfo {
	public uint	Level = 0;
	public uint	NeedExp = 0;
	public float	HealBonusRate = 0.0f;
	public float	CooltimeBonusRate = 0.0f;
	public float	ExpBonusRate = 0.0f;
	public float	ItemBonusRate = 0.0f;
	public float	GoldBonusRate = 0.0f;
	public float	DamageBonusRate = 0.0f;
	public XXPlayerStatInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize = base.Size();
			nSize += sizeof(uint);
			nSize += sizeof(uint);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			base.Store(_buf_);
			_buf_.Write(BitConverter.GetBytes(Level), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(NeedExp), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(HealBonusRate), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(CooltimeBonusRate), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(ExpBonusRate), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(ItemBonusRate), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(GoldBonusRate), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(DamageBonusRate), 0, sizeof(float));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == base.Load(_buf_)) return false;
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			Level = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			NeedExp = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			HealBonusRate = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			CooltimeBonusRate = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			ExpBonusRate = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			ItemBonusRate = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			GoldBonusRate = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			DamageBonusRate = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXPlayerStatInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXPlayerStatInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXPlayerStatInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXPlayerStatInfo obj) { return obj.Size(); }
};
public enum XX_UNIT_TYPE {
	XX_UNIT_GROUND,
	XX_UNIT_AIR,
}; // XX_UNIT_TYPE
public struct XX_UNIT_TYPE_Serializer {
	public static bool Store(System.IO.MemoryStream _buf_, XX_UNIT_TYPE obj) { 
		try {
			_buf_.Write(System.BitConverter.GetBytes((int)obj), 0, sizeof(XX_UNIT_TYPE));
		}
		catch(System.Exception) {
			return false;
		}
		return true;
	}
	public static bool Load(ref XX_UNIT_TYPE obj, MemoryStream _buf_) { 
		try {
			obj = (XX_UNIT_TYPE)System.BitConverter.ToInt32(_buf_.ToArray(), (int)_buf_.Position);
			_buf_.Position += sizeof(XX_UNIT_TYPE);
		}
		catch(System.Exception) { 
			return false;
		}
		return true;
	}
	public static System.Int32 Size(XX_UNIT_TYPE obj) { return sizeof(XX_UNIT_TYPE); }
};

[System.Serializable]

public class XXUnitInfo {
	public string	Name = "";
	public uint	Level = 0;
	public XX_UNIT_TYPE	UnitType = new XX_UNIT_TYPE();
	public ushort	MaxJumpCount = 0;
	public float	JumpPower = 0.0f;
	public float	JumpTime = 0.0f;
	public float	Gravity = 0.0f;
	public float	GroundDamp = 0.0f;
	public float	AirDamp = 0.0f;
	public float	CriticalChance = 0.0f;
	public float	CriticalPoint = 0.0f;
	public float	DodgeChance = 0.0f;
	public float	HitChance = 0.0f;
	public float	MoveSpeed = 0.0f;
	public float	AttackSpeed = 0.0f;
	public float	MinAttackRange = 0.0f;
	public float	MaxAttackRange = 0.0f;
	public float	SightRange = 0.0f;
	public float	HitBackResistance = 0.0f;
	public float	StiffRegistance = 0.0f;
	public float	KnockDownTime = 0.0f;
	public XXUnitInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != Name) { nSize += Encoding.UTF8.GetByteCount(Name); }
			nSize += sizeof(uint);
			nSize += XX_UNIT_TYPE_Serializer.Size(UnitType);
			nSize += sizeof(ushort);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
			nSize += sizeof(float);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != Name) {
				int Name_length = Encoding.UTF8.GetByteCount(Name);
				_buf_.Write(BitConverter.GetBytes(Name_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(Name), 0, Name_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(Level), 0, sizeof(uint));
			if(false == XX_UNIT_TYPE_Serializer.Store(_buf_, UnitType)) { return false; }
			_buf_.Write(BitConverter.GetBytes(MaxJumpCount), 0, sizeof(ushort));
			_buf_.Write(BitConverter.GetBytes(JumpPower), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(JumpTime), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(Gravity), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(GroundDamp), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(AirDamp), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(CriticalChance), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(CriticalPoint), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(DodgeChance), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(HitChance), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(MoveSpeed), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(AttackSpeed), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(MinAttackRange), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(MaxAttackRange), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(SightRange), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(HitBackResistance), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(StiffRegistance), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(KnockDownTime), 0, sizeof(float));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int Name_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(Name_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] Name_buf = new byte[Name_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, Name_buf, 0, Name_length);
			Name = System.Text.Encoding.UTF8.GetString(Name_buf);
			_buf_.Position += Name_length;
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			Level = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(false == XX_UNIT_TYPE_Serializer.Load(ref UnitType, _buf_)) { return false; }
			if(sizeof(ushort) > _buf_.Length - _buf_.Position) { return false; }
			MaxJumpCount = BitConverter.ToUInt16(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(ushort);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			JumpPower = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			JumpTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			Gravity = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			GroundDamp = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			AirDamp = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			CriticalChance = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			CriticalPoint = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			DodgeChance = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			HitChance = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			MoveSpeed = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			AttackSpeed = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			MinAttackRange = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			MaxAttackRange = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			SightRange = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			HitBackResistance = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			StiffRegistance = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			KnockDownTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXUnitInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXUnitInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXUnitInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXUnitInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXMonsterInfo : XXUnitInfo {
	public string	MonsterID = "";
	public uint	MaxHealthPoint = 0;
	public float	AttackPoint = 0.0f;
	public float	DefensePoint = 0.0f;
	public XXMonsterInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize = base.Size();
			nSize += sizeof(int); 
			if(null != MonsterID) { nSize += Encoding.UTF8.GetByteCount(MonsterID); }
			nSize += sizeof(uint);
			nSize += sizeof(float);
			nSize += sizeof(float);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			base.Store(_buf_);
			if(null != MonsterID) {
				int MonsterID_length = Encoding.UTF8.GetByteCount(MonsterID);
				_buf_.Write(BitConverter.GetBytes(MonsterID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(MonsterID), 0, MonsterID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(MaxHealthPoint), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(AttackPoint), 0, sizeof(float));
			_buf_.Write(BitConverter.GetBytes(DefensePoint), 0, sizeof(float));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == base.Load(_buf_)) return false;
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MonsterID_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			if(MonsterID_length > _buf_.Length - _buf_.Position) { return false; }
			byte[] MonsterID_buf = new byte[MonsterID_length];
			Array.Copy(_buf_.GetBuffer(), (int)_buf_.Position, MonsterID_buf, 0, MonsterID_length);
			MonsterID = System.Text.Encoding.UTF8.GetString(MonsterID_buf);
			_buf_.Position += MonsterID_length;
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			MaxHealthPoint = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			AttackPoint = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			DefensePoint = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMonsterInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXMonsterInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMonsterInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMonsterInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXPlayerData : XXUnitData {
	public uint	UserSEQ = 0;
	public XXPlayerData() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize = base.Size();
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			base.Store(_buf_);
			_buf_.Write(BitConverter.GetBytes(UserSEQ), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == base.Load(_buf_)) return false;
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			UserSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXPlayerData_Serializer {
	public static bool Store(MemoryStream _buf_, XXPlayerData obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXPlayerData obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXPlayerData obj) { return obj.Size(); }
};

[System.Serializable]

public class XXPlayerStatData {
	public string	UserID = "";
	public uint	UserSEQ = 0;
	public uint	Level = 0;
	public uint	Exp = 0;
	public XX_PLAYER_STATE_TYPE	State = new XX_PLAYER_STATE_TYPE();
	public XXPlayerStatData() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != UserID) { nSize += Encoding.UTF8.GetByteCount(UserID); }
			nSize += sizeof(uint);
			nSize += sizeof(uint);
			nSize += sizeof(uint);
			nSize += XX_PLAYER_STATE_TYPE_Serializer.Size(State);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(null != UserID) {
				int UserID_length = Encoding.UTF8.GetByteCount(UserID);
				_buf_.Write(BitConverter.GetBytes(UserID_length), 0, sizeof(int));
				_buf_.Write(Encoding.UTF8.GetBytes(UserID), 0, UserID_length);
			}
			else {
				_buf_.Write(BitConverter.GetBytes(0), 0, sizeof(int));
			}
			_buf_.Write(BitConverter.GetBytes(UserSEQ), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(Level), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(Exp), 0, sizeof(uint));
			if(false == XX_PLAYER_STATE_TYPE_Serializer.Store(_buf_, State)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
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
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			Level = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			Exp = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(false == XX_PLAYER_STATE_TYPE_Serializer.Load(ref State, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXPlayerStatData_Serializer {
	public static bool Store(MemoryStream _buf_, XXPlayerStatData obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXPlayerStatData obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXPlayerStatData obj) { return obj.Size(); }
};

[System.Serializable]

public class XXMonsterStatData {
	public uint	MonsterSEQ = 0;
	public XXMonsterStatData() {
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
			_buf_.Write(BitConverter.GetBytes(MonsterSEQ), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			MonsterSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXMonsterStatData_Serializer {
	public static bool Store(MemoryStream _buf_, XXMonsterStatData obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMonsterStatData obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMonsterStatData obj) { return obj.Size(); }
};

[System.Serializable]

public class XXFieldInfo {
	public List<XXMonsterStatInfo >	MonsterStatInfos = new List<XXMonsterStatInfo >();
	public List<XXRoomInfo >	RoomInfos = new List<XXRoomInfo >();
	public XXFieldInfo() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int);
			foreach(var MonsterStatInfos_itr in MonsterStatInfos) { 
				XXMonsterStatInfo MonsterStatInfos_elmt = MonsterStatInfos_itr;
				nSize += XXMonsterStatInfo_Serializer.Size(MonsterStatInfos_elmt);
			}
			nSize += sizeof(int);
			foreach(var RoomInfos_itr in RoomInfos) { 
				XXRoomInfo RoomInfos_elmt = RoomInfos_itr;
				nSize += XXRoomInfo_Serializer.Size(RoomInfos_elmt);
			}
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(MonsterStatInfos.Count), 0, sizeof(int));
			foreach(var MonsterStatInfos_itr in MonsterStatInfos) { 
				XXMonsterStatInfo MonsterStatInfos_elmt = MonsterStatInfos_itr;
				if(false == XXMonsterStatInfo_Serializer.Store(_buf_, MonsterStatInfos_elmt)) { return false; }
			}
			_buf_.Write(BitConverter.GetBytes(RoomInfos.Count), 0, sizeof(int));
			foreach(var RoomInfos_itr in RoomInfos) { 
				XXRoomInfo RoomInfos_elmt = RoomInfos_itr;
				if(false == XXRoomInfo_Serializer.Store(_buf_, RoomInfos_elmt)) { return false; }
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MonsterStatInfos_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int MonsterStatInfos_itr=0; MonsterStatInfos_itr<MonsterStatInfos_length; MonsterStatInfos_itr++) {
				XXMonsterStatInfo MonsterStatInfos_val = new XXMonsterStatInfo();
				if(false == XXMonsterStatInfo_Serializer.Load(ref MonsterStatInfos_val, _buf_)) { return false; }
				MonsterStatInfos.Add(MonsterStatInfos_val);
			}
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int RoomInfos_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int RoomInfos_itr=0; RoomInfos_itr<RoomInfos_length; RoomInfos_itr++) {
				XXRoomInfo RoomInfos_val = new XXRoomInfo();
				if(false == XXRoomInfo_Serializer.Load(ref RoomInfos_val, _buf_)) { return false; }
				RoomInfos.Add(RoomInfos_val);
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXFieldInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXFieldInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXFieldInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXFieldInfo obj) { return obj.Size(); }
};

[System.Serializable]

public class XXRoomData {
	public uint	FrameCount = 0;
	public uint	CurrentTime = 0;
	public List<XXPlayerData >	PlayerDatas = new List<XXPlayerData >();
	public List<XXMonsterData >	MonsterDatas = new List<XXMonsterData >();
	public XXRoomData() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(uint);
			nSize += sizeof(uint);
			nSize += sizeof(int);
			foreach(var PlayerDatas_itr in PlayerDatas) { 
				XXPlayerData PlayerDatas_elmt = PlayerDatas_itr;
				nSize += XXPlayerData_Serializer.Size(PlayerDatas_elmt);
			}
			nSize += sizeof(int);
			foreach(var MonsterDatas_itr in MonsterDatas) { 
				XXMonsterData MonsterDatas_elmt = MonsterDatas_itr;
				nSize += XXMonsterData_Serializer.Size(MonsterDatas_elmt);
			}
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(FrameCount), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(CurrentTime), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(PlayerDatas.Count), 0, sizeof(int));
			foreach(var PlayerDatas_itr in PlayerDatas) { 
				XXPlayerData PlayerDatas_elmt = PlayerDatas_itr;
				if(false == XXPlayerData_Serializer.Store(_buf_, PlayerDatas_elmt)) { return false; }
			}
			_buf_.Write(BitConverter.GetBytes(MonsterDatas.Count), 0, sizeof(int));
			foreach(var MonsterDatas_itr in MonsterDatas) { 
				XXMonsterData MonsterDatas_elmt = MonsterDatas_itr;
				if(false == XXMonsterData_Serializer.Store(_buf_, MonsterDatas_elmt)) { return false; }
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			FrameCount = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			CurrentTime = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int PlayerDatas_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int PlayerDatas_itr=0; PlayerDatas_itr<PlayerDatas_length; PlayerDatas_itr++) {
				XXPlayerData PlayerDatas_val = new XXPlayerData();
				if(false == XXPlayerData_Serializer.Load(ref PlayerDatas_val, _buf_)) { return false; }
				PlayerDatas.Add(PlayerDatas_val);
			}
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int MonsterDatas_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int MonsterDatas_itr=0; MonsterDatas_itr<MonsterDatas_length; MonsterDatas_itr++) {
				XXMonsterData MonsterDatas_val = new XXMonsterData();
				if(false == XXMonsterData_Serializer.Load(ref MonsterDatas_val, _buf_)) { return false; }
				MonsterDatas.Add(MonsterDatas_val);
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXRoomData_Serializer {
	public static bool Store(MemoryStream _buf_, XXRoomData obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXRoomData obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXRoomData obj) { return obj.Size(); }
};

[System.Serializable]

public class XXFieldData {
	public string	MapID = "";
	public uint	FieldSEQ = 0;
	public XX_FIELD_STATE_TYPE	State = new XX_FIELD_STATE_TYPE();
	public List<XXPlayerStatData >	PlayerStatDatas = new List<XXPlayerStatData >();
	public XXFieldData() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != MapID) { nSize += Encoding.UTF8.GetByteCount(MapID); }
			nSize += sizeof(uint);
			nSize += XX_FIELD_STATE_TYPE_Serializer.Size(State);
			nSize += sizeof(int);
			foreach(var PlayerStatDatas_itr in PlayerStatDatas) { 
				XXPlayerStatData PlayerStatDatas_elmt = PlayerStatDatas_itr;
				nSize += XXPlayerStatData_Serializer.Size(PlayerStatDatas_elmt);
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
			_buf_.Write(BitConverter.GetBytes(FieldSEQ), 0, sizeof(uint));
			if(false == XX_FIELD_STATE_TYPE_Serializer.Store(_buf_, State)) { return false; }
			_buf_.Write(BitConverter.GetBytes(PlayerStatDatas.Count), 0, sizeof(int));
			foreach(var PlayerStatDatas_itr in PlayerStatDatas) { 
				XXPlayerStatData PlayerStatDatas_elmt = PlayerStatDatas_itr;
				if(false == XXPlayerStatData_Serializer.Store(_buf_, PlayerStatDatas_elmt)) { return false; }
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
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			FieldSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(false == XX_FIELD_STATE_TYPE_Serializer.Load(ref State, _buf_)) { return false; }
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int PlayerStatDatas_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int PlayerStatDatas_itr=0; PlayerStatDatas_itr<PlayerStatDatas_length; PlayerStatDatas_itr++) {
				XXPlayerStatData PlayerStatDatas_val = new XXPlayerStatData();
				if(false == XXPlayerStatData_Serializer.Load(ref PlayerStatDatas_val, _buf_)) { return false; }
				PlayerStatDatas.Add(PlayerStatDatas_val);
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct XXFieldData_Serializer {
	public static bool Store(MemoryStream _buf_, XXFieldData obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXFieldData obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXFieldData obj) { return obj.Size(); }
};
public class MsgCliSvr_Field_CreateField_Req {
	public const int MSG_ID = 1;
	public string	AccountID = "";
	public MsgCliSvr_Field_CreateField_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != AccountID) { nSize += Encoding.UTF8.GetByteCount(AccountID); }
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
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgCliSvr_Field_CreateField_Req_Serializer {
	public static bool Store(MemoryStream _buf_, MsgCliSvr_Field_CreateField_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgCliSvr_Field_CreateField_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgCliSvr_Field_CreateField_Req obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_CreateField_Ans {
	public const int MSG_ID = 1;
	public XXError	Error = new XXError();
	public XXAccountInfo	AccountInfo = new XXAccountInfo();
	public XXFieldInfo	FieldInfo = new XXFieldInfo();
	public XXFieldData	FieldData = new XXFieldData();
	public XXPlayerStatData	PlayerStatData = new XXPlayerStatData();
	public MsgSvrCli_Field_CreateField_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXError_Serializer.Size(Error);
			nSize += XXAccountInfo_Serializer.Size(AccountInfo);
			nSize += XXFieldInfo_Serializer.Size(FieldInfo);
			nSize += XXFieldData_Serializer.Size(FieldData);
			nSize += XXPlayerStatData_Serializer.Size(PlayerStatData);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Store(_buf_, Error)) { return false; }
			if(false == XXAccountInfo_Serializer.Store(_buf_, AccountInfo)) { return false; }
			if(false == XXFieldInfo_Serializer.Store(_buf_, FieldInfo)) { return false; }
			if(false == XXFieldData_Serializer.Store(_buf_, FieldData)) { return false; }
			if(false == XXPlayerStatData_Serializer.Store(_buf_, PlayerStatData)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXError_Serializer.Load(ref Error, _buf_)) { return false; }
			if(false == XXAccountInfo_Serializer.Load(ref AccountInfo, _buf_)) { return false; }
			if(false == XXFieldInfo_Serializer.Load(ref FieldInfo, _buf_)) { return false; }
			if(false == XXFieldData_Serializer.Load(ref FieldData, _buf_)) { return false; }
			if(false == XXPlayerStatData_Serializer.Load(ref PlayerStatData, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgSvrCli_Field_CreateField_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_CreateField_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_CreateField_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_CreateField_Ans obj) { return obj.Size(); }
};
public class MsgCliSvr_Field_JoinField_Req {
	public const int MSG_ID = 2;
	public string	AccountID = "";
	public uint	FieldSEQ = 0;
	public MsgCliSvr_Field_JoinField_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int); 
			if(null != AccountID) { nSize += Encoding.UTF8.GetByteCount(AccountID); }
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
			_buf_.Write(BitConverter.GetBytes(FieldSEQ), 0, sizeof(uint));
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
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			FieldSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgCliSvr_Field_JoinField_Req_Serializer {
	public static bool Store(MemoryStream _buf_, MsgCliSvr_Field_JoinField_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgCliSvr_Field_JoinField_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgCliSvr_Field_JoinField_Req obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_JoinField_Ans : MsgSvrCli_Field_CreateField_Ans {
	public const int MSG_ID = 2;
	public MsgSvrCli_Field_JoinField_Ans() {
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
public struct MsgSvrCli_Field_JoinField_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_JoinField_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_JoinField_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_JoinField_Ans obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_JoinField_Ntf {
	public const int MSG_ID = 100006;
	public MsgSvrCli_Field_JoinField_Ntf() {
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
public struct MsgSvrCli_Field_JoinField_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_JoinField_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_JoinField_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_JoinField_Ntf obj) { return obj.Size(); }
};
public class MsgCliSvr_Field_ReadyGame_Ntf {
	public const int MSG_ID = 100001;
	public MsgCliSvr_Field_ReadyGame_Ntf() {
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
public struct MsgCliSvr_Field_ReadyGame_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgCliSvr_Field_ReadyGame_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgCliSvr_Field_ReadyGame_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgCliSvr_Field_ReadyGame_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_PlayerStatDatas_Ntf {
	public const int MSG_ID = 100002;
	public List<XXPlayerStatData >	PlayerStatDatas = new List<XXPlayerStatData >();
	public MsgSvrCli_Field_PlayerStatDatas_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(int);
			foreach(var PlayerStatDatas_itr in PlayerStatDatas) { 
				XXPlayerStatData PlayerStatDatas_elmt = PlayerStatDatas_itr;
				nSize += XXPlayerStatData_Serializer.Size(PlayerStatDatas_elmt);
			}
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(PlayerStatDatas.Count), 0, sizeof(int));
			foreach(var PlayerStatDatas_itr in PlayerStatDatas) { 
				XXPlayerStatData PlayerStatDatas_elmt = PlayerStatDatas_itr;
				if(false == XXPlayerStatData_Serializer.Store(_buf_, PlayerStatDatas_elmt)) { return false; }
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(int) > _buf_.Length - _buf_.Position) { return false; }
			int PlayerStatDatas_length = BitConverter.ToInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(int);
			for(int PlayerStatDatas_itr=0; PlayerStatDatas_itr<PlayerStatDatas_length; PlayerStatDatas_itr++) {
				XXPlayerStatData PlayerStatDatas_val = new XXPlayerStatData();
				if(false == XXPlayerStatData_Serializer.Load(ref PlayerStatDatas_val, _buf_)) { return false; }
				PlayerStatDatas.Add(PlayerStatDatas_val);
			}
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgSvrCli_Field_PlayerStatDatas_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_PlayerStatDatas_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_PlayerStatDatas_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_PlayerStatDatas_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_PlayerStatData_Ntf {
	public const int MSG_ID = 100003;
	public XXPlayerStatData	PlayerStatData = new XXPlayerStatData();
	public MsgSvrCli_Field_PlayerStatData_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXPlayerStatData_Serializer.Size(PlayerStatData);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXPlayerStatData_Serializer.Store(_buf_, PlayerStatData)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXPlayerStatData_Serializer.Load(ref PlayerStatData, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgSvrCli_Field_PlayerStatData_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_PlayerStatData_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_PlayerStatData_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_PlayerStatData_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_PlayerData_Ntf {
	public const int MSG_ID = 100013;
	public XXPlayerData	PlayerData = new XXPlayerData();
	public MsgSvrCli_Field_PlayerData_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXPlayerData_Serializer.Size(PlayerData);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXPlayerData_Serializer.Store(_buf_, PlayerData)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXPlayerData_Serializer.Load(ref PlayerData, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgSvrCli_Field_PlayerData_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_PlayerData_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_PlayerData_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_PlayerData_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_InitRoomData_Ntf {
	public const int MSG_ID = 100004;
	public XXRoomData	RoomData = new XXRoomData();
	public MsgSvrCli_Field_InitRoomData_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXRoomData_Serializer.Size(RoomData);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXRoomData_Serializer.Store(_buf_, RoomData)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXRoomData_Serializer.Load(ref RoomData, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgSvrCli_Field_InitRoomData_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_InitRoomData_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_InitRoomData_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_InitRoomData_Ntf obj) { return obj.Size(); }
};
public class MsgCliSvr_Field_CompleteRoomData_Ntf {
	public const int MSG_ID = 100005;
	public MsgCliSvr_Field_CompleteRoomData_Ntf() {
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
public struct MsgCliSvr_Field_CompleteRoomData_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgCliSvr_Field_CompleteRoomData_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgCliSvr_Field_CompleteRoomData_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgCliSvr_Field_CompleteRoomData_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_StartGame_Ntf {
	public const int MSG_ID = 100006;
	public MsgSvrCli_Field_StartGame_Ntf() {
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
public struct MsgSvrCli_Field_StartGame_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_StartGame_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_StartGame_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_StartGame_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_RoomData_Ntf {
	public const int MSG_ID = 100007;
	public XXRoomData	RoomData = new XXRoomData();
	public MsgSvrCli_Field_RoomData_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XXRoomData_Serializer.Size(RoomData);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XXRoomData_Serializer.Store(_buf_, RoomData)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XXRoomData_Serializer.Load(ref RoomData, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgSvrCli_Field_RoomData_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_RoomData_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_RoomData_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_RoomData_Ntf obj) { return obj.Size(); }
};
public class MsgCliSvr_Field_MoveButtonDown_Ntf {
	public const int MSG_ID = 100008;
	public XX_UNIT_DIRECTION_TYPE	Direction = new XX_UNIT_DIRECTION_TYPE();
	public XXVector2	Position = new XXVector2();
	public XXVector2	Velocity = new XXVector2();
	public MsgCliSvr_Field_MoveButtonDown_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += XX_UNIT_DIRECTION_TYPE_Serializer.Size(Direction);
			nSize += XXVector2_Serializer.Size(Position);
			nSize += XXVector2_Serializer.Size(Velocity);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			if(false == XX_UNIT_DIRECTION_TYPE_Serializer.Store(_buf_, Direction)) { return false; }
			if(false == XXVector2_Serializer.Store(_buf_, Position)) { return false; }
			if(false == XXVector2_Serializer.Store(_buf_, Velocity)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == XX_UNIT_DIRECTION_TYPE_Serializer.Load(ref Direction, _buf_)) { return false; }
			if(false == XXVector2_Serializer.Load(ref Position, _buf_)) { return false; }
			if(false == XXVector2_Serializer.Load(ref Velocity, _buf_)) { return false; }
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgCliSvr_Field_MoveButtonDown_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgCliSvr_Field_MoveButtonDown_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgCliSvr_Field_MoveButtonDown_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgCliSvr_Field_MoveButtonDown_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_MoveButtonDown_Ntf : MsgCliSvr_Field_MoveButtonDown_Ntf {
	public const int MSG_ID = 100008;
	public uint	UserSEQ = 0;
	public MsgSvrCli_Field_MoveButtonDown_Ntf() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize = base.Size();
			nSize += sizeof(uint);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			base.Store(_buf_);
			_buf_.Write(BitConverter.GetBytes(UserSEQ), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(false == base.Load(_buf_)) return false;
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			UserSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgSvrCli_Field_MoveButtonDown_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_MoveButtonDown_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_MoveButtonDown_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_MoveButtonDown_Ntf obj) { return obj.Size(); }
};
public class MsgCliSvr_Field_MoveButtonUp_Ntf {
	public const int MSG_ID = 100009;
	public MsgCliSvr_Field_MoveButtonUp_Ntf() {
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
public struct MsgCliSvr_Field_MoveButtonUp_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgCliSvr_Field_MoveButtonUp_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgCliSvr_Field_MoveButtonUp_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgCliSvr_Field_MoveButtonUp_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_MoveButtonUp_Ntf {
	public const int MSG_ID = 100009;
	public uint	UserSEQ = 0;
	public MsgSvrCli_Field_MoveButtonUp_Ntf() {
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
public struct MsgSvrCli_Field_MoveButtonUp_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_MoveButtonUp_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_MoveButtonUp_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_MoveButtonUp_Ntf obj) { return obj.Size(); }
};
public class MsgClisVr_Field_PlayerAttack_Ntf {
	public const int MSG_ID = 100010;
	public uint	MonsterSEQ = 0;
	public MsgClisVr_Field_PlayerAttack_Ntf() {
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
			_buf_.Write(BitConverter.GetBytes(MonsterSEQ), 0, sizeof(uint));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			MonsterSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgClisVr_Field_PlayerAttack_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgClisVr_Field_PlayerAttack_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgClisVr_Field_PlayerAttack_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgClisVr_Field_PlayerAttack_Ntf obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_Kickout_Ntf {
	public const int MSG_ID = 100011;
	public MsgSvrCli_Field_Kickout_Ntf() {
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
public struct MsgSvrCli_Field_Kickout_Ntf_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_Kickout_Ntf obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_Kickout_Ntf obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_Kickout_Ntf obj) { return obj.Size(); }
};
public class MsgCliSvr_Field_StressTest_Req {
	public const int MSG_ID = 200000;
	public uint	MsgSEQ = 0;
	public float	SendTime = 0.0f;
	public MsgCliSvr_Field_StressTest_Req() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(uint);
			nSize += sizeof(float);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(MsgSEQ), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(SendTime), 0, sizeof(float));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			MsgSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			SendTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgCliSvr_Field_StressTest_Req_Serializer {
	public static bool Store(MemoryStream _buf_, MsgCliSvr_Field_StressTest_Req obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgCliSvr_Field_StressTest_Req obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgCliSvr_Field_StressTest_Req obj) { return obj.Size(); }
};
public class MsgSvrCli_Field_StressTest_Ans {
	public const int MSG_ID = 200000;
	public uint	MsgSEQ = 0;
	public float	SendTime = 0.0f;
	public MsgSvrCli_Field_StressTest_Ans() {
	}
	public int Size() {
		int nSize = 0;
		try {
			nSize += sizeof(uint);
			nSize += sizeof(float);
		} catch(System.Exception) {
			return -1;
		}
		return nSize;
	}
	public bool Store(MemoryStream _buf_) {
		try {
			_buf_.Write(BitConverter.GetBytes(MsgSEQ), 0, sizeof(uint));
			_buf_.Write(BitConverter.GetBytes(SendTime), 0, sizeof(float));
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
	public bool Load(MemoryStream _buf_) {
		try {
			if(sizeof(uint) > _buf_.Length - _buf_.Position) { return false; }
			MsgSEQ = BitConverter.ToUInt32(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(uint);
			if(sizeof(float) > _buf_.Length - _buf_.Position) { return false; }
			SendTime = BitConverter.ToSingle(_buf_.GetBuffer(), (int)_buf_.Position);
			_buf_.Position += sizeof(float);
		} catch(System.Exception) {
			return false;
		}
		return true;
	}
};
public struct MsgSvrCli_Field_StressTest_Ans_Serializer {
	public static bool Store(MemoryStream _buf_, MsgSvrCli_Field_StressTest_Ans obj) { return obj.Store(_buf_); }
	public static bool Load(ref MsgSvrCli_Field_StressTest_Ans obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(MsgSvrCli_Field_StressTest_Ans obj) { return obj.Size(); }
};
}
