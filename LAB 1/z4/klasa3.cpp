#include<windows.h>
#include"Klasa3.h"
#include <cstdio>

extern volatile int usageCount;


Klasa3::Klasa3() {
	usageCount++;
};


Klasa3::~Klasa3() {
	usageCount--;
};


ULONG STDMETHODCALLTYPE Klasa3::AddRef() {
	/*
	Tutaj zaimplementuj dodawanie referencji na obiekt.
	 */
	InterlockedIncrement(&m_ref);
	return m_ref;
};


ULONG STDMETHODCALLTYPE Klasa3::Release() {
	/*
	Tutaj zaimplementuj usuwania referencji na obiekt.
	Jeżeli nie istnieje żadna referencja obiekt powinien zostać usunięty.
	 */
	ULONG rv = InterlockedDecrement(&m_ref);
	if (rv == 0) delete this;
	return rv;
};


HRESULT STDMETHODCALLTYPE Klasa3::QueryInterface(REFIID iid, void** ptr) {
	if (ptr == NULL) return E_POINTER;
	if (IsBadWritePtr(ptr, sizeof(void*))) return E_POINTER;
	*ptr = NULL;
	if (iid == IID_IUnknown) *ptr = this;
	if (iid == IID_IKlasa3) *ptr = this;
	if (*ptr != NULL) { AddRef(); return S_OK; };
	return E_NOINTERFACE;
};

HRESULT STDMETHODCALLTYPE Klasa3::Test(const char* napis) {
	printf("%s", napis);
	return S_OK;
	/*
	Tutaj zaimplementuj funkcję drukującą napis.
	 */
};
