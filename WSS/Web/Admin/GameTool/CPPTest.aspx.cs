using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;

namespace WSS.Web.Admin.GameTool
{
    public partial class CPPTest : System.Web.UI.Page
    {
        [DllImport("CSharpInvokeCPP.CPPDemo.dll")]
        public static extern int Add(int x, int y);

        [DllImport("CSharpInvokeCPP.CPPDemo.dll")]
        public static extern IntPtr Create(string name, int age);

        [StructLayout(LayoutKind.Sequential)]
        public struct User
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Name;

            public int Age;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Add(1, 5).ToString());
            IntPtr ptr = Create("Alex", 27);
            User user = (User)Marshal.PtrToStructure(ptr, typeof(User));
            string t = string.Format("Name: {0}, Age: {1}", user.Name, user.Age);
            Response.Write(t);
        }


//        // CSharpInvokeCPP.CPPDemo.cpp : 定义 DLL 应用程序的导出函数。
////

//#include "stdafx.h"
//#include "malloc.h"
//#include "userinfo.h"

//typedef struct {
//    char name[32];
//    int age;
//} User;  

//UserInfo* userInfo;

//extern "C" __declspec(dllexport) int Add(int x, int y) 
//{ 
//    return x + y; 
//}
//extern "C" __declspec(dllexport) int Sub(int x, int y) 
//{ 
//    return x - y; 
//}
//extern "C" __declspec(dllexport) int Multiply(int x, int y) 
//{ 
//    return x * y; 
//}
//extern "C" __declspec(dllexport) int Divide(int x, int y) 
//{ 
//    return x / y; 
//}

//extern "C" __declspec(dllexport) User* Create(char* name, int age)    
//{   
//    User* user = (User*)malloc(sizeof(User));

//    userInfo = new UserInfo(name, age);
//    strcpy(user->name, userInfo->GetName());  
//    user->age = userInfo->GetAge();

//    return user; 
//}             

    }
}
