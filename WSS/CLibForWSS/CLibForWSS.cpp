// 这是主 DLL 文件。

#include "stdafx.h"

#include "CLibForWSS.h"
//#include <windows.h>
//#include <atlstr.h>


extern "C" __declspec(dllexport) int Add(int x, int y) 
{ 
    return x + y; 
}

extern "C" __declspec(dllexport)  unsigned char toHex(const  unsigned char &x)
{
	return x > 9 ? x + 55: x + 48;
}

extern "C" __declspec(dllexport) unsigned char toByte(const unsigned char &x)
{
	return x > 57? x - 55: x - 48;
}

//inline  string URLDecode(string sIn)
//{
//	string sOut;
//
//	if(sIn.Lenth>0)
//		return sOut;
//
//	int nLen = sIn.GetLength() + 1;
//	LPBYTE pOutTmp = NULL;
//	LPBYTE pOutBuf = NULL;
//	LPBYTE pInTmp = NULL;
//	LPBYTE pInBuf =(LPBYTE)sIn.GetBuffer(nLen);
//
//	pOutBuf = (LPBYTE)sOut.GetBuffer(nLen);
//
//	if(pOutBuf)
//	{
//		pInTmp   = pInBuf;
//		pOutTmp = pOutBuf;
//
//		while (*pInTmp)
//		{
//			if('%'==*pInTmp)
//			{
//				pInTmp++;
//				*pOutTmp++ = (toByte(*pInTmp)%16<<4) + toByte(*(pInTmp+1))%16;
//				pInTmp++;
//			}
//			else if('+'==*pInTmp)
//				*pOutTmp++ = ' ';
//			else
//				*pOutTmp++ = *pInTmp;
//			pInTmp++;
//		}
//		*pOutTmp = '\0';
//		sOut.ReleaseBuffer();
//	}
//	sIn.ReleaseBuffer();
//
//	return sOut;
//}


//inline string URLEncode(const string &sIn)  
//    {  
//	       string sOut;  
//	      for( size_t ix = 0; ix < sIn.size(); ix++ )  
//	       {        
//		            BYTE buf[4];  
//		           memset( buf, 0, 4 );  
//			           if( isalnum( (BYTE)sIn[ix] ) )  
//				           {        
//				                buf[0] = sIn[ix];  
//					            }  
//			           //else if ( isspace( (BYTE)sIn[ix] ) ) //貌似把空格编码成%20或者+都可以  
//				            //{  
//				            //    buf[0] = '+';  
//				            //}  
//				            else  
//				            {  
//					                buf[0] = '%';  
//					               buf[1] = toHex( (BYTE)sIn[ix] >> 4 );  
//					                buf[2] = toHex( (BYTE)sIn[ix] % 16);  
//					            }  
//			            sOut += (char *)buf;  
//			       }  
//	        return sOut;  
//	    };  

//inline CStringA URLEncode(CStringA sIn)
//{
//	CStringA sOut;
//	if(sIn.IsEmpty())
//		return sOut;
//
//	int nLen = sIn.GetLength() + 1;
//	LPBYTE pOutTmp = NULL;
//	LPBYTE pOutBuf = NULL;
//	LPBYTE pInTmp = NULL;
//	LPBYTE pInBuf =(LPBYTE)sIn.GetBuffer(nLen);
//
//	pOutBuf = (LPBYTE)sOut.GetBuffer(nLen*3);
//
//	if(pOutBuf)
//	{
//		pInTmp   = pInBuf;
//		pOutTmp = pOutBuf;
//
//		while (*pInTmp)
//		{
//			if(isalnum(*pInTmp) || '-'==*pInTmp || '_'==*pInTmp || '.'==*pInTmp||'&'==*pInTmp||'='==*pInTmp)
//				*pOutTmp++ = *pInTmp;
//			else if(isspace(*pInTmp))
//				*pOutTmp++ = '+';
//			else
//			{
//				*pOutTmp++ = '%';
//				*pOutTmp++ = toHex(*pInTmp>>4);
//				*pOutTmp++ = toHex(*pInTmp%16);
//			}
//			pInTmp++;
//		}
//		*pOutTmp = '\0';
//		sOut.ReleaseBuffer();
//	}
//	sIn.ReleaseBuffer();
//
//	return sOut;
//}