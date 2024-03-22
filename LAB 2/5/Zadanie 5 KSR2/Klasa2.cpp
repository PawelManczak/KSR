#include "Klasa2.h"
#include <Windows.h>
#include <iostream>


extern volatile ULONG numberOfUsedInstances;

Klasa2::Klasa2()
{
	InterlockedIncrement(&numberOfUsedInstances);
	/*
	Tutaj zainicjuj wartością zerową licznik referencji
	*/
	refCount = 0;
}


Klasa2::~Klasa2()
{
	InterlockedDecrement(&numberOfUsedInstances);
}

HRESULT Klasa2::QueryInterface(REFIID interfaceIdentifier, void ** interfacePointer)
{
	if (interfacePointer == NULL) return E_POINTER;
	*interfacePointer = NULL;
	if (interfaceIdentifier == IID_IUnknown) *interfacePointer = this;
	else if (interfaceIdentifier == IID_IKlasa2) *interfacePointer = this;
	if (*interfacePointer != NULL) {
		AddRef();
		return S_OK;
	};
	return E_NOINTERFACE;

}

ULONG Klasa2::AddRef()
{
	/*
	Tutaj zaimplementuj dodawanie referencji na obiekt.
	 */

	return InterlockedIncrement(&refCount);
	
}

ULONG Klasa2::Release()
{
	/*
	Tutaj zaimplementuj usuwanie referencji na obiekt.
	Jeżeli nie istnieje żadna referencja obiekt powinien zostać usunięty.
	 */
	ULONG rv = InterlockedDecrement(&refCount);
	if (rv == 0) {
		delete this;
		return 0;
	}
	return rv;

}

HRESULT Klasa2::Test(int i)
{
	std::cout << "You have picked number " << i << std::endl;
	return S_OK;
}
