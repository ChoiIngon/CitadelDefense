using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace XXItem{
public class XXItemInfo {
	public XXItemInfo() {
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
public struct XXItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXItemInfo obj) { return obj.Size(); }
};
public class XXWeaponItemInfo {
	public XXWeaponItemInfo() {
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
public struct XXWeaponItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXWeaponItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXWeaponItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXWeaponItemInfo obj) { return obj.Size(); }
};
public class XXAncientWeaponItemInfo : XXWeaponItemInfo {
	public XXAncientWeaponItemInfo() {
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
public struct XXAncientWeaponItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXAncientWeaponItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXAncientWeaponItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXAncientWeaponItemInfo obj) { return obj.Size(); }
};
public class XXMadeWeaponItemInfo : XXWeaponItemInfo {
	public XXMadeWeaponItemInfo() {
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
public struct XXMadeWeaponItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXMadeWeaponItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMadeWeaponItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMadeWeaponItemInfo obj) { return obj.Size(); }
};
public class XXArmorItemInfo {
	public XXArmorItemInfo() {
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
public struct XXArmorItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXArmorItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXArmorItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXArmorItemInfo obj) { return obj.Size(); }
};
public class XXAncientArmorItemInfo : XXArmorItemInfo {
	public XXAncientArmorItemInfo() {
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
public struct XXAncientArmorItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXAncientArmorItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXAncientArmorItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXAncientArmorItemInfo obj) { return obj.Size(); }
};
public class XXMadeArmorItemInfo : XXArmorItemInfo {
	public XXMadeArmorItemInfo() {
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
public struct XXMadeArmorItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXMadeArmorItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMadeArmorItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMadeArmorItemInfo obj) { return obj.Size(); }
};
public class XXAccessaryItemInfo {
	public XXAccessaryItemInfo() {
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
public struct XXAccessaryItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXAccessaryItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXAccessaryItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXAccessaryItemInfo obj) { return obj.Size(); }
};
public class XXAncientAccessaryItemInfo : XXAccessaryItemInfo {
	public XXAncientAccessaryItemInfo() {
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
public struct XXAncientAccessaryItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXAncientAccessaryItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXAncientAccessaryItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXAncientAccessaryItemInfo obj) { return obj.Size(); }
};
public class XXMadeAccessaryItemInfo : XXAccessaryItemInfo {
	public XXMadeAccessaryItemInfo() {
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
public struct XXMadeAccessaryItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXMadeAccessaryItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMadeAccessaryItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMadeAccessaryItemInfo obj) { return obj.Size(); }
};
public class XXMaterialItemInfo {
	public XXMaterialItemInfo() {
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
public struct XXMaterialItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXMaterialItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXMaterialItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXMaterialItemInfo obj) { return obj.Size(); }
};
public class XXItemBluePrintInfo {
	public XXItemBluePrintInfo() {
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
public struct XXItemBluePrintInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXItemBluePrintInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXItemBluePrintInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXItemBluePrintInfo obj) { return obj.Size(); }
};
public class XXQuestItemInfo {
	public XXQuestItemInfo() {
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
public struct XXQuestItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXQuestItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXQuestItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXQuestItemInfo obj) { return obj.Size(); }
};
public class XXExpendableItemInfo {
	public XXExpendableItemInfo() {
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
public struct XXExpendableItemInfo_Serializer {
	public static bool Store(MemoryStream _buf_, XXExpendableItemInfo obj) { return obj.Store(_buf_); }
	public static bool Load(ref XXExpendableItemInfo obj, MemoryStream _buf_) { return obj.Load(_buf_); }
	public static int Size(XXExpendableItemInfo obj) { return obj.Size(); }
};
}
