#ifndef IKLASA_H
#define IKLASA_H

#include<windows.h>

DEFINE_GUID(CLSID_Klasa2, 0x6c211f4f, 0xb068, 0x4f9c, 0xb5, 0xb7, 0x7e, 0x9a, 0x94, 0x23, 0xa6, 0x4a);

DEFINE_GUID(IID_IKlasa2, 0xafeac528, 0x6033, 0x4e9b, 0x85, 0xd7, 0x36, 0xeb, 0xe5, 0x6a, 0x4a, 0x81);

class IKlasa2 : public IUnknown {
public:
	virtual HRESULT STDMETHODCALLTYPE Test(int i) = 0;
};

#endif
